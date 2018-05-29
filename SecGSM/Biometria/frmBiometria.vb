Imports NITGEN.SDK.NBioBSP
Imports System.Windows.Forms

Public Class frmBiometria
    Private objHuella As clsHuella
    Private dtbHuellas As DataTable
    Private dadHuellas As New SqlClient.SqlDataAdapter
    'Private CurrentDtr As DataRow
    Public Usuario As String
    Public DB As ClassCone
    Public connDb As SqlClient.SqlConnection

    Private Sub frmBiometria_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        objHuella = New clsHuella(DB, connDb)
        lblUsuario.Text = Usuario
        Call Llena_Form()


    End Sub

    Private Sub Huella10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Huella1.Click

    End Sub
    Private Sub Llena_Form()
        'Empleado

        Dim strSQL As String
        strSQL = "SELECT * FROM tblHuellas WHERE usuario='" & Usuario & "'"
        dtbHuellas = DB.RecDatatable(strSQL, connDb, dadHuellas)
        If dtbHuellas.Rows.Count = 0 Then
            Dim dtr2 As DataRow
            dtr2 = dtbHuellas.NewRow
            dtr2!usuario = Usuario
            dtbHuellas.Rows.Add(dtr2)
            dadHuellas.Update(dtbHuellas)
            For j As Byte = 1 To 3
                Dim pic As PictureBox = Me.Controls("Huella" & j)
                pic.Image = Nothing
            Next
        Else
            Dim dtr2 As DataRow = dtbHuellas.Rows(0)
            For j As Byte = 1 To 3
                Dim pic As PictureBox = Me.Controls("Huella" & j)
                If dtr2("Huella" & j).ToString <> "" Then
                    pic.Image = img.Images("FIR")
                Else
                    pic.Image = Nothing
                End If
            Next

            'Dim export As New NBioAPI.Export(objHuella.m_NBioAPI)
            'Dim HFIR As New NBioAPI.Type.FIR_TEXTENCODE
            'Dim ExportImage As New NBioAPI.Export.EXPORT_AUDIT_DATA
            'Dim ret As UInteger

            'HFIR.TextFIR = dtr2!Huella1
            'ret = export.NBioBSPToImage(HFIR, ExportImage)
            'MsgBox(ret)
            'clsHuella.DisplayErrorMsg(ret)

            '            Dim img As ImageConverter
            'img.
            'ExportImage.ImageHeight()
            'ExportImage.ImageWidth()

        End If
    End Sub

    Private Sub Huella1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Huella1.DoubleClick, Huella2.DoubleClick, Huella3.DoubleClick
        Dim strSQL As String
        Dim NoHuella As Byte = Strings.Right(sender.name, 1)
        Dim dtr As DataRow
        If IsNothing(sender.image) And sender.tag <> "OK" Then
            Dim strHuella As String
            NoHuella = IIf(NoHuella = 0, 10, NoHuella)

            strHuella = objHuella.Registrar(NoHuella, 0, Me.Controls("Huella" & NoHuella))
            If strHuella.ToString.Trim <> "" Then
                dtr = dtbHuellas.Rows(0)
                dtr("Huella" & NoHuella) = strHuella
                dtr!PaqEnvio = 1
                dtr.EndEdit()
                dadHuellas.Update(dtbHuellas)
                MsgBox("Huella registrada", MsgBoxStyle.Information, "AVISO")
            End If
        Else
            NoHuella = IIf(NoHuella = 0, 10, NoHuella)
            dtr = dtbHuellas.Rows(0)
            If dtr("Huella" & NoHuella).ToString <> "" Then
                Dim NombreDedo As String = ""
                Select Case sender.name
                    Case Is = "Huella1"
                        NombreDedo = "1"
                    Case Is = "Huella2"
                        NombreDedo = "2"
                    Case Is = "Huella3"
                        NombreDedo = "3"
                        'Case Is = "Huella4"
                        '    NombreDedo = "Meñique (Mano derecha)"
                        'Case Is = "Huella5"
                        '    NombreDedo = "Pulgar (Mano derecha)"
                        'Case Is = "Huella6"
                        '    NombreDedo = "Pulgar (Mano izquierda)"
                        'Case Is = "Huella7"
                        '    NombreDedo = "Indice (Mano izquierda)"
                        'Case Is = "Huella8"
                        '    NombreDedo = "Medio (Mano izquierda)"
                        'Case Is = "Huella9"
                        '    NombreDedo = "Anular (Mano izquierda)"
                        'Case Is = "Huella10"
                        '    NombreDedo = "Meñique (Mano izquierda)"
                End Select
                If MsgBox("¿Desea eliminar la huella del dedo " & NombreDedo & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "AVISO") = MsgBoxResult.Yes Then
                    If MsgBox("Esta opción no se puede deshacer. ¿Desea continuar?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "AVISO") = MsgBoxResult.Yes Then
                        NoHuella = IIf(NoHuella = 0, 3, NoHuella)

                        dtr = dtbHuellas.Rows(0)
                        dtr("Huella" & NoHuella) = DBNull.Value
                        dtr!PaqEnvio = 1
                        dtr.EndEdit()
                        dadHuellas.Update(dtbHuellas)

                        sender.image = Nothing
                        sender.Tag = ""
                    End If
                End If
            End If
        End If
    End Sub
End Class