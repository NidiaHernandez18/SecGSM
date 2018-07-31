Imports System.Data
Imports System.Data.SqlClient

Public Class Main
    Public condere As New SqlConnection
    Public condere2 As New SqlConnection
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
        Frmp = c

    End Sub
End Class
