Imports System.Windows.Forms
Public Class ListViewComparer
    Implements IComparer
    Private m_ColumnNumber As Integer
    Private m_SortOrder As Windows.Forms.SortOrder

    Public Sub New(ByVal column_number As Integer, ByVal sort_order As Windows.Forms.SortOrder)
        m_ColumnNumber = column_number
        m_SortOrder = sort_order
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        Dim item_x As Windows.Forms.ListViewItem = DirectCast(x, Windows.Forms.ListViewItem)
        Dim item_y As Windows.Forms.ListViewItem = DirectCast(y, Windows.Forms.ListViewItem)

        ' Get the sub-item values.
        Dim string_x As String
        If item_x.SubItems.Count <= m_ColumnNumber Then
            string_x = ""
        Else
            string_x = item_x.SubItems(m_ColumnNumber).Text
        End If

        Dim string_y As String
        If item_y.SubItems.Count <= m_ColumnNumber Then
            string_y = ""
        Else
            string_y = item_y.SubItems(m_ColumnNumber).Text
        End If


        If m_SortOrder = SortOrder.Ascending Then
            If IsNumeric(string_x) And IsNumeric(string_y) Then
                Return Val(string_x).CompareTo(Val(string_y))
            Else
                Return String.Compare(string_x, string_y)
            End If
        Else
            If IsNumeric(string_x) And IsNumeric(string_y) Then
                Return Val(string_y).CompareTo(Val(string_x))
            Else
                Return String.Compare(string_y, string_x)
            End If
        End If
    End Function
End Class