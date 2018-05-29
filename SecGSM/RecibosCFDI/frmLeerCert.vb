Public Class frmLeerCert
    Public x509 As X509
    Public rsa As Rsa
    Public Const PKI_EMSIG_DEFAULT As Integer = &H20
    Public Const PKI_HASH_MD5 As Integer = 1
    Private strPath As String
    Private dtbPath As DataTable
    Public BoolOK As Boolean = False
    Private Structure ValCert
        Dim DC As String
        Dim CN As String
        Dim OU As String
        Dim O As String
        Dim street As String
        Dim L As String
        Dim ST As String
        Dim C As String
        Dim UID As String
        Dim SN As String
        Dim d2_5_4_41 As String
        Dim d2_5_4_45 As String
        Dim d2_5_4_5 As String
    End Structure
    Private Sub cmdOpenPKI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cmdCertCli_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCertCli.Click
        FileDialog.Filter = "Certificado de Sellos Digitales (*.cer)|*.cer"
        FileDialog.FileName = ""
        If FileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                txtCertCli.Text = FileDialog.FileName
                Call LeerCert(txtCertCli.Text)
                BoolOK = True
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "AVISO")
                BoolOK = False
            End Try
        Else
            BoolOK = False
        End If
    End Sub

    Private Sub cmdOpenPKI_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        'cmdOpenPKI.OpenDialog.Filter = "Archivos PKI (*.pki)|*.pki"
    End Sub

    Private Sub LeerCert(ByVal strFile As String)
        Dim cert As Security.Cryptography.X509Certificates.X509Certificate
        Dim certinfo As ValCert
        cert = Security.Cryptography.X509Certificates.X509Certificate.CreateFromCertFile(strFile)
        lblSerie.Text = HexAsStringToCharactersAsString(x509.CertSerialNumber(strFile))
        txtParametros.Text = cert.Subject.ToString
        certinfo = GetSubjectValues(strFile)
        Dim strValores() As String
        strValores = cert.Subject.ToString.Split(",")
        For Each Str As String In strValores
            Str = Str.Trim
            With certinfo
                If Str.StartsWith("DC=") Then .DC = Str.Replace("DC=", "")
                If Str.StartsWith("CN=") Then .CN = Str.Replace("CN=", "")
                If Str.StartsWith("OU=") Then .OU = Str.Replace("OU=", "")
                If Str.StartsWith("O=") Then .O = Str.Replace("O=", "")
                If Str.StartsWith("STREET") Then .street = Str.Replace("STREET=", "")
                If Str.StartsWith("L=") Then .L = Str.Replace("L=", "")
                If Str.StartsWith("ST") Then .ST = Str.Replace("ST=", "")
                If Str.StartsWith("C=") Then .C = Str.Replace("C=", "")
                If Str.StartsWith("UID=") Then .UID = Str.Replace("UID=", "")
                If Str.StartsWith("SN=") Then .SN = Str.Replace("SN=", "")
                If Str.StartsWith("2.5.4.41") Then .d2_5_4_41 = Str.Replace("2.5.4.41=", "")
                If Str.StartsWith("2.5.4.45") Then .d2_5_4_45 = Str.Replace("2.5.4.45=", "")
                If Str.StartsWith("2.5.4.5") Then .d2_5_4_5 = Str.Replace("2.5.4.5=", "")
            End With
        Next
        If InStr(certinfo.d2_5_4_45, "/") > 0 Then
            txtRFC.Text = certinfo.d2_5_4_45.Substring(0, InStr(certinfo.d2_5_4_45, "/") - 1).Trim
        Else
            txtRFC.Text = certinfo.d2_5_4_45
        End If
        txtRazonSocial.Text = certinfo.CN
        txtVigencia.Text = DateDiff(DateInterval.Year, CDate(x509.CertIssuedOn(strFile)), CDate(x509.CertExpiresOn(strFile)))
    End Sub
    Private Function HexAsStringToCharactersAsString(ByVal HexString As String) As String
        'we`re assuming HexString passed is formatted as 2 chars for each individual Hex value
        'ie A = 0A, B=0B
        Dim UB As Integer = HexString.Length - 1
        Dim SB As New System.Text.StringBuilder
        For Idx As Integer = 0 To UB Step 2
            SB.Append(ChrW(System.Convert.ToInt32(HexString.Chars(Idx) & HexString.Chars(Idx + 1), 16)))
        Next
        Return SB.ToString
    End Function
    Private Function GetSubjectValues(ByVal str As String) As ValCert
        Dim strValCert As String
        Dim strValores() As String
        Dim Subject As ValCert
        strValCert = x509.CertSubjectName(str, "|")
        strValores = strValCert.Split("|")
        For Each str In strValores
            With Subject
                If str.StartsWith("DC=") Then .DC = str.Replace("DC=", "")
                If str.StartsWith("CN=") Then .CN = str.Replace("CN=", "")
                If str.StartsWith("OU=") Then .OU = str.Replace("OU=", "")
                If str.StartsWith("O=") Then .O = str.Replace("O=", "")
                If str.StartsWith("STREET") Then .street = str.Replace("STREET=", "")
                If str.StartsWith("L=") Then .L = str.Replace("L=", "")
                If str.StartsWith("ST") Then .ST = str.Replace("ST=", "")
                If str.StartsWith("C=") Then .C = str.Replace("C=", "")
                If str.StartsWith("UID=") Then .UID = str.Replace("UID=", "")
                If str.StartsWith("SN=") Then .SN = str.Replace("SN=", "")
                If str.StartsWith("2.5.4.41") Then .d2_5_4_41 = str.Replace("2.5.4.41=", "")
                If str.StartsWith("2.5.4.45") Then .d2_5_4_45 = str.Replace("2.5.4.45=", "")
                If str.StartsWith("2.5.4.5") Then .d2_5_4_5 = str.Replace("2.5.4.5=", "")
            End With
        Next
        Return Subject
    End Function

    Private Sub frmLeerCert_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub cmdAplicar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAplicar.Click
        Me.Close()
    End Sub
End Class
