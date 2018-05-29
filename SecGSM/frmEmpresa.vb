Imports System.Windows.Forms
Imports System.Windows.Forms.ListView
Public Class frmEmpresa

    Private VarConec As New OleDb.OleDbConnection
    Private contra As New SecGSM.Pk1
    Private empresa As New DataTable
    Private opcU As Integer

    Public Property conectar() As OleDb.OleDbConnection
        Get
            conectar = VarConec
        End Get
        Set(ByVal value As OleDb.OleDbConnection)
            VarConec = value
        End Set
    End Property

    Private Sub Frmpermisos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        empresa = Ejecutarsql.RecDatatable("select * from tbl_empresas ORDER BY numero", VarConec)
        Call datosshow()
    End Sub

    Private Sub btnsalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsalir.Click
        Me.Close()
    End Sub


    Private Sub BtnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOk.Click

        opcU = 1
        Enabdes(False)
        txtsocial.Focus()
    End Sub

    Private Sub Enabdes(ByVal verd As Boolean)
        BtnOk.Visible = verd
        btnEditar.Visible = verd
        Btneliminar.Visible = verd
        btnsalir.Visible = verd
        btnguardar.Visible = Not verd
        BtnCancel.Visible = Not verd
        txtrfc.Enabled = Not verd
        Txtpat.Enabled = Not verd
        txtdirec.Enabled = Not verd
        If Not verd Then
            txtrfc.Text = ""
            Txtpat.Text = ""
            txtdirec.Text = ""
            txtsocial.Text = ""
        End If

    End Sub

    Private Sub btnEditar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditar.Click

        opcU = 2
        Enabdes(False)
        txtsocial.Enabled = False
        txtrfc.Focus()

    End Sub

    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        opcU = 0
        Enabdes(True)
    End Sub

    Private Sub Btneliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btneliminar.Click
        If empresa.Rows.Count = 0 Then
            Call MessageBox.Show("No hay empresas para Eliminar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If MessageBox.Show("Esta seguro de continuar con este proceso? ", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Ejecutarsql.ExecuteSql("delete from tbl_empresas where empresa ='" & txtsocial.Text & "'", VarConec)
            Call MsgBox("Empresa eliminada", vbInformation, "Informacion")
        End If
        empresa = New DataTable
        empresa = Ejecutarsql.RecDatatable("select * from tbl_empresas ORDER BY numero", VarConec)
        datosshow()

    End Sub

    Private Sub datosshow()
        Dim i As Integer

        lstdatos.Clear()
        For i = 0 To empresa.Rows.Count - 1
            lstdatos.Items.Add(IIf(IsDBNull(empresa.Rows(i).Item("empresa")), "", empresa.Rows(i).Item("empresa")), i)
            With lstdatos.Items(i)
                .SubItems.Add(IIf(IsDBNull(empresa.Rows(i).Item("rfc")), "", empresa.Rows(i).Item("rfc")))
                .SubItems.Add(IIf(IsDBNull(empresa.Rows(i).Item("patronal")), "", empresa.Rows(i).Item("patronal")))
                .SubItems.Add(IIf(IsDBNull(empresa.Rows(i).Item("domicilio")), "", empresa.Rows(i).Item("domicilio")))
                .SubItems.Add(IIf(IsDBNull(empresa.Rows(i).Item("numero")), "", empresa.Rows(i).Item("numero")))
            End With

        Next

    End Sub

  



    Private Sub btnguardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnguardar.Click
        'Dim j As Integer
        'Dim pas As String

        'If Not validaciones() Then
        '    Exit Sub
        'End If
        'Ejecutarsql.ExecuteSql("delete from tbl_permisos where programa='" _
        '               & cmbprograma.Items(cmbprograma.SelectedIndex).ToString.Trim & "' and no_empresa='" _
        '               & cmbempresa.Items(cmbempresa.SelectedIndex).ToString.Trim & "' and modulos='" _
        '               & cmbmodulos.Items(cmbmodulos.SelectedIndex).ToString.Trim & "' and usuario='" _
        '               & txtusuario.Text.ToUpper.Trim & "'", VarConectar)
        'pas = txtusuario.Text.ToUpper.Trim & Space(16)
        'pas = Mid(pas, 1, 16)
        'pas = contra.Encriptar(pas)
        'Select Case opcU
        '    Case 2
        '        Ejecutarsql.ExecuteSql("delete from tbl_usuarios where usuario='" & txtusuario.Text.Trim.ToUpper & "'", VarConectar)
        'End Select
        'Ejecutarsql.ExecuteSql("insert into tbl_usuarios(empresa,usuario,nombre,passw) values('" _
        '               & cmbempresa.Items(cmbempresa.SelectedIndex).ToString.Trim & "','" _
        '               & txtusuario.Text.Trim.ToUpper & "','" _
        '               & txtnombre.Text.Trim.ToUpper & "','" _
        '               & pas & "')", VarConectar)
        'For j = 0 To CheModulos.Items.Count - 1
        '    If CheModulos.GetItemCheckState(j) = CheckState.Checked Then
        '        Ejecutarsql.ExecuteSql("insert into tbl_permisos(programa,No_empresa,Modulos,opcion,usuario)" _
        '                             & "values('" & cmbprograma.Items(cmbprograma.SelectedIndex) & "','" _
        '                             & Trim(cmbempresa.Items(cmbempresa.SelectedIndex)) & "','" _
        '                             & Trim(cmbmodulos.Items(cmbmodulos.SelectedIndex)) & "','" _
        '                             & CheModulos.Items(j).ToString & "','" _
        '                            & txtusuario.Text.Trim.ToUpper & "')", VarConectar)
        '    End If
        'Next j
        'Me.CheModulos.ClearSelected()
        'Me.CheUsuarios.ClearSelected()
        'Call BtnCancel_Click(Me, e)
        'Call informacion()
        'Call derechos()
        'Call SubUsuarios()

    End Sub

    Private Function validaciones() As Boolean
        validaciones = False
        If Len(txtsocial.Text) = 0 Then
            Call MsgBox("Por favor introduzca la razon social de la empresa", vbCritical, "Informacion")
            txtsocial.Focus()
            Exit Function
        End If
        If Len(txtrfc.Text) = 0 Then
            Call MsgBox("Por favor introduzca el RFC de la empresa", vbCritical, "Informacion")
            txtrfc.Focus()
            Exit Function
        End If
        If Len(Txtpat.Text) = 0 Then
            Call MsgBox("Por favor introduzca el registro patronal", vbCritical, "Informacion")
            Txtpat.Focus()
            Exit Function
        End If
        validaciones = True

    End Function


End Class