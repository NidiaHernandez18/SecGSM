Imports System.Windows.Forms
Imports System.Data
Imports System.Data.SqlClient

Public Class frmPermisosR
    
    Private query As String
    Private opcU As Integer
    Private objLectura As SqlDataReader

    Dim cadenaReloj As String
    Dim cadenaNomina As String
    Dim conexionReloj As SqlConnection
    Dim conexionNomina As SqlConnection
    Dim comando As SqlCommand



    'Public conex1 As New ADODB.Connection 'conexion a
    'Public conex2 As New ADODB.Connection ''
    ' Private regaux As New ADODB.Recordset
    ' Private reg As New ADODB.Recordset
    '
    'Private Empleado As New ADODB.Recordset

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

        cnn = New SqlConnection(conex1)
        query = "select * from tbl_programas ORDER BY programas"
        comando = New SqlCommand(query, cnn)
        cnn.Open()
        Try
            objLectura = comando.ExecuteReader()
            If objLectura.Read Then
                Do While objLectura.Read
                    comboB_SelecSistema.Items.Add(objLectura("programas"))
                Loop
                'comboB_SelecSistema.SelectedItem = 0

            End If

            If cnn.State = 1 Then cnn.Close()

            If conex2.Length > 0 Then

                cnn = New SqlConnection(conex1)
                query = "select * from Tbl_empresas ORDER BY empresas"
               
            Else

                cnn = New SqlConnection(conex2)
                query = "select * from nomcof ORDER BY empresa"

            End If
            auxnomina = 0
            MsgBox(conex1 & " " & conex2)

            comando = New SqlCommand(query, cnn)

            cnn.Open()
            MsgBox(query)
            objLectura = comando.ExecuteReader()
            If objLectura.Read Then
                MsgBox("3")
                Do While objLectura.Read
                    comboB_Empresa.Items.Add(Str(objLectura("numero")) & "-" & Trim(objLectura("empresa")))
                Loop
                '  comboB_SelecSistema.SelectedItem = 0
                auxnomina = Mid(comboB_Empresa.Text, 1, InStr(comboB_Empresa.Text, "-") - 1)
            End If

            If cnn.State = 1 Then cnn.Close()
            Enandes(True)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cnn.Close()

        End Try
        'MsgBox(query)



    End Sub

    Private Sub comboB_SelecSistema_Click(sender As Object, e As EventArgs) Handles comboB_SelecSistema.Click
        'cmbprogramas
        comboB_modulos.Items.Clear()
        checkList_modulos.Items.Clear()
        query = "select DISTINCT (MODULO)AS MOD1 from tbl_progmod where  UPPER(programa)='" & comboB_SelecSistema.Text & "'"
        'MsgBox(query)

    End Sub

    Private Sub comboB_Empresa_Click(sender As Object, e As EventArgs) Handles comboB_Empresa.Click
        'cmbempresa
        Call informacion()

    End Sub

    Private Sub comboB_modulos_Click(sender As Object, e As EventArgs) Handles comboB_modulos.Click
        'cmbmodulos
        Call informacion()

    End Sub

    Private Sub informacion()
        checkLis_Lectura.Items.Clear()
        checkList_modulos.Items.Clear()


        query = "select OPCION AS MOD1 from tbl_progmod where  UPPER(MODULO)='" + comboB_modulos.Text.ToUpper() + "' and UPPER(programa)='" + comboB_SelecSistema.Text.ToUpper() + "'"

        ' MsgBox(query)



        Try


        Catch ex As Exception
            MsgBox("Error de concurrencia:" & vbCrLf & ex.Message)
        Finally


        End Try
    End Sub

    Private Sub bttn_Cencelar_Click(sender As Object, e As EventArgs) Handles bttn_Cencelar.Click

    End Sub

    Private Sub bttn_Grabar_Click(sender As Object, e As EventArgs) Handles bttn_Grabar.Click

    End Sub

    Private Sub bttn_Cerrar_Click(sender As Object, e As EventArgs) Handles bttn_Cerrar.Click
        Me.Close()
    End Sub

    Private Sub bttn_Eliminar_Click(sender As Object, e As EventArgs) Handles bttn_Eliminar.Click

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
End Class