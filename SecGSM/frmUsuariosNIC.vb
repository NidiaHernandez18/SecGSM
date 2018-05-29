Imports System.Windows.Forms
Public Class frmusuariosNIC
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
        Dim dtbSucursales As DataTable
        Empresa = CDec(Strings.Left(cboEmpresas.Text, InStr(cboEmpresas.Text, "-") - 1))
        dtbSucursales = New DataView(dtbrutas, "Numero=" & Empresa, "", DataViewRowState.CurrentRows).ToTable
        'cboSucursales.Items.Clear()
        cboSucursales.DataSource = dtbSucursales
        cboSucursales.DisplayMember = "Sucursal"
        cboSucursales.ValueMember = "Sucursal"
        If cboSucursales.Items.Count > 0 Then
            cboSucursales.SelectedIndex = 0
            txtuser.Focus()
        End If
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
            MsgBox("Introduzca la contraseña de acceso", MsgBoxStyle.Exclamation, "AVISO")
            txtpass.Focus()
        Else
            If txtCert.Text <> "" Then
                'Verificar si existe la ruta del certificado
                If Not My.Computer.FileSystem.FileExists(txtCert.Text) Then
                    MsgBox("El certificado no es accesible" & vbCrLf & "Indique el certificado de licencia de uso del sistema", MsgBoxStyle.Exclamation, "AVISO")
                    'pnlCertificado.Visible = True
                    'Me.Height = 450
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
            dtrEmpresaSelected = dtbrutas.Select("Numero=" & Empresa & " AND Sucursal= " & cboSucursales.Text)(0)
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
                            If My.Application.Info.DirectoryPath & "\SAT\clu\CLU_" & dtrEmpresa!RFC.ToString & "_" & dtrEmpresa!Numero & "_" & dtrEmpresa!Sucursal & ".cer" = "" Then
                                MsgBox("Indique el certificado de licencia de uso del sistema", MsgBoxStyle.Exclamation, "AVISO")
                                'Me.Height = 450
                                'pnlCertificado.Visible = True
                                txtCert.Focus()
                                Exit Sub
                            Else
                                'Verificar si existe la ruta del certificado
                                txtCert.Text = My.Application.Info.DirectoryPath & "\SAT\CLU_" & dtrEmpresa!RFC.ToString & "_" & dtrEmpresa!Numero & "_" & dtrEmpresa!Sucursal & ".cer"
                                If Not My.Computer.FileSystem.FileExists(txtCert.Text) Then
                                    MsgBox("El certificado de licencia de uso no es accesible" & vbCrLf & "Indique el certificado de licencia de uso del sistema", MsgBoxStyle.Exclamation, "AVISO")
                                    'Me.Height = 450
                                    'pnlCertificado.Visible = True

                                    'cmdCert.Focus()
                                    Exit Sub
                                End If
                                DatosEquipo.certificado = txtCert.Text

                            End If
                        Else
                            DatosEquipo.certificado = txtCert.Text
                        End If

                        Dim cert As New SecGSM.ClassCandado
                        'Dim DatosCert As InfoCert = Nothing
                        If cert.ValidaCert(DatosCert, DatosEquipo.certificado, dtrEmpresa!Empresa.ToString, dtrEmpresa!RFC.ToString, DatosEquipo.Sistema) Then
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
        'Me.Height = 317
        'pnlCertificado.Visible = False

        If Not IsNothing(dtbrutas) Then
            dtbEmpresas = DistinctRowInDatatable(dtbrutas, "Numero", "Empresa")
            For Each dtr In dtbEmpresas.Rows
                cboEmpresas.Items.Add("" & dtr("Numero") & "-" & Trim(dtr("Empresa")))
            Next
            If cboEmpresas.Items.Count > 0 Then
                cboEmpresas.SelectedIndex = 0
                txtuser.Focus()
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
    'Private Sub lnkOpciones_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
    '    If Me.Height = 488 Then
    '        Me.Height = 317
    '        pnlCertificado.Visible = False
    '        lnkOpciones.Text = "Mas opciones"
    '    Else
    '        Me.Height = 488
    '        pnlCertificado.Visible = True
    '        lnkOpciones.Text = "Menos opciones"
    '    End If
    'End Sub

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
            CambioCertificado = True
        End If
    End Sub

    Private Sub cboSucursales_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSucursales.Click
        Dim dtr As DataRow
        Dim Security As New Pk1
        Dim candado As String
        dtr = cboSucursales.SelectedItem.row
        If Not IsNothing(dtr) Then
            If dtr.Table.Columns.Contains("RFC") Then
                lblRFC.Text = dtr!RFC.ToString
            Else
                lblRFC.Text = ""
            End If
            candado = Mid(dtr!Empresa & Space(39), 1, 39) & Mid(lblRFC.Text & Space(14), 1, 14) & Mid(lblAplicacion.Text.PadRight(3, " "), 1, 3)
            candado = Security.Encriptarpaso1(candado)
        End If
        txtCadena.Text = candado
    End Sub

    Private Sub cboSucursales_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSucursales.SelectedIndexChanged
        Dim dtr As DataRow
        Dim Security As New Pk1
        Dim candado As String
        dtr = cboSucursales.SelectedItem.row
        If Not IsNothing(dtr) Then
            If dtr.Table.Columns.Contains("RFC") Then
                lblRFC.Text = dtr!RFC.ToString
            Else
                lblRFC.Text = ""
            End If
            candado = Mid(dtr!Empresa & Space(95), 1, 95) & Mid(lblRFC.Text & Space(14), 1, 14) & Mid(lblAplicacion.Text.PadRight(3, " "), 1, 3)
            candado = Security.Encriptarpaso1(candado)
        End If
        txtCadena.Text = candado
    End Sub
End Class