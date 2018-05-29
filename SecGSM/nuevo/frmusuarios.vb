Public Class frmusuarios
    Dim usuariotabla As DataTable
    Dim xpass1 As String
    Dim xfecha As Date
    Dim xintentos As Integer
    Private Sub btnsalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsalir.Click
        infoemple.continuar = False
        Me.Close()
        Application.Exit()

    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        usuariotabla = New DataTable

        xpass1 = ""

        infoemple.correo = ""
        infoemple.nombre = ""
        infoemple.telefono = ""
        infoemple.ext = ""
        Try
            usuariotabla = New DataTable
            usuariotabla = clasEjecutar.RecDatatable("select usuario,passw,cambiarp,deshabili,bloqueada,expiraf,expira,intenuser,intentos," _
                           & "fechaentrada,email,nombre,tel,exten from tbl_usuarios where usuario='" & txtuser.Text & "'", Conn)
            If usuariotabla.Rows.Count > 0 Then
                If usuariotabla.Rows(0).Item("deshabili") = 1 Then
                    Call MessageBox.Show("Cuenta deshabilitada, favor de comunicarse con el administrador", "Favor de comunicarse con el administrador", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Exit Sub
                End If
                If usuariotabla.Rows(0).Item("Bloqueada") = 1 Then
                    Call MessageBox.Show("Cuenta bloqueada, favor de comunicarse con el administrador", "Favor de comunicarse con el administrador", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Exit Sub
                End If

                xpass1 = clasEncdesc.Decrypt(usuariotabla.Rows(0).Item("passw"))
                If xpass1 = txtpass.Text Then
                    If usuariotabla.Rows(0).Item("intenuser") > usuariotabla.Rows(0).Item("intentos") Then
                        Call MessageBox.Show("Cuenta bloqueada", "Favor de comunicarse con el administrador", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        Exit Sub
                    End If
                    xfecha = usuariotabla.Rows(0).Item("expiraf")
                    infoemple.continuar = True
                    If xfecha <= Now.Date Then
                        Call MessageBox.Show("Cuenta expiro, cambio de clave ", "Cambio de clave", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        infoemple.continuar = False
                        Dim frmcambio As New frmcambiarpassw
                        frmcambio.passant = clasEncdesc.Decrypt(usuariotabla.Rows(0).Item("passw"))
                        frmcambio.Label6.Text = usuariotabla.Rows(0).Item("usuario")
                        frmcambio.xdias = usuariotabla.Rows(0).Item("expira")
                        frmcambio.ShowDialog()
                    End If
                    If usuariotabla.Rows(0).Item("cambiarp") = 1 Then
                        infoemple.continuar = False
                        Call MessageBox.Show("Cambiar la clave por favor ", "Cambio de clave", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        infoemple.continuar = False
                        Dim frmcambio As New frmcambiarpassw
                        frmcambio.passant = clasEncdesc.Decrypt(usuariotabla.Rows(0).Item("passw"))
                        frmcambio.Label6.Text = usuariotabla.Rows(0).Item("usuario")
                        frmcambio.xdias = usuariotabla.Rows(0).Item("expira")
                        frmcambio.ShowDialog()
                    End If

                    If infoemple.continuar Then
                        infoemple.iduser = txtuser.Text
                        clasEjecutar.ExecuteSql("update tbl_usuarios set intenuser=0 where usuario='" & txtuser.Text & "'", Conn)

                        infoemple.correo = IIf(IsDBNull(usuariotabla.Rows(0).Item("email")), "", usuariotabla.Rows(0).Item("email"))
                        infoemple.nombre = IIf(IsDBNull(usuariotabla.Rows(0).Item("Nombre")), "", usuariotabla.Rows(0).Item("Nombre"))
                        infoemple.telefono = IIf(IsDBNull(usuariotabla.Rows(0).Item("TEL")), "", usuariotabla.Rows(0).Item("TEL"))
                        infoemple.ext = IIf(IsDBNull(usuariotabla.Rows(0).Item("EXTEN")), "", usuariotabla.Rows(0).Item("EXTEN"))

                        Me.Close()
                    End If
                Else
                    xintentos = usuariotabla.Rows(0).Item("intenuser") + 1

                    If xintentos = usuariotabla.Rows(0).Item("intentos") Then
                        clasEjecutar.ExecuteSql("update tbl_usuarios set intenuser=0,bloqueada=1 where usuario='" & txtuser.Text & "'", Conn)
                        MessageBox.Show("La cuenta del usuario se bloqueo, favor de comunicarse con su administrador", "Cuenta", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Else
                        clasEjecutar.ExecuteSql("update tbl_usuarios set intenuser=" & xintentos & " where usuario='" & txtuser.Text & "'", Conn)
                        MessageBox.Show("La clave es incorrecta", "Clave", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        txtpass.Focus()
                    End If

                End If

            Else
                Call MessageBox.Show("El usuario no existe favor de verificarlo", "Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error)

                Exit Sub
            End If
        Catch ex As Exception

        Finally
            usuariotabla.Clear()
            usuariotabla = Nothing
        End Try

    End Sub

    Private Sub frmusuarios_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = ChrW(13) Then
            SendKeys.Send("{TAB}")
        End If

       
    End Sub

    Private Sub frmusuarios_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Dim inicas As New classini
        'servidor.Basedatos = inicas.IniGet(Application.StartupPath & "\ivanet.ini", "BASEDEDATOS", "BASEDEDATOS", "")

        ''servidor.user = envString
        'servidor.password = "gsmeg4"
        'servidor.temporales = "C:\sistecom"
        'servidor.UserIva = ""

        Conn = clasEjecutar.conectarsql("Francisco", "AGUACOM", "comagua", "OPGSM")
        '  Conn = clasEjecutar.conectarsql("MX-REY-AS25", "sa", "cow1boy", "OPGSM")
        If Conn Is Nothing Then
            infoemple.continuar = False
            MessageBox.Show("No tiene derechos Favor de comunicarse con el de sistemas", "Error")
            Application.Exit()
        End If
    End Sub

    Private Sub txtuser_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtuser.GotFocus
        txtuser.SelectAll()
    End Sub

    Private Sub txtpass_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpass.GotFocus
        txtpass.SelectAll()
    End Sub

   
    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class