Imports System.Windows.Forms
Imports System.Data
Imports System.Data.SqlClient

Public Class frmPermisosR
    
    Private query As String
    Private opcU As Integer
    Private objLectura As SqlDataReader
    Private objDataAdapter As New SqlDataAdapter


    Dim cadenaReloj As String
    Dim cadenaNomina As String
    Dim conexionReloj As SqlConnection
    Dim conexionNomina As SqlConnection
    Dim comando As SqlCommand




    Public conex1 As SqlConnection 'ADODB.Connection 'conexion a
    Public conex2 As SqlConnection 'ADODB.Connection ''
    Private reg As New SqlConnection


    Dim dt As DataTable

    'Private regaux As New ADODB.Recordset
    ' Private reg As New ADODB.Recordset
    'Private Empleado As New ADODB.Recordset






    Public Property conectarReloj() As String
        Get
            conectarReloj = cadenaReloj

        End Get
        Set(value As String)
            cadenaReloj = value

        End Set
    End Property
    Public Property conectarNomina() As String
        Get
            conectarNomina = cadenaNomina
        End Get
        Set(value As String)
            cadenaNomina = value
        End Set
    End Property


    Private Sub frmPermisosR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim auxnomina As Integer 
        query = "select * from tbl_programas ORDER BY programas"
        dt = New DataTable

        Try
            reg = conex1
            If reg.State = 0 Then reg.Open()
            comando = New SqlCommand(query, reg)
            objDataAdapter.SelectCommand = comando
            objDataAdapter.Fill(dt)
            comboB_SelecSistema.DataSource = dt
            comboB_SelecSistema.DisplayMember = "Programas"
            reg.Close()
            If Not IsNothing(conex2) Then
                reg = conex2
                query = "select * from nomcof ORDER BY numero"
            Else
                reg = conex1
                query = "select * from TBl_empresas ORDER BY numero"
            End If

            If reg.State = 0 Then reg.Open()
            comando = New SqlCommand(query, reg)

            objLectura = comando.ExecuteReader()
            Do While objLectura.Read
                comboB_Empresa.Items.Add(Trim(Str(objLectura("numero")) & "-" & Trim(objLectura("empresa"))))
            Loop
            comboB_Empresa.SelectedIndex = 0
            auxnomina = Val(Mid(comboB_Empresa.Text, 1, InStr(comboB_Empresa.Text, "-") - 1))


        Catch ex As Exception
            MsgBox("LOAD" + ex.Message)
        Finally
            objLectura.Close()
            reg.Close()
        End Try
        Call llenaModulo()
        Call informacion()
        Enandes(True)
    End Sub

    Private Sub comboB_SelecSistema_Click(sender As Object, e As EventArgs) Handles comboB_SelecSistema.Click
        Call llenaModulo()
    End Sub
    Private Sub comboB_SelecSistema_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles comboB_SelecSistema.SelectionChangeCommitted
        Call llenaModulo()
    End Sub
    Private Sub llenaModulo()
        Try
            comboB_modulos.Items.Clear()
            checkList_modulos.Items.Clear()
            reg = conex1
            query = "select DISTINCT (MODULO)AS MOD1 from tbl_progmod where  UPPER(programa)='" & comboB_SelecSistema.Text.ToUpper & "'"
            If reg.State = 0 Then reg.Open()
            comando = New SqlCommand(query, reg)
            objLectura = comando.ExecuteReader()
            Do While objLectura.Read
                comboB_modulos.Items.Add(objLectura("MOD1"))
            Loop
            comboB_modulos.SelectedIndex = 0
        Catch ex As Exception
            MsgBox("SelecSistema " & vbCrLf & ex.Message)

        Finally
            objLectura.Close()
            reg.Close()
        End Try
    End Sub

    Private Sub comboB_Empresa_Click(sender As Object, e As EventArgs) Handles comboB_Empresa.Click
        'cmbempresa

        'Call informacion()


    End Sub
    Private Sub comboB_Empresa_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles comboB_Empresa.SelectionChangeCommitted
        Call informacion()
    End Sub

    Private Sub comboB_modulos_Click(sender As Object, e As EventArgs) Handles comboB_modulos.Click
        'cmbmodulos
        'Call informacion()

    End Sub
    Private Sub comboB_modulos_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles comboB_modulos.SelectionChangeCommitted
        Call informacion()
    End Sub

    Private Sub informacion()

        Try
            checkLis_Lectura.Items.Clear()
            checkList_modulos.Items.Clear()
            reg = conex1
            query = "select OPCION AS MOD1 from tbl_progmod where  UPPER(MODULO)='" + comboB_modulos.SelectedItem.ToString().ToUpper + "' and UPPER(programa)='" + comboB_SelecSistema.Text.ToUpper() + "'"
            If reg.State = 0 Then reg.Open()
            comando = New SqlCommand(query, reg)
            objLectura = comando.ExecuteReader()
            Do While objLectura.Read
                checkList_modulos.Items.Add(objLectura("MOD1"))
                checkLis_Lectura.Items.Add("Lectura")
            Loop
        Catch ex As Exception
            MsgBox("Error de concurrencia: informacion()" & vbCrLf & ex.Message)
        Finally

            objLectura.Close()
            reg.Close()
        End Try
        Call Usuarios()

    End Sub

    Private Sub Usuarios()
        Try
            dt = New DataTable
            If Not IsNothing(conex2) Then
                reg = conex2
            Else
                reg = conex1
            End If
            query = "SELECT * FROM TBL_USUARIOS where EMPRESA='" & comboB_Empresa.Text & "'"
            If reg.State = 0 Then reg.Open()
            comando = New SqlCommand(query, reg)
            objDataAdapter.SelectCommand = comando
            objDataAdapter.Fill(dt)
            dgv_permisos.DataSource = dt
            dgv_permisos.Columns(0).Visible = False
            dgv_permisos.Columns(2).Visible = False
            dgv_permisos.Columns(4).Visible = False
            dgv_permisos.DefaultCellStyle.WrapMode = DataGridViewTriState.True

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            objLectura.Close()
            reg.Close()
        End Try


    End Sub

    Private Sub bttn_Cencelar_Click(sender As Object, e As EventArgs) Handles bttn_Cencelar.Click
        cancelar()
    End Sub
    Private Sub cancelar()
        opcU = 0
        Enandes(True)
        txt_nombre.Text = "" 'text 4
        txt_password.Text = "" 'text 6
        txt_usuario.Text = "" 'text5
        txt_nombre.Focus()
    End Sub

    Private Sub bttn_Grabar_Click(sender As Object, e As EventArgs) Handles bttn_Grabar.Click
        If Not validaciones() Then
            Exit Sub
        End If
        Select Case opcU
            Case 1
                Try
                    reg = conex1
                    If reg.State = 0 Then reg.Open()

                    query = "delete from tbl_permisos where PROGRAMA='" & comboB_SelecSistema.Text & "' AND NO_EMPRESA='" & comboB_Empresa.Text & "' AND MODULOS='" & comboB_modulos.Text & "' and usuario='" & txt_usuario.Text & "'"
                    comando = New SqlCommand(query, reg)
                    comando.ExecuteNonQuery()

                    query = "INSERT INTO tbl_usuarios (Empresa, Usuario, passw, nombre) Values('" & comboB_Empresa.Text & "', '" & txt_usuario.Text & "', '" & txt_password.Text & "', '" & txt_nombre.Text & "')"
                    comando = New SqlCommand(query, reg)
                    comando.ExecuteNonQuery()

                    For contador As Integer = 0 To checkList_modulos.Items.Count - 1 Step 1
                        If (checkList_modulos.GetItemChecked(contador)) Then
                            query = "insert into tbl_permisos (programa,No_empresa,Modulos,opcion,tipo,usuario) values('" & comboB_SelecSistema.Text & "','" & comboB_Empresa.Text & "', '" & comboB_modulos.Text & "','" & checkList_modulos.Items(contador).ToString() & "','" & IIf(checkLis_Lectura.GetItemChecked(contador), "S", "N") & "','" & txt_usuario.Text & "')"
                            comando = New SqlCommand(query, reg)
                            comando.ExecuteNonQuery()
                        End If
                    Next

                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    reg.Close()
                End Try
               
            Case 2
                Try
                    reg = conex1
                    If reg.State = 0 Then reg.Open()

                    query = "delete from tbl_permisos where PROGRAMA='" & comboB_SelecSistema.Text & "' AND NO_EMPRESA='" & comboB_Empresa.Text & "' AND MODULOS='" & comboB_modulos.Text & "' and usuario='" & txt_usuario.Text & "'"
                    comando = New SqlCommand(query, reg)
                    comando.ExecuteNonQuery()

                    For contador As Integer = 0 To checkList_modulos.Items.Count - 1 Step 1
                        If (checkList_modulos.GetItemChecked(contador)) Then
                            query = "insert into tbl_permisos (programa,No_empresa,Modulos,opcion,tipo,usuario) values('" & comboB_SelecSistema.Text & "','" & comboB_Empresa.Text & "', '" & comboB_modulos.Text & "','" & checkList_modulos.Items(contador).ToString() & "','" & IIf(checkLis_Lectura.GetItemChecked(contador), "S", "N") & "','" & txt_usuario.Text & "')"
                            comando = New SqlCommand(query, reg)
                            comando.ExecuteNonQuery()
                        End If
                    Next
                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    reg.Close()
                End Try

        End Select
        cancelar()
        informacion()
        cargarPermisos()
        Usuarios()

    End Sub
    Private Function validaciones() As Boolean
        validaciones = False

        If Len(txt_nombre.Text) = 0 Then
            MsgBox("Por favor capture el nombre del usuario", vbCritical, "Informacion")
            Exit Function
        End If
        If Len(txt_usuario.Text) = 0 Then
            MsgBox("Por favor capture el usuario del usuario", vbCritical, "Informacion")
            Exit Function
        End If
        If Len(txt_password.Text) = 0 Then
            MsgBox("Por favor capture el password del usuario", vbCritical, "Informacion")
            Exit Function
        End If
        Select Case opcU
            Case 1
                Try
                    query = "select * from tbl_usuarios where EMPRESA='" & comboB_Empresa.Text & "' and usuario='" & txt_usuario.Text & "'"
                    reg = conex1
                    If reg.State = 0 Then reg.Open()
                    comando = New SqlCommand(query, reg)
                    If (comando.ExecuteScalar() > 0) Then
                        MsgBox("El usuario ya existe", vbCritical, "Informacion")
                        Exit Function
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    reg.Close()
                End Try
            Case 2

        End Select
        validaciones = True
    End Function
    Private Sub bttn_Cerrar_Click(sender As Object, e As EventArgs) Handles bttn_Cerrar.Click
        Me.Close()
    End Sub

    Private Sub bttn_Eliminar_Click(sender As Object, e As EventArgs) Handles bttn_Eliminar.Click
        Try
            If (dgv_permisos.RowCount > 0 And dgv_permisos.SelectedRows.Count > 0) Then
                Dim fila As Integer
                Dim usuario As String
                fila = dgv_permisos.CurrentRow.Index
                usuario = dgv_permisos.Rows(fila).Cells(1).Value.ToString()

                    If MsgBox("¿Estas seguro de continuar con este proceso?  " + vbLf + "el usuario a eliminar es " & usuario, vbCritical + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        reg = conex1
                        If reg.State = 0 Then reg.Open()
                        query = "delete from tbl_usuarios where usuario='" & usuario & "'"
                        comando = New SqlCommand(query, reg)
                        comando.ExecuteNonQuery()

                        query = "delete from tbl_permisos where usuario='" & usuario & "'"
                        comando = New SqlCommand(query, reg)
                        comando.ExecuteNonQuery()
                    End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            reg.Close()
        End Try

        
        Call informacion()
        
    End Sub

    Private Sub bttn_Editar_Click(sender As Object, e As EventArgs) Handles bttn_Editar.Click
        opcU = 2
        Dim usuario As String
        Dim password As String
        Dim nombre As String
        Dim fila As Integer
         If (dgv_permisos.RowCount > 0 And dgv_permisos.SelectedRows.Count > 0) Then
            If comboB_modulos.Items.Count > 0 Then
                fila = dgv_permisos.CurrentRow.Index
                usuario = dgv_permisos.Rows(fila).Cells(1).Value.ToString()
                password = dgv_permisos.Rows(fila).Cells(2).Value.ToString()
                nombre = dgv_permisos.Rows(fila).Cells(3).Value.ToString()

                txt_nombre.Text = nombre
                txt_usuario.Text = usuario
                txt_password.Text = password
                cargarPermisos()
                Enandes(False)
                txt_usuario.Enabled = False
            Else
                MsgBox("No hay Modulos para asignar derechos", vbCritical, "Error")
            End If
        Else
            MsgBox("No hay Usuarios para asignar derechos", vbCritical, "Error")
        End If

    End Sub
    Private Sub cargarPermisos()
        Try
            For contador As Integer = 0 To checkList_modulos.Items.Count - 1 Step 1
                checkList_modulos.SetItemChecked(contador, False)
                checkLis_Lectura.SetItemChecked(contador, False)
            Next
            query = "select * from Tbl_permisos where PROGRAMA='" & comboB_SelecSistema.Text & "' AND NO_EMPRESA='" & comboB_Empresa.Text & "' AND MODULOS='" & comboB_modulos.Text & "' and usuario='" & txt_usuario.Text & "'"
            reg = conex1
            If reg.State = 0 Then reg.Open()
            comando = New SqlCommand(query, reg)
            objLectura = comando.ExecuteReader()
            Do While objLectura.Read
                For contador As Integer = 0 To checkList_modulos.Items.Count - 1 Step 1
                    If (objLectura("opcion") = checkList_modulos.Items(contador).ToString()) Then
                        checkList_modulos.SetItemChecked(contador, True)
                        If (objLectura("Tipo") = "S") Then
                            checkLis_Lectura.SetItemChecked(contador, True)
                        End If
                    End If
                Next
            Loop

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            objLectura.Close()
            reg.Close()
        End Try


    End Sub
    Private Sub btt_Nuevo_Click(sender As Object, e As EventArgs) Handles btt_Nuevo.Click
        opcU = 1
        If comboB_modulos.Items.Count > 0 Then
            Enandes(False)
            txt_nombre.Text = "" 'text 4

            txt_password.Text = "" 'text 6
            txt_usuario.Text = "" 'text5
            txt_nombre.Focus()
        Else
            MsgBox("No hay Modulos par asignar derechos", vbCritical, "Error")
        End If
    End Sub
    Private Sub Enandes(ByVal verd As Boolean)
        btt_Nuevo.Visible = verd
        bttn_Editar.Visible = verd
        bttn_Eliminar.Visible = verd
        bttn_Cerrar.Visible = verd
        bttn_Grabar.Visible = Not verd
        bttn_Cencelar.Visible = Not verd
        comboB_SelecSistema.Enabled = verd
        comboB_Empresa.Enabled = verd
        checkLis_Lectura.Enabled = Not verd
        checkList_modulos.Enabled = Not verd
        comboB_modulos.Enabled = verd
        txt_nombre.Enabled = Not verd
        txt_password.Enabled = Not verd
        txt_usuario.Enabled = Not verd
    End Sub

    
    
 
End Class