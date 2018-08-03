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


    Dim selec() As String



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
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            objLectura.Close()
            reg.Close()
        End Try


    End Sub

    Private Sub bttn_Cencelar_Click(sender As Object, e As EventArgs) Handles bttn_Cencelar.Click
        opcU = 0
        Enandes(True)
        txt_nombre.Text = "" 'text 4
        txt_password.Text = "" 'text 6
        txt_usuario.Text = "" 'text5
        txt_nombre.Focus()
    End Sub

    Private Sub bttn_Grabar_Click(sender As Object, e As EventArgs) Handles bttn_Grabar.Click

    End Sub

    Private Sub bttn_Cerrar_Click(sender As Object, e As EventArgs) Handles bttn_Cerrar.Click
        Me.Close()
    End Sub

    Private Sub bttn_Eliminar_Click(sender As Object, e As EventArgs) Handles bttn_Eliminar.Click
        If (dgv_permisos.RowCount > 0) Then
            Dim fila As Integer
            Dim columnas As Integer
            selec=New String [columnas]

            If MsgBox("¿Estas seguro de continuar con este proceso?  " + vbLf + "el usuario a eliminar es", vbCritical + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

            End If
        End If
        
    End Sub

    Private Sub bttn_Editar_Click(sender As Object, e As EventArgs) Handles bttn_Editar.Click

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