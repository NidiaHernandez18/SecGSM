Imports System.Data
Imports System.Data.SqlClient

Public Class frmlogin
    Public conex1 As SqlConnection
    Public nombreaplic As String
    Public numEmp As Integer

    Private comando As SqlCommand
    Private objetoLectura As SqlDataReader

    Public usuario As String
    Public pass As String
    Public nombre As String
    Public Empresa As String
    Public numeroempre As Integer

    Private intentos As Integer
    Private consulta As String
    Public loginx As Boolean

    Private Sub bttn_OK_Click(sender As Object, e As EventArgs) Handles bttn_OK.Click
        'cmdok
        loginx = False
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
            comando = New SqlCommand(consulta, conex1)
            conex1.Open()
            objetoLectura = comando.ExecuteReader()
            If Not objetoLectura.Read Then
                MsgBox("El Usuario No se Encuentra, Intente de Nuevo ", vbExclamation)
                txt_Usuario.Focus()
                txt_Usuario.SelectionStart = 0
                txt_Usuario.SelectionLength = Len(txt_Usuario.Text)
                Exit Sub
            Else
                If objetoLectura("passw") = Trim(txt_password.Text) Then
                    loginx = True
                    usuario = IIf(IsDBNull(objetoLectura("Usuario")), "", objetoLectura("Usuario"))
                    pass = IIf(IsDBNull(objetoLectura("passw")), "", objetoLectura("passw"))
                    nombre = IIf(IsDBNull(objetoLectura("nombre")), "", objetoLectura("nombre"))
                    numEmp = Val(Mid(objetoLectura("Empresa"), 1, 5))
                    Empresa = comB_Empresas.Text

                    ' MsgBox(objetoLectura("Usuario") & " " & objetoLectura("passw") & " " & objetoLectura("nombre"))
                    Me.Close()

                Else
                    MsgBox("El Password no es Correcto, Intente de Nuevo", vbCritical)
                    txt_Usuario.Focus()
                    txt_Usuario.SelectionStart = 0
                    txt_Usuario.SelectionLength = Len(txt_Usuario.Text)
                    Exit Sub
                End If

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conex1.Close()
        End Try



    End Sub

    Private Sub bttn_Cancelar_Click(sender As Object, e As EventArgs) Handles bttn_Cancelar.Click
        'cmdCancel
        numEmp = 0
        Me.Close()
    End Sub

    Private Sub frmlogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim snumero As Integer
        label4.Text = nombreaplic
        consulta = "select * from tbl_empresas order by numero"
        Try
            comando = New SqlCommand(consulta, conex1)
            conex1.Open()
            objetoLectura = comando.ExecuteReader()
            Do While objetoLectura.Read
                comB_Empresas.Items.Add(Trim(Str(objetoLectura("numero")) & "-" & Trim(objetoLectura("empresa"))))
            Loop
            comB_Empresas.SelectedIndex = 0
            intentos = 0
            loginx = False
            label4.Text = nombreaplic
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conex1.Close()
        End Try
    End Sub

    Private Sub comB_Empresas_Click(sender As Object, e As EventArgs) Handles comB_Empresas.Click
        comB_Empresas.SelectionStart = 0
    End Sub
End Class