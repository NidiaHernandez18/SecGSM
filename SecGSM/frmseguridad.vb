Imports System.Windows.Forms
Public Class frmseguridad

    Private Passdata As New DataTable
    Private opcU As Integer
    Dim AuxSeleccion As String
    Dim selec As Integer
    Dim rec As New ClassCone
    Dim enc As New Cblowfish
    Dim varconectar As OleDb.OleDbConnection
    Dim varconectar2 As SqlClient.SqlConnection
    Dim usuariox As String = ""
    Dim opcionx As Long
    Public MSSQL As Boolean = False
    Dim dtbAdmin As DataTable
    Dim dtbSuper As DataTable
    Dim BolLoad As Boolean

    Public Property conectar() As OleDb.OleDbConnection
        Get
            conectar = VarConectar
        End Get
        Set(ByVal value As OleDb.OleDbConnection)
            VarConectar = value
        End Set
    End Property
    Public Property conectar(ByVal IsMsSQL As Boolean) As SqlClient.SqlConnection
        Get
            conectar = varconectar2
        End Get
        Set(ByVal value As SqlClient.SqlConnection)
            varconectar2 = value
            MSSQL = IsMsSQL
        End Set
    End Property

    Public Property Usuario() As String
        Get
            Usuario = usuariox
        End Get
        Set(ByVal value As String)
            usuariox = value
        End Set
    End Property


    Public Property opcionmenu() As Long
        Get
            opcionmenu = opcionx
        End Get
        Set(ByVal value As Long)
            opcionx = value
        End Set
    End Property


    Private Sub btnsalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsalir.Click
        Me.Close()
    End Sub

    Private Sub Enabdes(ByVal verd As Boolean)
        BtnOk.Visible = verd
        btnEditar.Visible = verd
        Btneliminar.Visible = verd
        btnsalir.Visible = verd
        btnguardar.Visible = Not verd
        BtnCancel.Visible = Not verd
        TxtUser.Enabled = Not verd
        TxtFull.Enabled = Not verd
        Txtpass.Enabled = Not verd
        txtpassc.Enabled = Not verd
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
            txtpassc.Text = ""
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
            Passdata = rec.RecDatatable("SELECT * FROM tbl_usuarios WHERE usuario='" & resul & "'", IIf(IsNothing(varconectar), varconectar2, varconectar))

            If Passdata.Rows.Count <= 0 Then
                MessageBox.Show("El Usuario No existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Function
            End If

            xpass = enc.Decrypt(Passdata.Rows(0).Item("PASSW"))
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
            If Passdata.Rows(0).Item("passwc").ToString.Trim.Length > 0 Then
                txtpassc.Text = Passdata.Rows(0).Item("passwc")
            End If
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
        If Not IsNothing(varconectar) Then
            buscar.conectar = varconectar
        Else
            buscar.conectar(True) = varconectar2
        End If

        buscar.infobuscar = ""
        buscar.datos = "Select usuario,nombre from tbl_usuarios order by usuario"
        buscar.ShowDialog()
        resu = buscar.infobuscar
        buscar = Nothing

        If resu.Length = 0 Then Exit Sub

        Enabdes(False)
        If informacion(resu) Then
            opcU = 0
            If MessageBox.Show("Seguro de eliminar el usuario", "Usuario", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                rec.ExecuteSql("delete tbl_usuarios where usuario='" & TxtUser.Text.Trim.ToUpper & "'", IIf(IsNothing(varconectar), varconectar2, varconectar))
                Call BtnCancel_Click(Me, e)
            Else
                Enabdes(True)
            End If
        Else
            Enabdes(True)

        End If
    End Sub


    Private Sub btnguardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnguardar.Click
        Dim fechafin As Date
        If Not validaciones() Then
            Exit Sub
        End If
        Try
            Select Case opcU
                Case 1
                    Passdata = New DataTable
                    Passdata = rec.RecDatatable("SELECT * FROM tbl_usuarios WHERE usuario='" & TxtUser.Text.Trim.ToUpper & "'", IIf(IsNothing(varconectar), varconectar2, varconectar))

                    If Passdata.Rows.Count > 0 Then
                        Passdata.Clear()
                        MessageBox.Show("El usuario ya existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        TxtUser.Focus()
                        Exit Sub

                    End If
                    Passdata.Clear()
                    txrexpira.Text = IIf(Val(txrexpira.Text) = 0, 90, Val(txrexpira.Text))
                    fechafin = DateAdd(DateInterval.Day, Val(txrexpira.Text), Now.Date)


                    rec.ExecuteSql("insert into tbl_usuarios(nombre,usuario,passw,cambiarp,deshabili," _
                                            & "bloqueada,expira,intentos,expiraf,email,tel,exten,passwc,Rol,IDAdmin,IDSuper)values('" _
                              & TxtFull.Text.Trim & "','" & TxtUser.Text.Trim.ToUpper & "','" & enc.Encrypt(Txtpass.Text) & "'," _
                              & IIf(CheckLogon.Checked, 1, 0) & "," & IIf(CheckDes.Checked, 1, 0) & "," _
                              & IIf(CheckBloquear.Checked, 1, 0) & "," & Val(txrexpira.Text) & "," _
                              & Val(txtNumBloq.Text) & ",'" & Format(fechafin, "yyyy/MM/dd") & "','" _
                              & txtcorreo.Text.Trim & "','" & txttel.Text.Trim & "','" & txtext.Text.Trim & "','" & txtpassc.Text & "','" & cboRol.Text & "','" & cboAdmin.Text & "','" & cboSuper.Text & "')", IIf(IsNothing(varconectar), varconectar2, varconectar))

                Case 2
                    rec.ExecuteSql("update tbl_usuarios set nombre='" & TxtFull.Text & "',passw='" & enc.Encrypt(Txtpass.Text) & "'," _
                                   & "cambiarp=" & IIf(CheckLogon.Checked, 1, 0) & "," _
                                   & "deshabili=" & IIf(CheckDes.Checked, 1, 0) & "," _
                                   & "bloqueada=" & IIf(CheckBloquear.Checked, 1, 0) & "," _
                                   & "expira=" & Val(txrexpira.Text) & ",intentos=" & Val(txtNumBloq.Text) _
                                   & ",email='" & (txtcorreo.Text.Trim) & "'," _
                                   & "tel='" & (txttel.Text.Trim) & "'," _
                                   & "exten='" & (txtext.Text.Trim) & "'," _
                                   & "passwc='" & (txtpassc.Text.Trim) & "'," _
                                   & "ROL='" & (cboRol.Text.Trim) & "'," _
                                   & "IDADMIN='" & (cboAdmin.Text.Trim) & "'," _
                                   & "IDSuper='" & (cboSuper.Text.Trim) & "'" _
                                   & " where usuario ='" & TxtUser.Text.Trim.ToUpper & "'", IIf(IsNothing(varconectar), varconectar2, varconectar))
            End Select

            Call BtnCancel_Click(Me, e)
            Call Llena_Users()
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

        If cboRol.Text = "USER" Then
            If cboAdmin.Text = "" Then
                MsgBox("Debe seleccionar un administrador para este usuario", MsgBoxStyle.Exclamation, "AVISO")
                cboAdmin.Focus()
            ElseIf cboSuper.Text = "" Then
                MsgBox("Debe seleccionar un supervisor para este usuario", MsgBoxStyle.Exclamation, "AVISO")
                cboSuper.Focus()
            End If
        ElseIf cboSuper.Text = "SUPER" Then
            If cboAdmin.Text = "" Then
                MsgBox("Debe seleccionar un administrador para este usuario", MsgBoxStyle.Exclamation, "AVISO")
                cboAdmin.Focus()
            End If
        End If
        validaciones = True

    End Function



    Private Sub BtnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        opcU = 1
        Enabdes(False)
        TxtUser.Focus()
    End Sub

    
    Private Sub frmseguridad_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = ChrW(13) Then
            SendKeys.Send("{TAB}")
        End If

        If e.KeyChar = ChrW(27) Then
            Me.Close()
        End If
    End Sub
    Private Sub Llena_Users()
        Dim strsql As String
        strsql = "SELECT * FROM tbl_usuarios WHERE Rol='ADMIN'"
        dtbAdmin = rec.RecDatatable(strsql, IIf(IsNothing(varconectar), varconectar2, varconectar))
        cboAdmin.Items.Clear()
        For Each dtr As DataRow In dtbAdmin.Rows
            cboAdmin.Items.Add(dtr!Usuario)
        Next

        strsql = "SELECT * FROM tbl_usuarios WHERE Rol='SUPER'"
        dtbSuper = rec.RecDatatable(strsql, IIf(IsNothing(varconectar), varconectar2, varconectar))
        
    End Sub
   

    Private Sub frmseguridad_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BtnCancel_Click(Me, e)
        derechousua(opcionx, Me, usuariox)
        Call Llena_Users()
        bolLoad = True
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
    Private Sub txtNumBloq_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtNumBloq.SelectAll()
    End Sub
    Private Sub txrexpira_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txrexpira.SelectAll()
    End Sub

    Private Sub txtcorreo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcorreo.GotFocus
        txtcorreo.SelectAll()
    End Sub
    Private Sub txtpassc_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpassc.GotFocus
        txtpassc.SelectAll()
    End Sub
    Private Sub txttel_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txttel.GotFocus
        txttel.SelectAll()
    End Sub
    Private Sub txtext_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtext.GotFocus
        txtext.SelectAll()
    End Sub

#End Region


    Private Sub btnEditar_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditar.Click
        Dim resu As String
        Dim buscar As New frmbuscar
        Dim strbuscar As String
        If Not IsNothing(varconectar) Then
            buscar.conectar = varconectar
        Else
            buscar.conectar(True) = varconectar2
        End If
        buscar.infobuscar = ""
        buscar.datos = "Select usuario,nombre from tbl_usuarios order by usuario"
        buscar.ShowDialog()
        strbuscar = buscar.infobuscar
        buscar.Dispose()
        buscar = Nothing


        If strbuscar.Length > 0 Then
            resu = strbuscar
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

    Public Sub derechousua(ByVal opcion As Long, ByVal XFORM As Form, ByVal Usuarioy As String)
        Dim Ocontrol As Control
        Dim permiopcion As DataTable

        Dim perm As Boolean
        Dim perm2 As Boolean
        perm = False
        perm2 = False


        permiopcion = New DataTable
        If MSSQL Then
            permiopcion = rec.RecDatatable("select permiso,impresion from tbl_permisos " _
                                   & " where Usuario='" & Usuarioy & "' and numero=" & opcion & " order by numero", varconectar2)
        Else
            permiopcion = rec.RecDatatable("select permiso,impresion from tbl_permisos " _
                                   & " where Usuario='" & Usuarioy & "' and numero=" & opcion & " order by numero", varconectar)
        End If
       


        If permiopcion.Rows.Count = 0 Then Exit Sub

        perm = IIf(permiopcion.Rows(0).Item(0) = 1, True, False)
        perm2 = IIf(permiopcion.Rows(0).Item(1) = 1, True, False)

        permiopcion.Clear()
        permiopcion.Dispose()
        If Not perm Then
            For Each Ocontrol In XFORM.Controls
                If TypeOf Ocontrol Is Button Then
                    If Ocontrol.Name.ToUpper <> "BTNSALIR" Then
                        Ocontrol.Enabled = False
                    End If
                End If

            Next
        End If

        If Not perm2 Then
            For Each Ocontrol In XFORM.Controls
                If TypeOf Ocontrol Is Button Then
                    If Ocontrol.Name.ToUpper = "BTNREP" Then
                        Ocontrol.Enabled = False
                    End If
                End If

            Next
        End If

    End Sub


    Private Sub cboRol_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboRol.SelectedIndexChanged
        Select Case cboRol.Text
            Case Is = "ADMIN"
                cboAdmin.Enabled = False
                cboAdmin.SelectedIndex = -1
                cboSuper.Enabled = False
                cboSuper.SelectedIndex = -1
            Case Is = "SUPER"
                cboAdmin.Enabled = True
                cboAdmin.SelectedIndex = -1
                cboSuper.Enabled = False
                cboSuper.SelectedIndex = -1
            Case Else
                cboAdmin.Enabled = True
                cboAdmin.SelectedIndex = -1
                cboSuper.Enabled = True
                cboSuper.SelectedIndex = -1
        End Select
    End Sub

    Private Sub cboAdmin_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboAdmin.SelectedIndexChanged
        If BolLoad Then
            cboSuper.Items.Clear()
            For Each dtr As DataRow In dtbSuper.Select("IDAdmin='" & cboAdmin.Text & "'")
                cboSuper.Items.Add(dtr!Usuario)
            Next
        End If
    End Sub

    Private Sub cmdHuella_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHuella.Click
        Dim frm As New frmBiometria
        frm.DB = rec
        frm.connDb = varconectar2
        frm.Usuario = usuariox
        frm.ShowDialog()
        frm.Close()
    End Sub
End Class