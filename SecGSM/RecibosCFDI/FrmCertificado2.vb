Option Explicit On
Imports System.Windows.Forms
Imports CryptoSysPKI

Public Class frmCertificado2
    Private conectarx As New OleDb.OleDbConnection
    Private conectary As New SqlClient.SqlConnection
    Private Empresax As String
    Private Security As New Pk1
    Private HDD1 As Long
    Private fila1 As String, fila2 As String
    Private fila3 As String
    Private aplicacion As String
    Private llave As String
    Private rfc1 As String
    Private patronal1 As String
    Private continuarx As Boolean
    Private RSA As rsa

    Public Property conectar(ByVal IsSQL As Boolean) As SqlClient.SqlConnection
        Get
            Return conectary
        End Get
        Set(ByVal value As SqlClient.SqlConnection)
            conectary = value
        End Set
    End Property
    Public Property conectar() As OleDb.OleDbConnection
        Get
            Return conectarx
        End Get
        Set(ByVal value As OleDb.OleDbConnection)
            conectarx = value
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
        txtllave1.Text = ""
        'lblCandado.Text = ""
    End Sub
    Private Sub BtnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSalir.Click
        Me.Close()
    End Sub
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
        'VALIDACION
        If Security.Verify_Sign(txtllave1.Text, txtCert.Text, lblCandado.Text) Then
            Ejecutarsql.ExecuteSql("insert into tbl_certLlave(candado,llave)values('" & txtllave1.Text & "','" & lblCandado.Text & "')", IIf(conectarx.State = ConnectionState.Closed, conectary, conectarx))
            Call MessageBox.Show("La Llave es correcta, la licencia en este equipo se ha activado", "Licencia", MessageBoxButtons.OK, MessageBoxIcon.Information)
            continuarx = True
            Me.Close()
        Else
            MsgBox("Error al leer llave de activación" & vbCrLf & "Verifique que la cadena proporcionada y el certificado se han los correctos", MsgBoxStyle.Exclamation, "AVISO")
            continuarx = False
        End If
    End Sub

    Private Sub cmdOpenCert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpenCert.Click
        OpenDialog.DefaultExt = ".cer"
        OpenDialog.Filter = "Certificado de Seguridad(*.cer)|*.cer"
        If OpenDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtcert.text = OpenDialog.FileName
        End If
    End Sub

End Class