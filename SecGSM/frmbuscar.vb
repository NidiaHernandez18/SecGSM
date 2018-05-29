Imports System.Windows.Forms
Public Class frmbuscar

    Dim buscador As DataTable
    Public tipo As Integer
    Public datos As String
    Public m_SortingColumn As ColumnHeader
    Dim ban As Boolean = True
    Dim i As Integer
    Dim columna As DataColumn
    Dim arreg(10, 1) As String
    Dim total As Integer = 0
    Dim recaux As New ClassCone
    Dim varcon2 As OleDb.OleDbConnection
    Dim varcon3 As SqlClient.SqlConnection
    Dim infobuscar2 As String


    Public Property conectar() As OleDb.OleDbConnection
        Get
            conectar = varcon2
        End Get
        Set(ByVal value As OleDb.OleDbConnection)
            varcon2 = value
        End Set
    End Property
    Public Property conectar(ByVal IsMSSQL As Boolean) As SqlClient.SqlConnection
        Get
            conectar = varcon3
        End Get
        Set(ByVal value As SqlClient.SqlConnection)
            varcon3 = value
        End Set
    End Property

    Public Property infobuscar() As String
        Get
            infobuscar = infobuscar2
        End Get
        Set(ByVal value As String)
            infobuscar2 = value
        End Set
    End Property


   
    Private Sub frmbuscar_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Dim total As Integer

        If ban Then
            buscador = New DataTable

            buscador = recaux.RecDatatable(datos, IIf(IsNothing(varcon2), varcon3, varcon2))

            ' buscador = datos
            Me.datosClientes.DataSource = buscador
            ban = False
            total = 0
            For i = 0 To 10
                arreg(i, 0) = ""
                arreg(i, 1) = ""
            Next
            total = buscador.Columns.Count
            For i = 0 To total - 1
                columna = buscador.Columns(i)
                cmbfield.Items.Add(columna.ColumnName)
                arreg(i, 0) = columna.ColumnName
                arreg(i, 1) = columna.DataType.Name.ToString
                Lstbuscar.Columns(i).Width = 100


                Lstbuscar.Columns(i).Text = columna.ColumnName
                If InStr(UCase(columna.ColumnName), "NOMBRE") > 0 Then
                    Lstbuscar.Columns(i).Width = 300

                End If
                total += 1
            Next
        End If

    End Sub

    Private Sub txtbuscar_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtbuscar.TextChanged
        buscar()
    End Sub

    Private Sub frmbuscar_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = ChrW(13) Then
            SendKeys.Send("{TAB}")
        End If

        If e.KeyChar = ChrW(27) Then
            Me.Close()
        End If
    End Sub

    Private Sub frmbuscar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ban = True
        infobuscar2 = ""
        txtbuscar.Text = ""
        buscar()
    End Sub

    Private Sub Lstbuscar_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles Lstbuscar.ColumnClick
        Dim new_sorting_column As ColumnHeader = _
        Lstbuscar.Columns(e.Column)
        Dim sort_order As System.Windows.Forms.SortOrder
        Try
            If m_SortingColumn Is Nothing Then
                sort_order = SortOrder.Ascending
            Else
                If new_sorting_column.Equals(m_SortingColumn) Then
                    If m_SortingColumn.Text.StartsWith("> ") Then
                        sort_order = SortOrder.Descending
                    Else
                        sort_order = SortOrder.Ascending
                    End If
                Else
                    sort_order = SortOrder.Ascending
                End If

                m_SortingColumn.Text = m_SortingColumn.Text.Substring(2)
            End If
            Lstbuscar.ListViewItemSorter = New ListViewComparer(e.Column, sort_order)
            m_SortingColumn = new_sorting_column
            If Lstbuscar.Sorting = SortOrder.Ascending Then
                m_SortingColumn.Text = "> " & m_SortingColumn.Text
                Lstbuscar.Sorting = SortOrder.Descending
            Else
                m_SortingColumn.Text = "< " & m_SortingColumn.Text
                Lstbuscar.Sorting = SortOrder.Ascending
            End If

            Lstbuscar.Sort()
        Catch ex As Exception
            MessageBox.Show("Error en " & ex.Message, "Error", MessageBoxButtons.OK)
        End Try

    End Sub

    Private Sub Lstbuscar_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Lstbuscar.MouseDoubleClick
        infobuscar2 = Lstbuscar.SelectedItems(0).Text
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        infobuscar2 = ""
        Me.Close()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Lstbuscar.SelectedItems.Count > 0 Then
            infobuscar2 = Lstbuscar.SelectedItems(0).Text
            Me.Close()
        Else
            Call MessageBox.Show("Seleccione un elemento de la información", "Información", MessageBoxButtons.OK, MessageBoxIcon.Stop)

        End If
    End Sub

    Private Sub buscar()
        Dim filas() As DataRow
        Dim j As Integer
        Dim jj As Integer
        Try
            If ban Then Exit Sub
            j = 0
            filas = buscador.Select(cmbfield.Text & " LIKE '%" & txtbuscar.Text & "%'")
            Me.Lstbuscar.Items.Clear()
            '   total = 2
            If filas.Length > 0 Then
                For Each dr As DataRow In filas

                    Me.Lstbuscar.Items.Add(dr(arreg(0, 0).ToString))
                    With Lstbuscar.Items(j)
                        For jj = 1 To buscador.Columns.Count - 1
                            .SubItems.Add(IIf(IsDBNull(dr(arreg(jj, 0)).ToString), "", dr(arreg(jj, 0)).ToString))

                        Next
                    End With

                    j += 1

                Next
            End If
        Catch ex As Exception
            MessageBox.Show("Error en la busqueda " & ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

 
End Class