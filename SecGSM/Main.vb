Imports System.Data
Imports System.Data.SqlClient

Public Class Main
    Public condere As SqlConnection
    Public condere2 As SqlConnection
    Public Validaruser As Boolean
    Public nomaplic As String
    Public nombreuser As String
    Public passuser As String
    Public iduser As String
    Public Empresa As String

    Public Sub abribasedato(conex As SqlConnection)
        condere = conex
    End Sub
    Public Sub abribasedatosnom(conex As SqlConnection)
        condere2 = conex
    End Sub

    Public Sub UsuariosDerechos()
        Dim Frmp As New frmPermisosR
        Frmp.conex1 = condere
        Frmp.conex2 = condere2
        Frmp.ShowDialog()
        Frmp.Close()
        Frmp = Nothing
    End Sub
End Class
