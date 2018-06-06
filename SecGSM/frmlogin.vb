Imports System.Data
Imports System.Data.SqlClient

Public Class frmlogin
    Public conex1 As SqlConnection

    Private EMPRESA As String

    Private intentos As Integer
    Public nombreaplic As String
    Public numEmp As Integer
    Private consulta As String
    Private Sub bttn_OK_Click(sender As Object, e As EventArgs) Handles bttn_OK.Click
        'cmdok
        If comB_Empresas.Text = "------- Seleccione Empresa --------" Then
            MsgBox("Seleccione una Empresa por favor", vbCritical, "LOGIN")
            Exit Sub
        End If
        intentos = intentos + 1
        If intentos > 3 Then
            MsgBox("Se ha Accedido de Intentos", vbCritical)
            'If EMPRESA.State = 1 Then EMPRESA.Close
            If conex1.State Then conex1.Close()
        Me.Close()
        Exit Sub
        End If
        'If EMPRESA.State = 1 Then EMPRESA.Close
        If conex1.State Then conex1.Close()
        consulta = "select * from tbl_usuarios where empresa='" & comB_Empresas.Text & "' and usuario='" & txt_Usuario.Text & "'"
        Try

        Catch ex As Exception

        End Try



    End Sub

    Private Sub bttn_Cancelar_Click(sender As Object, e As EventArgs) Handles bttn_Cancelar.Click
        'cmdCancel
        numEmp = 0
        Me.Close()
    End Sub

    Private Sub frmlogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim snumero As Integer
        consulta = "select * from tbl_empresas order by numero"
        Try

        Catch ex As Exception
            MsgBox(ex.Message)

        Finally

        End Try
    End Sub

    Private Sub comB_Empresas_Click(sender As Object, e As EventArgs) Handles comB_Empresas.Click



    End Sub
End Class