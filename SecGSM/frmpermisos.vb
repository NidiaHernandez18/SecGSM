Imports System.Windows.Forms
Public Class frmpermisos
    Dim menuadonet As DataTable
    Dim usuado As DataTable
    Dim ban1 As Boolean
    Dim ban2 As Boolean
    Dim nodes As TreeNodeCollection
    Dim n As TreeNode
    Dim tn As TreeNode
    Dim tn2 As TreeNode
    Dim tn3 As TreeNode
    Dim tn4 As TreeNode
    Dim connclass As New ClassCone
    Private VarConectar As New OleDb.OleDbConnection
    Private VarConectar2 As New SqlClient.SqlConnection
    Dim programax As String

    Public Property conectar() As OleDb.OleDbConnection
        Get
            conectar = VarConectar
        End Get
        Set(ByVal value As OleDb.OleDbConnection)
            VarConectar = value
        End Set
    End Property
    Public Property conectar(ByVal IsMsSql) As SqlClient.SqlConnection
        Get
            conectar = VarConectar2
        End Get
        Set(ByVal value As SqlClient.SqlConnection)
            VarConectar2 = value
        End Set
    End Property

    Public Property programa() As String
        Get
            programa = programax
        End Get
        Set(ByVal value As String)
            programax = value
        End Set
    End Property

    Private Sub frmpermisos_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        usuado.Clear()
        usuado = Nothing
    End Sub

    

    Private Sub frmpermisos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim tvRoot As TreeNode
        Dim tvNode As TreeNode
        Dim i As Integer
        Dim j As Integer = -1

        menuadonet = New DataTable
        menuadonet = connclass.RecDatatable("select * from tbl_apps where Programa='" & programax & "'order by numero ", IIf(VarConectar.State = ConnectionState.Closed, VarConectar2, VarConectar))
        tvRoot = Nothing

        For i = 0 To menuadonet.Rows.Count - 1
            If menuadonet.Rows(i).Item("HIJO").ToString.Trim.Length = 0 And menuadonet.Rows(i).Item("HIJO2").ToString.Trim.Length = 0 Then
                tvRoot = Menutreew.Nodes.Add(menuadonet.Rows(i).Item("Padre"), menuadonet.Rows(i).Item("Padre"), menuadonet.Rows(i).Item("dibujo"))
                j = -1
            ElseIf menuadonet.Rows(i).Item("HIJO").ToString.Trim.Length > 0 And menuadonet.Rows(i).Item("HIJO2").ToString.Trim.Length = 0 Then
                tvNode = tvRoot.Nodes.Add(menuadonet.Rows(i).Item("Hijo"), menuadonet.Rows(i).Item("Hijo"), menuadonet.Rows(i).Item("dibujo"))
                j += 1
                If Val(menuadonet.Rows(i).Item("IMPLEC").ToString) > 0 Then
                    tvNode.Nodes.Add("Escritura")
                    tvNode.Nodes.Add("Impresión")

                End If
            ElseIf menuadonet.Rows(i).Item("HIJO").ToString.Trim.Length > 0 And menuadonet.Rows(i).Item("HIJO2").ToString.Trim.Length > 0 Then
                tvNode = tvRoot.Nodes(j).Nodes.Add(menuadonet.Rows(i).Item("hijo2"), menuadonet.Rows(i).Item("hijo2"), menuadonet.Rows(i).Item("dibujo"))
                If Val(menuadonet.Rows(i).Item("IMPLEC").ToString) > 0 Then
                    tvNode.Nodes.Add("Escritura")
                    tvNode.Nodes.Add("Impresión")
                End If
            End If
        Next
        menuadonet.Clear()
        menuadonet = Nothing
        ban1 = False
        Call USUARIOS()
        ban1 = True
        cmbusuario_SelectedIndexChanged(Me, e)


    End Sub
    Private Sub USUARIOS()
        usuado = New DataTable
        usuado = connclass.RecDatatable("select usuario,nombre" _
                                 & " from tbl_usuarios order by usuario", IIf(VarConectar.State = ConnectionState.Closed, VarConectar2, VarConectar))

        With cmbusuario 'Componente ComboBox
            .DataSource = usuado 'Objeto DataTable
            .DisplayMember = "usuario" 'Campo a mostrar en el combo
            .ValueMember = "nombre" 'Campo del cual recupero el valor

        End With
    End Sub


    Private Sub SetSubtreeChecked(ByVal parent_node As TreeNode, ByVal is_checked As Boolean)
        ' Set the parent's Checked value.
        parent_node.Checked = is_checked
        ' Set the child nodes' Checked values.
        For i As Integer = 0 To parent_node.Nodes.Count - 1
            SetSubtreeChecked(parent_node.Nodes(i), is_checked)

        Next i
    End Sub

    Private Sub Menutreew_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles Menutreew.AfterCheck
        Dim parent_node As TreeNode = e.Node
        Dim is_checked As Boolean = parent_node.Checked

        If ban2 Then
            For i As Integer = 0 To e.Node.Nodes.Count - 1
                SetSubtreeChecked(parent_node.Nodes(i), is_checked)
            Next i
        End If
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsalir.Click
        Me.Close()
    End Sub




    Private Sub buscar()

        Dim i As Integer
        Dim Padre As String
        Dim Implec As Integer
        Dim perm As Integer
        Dim permp As Integer = 0
        Dim impre As Integer
      
        ban2 = False
        nodes = Menutreew.Nodes
        menuadonet = New DataTable
        menuadonet = connclass.RecDatatable("select A.Usuario,Padre,Hijo,hijo2,hijo3,Implec,permiso,impresion,A.numero from tbl_permisos " _
                               & " as A, tbl_apps p where A.numero=p.numero and A.Usuario='" & cmbusuario.Text & "' and Programa='" & UCase(programax) & "'", IIf(VarConectar.State = ConnectionState.Closed, VarConectar2, VarConectar))
        For i = 0 To menuadonet.Rows.Count - 1
            Padre = ""
            If Len(menuadonet.Rows(i).Item("Padre").ToString) > 0 Then
                Padre = menuadonet.Rows(i).Item("Padre") & "\"
            End If
            If Len(menuadonet.Rows(i).Item("Hijo").ToString) > 0 Then
                Padre = Padre & menuadonet.Rows(i).Item("Hijo") & "\"
            End If
            If Len(menuadonet.Rows(i).Item("hijo2").ToString) > 0 Then
                Padre = Padre & menuadonet.Rows(i).Item("hijo2") & "\"
            End If
            If Len(menuadonet.Rows(i).Item("hijo3").ToString) > 0 Then
                Padre = Padre & menuadonet.Rows(i).Item("hijo3") & "\"
            End If
            If Padre.Length > 0 Then
                Padre = Mid(Padre, 1, Len(Padre) - 1)

            End If
            perm = IIf(IsDBNull(menuadonet.Rows(i).Item("permiso")), 0, menuadonet.Rows(i).Item("permiso"))
            impre = IIf(IsDBNull(menuadonet.Rows(i).Item("impresion")), 0, menuadonet.Rows(i).Item("impresion"))
            Implec = IIf(IsDBNull(menuadonet.Rows(i).Item("Implec")), 0, menuadonet.Rows(i).Item("Implec"))
            For Each n In nodes
                If Padre = n.FullPath Then
                    n.Checked = True
                    If Implec = 1 Then
                        For Each tn2 In n.Nodes
                            If Padre & "\Escritura" = tn2.FullPath Then
                                tn2.Checked = True
                            End If
                            If Padre & "\Impresión" = tn2.FullPath Then
                                tn2.Checked = True
                            End If
                        Next
                    End If

                End If
                For Each tn2 In n.Nodes
                    If Padre = tn2.FullPath Then
                        tn2.Checked = True
                    End If
                    If Implec = 1 And Padre = tn2.FullPath Then
                        For Each tn3 In tn2.Nodes
                            If Padre & "\Escritura" = tn3.FullPath And perm = 1 Then
                                tn3.Checked = True
                            End If
                            If Padre & "\Impresión" = tn3.FullPath And impre = 1 Then
                                tn3.Checked = True
                            End If

                        Next
                    End If

                    For Each tn3 In tn2.Nodes
                        If Padre = tn3.FullPath Then
                            tn3.Checked = True
                        End If
                        If Implec = 1 And Padre = tn3.FullPath Then
                            For Each tn4 In tn3.Nodes
                                If Padre & "\Escritura" = tn4.FullPath And perm = 1 Then
                                    tn4.Checked = True
                                End If
                                If Padre & "\Impresión" = tn4.FullPath And impre = 1 Then
                                    tn4.Checked = True
                                End If
                            Next
                        End If
                    Next
                Next
            Next
        Next

        ban2 = True
        menuadonet.Clear()
        menuadonet = Nothing
    End Sub

    Private Sub LIMPIAR()
        nodes = Menutreew.Nodes
        For Each n In nodes
            n.Checked = False
            For Each tn In n.Nodes
                tn.Checked = False
                For Each tn2 In tn.Nodes
                    tn2.Checked = False
                    For Each tn3 In tn2.Nodes
                        tn3.Checked = False
                    Next
                Next
            Next
        Next
    End Sub

    Private Sub cmbusuario_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbusuario.SelectedIndexChanged
        If ban1 Then
            txtnombre.Text = cmbusuario.SelectedValue
            LIMPIAR()
            buscar()
        End If
    End Sub

    Private Sub Guardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Guardar.Click
        Dim foundRow() As DataRow
        Dim Padre() As String
        Dim cuanto As Integer

        If MessageBox.Show("Seguro de continuar", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        connclass.ExecuteSql("DELETE FROM tbl_permisos WHERE Usuario='" & cmbusuario.Text & "'" _
                          & " and numero in(select numero from tbl_apps where Programa='" & programax & "')", IIf(VarConectar.State = ConnectionState.Closed, VarConectar2, VarConectar))

        menuadonet = New DataTable '
        menuadonet = connclass.RecDatatable("select * from tbl_apps order by numero", IIf(VarConectar.State = ConnectionState.Closed, VarConectar2, VarConectar))

        nodes = Menutreew.Nodes
        For Each n In nodes
            cuanto = 0
            If n.Checked Then
                Padre = Split(n.FullPath, "\")
                foundRow = menuadonet.Select("Padre='" & Padre(0) & "'")
                connclass.ExecuteSql("INSERT INTO tbl_permisos(Usuario,numero,permiso,impresion)VALUES('" _
                & cmbusuario.Text & "'," & foundRow(0).Item("numero") & ",0,0)", IIf(VarConectar.State = ConnectionState.Closed, VarConectar2, VarConectar))
                For Each tn2 In n.Nodes
                    If tn2.Checked Then
                        Padre = Split(tn2.FullPath, "\")
                        If Padre(1) & "\Escritura" = tn2.FullPath Or Padre(1) & "\Impresión" = tn2.FullPath Then
                            foundRow = menuadonet.Select("Padre='" & Padre(0) & "'")
                            If Padre(1) = "Escritura" Then
                                connclass.ExecuteSql("update tbl_permisos set permiso=1 where Usuario='" & cmbusuario.Text _
                                                     & "' and numero=" & foundRow(0).Item("numero"), IIf(VarConectar.State = ConnectionState.Closed, VarConectar2, VarConectar))
                            Else
                                connclass.ExecuteSql("update tbl_permisos set impresion=1 where Usuario='" & cmbusuario.Text _
                                                    & "' and numero=" & foundRow(0).Item("numero"), IIf(VarConectar.State = ConnectionState.Closed, VarConectar2, VarConectar))
                            End If
                        Else
                            foundRow = menuadonet.Select("Padre='" & Padre(0) & "' and Hijo='" & Padre(1) & "'")
                            connclass.ExecuteSql("INSERT INTO tbl_permisos(Usuario,numero,permiso,impresion)VALUES('" _
                            & cmbusuario.Text & "'," & foundRow(0).Item("numero") & ",0,0)", IIf(VarConectar.State = ConnectionState.Closed, VarConectar2, VarConectar))
                        End If
                        For Each tn3 In tn2.Nodes
                            If tn3.Checked Then
                                Padre = Split(tn3.FullPath, "\")

                                If Padre(0) & "\" & Padre(1) & "\Escritura" = tn3.FullPath Or Padre(0) & "\" & Padre(1) & "\Impresión" = tn3.FullPath Then
                                    foundRow = menuadonet.Select("Padre='" & Padre(0) & "' and Hijo='" & Padre(1) & "'")
                                    If Padre(2) = "Escritura" Then
                                        connclass.ExecuteSql("update tbl_permisos set permiso=1 where Usuario='" & cmbusuario.Text _
                                                             & "' and numero=" & foundRow(0).Item("numero"), IIf(VarConectar.State = ConnectionState.Closed, VarConectar2, VarConectar))
                                    Else
                                        connclass.ExecuteSql("update tbl_permisos set impresion=1 where Usuario='" & cmbusuario.Text _
                                                            & "' and numero=" & foundRow(0).Item("numero"), IIf(VarConectar.State = ConnectionState.Closed, VarConectar2, VarConectar))
                                    End If
                                Else
                                    foundRow = menuadonet.Select("Padre='" & Padre(0) & "' and Hijo='" & Padre(1) & "' and hijo2='" & Padre(2) & "'")
                                    connclass.ExecuteSql("INSERT INTO tbl_permisos(Usuario,numero,permiso,impresion)VALUES('" _
                                    & cmbusuario.Text & "'," & foundRow(0).Item("numero") & ",0,0)", IIf(VarConectar.State = ConnectionState.Closed, VarConectar2, VarConectar))

                                End If
                                For Each tn4 In tn3.Nodes


                                    Padre = Split(tn4.FullPath, "\")
                                    If Padre(0) & "\" & Padre(1) & "\" & Padre(2) & "\Escritura" = tn4.FullPath Or Padre(0) & "\" & Padre(1) & "\" & Padre(2) & "\Impresión" = tn4.FullPath Then
                                        foundRow = menuadonet.Select("Padre='" & Padre(0) & "' and Hijo='" & Padre(1) & "' and hijo2='" & Padre(2) & "'")
                                        If Padre(3) = "Escritura" And tn4.Checked Then
                                            connclass.ExecuteSql("update tbl_permisos set permiso=1 where Usuario='" & cmbusuario.Text _
                                                                 & "' and numero=" & foundRow(0).Item("numero"), IIf(VarConectar.State = ConnectionState.Closed, VarConectar2, VarConectar))
                                        ElseIf padre(3) = "Impresión" And tn4.Checked Then
                                            connclass.ExecuteSql("update tbl_permisos set impresion=1 where Usuario='" & cmbusuario.Text _
                                                                & "' and numero=" & foundRow(0).Item("numero"), IIf(VarConectar.State = ConnectionState.Closed, VarConectar2, VarConectar))
                                        End If
                                    Else
                                        foundRow = menuadonet.Select("Padre='" & padre(0) & "' and Hijo='" & padre(1) & "' and hijo2='" & padre(2) & "'")
                                        connclass.ExecuteSql("INSERT INTO tbl_permisos(Usuario,numero,permiso,impresion)VALUES('" _
                                        & cmbusuario.Text & "'," & foundRow(0).Item("numero") & ",0,0)", IIf(VarConectar.State = ConnectionState.Closed, VarConectar2, VarConectar))
                                    End If


                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next

        MessageBox.Show("Datos guardados", "Datos", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Menutreew.ExpandAll()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Menutreew.CollapseAll()
    End Sub
End Class