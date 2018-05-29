Imports System.Windows.Forms
Public Class frmusuarios
    Dim usuariotabla As DataTable
    Dim xpass1 As String
    Dim xfecha As Date
    Dim xintentos As Integer
    Dim recux As New ClassCone
    Dim connprinc As OleDb.OleDbConnection
    Dim reccon1 As New Cblowfish
    Dim contprog As Boolean
    Dim userc As String
    Dim datac As String
    Dim serverc As String
    Dim passc As String
    Dim tipo As Integer
    Dim userapl As String

    Enum Basededatos
        Sqlserver = 1
        Access = 2
        Mysql = 3

    End Enum
    Public Property conectar() As OleDb.OleDbConnection
        Get
            conectar = connprinc
        End Get
        Set(ByVal value As OleDb.OleDbConnection)
            connprinc = value
        End Set
    End Property
    Public Property continuar() As Boolean
        Get
            continuar = contprog
        End Get
        Set(ByVal value As Boolean)
            contprog = value
        End Set
    End Property
    Public Property USER() As String
        Get
            USER = userc
        End Get
        Set(ByVal value As String)
            userc = value
        End Set
    End Property
    Public Property Useraplicacion() As String
        Get
            Useraplicacion = userapl
        End Get
        Set(ByVal value As String)
            userapl = value

        End Set
    End Property
    Public Property DATABASE() As String
        Get
            DATABASE = datac
        End Get
        Set(ByVal value As String)
            datac = value
        End Set
    End Property
    Public Property SERVER() As String
        Get
            SERVER = serverc
        End Get
        Set(ByVal value As String)
            serverc = value
        End Set
    End Property
    Public Property Password() As String
        Get
            Password = passc
        End Get
        Set(ByVal value As String)
            passc = value
        End Set
    End Property
    Public Property cliente() As Basededatos
        Get
            cliente = tipo
        End Get
        Set(ByVal value As Basededatos)
            tipo = value
        End Set
    End Property
    Private Sub btnsalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsalir.Click
        contprog = False
        Me.Close()
    End Sub
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

        Call btnAceptarClick()
    End Sub
    Private Sub frmusuarios_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = ChrW(13) Then
            SendKeys.Send("{TAB}")
        End If
        If e.KeyChar = ChrW(27) Then
            Me.Close()
        End If
    End Sub
    Protected Overridable Sub frmusuarios_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsNothing(serverc) Then
            connprinc = New OleDb.OleDbConnection
            connprinc = recux.conectarsql(serverc, userc, passc, datac, tipo)
            contprog = True
            If connprinc Is Nothing Then
                contprog = False
                MessageBox.Show("No tiene derechos favor de comunicarse con el de sistemas", "Error")
            End If
        End If
    End Sub
    Private Sub txtuser_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtuser.GotFocus
        txtuser.SelectAll()
    End Sub
    Private Sub txtpass_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpass.GotFocus
        txtpass.SelectAll()
    End Sub
    Public Overridable Sub btnAceptarClick()
        usuariotabla = New DataTable
        Try
            usuariotabla = New DataTable
            usuariotabla = recux.RecDatatable("select usuario,passw,cambiarp,deshabili,bloqueada,expiraf,expira,intenuser,intentos," _
                           & "fechaentrada,email,nombre,tel,exten from tbl_usuarios where usuario='" & txtuser.Text & "'", connprinc)
            If usuariotabla.Rows.Count > 0 Then
                If usuariotabla.Rows(0).Item("deshabili") = 1 Then
                    Call MessageBox.Show("Cuenta deshabilitada, favor de comunicarse con el administrador", "Favor de comunicarse con el administrador", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Exit Sub
                End If
                If usuariotabla.Rows(0).Item("Bloqueada") = 1 Then
                    Call MessageBox.Show("Cuenta bloqueada, favor de comunicarse con el administrador", "Favor de comunicarse con el administrador", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Exit Sub
                End If

                xpass1 = reccon1.Decrypt(usuariotabla.Rows(0).Item("passw"))
                If xpass1 = txtpass.Text Then
                    If usuariotabla.Rows(0).Item("intenuser") > usuariotabla.Rows(0).Item("intentos") Then
                        Call MessageBox.Show("Cuenta bloqueada", "Favor de comunicarse con el administrador", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        Exit Sub
                    End If
                    xfecha = usuariotabla.Rows(0).Item("expiraf")
                    contprog = True
                    If xfecha <= Now.Date Then
                        Call MessageBox.Show("Cuenta expiro, cambio de clave ", "Cambio de clave", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        contprog = False
                        Dim frmcambio As New frmcambiarpassw
                        frmcambio.passant = reccon1.Decrypt(usuariotabla.Rows(0).Item("passw"))
                        frmcambio.conectar = connprinc
                        frmcambio.Label6.Text = usuariotabla.Rows(0).Item("usuario")
                        frmcambio.xdias = usuariotabla.Rows(0).Item("expira")
                        frmcambio.ShowDialog()
                    End If
                    If usuariotabla.Rows(0).Item("cambiarp") = 1 Then
                        Call MessageBox.Show("Cambiar la clave por favor ", "Cambio de clave", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        contprog = False
                        Dim frmcambio As New frmcambiarpassw
                        frmcambio.passant = reccon1.Decrypt(usuariotabla.Rows(0).Item("passw"))
                        frmcambio.Label6.Text = usuariotabla.Rows(0).Item("usuario")
                        frmcambio.xdias = usuariotabla.Rows(0).Item("expira")
                        frmcambio.ShowDialog()
                    End If

                    If contprog Then
                        recux.ExecuteSql("update tbl_usuarios set intenuser=0 where usuario='" & txtuser.Text & "'", connprinc)
                        userapl = txtuser.Text
                        Me.Close()

                    End If
                Else
                    xintentos = usuariotabla.Rows(0).Item("intenuser") + 1

                    If xintentos = usuariotabla.Rows(0).Item("intentos") Then
                        recux.ExecuteSql("update tbl_usuarios set intenuser=0,bloqueada=1 where usuario='" & txtuser.Text & "'", connprinc)
                        MessageBox.Show("La cuenta del usuario se bloqueo, favor de comunicarse con su administrador", "Cuenta", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Else
                        recux.ExecuteSql("update tbl_usuarios set intenuser=" & xintentos & " where usuario='" & txtuser.Text & "'", connprinc)
                        MessageBox.Show("La clave es incorrecta", "Clave", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        txtpass.Focus()
                    End If
                End If
            Else
                Call MessageBox.Show("El usuario no existe favor de verificarlo", "Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error)

                Exit Sub
            End If
        Catch ex As Exception

        Finally
            usuariotabla.Clear()
            usuariotabla = Nothing
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class