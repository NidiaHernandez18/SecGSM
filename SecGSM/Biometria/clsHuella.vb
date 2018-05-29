Imports NITGEN.SDK.NBioBSP
Imports System.Windows.Forms

Public Class clsHuella
    Public m_NBioAPI As New NBioAPI
    Private msearch As New NBioAPI.IndexSearch(m_NBioAPI)
    Private ret As UInteger
    Public ProgressB As ProgressBar
    Private DB As ClassCone
    Private connDB As SqlClient.SqlConnection
    Private SucursalID As Integer
    Shared Sub DisplayErrorMsg(ByVal ret As UInteger)
        MessageBox.Show(NBioAPI.Error.GetErrorDescription(ret) + " [" + ret.ToString() + "]", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Private Function myCallback(ByRef cbParam0 As NBioAPI.IndexSearch.CALLBACK_PARAM_0, ByVal userParam As IntPtr) As UInteger
        If Not IsNothing(ProgressB) Then ProgressB.Value = Convert.ToInt32(cbParam0.ProgressPos)
        Return NBioAPI.IndexSearch.CALLBACK_RETURN.OK
    End Function
    Sub New(ByVal DBs As ClassCone, ByVal conn As SqlClient.SqlConnection)
        m_NBioAPI.OpenDevice(NBioAPI.Type.DEVICE_ID.AUTO)
        DB = DBs
        conn = connDB
    End Sub
    Public Sub LeerHuellaFromDB()
        Dim dtb As DataTable
        Dim dad As SqlClient.SqlDataAdapter
        Dim strSQL As String

        ' Dim StoredFIR As New NBioAPI.Type.FIR
        Dim StoredFIRT As New NBioAPI.Type.FIR_TEXTENCODE
        'Dim stored As New NBioAPI.Type.HFIR
        'Dim storedIN As New NBioAPI.Type.INPUT_FIR
        'storedIN.Form = NITGEN.SDK.NBioBSP.NBioAPI.Type.INPUT_FIR_FORM.TEXTENCODE

        strSQL = "SELECT * FROM tblHuellas"
        dtb = DB.RecDatatable(strSQL, connDB, dad)
        Dim SampleInfo() As NBioAPI.IndexSearch.FP_INFO

        msearch.InitEngine()

        For Each dtr As DataRow In dtb.Rows
            Dim BolOk As Boolean
            For j As Byte = 1 To 10
                StoredFIRT.TextFIR = dtr("Huella" & j).ToString.Trim
                If StoredFIRT.TextFIR <> "" Then
                    Dim ret As UInteger
                    ret = msearch.AddFIR(StoredFIRT, dtr!NumeroUsuario, SampleInfo)
                    If ret <> NBioAPI.Error.NONE Then
                        DisplayErrorMsg(ret)
                        Exit For
                    Else
                        BolOk = True
                    End If
                End If
            Next
            If BolOk Then
                dtr.BeginEdit()
                dtr!PaEnvio = 0
                dtr.AcceptChanges()
                dad.Update(dtb)
            End If
        Next
    End Sub
    Public Sub GuardarHuellasToFile()
        Dim strFile As String
        Dim tempfile As String
        strFile = My.Application.Info.DirectoryPath & "\Config\" & SucursalID & "FIRs.gsm"
        Try
            If My.Computer.FileSystem.FileExists(strFile) Then
                tempfile = strFile & Format(Date.Now, "yyMMddHHmmss").ToString
                My.Computer.FileSystem.RenameFile(strFile, tempfile)
            End If
            ret = msearch.SaveDBToFile(strFile)
            If ret <> NBioAPI.Error.NONE Then
                DisplayErrorMsg(ret)
                If My.Computer.FileSystem.FileExists(tempfile) Then
                    My.Computer.FileSystem.RenameFile(tempfile, strFile)
                End If
            Else
                If My.Computer.FileSystem.FileExists(tempfile) Then
                    My.Computer.FileSystem.DeleteFile(tempfile)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "AVISO")
        End Try
    End Sub
    Public Sub LoadFIRs()
        Dim strFile As String = My.Application.Info.DirectoryPath & "\Config\" & SucursalID & "FIRs.gsm"
        If Not My.Computer.FileSystem.FileExists(strFile) Then
            Call LeerHuellaFromDB()
            Call GuardarHuellasToFile()
        Else
            'Verificar si hay cambios en huellas
            Dim strsql As String
            Dim dtb As DataTable
            strsql = "SELECT * FROM tblHuellas WHERE PaqEnvio=1"
            dtb = DB.RecDatatable(strsql, connDB)
            If dtb.Rows.Count > 0 Then
                Call LeerHuellaFromDB()
                Call GuardarHuellasToFile()
            Else
                msearch.InitEngine()
                ret = msearch.LoadDBFromFile(strFile)
                If ret <> NBioAPI.Error.NONE Then
                    DisplayErrorMsg(ret)
                End If
            End If
        End If
    End Sub
    Public Sub Verify(ByRef id As Object, Optional ByRef PictureBox As PictureBox = Nothing)
        Dim hCapturedFIR As NBioAPI.Type.HFIR
        ' Get FIR data
        Dim ret As UInteger
        'ret = m_NBioApi.Capture(hCapturedFIR)


        Dim WinOption As NBioAPI.Type.WINDOW_OPTION
        WinOption = New NBioAPI.Type.WINDOW_OPTION
        WinOption.WindowStyle = NBioAPI.Type.WINDOW_STYLE.INVISIBLE
        WinOption.Option2 = New NBioAPI.Type.WINDOW_OPTION_2

        WinOption.Option2.FPForeColor(0) = CByte(255)
        WinOption.Option2.FPForeColor(1) = CByte(255)
        WinOption.Option2.FPForeColor(2) = CByte(255)

        WinOption.Option2.FPBackColor(0) = CByte(105)
        WinOption.Option2.FPBackColor(1) = CByte(105)
        WinOption.Option2.FPBackColor(2) = CByte(105)



        If Not IsNothing(PictureBox) Then
            WinOption.FingerWnd = PictureBox.Handle.ToInt32
        End If

        'uint ret = m_NBioAPI.Capture (out m_hNewFIR, NBioAPI.Type.TIMEOUT.DEFAULT, WinOption);
        Dim frm As New frmCapturandoHuella
        frm.Show()
        frm.BringToFront()
        'My.Application.DoEvents()
        ret = m_NBioAPI.Capture(hCapturedFIR, NBioAPI.Type.TIMEOUT.DEFAULT, WinOption)
        frm.Close()
        If (ret <> NBioAPI.Error.NONE) Then
            'DisplayErrorMsg(ret)
            m_NBioAPI.CloseDevice(NBioAPI.Type.DEVICE_ID.AUTO)
            id = -1
        Else
            m_NBioAPI.CloseDevice(NBioAPI.Type.DEVICE_ID.AUTO)
            Dim nMax As UInteger

            msearch.GetDataCount(nMax)
            If Not IsNothing(ProgressB) Then
                ProgressB.Minimum = 0
                ProgressB.Maximum = Convert.ToInt32(nMax)
            End If
            Dim cbinfo0 As New NBioAPI.IndexSearch.CALLBACK_INFO_0()
            cbinfo0.CallBackFunction = New NBioAPI.IndexSearch.INDEXSEARCH_CALLBACK(AddressOf myCallback)

            ' Identify FIR to IndexSearch DB
            Dim fpInfo As NBioAPI.IndexSearch.FP_INFO
            ret = msearch.IdentifyData(hCapturedFIR, NBioAPI.Type.FIR_SECURITY_LEVEL.NORMAL, fpInfo, cbinfo0)
            If (ret <> NBioAPI.Error.NONE) Then
                'DisplayErrorMsg(ret)
                id = 0
            Else
                'm_NBioAPI.CloseDevice(NBioAPI.Type.DEVICE_ID.AUTO)
                ' Add item to list of result
                'Dim strSQl As String
                'strSQl = "ID:" & fpInfo.ID.ToString() & vbCrLf
                'strSQl &= "FingerID:" & fpInfo.FingerID.ToString() & vbCrLf
                'strSQl &= "SampleNumber:" & fpInfo.SampleNumber.ToString()
                'MsgBox(strSQl, MsgBoxStyle.Information, "AVISO")
                id = fpInfo.ID
            End If
        End If
        m_NBioAPI.OpenDevice(NBioAPI.Type.DEVICE_ID.AUTO)
    End Sub
    Public Function Registrar(ByVal NoHuella As Byte, ByVal UserID As String, Optional ByRef PictureBox As PictureBox = Nothing) As String
        Dim hnewFir As NBioAPI.Type.HFIR
        Dim textFir As New NBioAPI.Type.FIR_TEXTENCODE
        'Dim myPayload As NBioAPI.Type.FIR_PAYLOAD = New NBioAPI.Type.FIR_PAYLOAD()
        'myPayload.Data = UserID & NoHuella
        Try
            Dim WinOption As NBioAPI.Type.WINDOW_OPTION
            WinOption = New NBioAPI.Type.WINDOW_OPTION
            WinOption.WindowStyle = NBioAPI.Type.WINDOW_STYLE.INVISIBLE
            If Not IsNothing(PictureBox) Then WinOption.FingerWnd = PictureBox.Handle.ToInt32
            m_NBioAPI.OpenDevice(NBioAPI.Type.DEVICE_ID.AUTO)
            ' Get FIR data
            Dim ret As UInteger = m_NBioAPI.Capture(hnewFir, NBioAPI.Type.TIMEOUT.DEFAULT, WinOption)
            If (ret <> NBioAPI.Error.NONE) Then
                Return ""
            Else
                'Dim hNewFir2 As NBioAPI.Type.HFIR
                'Dim hCapturedFIR As NBioAPI.Type.HFIR
                'ret = m_NBioAPI.CreateTemplate(hnewFir, hnewFir, hNewFir2, myPayload)
                'If ret = NBioAPI.Error.NONE Then
                PictureBox.Tag = "OK"
                m_NBioAPI.GetTextFIRFromHandle(hnewFir, textFir, True)
                Return textFir.TextFIR
                'Else
                'DisplayErrorMsg(ret)
                'Return Nothing
                'End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "AVISO")
            Return ""
        Finally
            m_NBioAPI.CloseDevice(NBioAPI.Type.DEVICE_ID.AUTO)
        End Try
    End Function
    Public Function CheckFinger() As Boolean
        Dim HayHuella As Boolean
        'If m_NBioAPI.then Then
        m_NBioAPI.OpenDevice(NBioAPI.Type.DEVICE_ID.AUTO)
        'End If
        ret = m_NBioAPI.CheckFinger(HayHuella)
        Return HayHuella
    End Function
    Public Sub Close()
        m_NBioAPI.CloseDevice(NBioAPI.Type.DEVICE_ID.AUTO)
        msearch.TerminateEngine()
        msearch = Nothing
    End Sub
End Class
