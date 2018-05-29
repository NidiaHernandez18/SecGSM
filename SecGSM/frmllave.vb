Imports System.Windows.Forms
Public Class frmllave
    Dim Seguri As New Pk1
    Private Sub Salir(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSalir.Click, Btnsalir2.Click
        Me.Close()
    End Sub
    Private Sub Btnllave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnllave.Click
        Dim enc1 As String

        If Len(txtCandado1.Text) > 0 Then
            txtllave1.Text = Seguri.Encriptarpaso2(txtCandado1.Text)
            enc1 = Seguri.Desencriptar(txtllave1.Text)
            txtrfc.Text = Mid(enc1, 42, 12)
            Txtpat.Text = Mid(enc1, 1, 11)
            txtsocial.Text = Mid(enc1, 12, 30)
            txtmod.Text = Mid(enc1, 54, 3)
        Else
            Call MessageBox.Show("Tiene que escribir el canado", "Llave", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtCandado1.Focus()
        End If

    End Sub
    Private Sub BtnLLave2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLLave2.Click
        Dim extrallave As String
        extrallave = Mid(Txtpat.Text & Space(11), 1, 11) & Mid(txtsocial.Text & Space(30), 1, 30) & Mid(txtrfc.Text & Space(12), 1, 12) & Mid(txtmod.Text & Space(3), 1, 3)
        extrallave = UCase(extrallave)
        extrallave = Seguri.Encriptarpaso1(extrallave)
        txtllave2.Text = Seguri.Encriptarpaso2(extrallave)
    End Sub

    Private Sub frmllave_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Seguri = Nothing
    End Sub
    Private Sub frmllave_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtllave2.Text = ""
        txtllave1.Text = ""
    End Sub
End Class