Public Class frmcambiarpassw
    Public passant As String
    Public xdias As Double

    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        infoemple.continuar = False
        Me.Close()
    End Sub

    Private Function validacion2() As Boolean
        Dim i, BanL, BanD As Integer
        Dim letter As Char
        Try
            If String.Compare(passant.Trim, TEXTOLD.Text.Trim) <> 0 Then
                MessageBox.Show("La clave anterior es incorrecta", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TEXTOLD.Text = ""
                Return False

            End If
            If String.Compare(Txtconf.Text.Trim, Txtpass.Text.Trim) <> 0 Then
                MessageBox.Show("Por favor verifique la clave", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                Txtpass.Text = ""
                Txtconf.Text = ""
                Txtpass.Focus()
                Return False
            ElseIf Txtpass.Text.Length < 6 Or Txtpass.Text.Length > 16 Then
                MessageBox.Show("Por favor introduzca una clave mayor 6 digitos O menor a 16", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Txtpass.Text = ""
                Txtconf.Text = ""
                Txtpass.Focus()
                Return False
            Else
                i = 1
                BanL = 0 : BanD = 0
                Do While i < Txtpass.Text.Length
                    letter = Txtpass.Text.Substring(i, 1)
                    If Char.IsDigit(letter) Then
                        BanL = 1
                    End If
                    If Char.IsLetter(letter) Then
                        BanD = 1
                    End If
                    i = i + 1
                Loop
                If BanL = 0 Or BanD = 0 Then
                    MessageBox.Show("Por favor introduzca en la clave por lo menos con un caracter numérico", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Txtpass.Text = ""
                    Txtconf.Text = ""
                    Txtpass.Focus()
                    Return False
                End If
            End If
            

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Application Error")
        Finally

        End Try
        Return True
    End Function

    Private Sub BtnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGuardar.Click
        Dim fecha As Date
        Try
            If validacion2() Then
                infoemple.continuar = True
                fecha = DateAdd(DateInterval.Day, xdias, Now.Date)
                clasEjecutar.ExecuteSql("update tbl_usuarios set cambiarp=0,passw='" & clasEncdesc.Encrypt(Txtpass.Text) & "'," _
                                 & "intenuser=0,expiraf='" & VB6.Format(fecha, infoemple.formato) & "',fechaentrada='" & VB6.Format(DateTime.Now, infoemple.formato & " HH:mm:ss") _
                                 & "' where usuario ='" & Label6.Text & "'", Conn)
                MessageBox.Show("El cambio del password fue Existoso", "Cambio", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Application Error")
        Finally

        End Try

    End Sub

    Private Sub frmcambiarpassw_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = ChrW(13) Then
            SendKeys.Send("{TAB}")
        End If
        If e.KeyChar = ChrW(27) Then
            Me.Close()
        End If
    End Sub

    Private Sub frmcambiarpassw_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        infoemple.continuar = False
    End Sub
End Class