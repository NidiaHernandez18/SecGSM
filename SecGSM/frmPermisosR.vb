Imports System.Windows.Forms
Imports System.Data
Imports System.Data.SqlClient

Public Class frmPermisosR
    Private conex1 As String
    Private conex2 As String
    Private query As String
    Private opcU As Integer

    Dim cnn As sql SqlConnection


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



    Private Sub frmPermisosR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim auxnomina As Integer
    End Sub

    Private Sub comboB_SelecSistema_Click(sender As Object, e As EventArgs) Handles comboB_SelecSistema.Click

    End Sub

    Private Sub comboB_Empresa_Click(sender As Object, e As EventArgs) Handles comboB_Empresa.Click

    End Sub

    Private Sub comboB_modulos_Click(sender As Object, e As EventArgs) Handles comboB_modulos.Click

    End Sub

    Private Sub informacion()
        checkLis_Lectura.Items.Clear()
        checkList_modulos.Items.Clear()


            query = "select OPCION AS MOD1 from tbl_progmod where  UPPER(MODULO)='" +comboB_modulos.Text.ToUpper() + "' and UPPER(programa)='"+comboB_SelecSistema.Text.ToUpper()+"'";




        Try


        Catch ex As Exception
            MessageBox.Show("Error de concurrencia:" & vbCrLf & ex.Message)
        Finally
            If 

        End Try
    End Sub
End Class