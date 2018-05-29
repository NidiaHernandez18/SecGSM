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

    Private Sub frmpermisos_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        usuado.Clear()
        usuado = Nothing
    End Sub


  
    Private Sub frmpermisos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim tvRoot As TreeNode
        Dim tvNode As TreeNode
        Dim i As Integer
        Dim j As Integer = -1

        menuadonet = New DataTable
        menuadonet = clasEjecutar.RecDatatable("select * from tbl_apps order by numero ", Conn)
        tvRoot = Nothing
        For i = 0 To menuadonet.Rows.Count - 1
            If menuadonet.Rows(i).Item("HIJO").ToString.Trim.Length = 0 And menuadonet.Rows(i).Item("HIJO2").ToString.Trim.Length = 0 Then
                tvRoot = Me.Menutreew.Nodes.Add(menuadonet.Rows(i).Item("Padre"), menuadonet.Rows(i).Item("Padre"), menuadonet.Rows(i).Item("dibujo"), 1)
            ElseIf menuadonet.Rows(i).Item("HIJO").ToString.Trim.Length > 0 And menuadonet.Rows(i).Item("HIJO2").ToString.Trim.Length = 0 Then
                tvNode = tvRoot.Nodes.Add(menuadonet.Rows(i).Item("hijo"), menuadonet.Rows(i).Item("hijo"), menuadonet.Rows(i).Item("dibujo"))
                j += 1
            ElseIf menuadonet.Rows(i).Item("HIJO").ToString.Trim.Length > 0 And menuadonet.Rows(i).Item("HIJO2").ToString.Trim.Length > 0 Then
                If j > 1 Then j = 0
                tvNode = tvRoot.Nodes(j).Nodes.Add(menuadonet.Rows(i).Item("HIJO2"), menuadonet.Rows(i).Item("HIJO2"), menuadonet.Rows(i).Item("dibujo"))
                tvNode.Nodes.Add("Escritura")
                tvNode.Nodes.Add("Impresión")

             


            End If
        Next
        menuadonet.Clear()
        menuadonet = Nothing
        ban1 = False
        Call USUARIOS()
        ban1 = True
        cmbusuario_SelectedIndexChanged(Me, e)

        derechousua("PERMISOS DEL USUARIO", Me)
    End Sub
    Private Sub USUARIOS()
        usuado = New DataTable
        usuado = clasEjecutar.RecDatatable("select Usuario,nombre" _
                                 & " from tbl_usuarios order by usuario", Conn)

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
        Dim valor As String
        Dim valor2 As Integer
        Dim valor3 As Integer
        ban2 = False

        nodes = Menutreew.Nodes
        menuadonet = New DataTable
        menuadonet = clasEjecutar.RecDatatable("select a.usuario,padre,hijo,hijo2,permiso,impresion,a.numero from tbl_permisos " _
                               & " as a, tbl_apps p where a.numero=p.numero and a.usuario='" & cmbusuario.Text & "'", Conn)

        valor = ""
        valor3 = 0

        For i = 0 To menuadonet.Rows.Count - 1
            valor2 = 0
            If menuadonet.Rows(i).Item("HIJO").ToString.Trim.Length = 0 And menuadonet.Rows(i).Item("HIJO2").ToString.Trim.Length = 0 Then
                valor = menuadonet.Rows(i).Item("Padre")
            ElseIf menuadonet.Rows(i).Item("HIJO").ToString.Trim.Length > 0 And menuadonet.Rows(i).Item("HIJO2").ToString.Trim.Length = 0 Then
                valor = menuadonet.Rows(i).Item("hijo")
            ElseIf menuadonet.Rows(i).Item("HIJO").ToString.Trim.Length > 0 And menuadonet.Rows(i).Item("HIJO2").ToString.Trim.Length > 0 Then
                valor = menuadonet.Rows(i).Item("HIJO2")
                valor2 = menuadonet.Rows(i).Item("permiso")
                valor3 = menuadonet.Rows(i).Item("impresion")
            End If




            For Each n In nodes

                If valor = n.Text Then
                    n.Checked = True
                    Exit For
                End If
                For Each tn In n.Nodes
                    If valor = tn.Text Then
                        tn.Checked = True
                        Exit For
                    End If
                    For Each tn2 In tn.Nodes
                        If valor = tn2.Text Then
                            tn2.Checked = True

                        End If
                        For Each tn3 In tn2.Nodes
                            tn3.Checked = False
                            If tn3.Text = "Escritura" And valor2 = 1 Then
                                tn3.Checked = True
                            ElseIf tn3.Text = "Impresión" And valor3 = 1 Then
                                tn3.Checked = True
                            End If

                        Next


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
        Dim xnumero As Integer
        Dim foundRow() As DataRow

        If MessageBox.Show("Seguro de continuar", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        clasEjecutar.ExecuteSql("DELETE FROM TBL_PERMISOS WHERE USUARIO='" & cmbusuario.Text & "'", Conn)
        menuadonet = New DataTable '
        menuadonet = clasEjecutar.RecDatatable("select * from tbl_apps order by numero", Conn)

        nodes = Menutreew.Nodes
        For Each n In nodes

            If n.Checked Then
                foundRow = menuadonet.Select("Padre='" & n.Text.Trim & "'")
                clasEjecutar.ExecuteSql("INSERT INTO TBL_PERMISOS(USUARIO,NUMERO,PERMISO)VALUES('" _
                & cmbusuario.Text & "'," & foundRow(0).Item("NUMERO") & ",0)", Conn)
            End If

            For Each tn In n.Nodes
                If tn.Checked Then
                    foundRow = menuadonet.Select("hijo='" & tn.Text.Trim & "'")
                    clasEjecutar.ExecuteSql("INSERT INTO TBL_PERMISOS(USUARIO,NUMERO,PERMISO)VALUES('" _
                    & cmbusuario.Text & "'," & foundRow(0).Item("NUMERO") & ",0)", Conn)
                End If
                For Each tn2 In tn.Nodes
                    xnumero = 0
                    If tn2.Checked Then
                        foundRow = menuadonet.Select("hijo2='" & tn2.Text.Trim & "'")
                        clasEjecutar.ExecuteSql("INSERT INTO TBL_PERMISOS(USUARIO,NUMERO,PERMISO)VALUES('" _
                        & cmbusuario.Text & "'," & foundRow(0).Item("NUMERO") & ",0)", Conn)
                        xnumero = foundRow(0).Item("NUMERO")
                    End If
                    For Each tn3 In tn2.Nodes
                        If tn3.Checked And tn3.Text = "Escritura" Then
                            clasEjecutar.ExecuteSql("update TBL_PERMISOS set permiso=1,IMPRESION=0 where " _
                            & "USUARIO='" & cmbusuario.Text & "' and numero=" & xnumero, Conn)
                        ElseIf tn3.Checked And tn3.Text = "Impresión" Then
                            clasEjecutar.ExecuteSql("update TBL_PERMISOS set impresion=1 where " _
                                                    & "USUARIO='" & cmbusuario.Text & "' and numero=" & xnumero, Conn)
                        End If


                    Next
                Next
            Next
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