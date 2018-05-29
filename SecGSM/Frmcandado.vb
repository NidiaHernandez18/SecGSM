Option Explicit On
Imports System.Windows.Forms


Public Class Frmcandado
    Private conectarx As New OleDb.OleDbConnection
    Private conectary As New SqlClient.SqlConnection
    Private Empresax As String
    Private conx As New Pk1
    Private HDD1 As Long
    Private fila1 As String, fila2 As String
    Private fila3 As String
    Private aplicacion As String
    Private llave As String
    Private rfc1 As String
    Private patronal1 As String
    Private continuarx As Boolean

    Public Property conectar() As OleDb.OleDbConnection
        Get
            conectar = conectarx
        End Get
        Set(ByVal value As OleDb.OleDbConnection)
            conectarx = value
        End Set
    End Property
    Public Property conectar(ByVal IsMsSQL As Boolean) As SqlClient.SqlConnection
        Get
            conectar = conectary
        End Get
        Set(ByVal value As SqlClient.SqlConnection)
            conectary = value
        End Set
    End Property
    Public Property Empresa() As String
        Get
            Empresa = Empresax
        End Get
        Set(ByVal value As String)
            Empresax = value
        End Set
    End Property
    Public Property RFC() As String
        Get
            RFC = rfc1
        End Get
        Set(ByVal value As String)
            rfc1 = value
        End Set
    End Property
    Public Property patronal() As String
        Get
            patronal = patronal1
        End Get
        Set(ByVal value As String)
            patronal1 = value
        End Set
    End Property
    Public Property Aplicac() As String
        Get
            Aplicac = aplicacion
        End Get
        Set(ByVal value As String)
            aplicacion = value
        End Set
    End Property
    Public Property continuar() As Boolean
        Get
            continuar = continuarx
        End Get
        Set(ByVal value As Boolean)
            continuarx = value
        End Set
    End Property
    Private Sub Frmcandado_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Visible = False
        continuarx = False
        If conectary.State = ConnectionState.Closed Then
            If Verificar() Then
                continuarx = True
                Me.Close()
            End If
        Else
            If Verificar(True) Then
                continuarx = True
                Me.Close()
            End If
        End If
    End Sub
    Private Sub BtnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSalir.Click
        Me.Close()
    End Sub
    Public Function Verificar() As Boolean
        Dim Dt As New DataTable
        Verificar = False
        fila1 = rfc1
        fila2 = patronal1
        fila3 = Empresax

        CanLla.empresas = fila3
        CanLla.PATRONAL = fila2
        CanLla.RFC = fila1

        Dt = New DataTable
        HDD1 = SerieDisk("C:\")
        llave = Mid(HDD1 & Space(11), 1, 11) & Mid(fila3 & Space(30), 1, 30) & Mid(fila1 & Space(12), 1, 12) & Mid(aplicacion.PadRight(3, " "), 1, 3)
        llave = conx.Encriptarpaso1(llave)
        Dt = Ejecutarsql.RecDatatable("select * from tbl_regllave where id1='" _
                           & HDD1 & "' AND patronal='" & Trim(UCase(fila2)) & "' AND LLAVE1='" & llave & "'", conectarx)

        If Not Dt Is Nothing Then
            If Dt.Rows.Count > 0 Then
                Verificar = True
            End If
        End If
        txtCandado1.Text = llave
        Dt = Nothing
    End Function
    Public Function Verificar(ByVal IsMsSQL As Boolean) As Boolean
        Dim Dt As New DataTable
        Verificar = False
        fila1 = rfc1
        fila2 = patronal1
        fila3 = Empresax

        CanLla.empresas = fila3
        CanLla.PATRONAL = fila2
        CanLla.RFC = fila1

        Dt = New DataTable
        HDD1 = SerieDisk("C:\")
        llave = Mid(HDD1 & Space(11), 1, 11) & Mid(fila3 & Space(30), 1, 30) & Mid(fila1 & Space(12), 1, 12) & Mid(aplicacion.PadRight(3, " "), 1, 3)
        llave = conx.Encriptarpaso1(llave)
        Dt = Ejecutarsql.RecDatatable("select * from tbl_regllave where id1='" _
                           & HDD1 & "' AND patronal='" & Trim(UCase(fila2)) & "' AND LLAVE1='" & llave & "'", conectary)

        If Not Dt Is Nothing Then
            If Dt.Rows.Count > 0 Then
                Verificar = True
            End If
        End If
        txtCandado1.Text = llave
        Dt = Nothing
    End Function

    Public Function SerieDisk(ByVal strDrive As String) As Long
        Dim Res As Long
        Dim DrvVolumeName$
        Dim VolumeSN As Long
        Dim UnusedStr As String
        Dim UnusedVal1 As Long
        Dim UnusedVal2 As Long

        DrvVolumeName$ = Space$(14)
        UnusedStr$ = Space$(32)
        Res = GetVolumeInformation(strDrive, _
        DrvVolumeName$, Len(DrvVolumeName$), VolumeSN&, _
        UnusedVal1&, UnusedVal2&, UnusedStr$, Len(UnusedStr$))
        SerieDisk = Math.Abs(VolumeSN&)
    End Function

    Private Sub Btnllave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnllave.Click
        Dim enc1 As String
        enc1 = conx.Encriptarpaso2(txtCandado1.Text)
        If Trim(txtllave1.Text) = enc1 Then
            Ejecutarsql.ExecuteSql("insert into tbl_regllave(patronal,pass1,LLAVE1,id1)values('" _
                                  & CanLla.PATRONAL & "','" & enc1 & "','" & txtCandado1.Text & "','" _
                                  & HDD1 & "')", IIf(conectarx.State = ConnectionState.Closed, conectary, conectarx))
            CanLla.candado = txtCandado1.Text
            CanLla.HDD1 = HDD1
            CanLla.llave = txtllave1.Text
            Call MessageBox.Show("Llave es correcta la empresa se a activado", "Licencia", MessageBoxButtons.OK, MessageBoxIcon.Information)
            continuarx = True
            Me.Close()
        Else
            continuarx = False
            Call MessageBox.Show("Llave es incorrecta por favor de verificar la llave", "Licencia", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

 
    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub
End Class