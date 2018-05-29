Imports System.Windows.Forms
Public Class frmNew
    Public dtbrutas As DataTable = Nothing
    Public dtrEmpresaSelected As DataRow
    Dim userapl As String
    Dim contprog As Boolean
    Dim connPrinc As SqlClient.SqlConnection
    Dim connPrinc2 As OleDb.OleDbConnection
    Dim usuariotabla As New DataTable
    Public ErrorEnPathdeEmpresa As Boolean
    Public DatosEquipo As Infoequipo
    Public DatosCert As InfoCert
    Public CambioCertificado As Boolean
    Public dtrEmpresa As DataRow
    Public IsMultiDBMS As Boolean = False
    Public AutoLogin As Boolean = False
    Public AutoEmpresa As String
    Public AutoSucursal As String
    Public AutoUsuario As String
    Public AutoPassword As String

    Public Shadows Property conectar(ByVal IsSQL As Boolean) As SqlClient.SqlConnection
        Get
            conectar = connPrinc
        End Get
        Set(ByVal value As SqlClient.SqlConnection)
            connPrinc = value
        End Set
    End Property
    Public Shadows Property Conectar() As OleDb.OleDbConnection
        Get
            Return connPrinc2
        End Get
        Set(ByVal value As OleDb.OleDbConnection)
            connPrinc2 = value
        End Set
    End Property
    Public Shadows Property continuar() As Boolean
        Get
            continuar = contprog
        End Get
        Set(ByVal value As Boolean)
            contprog = value
        End Set
    End Property
    Public Shadows Property Useraplicacion() As String
        Get
            Useraplicacion = userapl
        End Get
        Set(ByVal value As String)
            userapl = value

        End Set
    End Property
    Private Function DistinctRowInDatatable(ByVal dtb As DataTable, ByVal ParamArray columns() As String)
        Dim result As DataTable
        Dim dvw As DataView = dtb.DefaultView
        result = dvw.ToTable(True, columns)
        Return result
    End Function
   
    Private Function Validacion() As Boolean
        Dim result As Boolean
        'If cboEmpresas.Text.Trim.Length = 0 Then
        '    MsgBox("Introduzca la Empresa a la cual desea conectarse", MsgBoxStyle.Exclamation, "AVISO")
        '    cboEmpresas.Focus()
        'ElseIf cboSucursales.Text.Trim.Length = 0 Then
        '    MsgBox("Seleccione la Empresa o Sucursal a la cual desea conectarse", MsgBoxStyle.Exclamation, "AVISO")
        '    cboSucursales.Focus()
        'Else
        If txtLlavePrivada.Text.Trim.Length = 0 Then
            MsgBox("Introduzca el usuario de acceso", MsgBoxStyle.Exclamation, "AVISO")
            txtLlavePrivada.Focus()
        ElseIf txtPwdKey.Text.Trim.Length = 0 Then
            MsgBox("Introduzca la contraseña de acceso", MsgBoxStyle.Exclamation, "AVISO")
            txtPwdKey.Focus()
        ElseIf Not VerifyPrivateKeyPassword(txtLlavePrivada.Text, txtPwdKey.Text) Then
            txtPwdKey.SelectAll()
            txtPwdKey.Focus()
        ElseIf txtPwdUsuario.Text <> txtPwdUsuarioConf.Text Then
            MsgBox("No coincide la contraseña del usuario", MsgBoxStyle.Exclamation, "AVISO")
            txtPwdUsuario.Focus()
        Else
            result = True
        End If
        Return result
    End Function
    Public Function VerifyPrivateKeyPassword(ByVal strKeyFile As String, ByVal strpassword As String)
        ' Read in the Private Key
        If Rsa.ReadEncPrivateKey(strKeyFile, strpassword).ToString.Length = 0 Then
            MsgBox("No se puede leer la clave privada '" & strKeyFile & "'", vbCritical, "La clave privada es incorrecta")
            Return False
        Else
            Return True
        End If
    End Function
    Private Function zipToByte(ByVal ParamArray AstrFile() As String) As Byte()
        Dim sxml As String = ""
        Dim zip64 As Byte()
        Dim rZip64 As Byte() = Nothing
        Dim StreamsXML() As IO.FileStream
        Dim ZipStream As IO.FileStream

        ReDim StreamsXML(AstrFile.Length - 1)
        ' Crear Zip de XML
        For i As Integer = 0 To AstrFile.Length - 1
            Dim strfile As String = AstrFile(i)
            StreamsXML(i) = IO.File.OpenRead(strfile)
        Next

        ZipStream = ZIPXML(".zip", StreamsXML)
        StreamsXML(0).Close()
        StreamsXML(0).Dispose()

        ReDim zip64(ZipStream.Length)
        Dim BytesRead As Long = ZipStream.Read(zip64, 0, ZipStream.Length)
        ZipStream.Close()
        Return zip64
    End Function
    Public Function ZIPXML(ByVal strZIPFile As String, ByVal FileStreamS() As IO.FileStream) As IO.Stream
        Dim zipStream As New IO.FileStream(strZIPFile, IO.FileMode.Create)
        Dim strmZipOutputStream As New ICSharpCode.SharpZipLib.Zip.ZipOutputStream(zipStream)
        Dim strmFile As IO.FileStream = Nothing
        Dim objZipEntry As ICSharpCode.SharpZipLib.Zip.ZipEntry
        Try
            REM Compression Level: 0-9
            REM 0: no(Compression)
            REM 9: maximum compression
            strmZipOutputStream.SetLevel(9)
            For Each strmFile In FileStreamS
                If Not IsNothing(strmFile) Then
                    Dim abyBuffer(strmFile.Length - 1) As Byte

                    strmFile.Read(abyBuffer, 0, abyBuffer.Length)

                    objZipEntry = New ICSharpCode.SharpZipLib.Zip.ZipEntry(IO.Path.GetFileName(strmFile.Name))
                    objZipEntry.DateTime = DateTime.Now
                    objZipEntry.Size = strmFile.Length
                    strmFile.Close()
                    strmZipOutputStream.PutNextEntry(objZipEntry)
                    strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
                Else
                    'Dim exc As New Exception("No se encontro un archivo xml")
                    'Throw exc
                End If
            Next
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()
            zipStream = New IO.FileStream(strZIPFile, IO.FileMode.Open)
            Return zipStream
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "AVISO")
            zipStream.Close()
            zipStream.Dispose()
            Return Nothing
        Finally
            If Not IsNothing(strmFile) Then
                strmFile.Close()
                strmFile.Dispose()
            End If
            strmFile = Nothing
            strmZipOutputStream.Close()
            strmZipOutputStream.Dispose()
        End Try
        'MessageBox.Show("Operation complete")
    End Function
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim recux As New ClassCone
        Dim reccon1 As New Cblowfish
        If Validacion() Then
            Dim strDatos As String
            Dim HDD1 As String
            Dim Security As New Pk1
            HDD1 = SerieDisk(My.Computer.FileSystem.SpecialDirectories.Desktop)

            '$Origen
            strDatos = txtEquipo.Text & "(" & txtIP.Text & ")"
            '$Numero 
            strDatos &= "|" & txtNumero.Text
            '$Empresa
            strDatos &= "|" & txtRazonSocial.Text
            '$Sucursal
            strDatos &= "|" & txtSucursal.Text
            '$RFC
            strDatos &= "|" & txtRFC.Text
            '$NoCertificado
            strDatos &= "|" & txtNoCSD.Text
            '$Sistema
            strDatos &= "|" & "ePayroll"
            '$Nombre
            strDatos &= "|" & txtNombre.Text
            '$Usuario
            strDatos &= "|" & txtUsuario.Text
            '$Password
            strDatos &= "|" & txtPwdUsuario.Text
            '$Email
            strDatos &= "|" & txtEmail.Text
            '$Telefono
            strDatos &= "|" & txtTelefono.Text
            '$SNEquipo
            strDatos &= "|" & Mid(HDD1 & Space(11), 1, 11)
            '$Equipo
            strDatos &= "|" & txtEquipo.Text
            '$IP
            strDatos &= "|" & txtIP.Text


            Dim ws As New clsWs
            Dim log As clsWs.RequestCLUTypeResult
            Dim csd() As Byte
            Dim strFilePWD As String = ""
            Dim file As IO.StreamWriter
            strFilePWD = My.Computer.FileSystem.GetTempFileName

            file = New IO.StreamWriter(strFilePWD)
            file.Write(txtPwdKey.Text)
            file.Close()
            IO.File.Copy(strFilePWD, IO.Path.GetDirectoryName(strFilePWD) & "\password.txt", True)
            strFilePWD = IO.Path.GetDirectoryName(strFilePWD) & "\password.txt"

            csd = zipToByte(txtCSD.Text, txtLlavePrivada.Text, strFilePWD)
            log = ws.fxNewLic(strDatos, txtRFC.Text, 1, 1, csd)

            If Not log.wsError Then
                If RegistraSolicitud(strDatos, txtCSD.Text, txtLlavePrivada.Text, log.EstadoAut.ToString, log.IDRequest.ToString) Then
                    MsgBox("Solicitud registrada exitosamente", vbInformation, "AVISO")
                End If
            End If

        End If
    End Sub
    Private Sub frmusuarios_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dtr As DataRow
        Dim dtbEmpresas As DataTable
        'Me.Height = 278
        'pnlCertificado.Visible = False

        If Not IsNothing(dtbrutas) Then
            dtbEmpresas = DistinctRowInDatatable(dtbrutas, "Numero", "Empresa")
            For Each dtr In dtbEmpresas.Rows
                'cboEmpresas.Items.Add("" & dtr("Numero") & "-" & Trim(dtr("Empresa")))
            Next
            'If cboEmpresas.Items.Count > 0 Then
            '    cboEmpresas.SelectedIndex = 0
            'End If
            dtbEmpresas.Clear()
            dtbEmpresas = Nothing
            dtr = Nothing
            'Label4.Text = nombreaplic
            'intentos = 0
        End If

    End Sub
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub


    Private Sub cmdCert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        OpenDialog.DefaultExt = ".cer"
        OpenDialog.Filter = "Certificado de Seguridad(*.cer)|*.cer"
        If OpenDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            'txtCert.Text = OpenDialog.FileName
            CambioCertificado = True
        End If
    End Sub
    Private Sub btnsalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsalir.Click
        Me.Close()
    End Sub


    Private Sub txtpass_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPwdKey.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call btnAceptar_Click(btnAceptar, e)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        OpenDialog.DefaultExt = ".cer"
        OpenDialog.Filter = "Certificado de Seguridad(*.cer)|*.cer"
        If OpenDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            'txtCertAdicional.Text = OpenDialog.FileName
            CambioCertificado = True
        End If
    End Sub

    Private Sub ToolStripStatusLabel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub frmNew_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel

    End Sub

    Private Sub frmusuarios3_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim host As String
        Dim LocalHostAddress As String = ""
        host = System.Net.Dns.GetHostName()
        If Not IsNothing(System.Net.Dns.GetHostEntry(host)) Then
            For Each StrIp As System.Net.IPAddress In System.Net.Dns.GetHostEntry(host).AddressList
                If StrIp.AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then
                    LocalHostAddress = StrIp.ToString
                End If
            Next
        End If
        txtEquipo.Text = host
        txtIP.Text = LocalHostAddress
    End Sub

    Private Sub cmdCSD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCSD.Click
        Dim frm As New frmLeerCert
        frm.ShowDialog()
        If frm.BoolOK Then
            txtCSD.Text = frm.txtCertCli.Text
            txtRFC.Text = frm.txtRFC.Text
            txtRazonSocial.Text = frm.txtRazonSocial.Text
            txtNoCSD.Text = frm.lblserie.Text
        End If
    End Sub

    Private Sub cmdKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdKey.Click
        OpenDialog.Filter = "Llave Privada (*.key)|*.key"
        If OpenDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtLlavePrivada.Text = OpenDialog.FileName
        End If
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim frm As New frmWS
        frm.ShowDialog()
        frm.Close()
    End Sub
End Class