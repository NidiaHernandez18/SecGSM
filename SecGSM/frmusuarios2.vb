Imports System.Windows.Forms
Public Class frmusuarios2
    Inherits frmusuarios
    Public dtbrutas As DataTable
    Public dtrEmpresaSelected As DataRow
    Dim userapl As String
    Dim contprog As Boolean
    Dim connprinc As OleDb.OleDbConnection
    Dim usuariotabla As New DataTable
    Public ErrorEnPathdeEmpresa As Boolean
    Public Shadows Property conectar() As OleDb.OleDbConnection
        Get
            conectar = connprinc
        End Get
        Set(ByVal value As OleDb.OleDbConnection)
            connprinc = value
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
    'Public Overrides Sub conectarBD()
    '    Dim dtr As DataRow
    '    Dim dtbEmpresas As DataTable
    '    dtbEmpresas = DistinctRowInDatatable(dtbrutas, "Numero", "Empresa")
    '    For Each dtr In dtbEmpresas.Rows
    '        cboEmpresas.Items.Add("" & dtr("Numero") & "-" & Trim(dtr("Empresa")))
    '    Next
    '    If cboEmpresas.Items.Count > 0 Then
    '    End If
    '    dtbEmpresas.Clear()
    '    dtbEmpresas = Nothing
    '    dtr = Nothing
    '    'Label4.Text = nombreaplic
    '    'intentos = 0
    'End Sub
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
        Empresa = Strings.Left(cboEmpresas.Text, InStr(cboEmpresas.Text, "-") - 1)
        dtrSucursales = dtbrutas.Select("Numero=" & Empresa)
        cboSucursales.Items.Clear()
        For Each dtr In dtrSucursales
            cboSucursales.Items.Add(dtr!Sucursal)
        Next
    End Sub
    Public Overrides Sub btnAceptarClick()
        Dim xpass1 As String
        Dim xfecha As Date
        Dim xintentos As Integer
        Dim recux As New ClassCone
        Dim reccon1 As New Cblowfish
        Dim Empresa As String
        If Validacion() Then
            Empresa = Strings.Left(cboEmpresas.Text, InStr(cboEmpresas.Text, "-") - 1)
            dtrEmpresaSelected = dtbrutas.Select("Numero=" & Empresa & " AND Sucursal= " & cboSucursales.Text)(0)
            connprinc = recux.conectarsql(dtrEmpresaSelected!Server, dtrEmpresaSelected!User, dtrEmpresaSelected!Password, dtrEmpresaSelected!Database, IIf(dtrEmpresaSelected!DBMS = "SQL", ClassCone.Basededatos.Sqlserver, ClassCone.Basededatos.Access))
            If Not connprinc Is Nothing Then
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
                                'Me.Close()
                                Me.Hide()
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
                    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "AVISO")
                Finally
                    usuariotabla.Clear()
                    usuariotabla = Nothing
                End Try
            Else
                Call MessageBox.Show("No se puede conectar a la empresa seleccionada", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ErrorEnPathdeEmpresa = True
                Me.Hide()
            End If
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
            result = True
        End If
        Return result
    End Function
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

    End Sub
End Class