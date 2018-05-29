Public Class frmTables
    Private dts As DataSet

    Private Sub frmTables_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub frmTables_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For Each dtb As DataTable In dts.Tables
            List.Items.Add(dtb.TableName)
        Next
    End Sub

    Public Sub New(ByVal cdts As DataSet)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        dts = cdts
    End Sub

    Private Sub List_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles List.DoubleClick
        Dim frm As New frmViewDatatable
        frm.DataGridView1.DataSource = dts.Tables(List.Text)
        frm.ShowDialog(Me)
    End Sub


    Private Sub cmdVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdVer.Click
        Dim frm As New frmViewDatatable
        frm.DataGridView1.DataSource = dts.Tables(List.Text)
        frm.ShowDialog(Me)
    End Sub
End Class