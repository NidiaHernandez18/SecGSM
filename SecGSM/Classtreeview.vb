
Imports System.Windows.Forms

Public Class Classtreeview

    Public Sub LoadTreePrinc(ByVal treemenu2 As TreeView, ByVal conndata As OleDb.OleDbConnection, ByVal iduser As String, ByVal Program As String)
        Dim menuadonet As DataTable
        Dim rec As New ClassCone
        Dim tvRoot As TreeNode
        Dim tvNode As TreeNode

        Dim i As Integer
        Dim j As Integer = -1

        menuadonet = New DataTable
        treemenu2.Visible = True
        treemenu2.Nodes.Clear()
        menuadonet = rec.RecDatatable("select A.*,B.permiso from tbl_apps AS A,tbl_permisos AS B where " _
                    & "A.numero=B.numero AND B.Usuario='" & iduser & "' and Programa='" & Program & "' order by A.numero", conndata)

        tvRoot = Nothing
        For i = 0 To menuadonet.Rows.Count - 1
            If menuadonet.Rows(i).Item("HIJO").ToString.Trim.Length = 0 And menuadonet.Rows(i).Item("HIJO2").ToString.Trim.Length = 0 Then
                tvRoot = treemenu2.Nodes.Add(menuadonet.Rows(i).Item("Padre"), menuadonet.Rows(i).Item("Padre"), menuadonet.Rows(i).Item("dibujo"))
                tvRoot.Tag = menuadonet.Rows(i)("numero")
                j = -1
            ElseIf menuadonet.Rows(i).Item("HIJO").ToString.Trim.Length > 0 And menuadonet.Rows(i).Item("HIJO2").ToString.Trim.Length = 0 Then
                tvNode = tvRoot.Nodes.Add(menuadonet.Rows(i).Item("hijo"), menuadonet.Rows(i).Item("hijo"), menuadonet.Rows(i).Item("dibujo"))
                tvNode.Tag = menuadonet.Rows(i)("numero")
                j += 1
            ElseIf menuadonet.Rows(i).Item("HIJO").ToString.Trim.Length > 0 And menuadonet.Rows(i).Item("HIJO2").ToString.Trim.Length > 0 Then
                tvNode = tvRoot.Nodes(j).Nodes.Add(menuadonet.Rows(i).Item("hijo2"), menuadonet.Rows(i).Item("hijo2"), menuadonet.Rows(i).Item("dibujo"))
                tvNode.Tag = menuadonet.Rows(i)("numero")
            End If
        Next
        menuadonet.Clear()
        menuadonet = Nothing
        rec = Nothing

    End Sub
    'Public Sub LoadTreePrinc(ByVal treemenu2 As TreeView, ByVal conndata As SqlClient.SqlConnection, ByVal iduser As String, ByVal Program As String)
    '    Dim menuadonet As DataTable
    '    Dim rec As New ClassCone
    '    Dim tvRoot As TreeNode
    '    Dim tvNode As TreeNode

    '    Dim i As Integer
    '    Dim j As Integer = -1

    '    menuadonet = New DataTable
    '    treemenu2.Visible = True
    '    treemenu2.Nodes.Clear()
    '    menuadonet = rec.RecDatatable("select A.*,B.permiso from tbl_apps AS A,tbl_permisos AS B where " _
    '                & "A.numero=B.numero AND B.Usuario='" & iduser & "' and Programa='" & Program & "' order by A.numero", conndata)

    '    tvRoot = Nothing
    '    For i = 0 To menuadonet.Rows.Count - 1
    '        If menuadonet.Rows(i).Item("HIJO").ToString.Trim.Length = 0 And menuadonet.Rows(i).Item("HIJO2").ToString.Trim.Length = 0 Then
    '            tvRoot = treemenu2.Nodes.Add(menuadonet.Rows(i).Item("Padre"), menuadonet.Rows(i).Item("Padre"), menuadonet.Rows(i).Item("dibujo"))
    '            tvRoot.Tag = menuadonet.Rows(i)("numero")
    '            j = -1
    '        ElseIf menuadonet.Rows(i).Item("HIJO").ToString.Trim.Length > 0 And menuadonet.Rows(i).Item("HIJO2").ToString.Trim.Length = 0 Then
    '            tvNode = tvRoot.Nodes.Add(menuadonet.Rows(i).Item("hijo"), menuadonet.Rows(i).Item("hijo"), menuadonet.Rows(i).Item("dibujo"))
    '            tvNode.Tag = menuadonet.Rows(i)("numero")
    '            j += 1
    '        ElseIf menuadonet.Rows(i).Item("HIJO").ToString.Trim.Length > 0 And menuadonet.Rows(i).Item("HIJO2").ToString.Trim.Length > 0 Then
    '            tvNode = tvRoot.Nodes(j).Nodes.Add(menuadonet.Rows(i).Item("hijo2"), menuadonet.Rows(i).Item("hijo2"), menuadonet.Rows(i).Item("dibujo"))
    '            tvNode.Tag = menuadonet.Rows(i)("numero")
    '        End If
    '    Next
    '    menuadonet.Clear()
    '    menuadonet = Nothing
    '    rec = Nothing
    'End Sub
End Class
