Imports System.Windows.Forms
Public Class frmUsuariosAMMAC
    Public dtbrutas As DataTable = Nothing
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
    Public dtrEmpresa As DataRow
    Public IsMultiDBMS As Boolean = False
    Public AutoLogin As Boolean = False
    Public AutoEmpresa As String
    Public AutoSucursal As String
    Public AutoUsuario As String
    Public AutoPassword As String

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
    Private Function DistinctRowInDatatable(ByVal dtb As DataTable, ByVal ParamArray columns() As String)
        Dim result As DataTable
        Dim dvw As DataView = dtb.DefaultView
        result = dvw.ToTable(True, columns)
        Return result
    End Function
    Private Sub cboEmpresas_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboEmpresas.SelectedIndexChanged
        Dim Empresa As Integer
        Dim dtr As DataRow
        Dim dtrSucursales() As DataRow
        Empresa = CDec(Strings.Left(cboEmpresas.Text, InStr(cboEmpresas.Text, "-") - 1))
        dtrSucursales = dtbrutas.Select("Numero='" & Empresa & "'")
        cboSucursales.Items.Clear()
        For Each dtr In dtrSucursales
            cboSucursales.Items.Add(dtr!Sucursal)
        Next
    End Sub
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
            MsgBox("Introduzca la contrase�a de acceso", MsgBoxStyle.Exclamation, "AVISO")
            txtpass.Focus()
        Else
            If txtCert.Text <> "" Then
                'Verificar si existe la ruta del certificado
                If Not My.Computer.FileSystem.FileExists(txtCert.Text) Then
                    MsgBox("El certificado no es accesible" & vbCrLf & "Indique el certificado de licencia de uso del sistema", MsgBoxStyle.Exclamation, "AVISO")
                    pnlCertificado.Visible = True
                    Me.Height = 400
                Else
                    CambioCertificado = True
                    Return True
                End If
            Else
                Return True
            End If
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
        If Validacion() Then
            Empresa = Strings.Left(cboEmpresas.Text, InStr(cboEmpresas.Text, "-") - 1)
            dtrEmpresaSelected = dtbrutas.Select("Numero='" & Empresa & "' AND Sucursal='" & cboSucursales.Text & "'")(0)
            Try
                If dtrEmpresaSelected!DBMS = "SQL" And Not IsMultiDBMS Then
                    connPrinc = recux.msConectarSQL(dtrEmpresaSelected!Server, dtrEmpresaSelected!User, dtrEmpresaSelected!Password, dtrEmpresaSelected!Database)
                    conn = connPrinc
                Else
                    If dtrEmpresaSelected!DBMS = "SQL" Then
                        connPrinc2 = recux.conectarsql(dtrEmpresaSelected!Server, dtrEmpresaSelected!User, dtrEmpresaSelected!Password, dtrEmpresaSelected!Database)
                    Else
                        connPrinc2 = recux.conectarsql(dtrEmpresaSelected!Server, dtrEmpresaSelected!User, dtrEmpresaSelected!Password, dtrEmpresaSelected!Database, ClassCone.Basededatos.Access)
                    End If
                    conn = connPrinc2
                End If
            Catch ex As Exception

            End Try
            If Not conn Is Nothing Then
                Try
                    'EMPRESA
                    Dim dtb As DataTable
                    Dim strSQL As String
                    strSQL = "SELECT * FROM tbl_Empresas WHERE Numero=" & Empresa & " AND Sucursal=" & cboSucursales.Text
                    dtb = recux.RecDatatable(strSQL, conn)
                    If dtb.Rows.Count > 0 Then
                        dtrEmpresa = dtb.Rows(0)
                        If Not CambioCertificado Then
                            ' ''If My.Application.Info.DirectoryPath & "\SAT\clu\CLU_" & dtrEmpresa!RFC.ToString & "_" & dtrEmpresa!Numero & "_" & dtrEmpresa!Sucursal & ".cer" = "" Then
                            ' ''    MsgBox("Indique el certificado de licencia de uso del sistema", MsgBoxStyle.Exclamation, "AVISO")
                            ' ''    Me.Height = 400
                            ' ''    pnlCertificado.Visible = True
                            ' ''    txtCert.Focus()
                            ' ''    Exit Sub
                            ' ''Else
                            'Verificar si existe la ruta del certificado
                            txtCert.Text = My.Application.Info.DirectoryPath & "\SAT\CLU_" & dtrEmpresa!RFC.ToString & "_" & dtrEmpresa!Numero & "_" & dtrEmpresa!Sucursal & ".cer"
                            ' ''txtCertAdicional.Text = My.Application.Info.DirectoryPath & "\SAT\CLU2_" & dtrEmpresa!RFC.ToString & "_" & dtrEmpresa!Numero & "_" & dtrEmpresa!Sucursal & ".cer"
                            If Not My.Computer.FileSystem.FileExists(txtCert.Text) Then
                                MsgBox("El certificado de licencia de uso no es accesible" & vbCrLf & "Indique el certificado de licencia de uso del sistema", MsgBoxStyle.Exclamation, "AVISO")
                                Me.Height = 400
                                pnlCertificado.Visible = True

                                'cmdCert.Focus()
                                Exit Sub
                            End If
                            DatosEquipo.certificado = txtCert.Text
                            ' ''DatosEquipo.certificado2 = txtCertAdicional.Text
                            ' ''End If
                        Else
                            DatosEquipo.certificado = txtCert.Text
                            ' ''DatosEquipo.certificado2 = txtCertAdicional.Text
                        End If

                        '' Certificado Valido
                        'If X509.CertIsValidNow(DatosEquipo.certificado.ToString.Trim) Then

                        'Else
                        '    MsgBox("El certificado no es v�lido" & vbCrLf & "Solicite un nuevo certificado de licencia de uso del sistema", MsgBoxStyle.Exclamation, "AVISO")
                        '    Me.Height = 367
                        '    pnlCertificado.Visible = True
                        '    Exit Sub
                        'End If

                        '' FIEL o CSD o Certificado 2 Validacion
                        'Dim Fiel As InfoCert
                        'Fiel.Subject = GetSubjectValues(DatosEquipo.certificado2)
                        'If InStr(Fiel.Subject.d2_5_4_45, "/") > 0 Then
                        '    Fiel.RFC = Fiel.Subject.d2_5_4_45.Substring(0, InStr(Fiel.Subject.d2_5_4_45, "/") - 1).Trim
                        'Else
                        '    Fiel.RFC = Fiel.Subject.d2_5_4_45
                        'End If
                        'Fiel.Nombre = Fiel.Subject.O
                        'Fiel.NoSerie = HexAsStringToCharactersAsString(X509.CertSerialNumber(DatosEquipo.certificado2))

                        '' CERTIFICADO VALIDACION
                        'Dim qryCert As String
                        'qryCert = CryptoSysPKI.X509.CertSubjectName(DatosEquipo.certificado, "|")

                        'Dim Datos() As String
                        'Datos = qryCert.Split("|")
                        'For Each Str As String In Datos
                        '    DatosCert.Subject = GetSubjectValues(DatosEquipo.certificado)
                        '    ' RFC
                        '    DatosCert.RFC = DatosCert.Subject.O
                        '    ' Nombre o Razon Social
                        '    DatosCert.Nombre = DatosCert.Subject.CN
                        '    ' NoSerie FIEL
                        '    DatosCert.NoSerieFIEL = Fiel.NoSerie

                        '    If Str.StartsWith("OU=") Then
                        '        Dim DatosVer As String() = Str.Split(",")
                        '        ' 1. Verifica IDSystem
                        '        DatosCert.IDSistema = DatosVer(0).Replace("OU=", "").Replace(";", "")
                        '        ' 2. Verifica NoVersion
                        '        DatosCert.NoVersion = DatosVer(1).Replace("OU=", "").Replace(";", "")
                        '        ' 3. Verifica TipoVersion
                        '        DatosCert.TipoVersion = DatosVer(2).Replace("OU=", "").Replace(";", "")

                        '        Dim b() As Byte
                        '        Dim encoding As New System.Text.UTF8Encoding()
                        '        b = encoding.GetBytes(dtrEmpresa!RFC.ToString)

                        '        Dim vALORfINAL As Integer = 0
                        '        Dim i As Byte
                        '        For Each i In b
                        '            Dim ValTotal As Integer
                        '            ValTotal = i
                        '            vALORfINAL += ValTotal
                        '        Next
                        '        vALORfINAL += DatosCert.TipoVersion

                        '        DatosCert.TipoVersion = ""
                        '        For j As Single = 0 To vALORfINAL.ToString.Length - 1 Step 2
                        '            Dim hexVal As String
                        '            Dim ValTotal As Integer
                        '            hexVal = vALORfINAL.ToString.Substring(j, 2)
                        '            hexVal = Convert.ToChar(CInt(hexVal))
                        '            DatosCert.TipoVersion &= hexVal
                        '        Next
                        '    ElseIf Str.StartsWith("O=") Then
                        '        DatosCert.RFC = Str.Replace("O=", "").Replace(";", "")
                        '    End If
                        'Next
                        '' Verificar el IDSYSTEM
                        'If DatosEquipo.Sistema <> datosCert.IDSistema Then
                        '    MsgBox("El certificado no es correcto." & vbCrLf & "Indique el certificado de licencia de uso del sistema", MsgBoxStyle.Exclamation, "AVISO")
                        '    Me.Height = 450
                        '    pnlCertificado.Visible = True
                        '    Exit Sub
                        'ElseIf Not (dtrEmpresa!RFC.ToString = DatosCert.RFC And DatosCert.RFC = FIEL.rfc) Then
                        '    MsgBox("El RFC del certificado de licencia de uso, no coincide con el de la empresa registrada." & vbCrLf & "Indique el certificado de licencia de uso del sistema para: " & dtrEmpresa!RFC.ToString, MsgBoxStyle.Exclamation, "AVISO")
                        '    Me.Height = 450
                        '    pnlCertificado.Visible = True
                        '    Exit Sub
                        'ElseIf Not (dtrEmpresa!Empresa.ToString = DatosCert.Nombre And DatosCert.Nombre = Fiel.Nombre) Then
                        '    MsgBox("El Nombre o Razon Social del certificado de licencia de uso no coincide con la empresa registrada." & vbCrLf & "Indique el certificado de licencia de uso correcto para: " & dtrEmpresa!Empresa.ToString, MsgBoxStyle.Exclamation, "AVISO")
                        '    Me.Height = 450
                        '    pnlCertificado.Visible = True
                        '    Exit Sub
                        'Else
                        '    'If DatosEquipo.TipoVersion = DatosEquipo.TipoVersionLlave Then

                        '    'End If
                        'End If
                        Dim cert As New SecGSM.ClassCandado
                        'Dim DatosCert As InfoCert = Nothing
                        If cert.ValidaCertExpoAMMAC(DatosCert, DatosEquipo.certificado, dtrEmpresa!Empresa.ToString, dtrEmpresa!RFC.ToString, DatosEquipo.Sistema) Then
                            Dim Dt As New DataTable
                            Dim HDD1 As String
                            Dim candado As String
                            Dim Security As New Pk1
                            Dim SelloValidado As Boolean = False

                            CanLla.empresas = dtrEmpresa!Empresa
                            CanLla.PATRONAL = dtrEmpresa!RegPatronal.ToString
                            CanLla.RFC = dtrEmpresa!RFC.ToString


                            Dt = New DataTable
                            HDD1 = SerieDisk("C:\")
                            'candado = llave and llave = candado
                            candado = Mid(HDD1 & Space(11), 1, 11) & Mid(CanLla.empresas & Space(30), 1, 30) & Mid(dtrEmpresa!RFC.ToString & Space(12), 1, 12) & Mid(lblAplicacion.Text.PadRight(3, " "), 1, 3)
                            candado = Security.Encriptarpaso1(candado)
                            Dt = Ejecutarsql.RecDatatable("select * from tbl_certLlave where llave='" & candado & "'", conn)

                            If Dt.Rows.Count > 0 Then
                                Dim dtr As DataRow
                                Dim ParametrosAdic As String
                                For Each dtr In Dt.Rows
                                    If Security.Verify_Sign(dtr!Candado, DatosEquipo.certificado, candado) Then
                                        SelloValidado = True
                                    End If
                                Next
                            End If
                            If Not SelloValidado Then
                                Me.Opacity = 75%
                                Dim frm As New frmCertificado
                                If dtrEmpresaSelected!DBMS = "SQL" And Not IsMultiDBMS Then
                                    frm.conectar(True) = conn
                                Else
                                    frm.conectar() = conn
                                End If
                                frm.lblCandado.Text = candado
                                frm.txtCert.Text = DatosEquipo.certificado.ToString.Trim
                                frm.ShowDialog()
                                Me.Opacity = 100%
                                If Not frm.continuar Then
                                    Exit Sub
                                End If
                            End If
                            Dt = Nothing

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
                                        If dtrEmpresaSelected!DBMS = "SQL" And Not IsMultiDBMS Then
                                            frmcambio.conectar(True) = conn
                                        Else
                                            frmcambio.conectar() = conn
                                        End If
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
                                        If dtrEmpresaSelected!DBMS = "SQL" And Not IsMultiDBMS Then
                                            frmcambio.conectar(True) = conn
                                        Else
                                            frmcambio.conectar() = conn
                                        End If
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
                Call MessageBox.Show("No se puede conectar a la empresa seleccionada", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ErrorEnPathdeEmpresa = True
                Me.Hide()
            End If
        End If
    End Sub
    Private Sub frmusuarios_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dtr As DataRow
        Dim dtbEmpresas As DataTable
        Me.Height = 278
        pnlCertificado.Visible = False

        If Not IsNothing(dtbrutas) Then
            dtbEmpresas = DistinctRowInDatatable(dtbrutas, "Numero", "Empresa")
            For Each dtr In dtbEmpresas.Rows
                cboEmpresas.Items.Add("" & dtr("Numero") & "-" & Trim(dtr("Empresa")))
            Next
            If cboEmpresas.Items.Count > 0 Then
                cboEmpresas.SelectedIndex = 0
            End If
            dtbEmpresas.Clear()
            dtbEmpresas = Nothing
            dtr = Nothing
            'Label4.Text = nombreaplic
            'intentos = 0
        End If

    End Sub
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub


    Private Sub cmdCert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        OpenDialog.DefaultExt = ".cer"
        OpenDialog.Filter = "Certificado de Seguridad(*.cer)|*.cer"
        If OpenDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtCert.Text = OpenDialog.FileName
            CambioCertificado = True
        End If
    End Sub
    Private Sub btnsalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsalir.Click
        Me.Close()
    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub txtpass_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtpass.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call btnAceptar_Click(btnAceptar, e)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        OpenDialog.DefaultExt = ".cer"
        OpenDialog.Filter = "Certificado de Seguridad(*.cer)|*.cer"
        If OpenDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtCertAdicional.Text = OpenDialog.FileName
            CambioCertificado = True
        End If
    End Sub

    Private Sub ToolStripStatusLabel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub frmusuarios3_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If AutoLogin Then
            cboEmpresas.Text = AutoEmpresa
            cboSucursales.Text = AutoSucursal
            txtuser.Text = AutoUsuario
            txtpass.Text = AutoPassword
            Call btnAceptar_Click(sender, e)
        End If
    End Sub
End Class