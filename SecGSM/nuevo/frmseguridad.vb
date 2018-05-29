Public Class frmseguridad

    Private Passdata As New DataTable
    Private opcU As Integer
    Dim AuxSeleccion As String
    Dim selec As Integer

    Private Sub btnsalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsalir.Click
        Me.Close()
    End Sub

    Private Sub Enabdes(ByVal verd As Boolean)
        BtnOk.Visible = verd
        btnEditar.Visible = verd
        Btneliminar.Visible = verd
        btnsalir.Visible = verd
        Button1.Visible = verd
        btnguardar.Visible = Not verd
        BtnCancel.Visible = Not verd
        TxtUser.Enabled = Not verd
        TxtFull.Enabled = Not verd
        Txtpass.Enabled = Not verd
        Txtconf.Enabled = Not verd
        txtNumBloq.Enabled = Not verd
        txrexpira.Enabled = Not verd
        CheckLogon.Enabled = Not verd
        CheckDes.Enabled = Not verd
        CheckBloquear.Enabled = Not verd
        txtNumBloq.Enabled = Not verd
        txrexpira.Enabled = Not verd
        txtcorreo.Enabled = Not verd
        txttel.Enabled = Not verd
        txtext.Enabled = Not verd

        If Not verd Then
            CheckLogon.CheckState = CheckState.Unchecked
            CheckDes.CheckState = CheckState.Unchecked
            CheckBloquear.CheckState = CheckState.Unchecked
            TxtUser.Text = ""
            TxtFull.Text = ""
            Txtpass.Text = ""
            Txtconf.Text = ""
            txtNumBloq.Text = 0
            txrexpira.Text = 0
            txtcorreo.Text = ""
            txttel.Text = ""
            txtext.Text = ""


        End If
    End Sub

    Private Sub btnEditar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim resu As String
        resu = InputBox("Usuario a modificar", "Introduzca el usuario")
        If resu.Length = 0 Then
            Exit Sub
        End If
        Enabdes(False)

        If informacion(resu) Then
            opcU = 2
            TxtUser.Enabled = False
        Else
            Enabdes(True)

        End If

    End Sub

    Private Function informacion(ByVal resul As String) As Boolean
        Dim xpass As String
        Try
            informacion = False

            Passdata = New DataTable
            Passdata = clasEjecutar.RecDatatable("SELECT * FROM TBL_USUARIOS WHERE usuario='" & resul & "'", Conn)

            If Passdata.Rows.Count <= 0 Then
                MessageBox.Show("El Usuario No existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Function
            End If

            xpass = clasEncdesc.Decrypt(Passdata.Rows(0).Item("PASSW"))
            TxtUser.Text = resul
            TxtFull.Text = IIf(IsDBNull(Passdata.Rows(0).Item("nombre")), "", Passdata.Rows(0).Item("nombre"))
            Txtpass.Text = xpass
            Txtconf.Text = xpass
            CheckLogon.Checked = IIf(Passdata.Rows(0).Item("cambiarp") = 1, True, False)
            CheckDes.Checked = IIf(Passdata.Rows(0).Item("deshabili") = 1, True, False)
            CheckBloquear.Checked = IIf(Passdata.Rows(0).Item("bloqueada") = 1, True, False)
            txtNumBloq.Text = Passdata.Rows(0).Item("intentos")
            txrexpira.Text = Passdata.Rows(0).Item("expira")
            txtcorreo.Text = Passdata.Rows(0).Item("email")
            txttel.Text = Passdata.Rows(0).Item("tel")
            txtext.Text = Passdata.Rows(0).Item("exten")
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Application Error")
        Finally
            Passdata.Clear()
            Passdata = Nothing
        End Try
    End Function

    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        opcU = 0
        Enabdes(True)
    End Sub

    Private Sub Btneliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btneliminar.Click
        Dim resu As String
        Dim buscar As New frmbuscar
        infoemple.buscares = ""
        buscar.datos = "Select Usuario,nombre from tbl_usuarios order by usuario"
        buscar.ShowDialog()
        buscar = Nothing

        If infoemple.buscares.Length > 0 Then
            resu = infoemple.buscares
        Else
            Exit Sub
        End If
        Enabdes(False)
        If informacion(resu) Then
            opcU = 0
            If MessageBox.Show("Seguro de eliminar el usuario", "Usuario", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                clasEjecutar.ExecuteSql("delete tbl_usuarios where usuario='" & TxtUser.Text.Trim.ToUpper & "'", Conn)
                Call BtnCancel_Click(Me, e)
            Else
                Enabdes(True)
            End If
        Else
            Enabdes(True)

        End If
    End Sub

   
    Private Sub btnguardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnguardar.Click

        If Not validaciones() Then
            Exit Sub
        End If
        Try
            Select Case opcU
                Case 1
                    Passdata = New DataTable
                    Passdata = clasEjecutar.RecDatatable("SELECT * FROM TBL_USUARIOS WHERE usuario='" & TxtUser.Text.Trim.ToUpper & "'", Conn)

                    If Passdata.Rows.Count > 0 Then
                        Passdata.Clear()
                        MessageBox.Show("El usuario ya existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        TxtUser.Focus()
                        Exit Sub

                    End If
                    Passdata.Clear()
                    clasEjecutar.ExecuteSql("insert into tbl_usuarios(nombre,usuario,passw,cambiarp,deshabili," _
                                            & "bloqueada,expira,intentos,expiraf,email,tel,exten)values('" _
                              & TxtFull.Text.Trim & "','" & TxtUser.Text.Trim.ToUpper & "','" & clasEncdesc.Encrypt(Txtpass.Text) & "'," _
                              & IIf(CheckLogon.Checked, 1, 0) & "," & IIf(CheckDes.Checked, 1, 0) & "," _
                              & IIf(CheckBloquear.Checked, 1, 0) & "," & Val(txrexpira.Text) & "," _
                              & Val(txtNumBloq.Text) & ",'" & VB6.Format(DateTime.Now, infoemple.formato & " HH:mm:ss") & "','" _
                              & txtcorreo.Text.Trim & "','" & txttel.Text.Trim & "','" & txtext.Text.Trim & "')", Conn)

                Case 2
                    clasEjecutar.ExecuteSql("update tbl_usuarios set nombre='" & TxtFull.Text & "',passw='" & clasEncdesc.Encrypt(Txtpass.Text) & "'," _
                                   & "cambiarp=" & IIf(CheckLogon.Checked, 1, 0) & "," _
                                   & "deshabili=" & IIf(CheckDes.Checked, 1, 0) & "," _
                                   & "bloqueada=" & IIf(CheckBloquear.Checked, 1, 0) & "," _
                                   & "expira=" & Val(txrexpira.Text) & ",intentos=" & Val(txtNumBloq.Text) _
                                   & ",email='" & (txtcorreo.Text.Trim) & "'," _
                                   & "tel='" & (txttel.Text.Trim) & "'," _
                                   & "exten='" & (txtext.Text.Trim) & "'" _
                                   & " where usuario ='" & TxtUser.Text.Trim.ToUpper & "'", Conn)

            End Select

            Call BtnCancel_Click(Me, e)

        Catch ex As Exception
            MessageBox.Show("Error en " & ex.Message, "Error departamentos", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try


    End Sub

    Private Function validaciones() As Boolean
        validaciones = False
        If Len(TxtUser.Text.Trim) = 0 Then
            Call MsgBox("Introduzca el usuario por favor", vbCritical, "Información faltante")
            TxtUser.Focus()
            Exit Function
        End If
        If Len(Txtpass.Text.Trim) = 0 Then
            Call MsgBox("Introduzca la clave por favor ", vbCritical, "Información faltante")
            Txtpass.Focus()
            Exit Function
        End If

        If Len(Txtconf.Text.Trim) = 0 Then
            Call MsgBox("Introduzca la clave por favor ", vbCritical, "Información faltante")
            Txtconf.Focus()
            Exit Function
        End If
        If Txtpass.Text <> Txtconf.Text Then
            Call MsgBox("La clave y la confirmación no coinciden", vbCritical, "Información faltante")
            Txtpass.Focus()
            Exit Function
        End If

        validaciones = True

    End Function

  

    Private Sub BtnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        opcU = 1
        Enabdes(False)
        TxtUser.Focus()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim reportes As New NetUsuariossql.frmreportes
        Dim data1 As New DataSet
        clasEjecutar.ArrayDataSET(data1, "Select  nombre,usuario,(CASE CAMBIARP WHEN 1 THEN 'SI' ELSE 'NO' END) as CP," _
                                       & "(CASE DESHABILI WHEN 1 THEN 'SI' ELSE 'NO' END) as DP," _
                                       & "(CASE BLOQUEADA WHEN 1 THEN 'SI' ELSE 'NO' END) as BLOQUEADA," _
                                       & "expiraf  as expf,expira,intentos" _
                                       & " from tbl_USUarioS", Conn, , "Oseguridad")

        reportes.Datasetobj = data1
        reportes.formulasStr(0) = "empresa"
        reportes.ValueForm(0) = infoemple.Empresa
        reportes.namereporte(0) = New rptseguridad
        reportes.titulorep(0) = "Seguridad"
        reportes.ShowDialog()
        reportes.Close()
        reportes.Dispose()
        data1.Clear()
        data1.Dispose()



    End Sub

    Private Sub frmseguridad_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = ChrW(13) Then
            SendKeys.Send("{TAB}")
        End If

        If e.KeyChar = ChrW(27) Then
            Me.Close()
        End If
    End Sub

    Private Sub frmseguridad_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
     
    End Sub


    Private Sub frmseguridad_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BtnCancel_Click(Me, e)
        derechousua("SEGURIDAD DEL USUARIO", Me)

    End Sub
#Region "Seleccion"
    Private Sub TxtUser_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtUser.GotFocus
        TxtUser.SelectAll()
    End Sub
    Private Sub TxtFull_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtFull.GotFocus
        TxtFull.SelectAll()
    End Sub
    Private Sub Txtpass_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txtpass.GotFocus
        Txtpass.SelectAll()
    End Sub
    Private Sub Txtconf_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txtconf.GotFocus
        Txtconf.SelectAll()
    End Sub
    Private Sub txtNumBloq_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNumBloq.GotFocus
        txtNumBloq.SelectAll()
    End Sub
    Private Sub txrexpira_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txrexpira.GotFocus
        txrexpira.SelectAll()
    End Sub

#End Region

    
    Private Sub btnEditar_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditar.Click
        Dim resu As String
        Dim buscar As New frmbuscar
        infoemple.buscares = ""
        buscar.datos = "Select Usuario,nombre from tbl_usuarios order by usuario"
        buscar.ShowDialog()
        buscar = Nothing

        If infoemple.buscares.Length > 0 Then
            resu = infoemple.buscares
        Else
            Exit Sub
        End If
        Enabdes(False)
        If informacion(resu) Then
            TxtUser.Enabled = False
            opcU = 2
        Else
            Enabdes(True)

        End If

    End Sub
End Class