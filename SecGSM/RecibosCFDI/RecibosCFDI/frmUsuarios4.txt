Imports System.Windows.Forms
Public Class frmusuarios4

    Public dtrEmpresaSelected As DataRow
    Dim userapl As String
    Dim contprog As Boolean
    Dim connPrinc As SqlClient.SqlConnection
    Dim connPrinc2 As OleDb.OleDbConnection
    Dim usuariotabla As New DataTable
    Public ErrorEnPathdeEmpresa As Boolean
    Public DatosEquipo As Infoequipo
    Public DatosCert As InfoCert
    Public CambioCertificado As Boolean

    Dim dtbEmpresa As DataTable

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
    Public Shadows Property conectar(ByVal IsSQL As Boolean) As SqlClient.SqlConnection
        Get
            conectar = connPrinc
        End Get
        Set(ByVal value As SqlClient.SqlConnection)
            connPrinc = value
        End Set
    End Property
    Public Shadows Property Conectar() As OleDb.OleDbConnection
        Get
            Return connPrinc2
        End Get
        Set(ByVal value As OleDb.OleDbConnection)
            connPrinc2 = value
        End Set
    End Property
    Public Shadows Property continuar() As Boolean
        Get
            continuar = contprog
        End Get
        Set(ByVal value As Boolean)
            contprog = value
        End Set
    End Property
    Public Shadows Property Useraplicacion() As String
        Get
            Useraplicacion = userapl
        End Get
        Set(ByVal value As String)
            userapl = value

        End Set
    End Property
    
    Private Sub cboEmpresas_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboEmpresas.SelectedIndexChanged
        Dim Empresa As String
        Dim Numero As Integer
        cboSucursales.Items.Clear()
        Numero = CDec(Strings.Left(cboEmpresas.Text, InStr(cboEmpresas.Text, "-") - 1))
        Empresa = Strings.Mid(cboEmpresas.Text, InStr(cboEmpresas.Text, "-") + 1).Trim
        Empresa = ApostropheStr(Empresa)
        For Each dtr As DataRow In dtbEmpresa.Select("Numero=" & Numero & " AND Empresa='" & Empresa & "'")
            cboSucursales.Items.Add(dtr!Sucursal)
            lblRFC.Text = dtr!RFC.ToString.Replace("&", "&&")
        Next
        If cboSucursales.Items.Count > 0 Then
            cboSucursales.SelectedIndex = 0
            txtuser.Focus()
        End If
    End Sub

    Private Function ApostropheStr(ByVal Str As String) As String
        Dim pos As Integer
        Dim CorrectStr As String
        'Se inicia la busqueda de apostrofe en la razon social.
        pos = InStr(Str, "'", CompareMethod.Text)
        If pos > 0 Then
            'Se reemplaza por doble apostrofe.
            CorrectStr = Str.Replace("'", "''")
        Else
            CorrectStr = Str
        End If
        Return CorrectStr
    End Function

    Private Function Validacion() As Boolean
        Dim result As Boolean
        If cboEmpresas.Text.Trim.Length = 0 Then
            MsgBox("Introduzca la Empresa a la cual desea conectarse", MsgBoxStyle.Exclamation, "AVISO")
            cboEmpresas.Focus()
        ElseIf cboSucursales.Text.Trim.Length = 0 Then
            MsgBox("Seleccione la Empresa o Sucursal a la cual desea conectarse", MsgBoxStyle.Exclamation, "AVISO")
            cboSucursales.Focus()
        ElseIf txtuser.Text.Trim.Length = 0 Then
            MsgBox("Introduzca el usuario de acceso", MsgBoxStyle.Exclamation, "AVISO")
            txtuser.Focus()
        ElseIf txtpass.Text.Trim.Length = 0 Then
            MsgBox("Introduzca la contraseña de acceso", MsgBoxStyle.Exclamation, "AVISO")
            txtpass.Focus()
        Else
            Return True
        End If
        Return result
    End Function
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim xpass1 As String
        Dim xfecha As Date
        Dim xintentos As Integer
        Dim recux As New ClassCone
        Dim reccon1 As New Cblowfish
        Dim Empresa As String
        Dim conn As Object
        Dim ErrorCon As String
        If Validacion() Then
            Dim strRuta As String
            Dim strRFC As String
            strRFC = lblRFC.Text.Replace("&&", "&")
            Empresa = Strings.Left(cboEmpresas.Text, InStr(cboEmpresas.Text, "-") - 1)
            strRuta = My.Application.Info.DirectoryPath & "\Data\" & strRFC & "\BD\master.mdb"
            Try

                conn = recux.conectarsql(strRuta, "", "gsdmexico", "", ClassCone.Basededatos.Access)
                conectar = conn
            Catch ex As Exception
                ErrorCon = ex.Message
                conn = Nothing
            End Try
            If Not IsNothing(conn) Then
                Try
                    'EMPRESA
                    Dim dtb As DataTable
                    Dim strSQL As String
                    strSQL = "SELECT * FROM tbl_Empresas WHERE Numero=" & Empresa & " AND Sucursal=" & cboSucursales.Text
                    dtb = recux.RecDatatable(strSQL, conn)
                    If dtb.Rows.Count > 0 Then
                        dtrEmpresaSelected = dtb.Rows(0)
                        If Not IO.File.Exists(My.Application.Info.DirectoryPath & "\Data\" & dtrEmpresaSelected!RFC.ToString & "\CLU\CLU_" & dtrEmpresaSelected!RFC.ToString & "_" & dtrEmpresaSelected!Numero & "_" & dtrEmpresaSelected!Sucursal & ".cer") Then
                            MsgBox("No se encuentra el certificado de licencia de uso del sistema", MsgBoxStyle.Exclamation, "AVISO")
                            Me.Height = 400
                            Exit Sub
                        Else
                            DatosEquipo.certificado = My.Application.Info.DirectoryPath & "\Data\" & dtrEmpresaSelected!RFC.ToString & "\CLU\CLU_" & dtrEmpresaSelected!RFC.ToString & "_" & dtrEmpresaSelected!Numero & "_" & dtrEmpresaSelected!Sucursal & ".cer"
                            DatosEquipo.certificado2 = My.Application.Info.DirectoryPath & "\Data\" & dtrEmpresaSelected!RFC.ToString & "\CSD\" & dtrEmpresaSelected!NoCertificado.ToString & ".cer"
                            DatosEquipo.Sistema = "C4"
                        End If


                        Dim cert As New SecGSM.ClassCandado
                        'Dim DatosCert As InfoCert = Nothing
                        If cert.ValidaCert(DatosCert, DatosEquipo.certificado, DatosEquipo.certificado2, dtrEmpresaSelected!Empresa.ToString, dtrEmpresaSelected!RFC.ToString, DatosEquipo.Sistema) Then
                            If GetLlaveFromWS(dtrEmpresaSelected, DatosEquipo, txtEquipo.Text, txtIP.Text, conn) Then
                                'USUARIO
                                usuariotabla = New DataTable
                                usuariotabla = recux.RecDatatable("select usuario,passw,cambiarp,deshabili,bloqueada,expiraf,expira,intenuser,intentos," _
                                               & "fechaentrada,email,nombre,tel,exten from tbl_usuarios where usuario='" & txtuser.Text & "'", conn)
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
                                            frmcambio.conectar() = conn

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
                                            frmcambio.conectar() = conn
                                            frmcambio.ShowDialog()
                                        End If

                                        If contprog Then
                                            recux.ExecuteSql("update tbl_usuarios set intenuser=0 where usuario='" & txtuser.Text & "'", conn)
                                            userapl = txtuser.Text
                                            'Me.Close()
                                            Me.Hide()

                                        End If
                                    Else
                                        xintentos = usuariotabla.Rows(0).Item("intenuser") + 1

                                        If xintentos = usuariotabla.Rows(0).Item("intentos") Then
                                            recux.ExecuteSql("update tbl_usuarios set intenuser=0,bloqueada=1 where usuario='" & txtuser.Text & "'", conn)
                                            MessageBox.Show("La cuenta del usuario se bloqueo, favor de comunicarse con su administrador", "Cuenta", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                        Else
                                            recux.ExecuteSql("update tbl_usuarios set intenuser=" & xintentos & " where usuario='" & txtuser.Text & "'", conn)
                                            MessageBox.Show("La clave es incorrecta", "Clave", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                            txtpass.Focus()
                                        End If
                                    End If
                                Else
                                    Call MessageBox.Show("El usuario no existe favor de verificarlo", "Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                    Exit Sub
                                End If
                            End If
                        Else
                            'Call MessageBox.Show("No se puede conectar a la empresa seleccionada", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            ErrorEnPathdeEmpresa = False
                            Me.Hide()
                        End If
                    Else
                        Me.Hide()
                    End If
                Catch ex As Exception
                    MsgBox("No se puede conectar a la empresa seleccionada" & vbCrLf & "Error:" & ex.Message, MsgBoxStyle.Exclamation, "AVISO")
                    ErrorEnPathdeEmpresa = True
                    Me.Hide()
                Finally
                    If Not IsNothing(usuariotabla) Then
                        usuariotabla.Clear()
                        usuariotabla = Nothing
                    End If
                End Try
            Else
                Call MessageBox.Show("No se puede conectar a la empresa seleccionada" & vbCrLf & ErrorCon, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ErrorEnPathdeEmpresa = True
                Me.Hide()
            End If
        End If
    End Sub
    Private Sub frmusuarios_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtbEmpresa = New DataTable
        With dtbEmpresa.Columns
            .Add("Numero", Type.GetType("System.String"))
            .Add("Empresa", Type.GetType("System.String"))
            .Add("Sucursal", Type.GetType("System.String"))
            .Add("RFC", Type.GetType("System.String"))
            .Add("RutaCSD", Type.GetType("System.String"))
            .Add("RutaCLU", Type.GetType("System.String"))
        End With
        Dim host As String
        Dim LocalHostAddress As String = ""
        host = System.Net.Dns.GetHostName()
        If Not IsNothing(System.Net.Dns.GetHostEntry(host)) Then
            For Each StrIp As System.Net.IPAddress In System.Net.Dns.GetHostEntry(host).AddressList
                If StrIp.AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then
                    LocalHostAddress = StrIp.ToString
                End If
            Next
        End If
        txtEquipo.Text = host
        txtIP.Text = LocalHostAddress
        Dim strRuta As String
        Dim DirApp As String = My.Application.Info.DirectoryPath
        strRuta = DirApp & "\Data"
        If Not IO.Directory.Exists(strRuta) Then IO.Directory.CreateDirectory(strRuta)
        strRuta = DirApp & "\Data\clu"
        If Not IO.Directory.Exists(strRuta) Then IO.Directory.CreateDirectory(strRuta)
    End Sub
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub btnsalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsalir.Click
        Me.Close()
    End Sub
    Private Sub txtpass_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtpass.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call btnAceptar_Click(btnAceptar, e)
        End If
    End Sub


    Private Sub ToolStripStatusLabel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub frmusuarios3_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Call LlenaForm()
    End Sub
    Private Sub LlenaForm()
        Dim DirApp As String
        cboEmpresas.Items.Clear()
        DirApp = My.Application.Info.DirectoryPath
        For Each Str As String In IO.Directory.GetFiles(DirApp & "\Data\clu", "*.cer")
            Dim CLU As InfoCert
            Dim ADatos() As String = IO.Path.GetFileNameWithoutExtension(Str).Split("_")

            CLU = LeerCLU(Str)
            If Not IsNothing(CLU) Then
                If UCase(CLU.IDSistema) = "ePayroll" Or UCase(CLU.IDSistema) = "C4" Then
                    If Not cboEmpresas.Items.Contains(ADatos(2) & " - " & CLU.Nombre.ToString) Then
                        cboEmpresas.Items.Add(ADatos(2) & " - " & CLU.Nombre.ToString)
                    End If
                    Dim dtr As DataRow
                    dtr = dtbEmpresa.NewRow
                    With dtr
                        !Numero = ADatos(2)
                        !Empresa = CLU.Nombre.ToString
                        !Sucursal = ADatos(3).ToString
                        !RutaCSD = ""
                        !RFC = CLU.RFC.ToString
                        !RutaCLU = Str
                    End With
                    dtbEmpresa.Rows.Add(dtr)
                End If
            End If
        Next
        If cboEmpresas.Items.Count > 0 Then
            cboEmpresas.SelectedIndex = 0
        End If
    End Sub
    Private Sub cmdVerificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Public Function LeerCLU(ByVal strCLU As String) As InfoCert
        ' CLU  -  Certificado de Licencia de USO
        ' Certificado Valido
        Dim strRFC As String
        If X509.CertIsValidNow(strCLU) Then
            DatosCert.NoSerie = HexAsStringToCharactersAsString(X509.CertSerialNumber(strCLU))
        Else
            MsgBox("El certificado no es válido" & vbCrLf & "Solicite un nuevo certificado de licencia de uso del sistema", MsgBoxStyle.Exclamation, "AVISO")
            Return Nothing
        End If

        ' CERTIFICADO VALIDACION
        Dim qryCert As String
        qryCert = CryptoSysPKI.X509.CertSubjectName(strCLU, "|")

        Dim Datos() As String
        'Dim DatosCert As InfoCert = Nothing
        Datos = qryCert.Split("|")
        For Each Str As String In Datos
            DatosCert.Subject = Cert.GetSubjectValues(strCLU)
            ' RFC
            DatosCert.RFC = DatosCert.Subject.O
            If strCLU.Contains("&") Then
                DatosCert.RFC = HexAsStringToCharactersAsString(DatosCert.RFC.Trim.Replace(" ", ""))
            End If
            ' Nombre o Razon Social
            DatosCert.Nombre = DatosCert.Subject.CN
            ' NoSerie FIEL
            strRFC = DatosCert.RFC

            If Str.StartsWith("OU=") Then
                Dim DatosVer As String() = Str.Split(",")
                ' 1. Verifica IDSystem
                DatosCert.IDSistema = DatosVer(0).Replace("OU=", "").Replace(";", "")
                ' 2. Verifica NoVersion
                DatosCert.NoVersion = DatosVer(1).Replace("OU=", "").Replace(";", "")
                ' 3. Verifica TipoVersion
                DatosCert.TipoVersion = DatosVer(2).Replace("OU=", "").Replace(";", "")

                Dim b() As Byte
                Dim encoding As New System.Text.UTF8Encoding()
                b = encoding.GetBytes(strRFC)

                Dim vALORfINAL As Integer = 0
                Dim i As Byte
                For Each i In b
                    Dim ValTotal As Integer
                    ValTotal = i
                    vALORfINAL += ValTotal
                Next
                vALORfINAL += DatosCert.TipoVersion

                DatosCert.TipoVersion = ""
                For j As Single = 0 To vALORfINAL.ToString.Length - 1 Step 2
                    Dim hexVal As String
                    Dim ValTotal As Integer
                    hexVal = vALORfINAL.ToString.Substring(j, 2)
                    hexVal = Convert.ToChar(CInt(hexVal))
                    DatosCert.TipoVersion &= hexVal
                Next
            ElseIf Str.StartsWith("O=") Then
                DatosCert.RFC = Str.Replace("O=", "").Replace(";", "")
            End If
        Next
        ' Verificar el IDSYSTEM
        If Not IsNothing(DatosCert) Then
            Dim strTipoVersion As String = Convert.ToString(CInt(DatosCert.TipoVersion), 2)
            Dim Version() As Char
            Dim certif As Datos_Cert
            strTipoVersion = strTipoVersion.PadLeft(8, "0")
            Version = strTipoVersion.ToCharArray
            With certif
                ._8INV = IIf(Version(0) = "0", False, True)
                ._7CTB = IIf(Version(1) = "0", False, True)
                ._6CxCr = IIf(Version(2) = "0", False, True)
                ._5CXCm = IIf(Version(3) = "0", False, True)
                ._4CXCi = IIf(Version(4) = "0", False, True)
                ._3CXC = IIf(Version(5) = "0", False, True)
                ._2CHQ = IIf(Version(6) = "0", False, True)
                ._1CXP = IIf(Version(7) = "0", False, True)
            End With
            Return DatosCert
        Else
            Return Nothing
        End If
    End Function
    Public Function ValidaCert(ByRef datoscert As InfoCert, ByVal strCLU As String, ByVal strFIEL As String, ByVal strNombre As String, ByVal strRFC As String, ByVal strIDSystem As String) As Boolean
        ' CLU  -  Certificado de Licencia de USO
        ' FIEL -  Firma Electronica o Certificado de Sellos Digitales

        ' Certificado Valido
        If X509.CertIsValidNow(strCLU) Then
            datoscert.NoSerie = HexAsStringToCharactersAsString(X509.CertSerialNumber(strCLU))
        Else
            MsgBox("El certificado no es válido" & vbCrLf & "Solicite un nuevo certificado de licencia de uso del sistema", MsgBoxStyle.Exclamation, "AVISO")
            Exit Function
        End If

        ' FIEL o CSD o Certificado 2 Validacion
        Dim Fiel As InfoCert
        If My.Computer.FileSystem.FileExists(strFIEL) Then
            Fiel.Subject = Cert.GetSubjectValues(strFIEL)
            If InStr(Fiel.Subject.d2_5_4_45, "/") > 0 Then
                Fiel.RFC = Fiel.Subject.d2_5_4_45.Substring(0, InStr(Fiel.Subject.d2_5_4_45, "/") - 1).Trim
            Else
                Fiel.RFC = Fiel.Subject.d2_5_4_45
            End If
            Fiel.Nombre = Fiel.Subject.O
            Fiel.NoSerie = HexAsStringToCharactersAsString(X509.CertSerialNumber(strFIEL))
        Else
            Fiel.RFC = ""
            Fiel.NoSerie = 0
        End If
        ' CERTIFICADO VALIDACION
        Dim qryCert As String
        qryCert = CryptoSysPKI.X509.CertSubjectName(strCLU, "|")

        Dim Datos() As String
        'Dim DatosCert As InfoCert = Nothing
        Datos = qryCert.Split("|")
        For Each Str As String In Datos
            datoscert.Subject = Cert.GetSubjectValues(strCLU)
            ' RFC
            datoscert.RFC = datoscert.Subject.O
            ' Nombre o Razon Social
            datoscert.Nombre = datoscert.Subject.CN
            ' NoSerie FIEL
            datoscert.NoSerieFIEL = Fiel.NoSerie

            If Str.StartsWith("OU=") Then
                Dim DatosVer As String() = Str.Split(",")
                ' 1. Verifica IDSystem
                datoscert.IDSistema = DatosVer(0).Replace("OU=", "").Replace(";", "")
                ' 2. Verifica NoVersion
                datoscert.NoVersion = DatosVer(1).Replace("OU=", "").Replace(";", "")
                ' 3. Verifica TipoVersion
                datoscert.TipoVersion = DatosVer(2).Replace("OU=", "").Replace(";", "")

                Dim b() As Byte
                Dim encoding As New System.Text.UTF8Encoding()
                b = encoding.GetBytes(strRFC)

                Dim vALORfINAL As Integer = 0
                Dim i As Byte
                For Each i In b
                    Dim ValTotal As Integer
                    ValTotal = i
                    vALORfINAL += ValTotal
                Next
                vALORfINAL += datoscert.TipoVersion

                datoscert.TipoVersion = ""
                For j As Single = 0 To vALORfINAL.ToString.Length - 1 Step 2
                    Dim hexVal As String
                    Dim ValTotal As Integer
                    hexVal = vALORfINAL.ToString.Substring(j, 2)
                    hexVal = Convert.ToChar(CInt(hexVal))
                    datoscert.TipoVersion &= hexVal
                Next
            ElseIf Str.StartsWith("O=") Then
                datoscert.RFC = Str.Replace("O=", "").Replace(";", "")
            End If
        Next
        ' Verificar el IDSYSTEM
        If Not IsNothing(datoscert) Then
            Dim strTipoVersion As String = Convert.ToString(CInt(datoscert.TipoVersion), 2)
            Dim Version() As Char
            Dim certif As Datos_Cert
            strTipoVersion = strTipoVersion.PadLeft(8, "0")
            Version = strTipoVersion.ToCharArray
            With certif
                ._8INV = IIf(Version(0) = "0", False, True)
                ._7CTB = IIf(Version(1) = "0", False, True)
                ._6CxCr = IIf(Version(2) = "0", False, True)
                ._5CXCm = IIf(Version(3) = "0", False, True)
                ._4CXCi = IIf(Version(4) = "0", False, True)
                ._3CXC = IIf(Version(5) = "0", False, True)
                ._2CHQ = IIf(Version(6) = "0", False, True)
                ._1CXP = IIf(Version(7) = "0", False, True)
            End With

            If strIDSystem <> datoscert.IDSistema Then
                MsgBox("El certificado no es correcto." & vbCrLf & "Indique el certificado de licencia de uso del sistema", MsgBoxStyle.Exclamation, "AVISO")
                Exit Function
            Else
                If certif._3CXC Or certif._4CXCi Or certif._5CXCm Or certif._6CxCr Then
                    If My.Computer.FileSystem.FileExists(strFIEL) Then
                        If Not (strRFC = datoscert.RFC And datoscert.RFC = Fiel.RFC) Then
                            MsgBox("El RFC del certificado de licencia de uso, no coincide con el de la empresa registrada." & vbCrLf & "Indique el certificado de licencia de uso del sistema para: " & strRFC.ToString, MsgBoxStyle.Exclamation, "AVISO")
                            Exit Function
                            'ElseIf Not (strNombre = DatosCert.Nombre And DatosCert.Nombre = Fiel.Nombre) Then
                            '    MsgBox("El Nombre o Razon Social del certificado de licencia de uso no coincide con la empresa registrada." & vbCrLf & "Indique el certificado de licencia de uso correcto para: " & strNombre.ToString, MsgBoxStyle.Exclamation, "AVISO")
                            '    Exit Function
                        Else
                            Return True
                        End If

                    Else
                        MsgBox("El certificado adicional no es accesible" & vbCrLf & "Indique el certificado de identificación adicional del sistema" & vbCrLf & strFIEL, MsgBoxStyle.Exclamation, "AVISO")
                    End If
                Else
                    Return True
                End If
            End If
        Else
            Return False
        End If
    End Function
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

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub cboSucursales_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSucursales.SelectedIndexChanged

    End Sub

    Private Sub lnkRevSolicitud_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkRevSolicitud.LinkClicked
        Dim frm As New frmSolCLU
        frm.ShowDialog()
        frm.Close()
        Call LlenaForm()
    End Sub

    Private Sub lnkNuevaEmpresa_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkNuevaEmpresa.LinkClicked
        Dim frm As New frmNew
        frm.ShowDialog()
        frm.Close()
        Call LlenaForm()
    End Sub


    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    End Sub
End Class