Imports System.Windows.Forms
Public Class frmreportes

    Dim crformulas As CrystalDecisions.CrystalReports.Engine.FieldDefinition
    Private obDataset As DataSet
    Private StrPara As String = ""
    Private StrValue As String = ""
    Private strForm(20) As String
    Private strValueForm(20) As String
    Public namereporte(20) As CrystalDecisions.CrystalReports.Engine.ReportDocument
    Public titulorep(20) As String
    Private Reporte As CrystalDecisions.CrystalReports.Engine.ReportDocument
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents ReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer

    Public Property Datasetobj() As DataSet
        Get
            Return obDataset
        End Get
        Set(ByVal Value As DataSet)
            obDataset = Value
        End Set
    End Property

    Public Property ParaStr() As String
        Get
            Return StrPara
        End Get
        Set(ByVal Value As String)
            StrPara = Value
        End Set
    End Property
    Public Property ValueParametros() As String
        Get
            Return StrValue
        End Get
        Set(ByVal Value As String)
            StrValue = Value
        End Set
    End Property

    Public Property formulasStr() As String()
        Get
            Return strForm
        End Get
        Set(ByVal Value As String())
            strForm = Value
        End Set
    End Property
    Public Property ValueForm() As String()
        Get
            Return strValueForm
        End Get
        Set(ByVal Value As String())
            strValueForm = Value
        End Set
    End Property


    Private Sub Repo(ByVal reportename As String, ByVal rep As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal posicion As Integer)
        Dim Paramet As New CrystalDecisions.Shared.ParameterValues
        Dim RepValue As New CrystalDecisions.Shared.ParameterDiscreteValue

        Dim crFormulaTextField1 As CrystalDecisions.CrystalReports.Engine.FormulaFieldDefinition
        Dim arrFields(), arrvalores() As String
        Dim arrFieldsF(), arrvaloresF() As String
        Dim strField As String
        Dim i As Integer = 0
        Try
            Reporte = rep


            If StrPara.Length > 0 Then
                arrFields = StrPara.Split("|")
                arrvalores = StrValue.Split("|")
                For Each strField In arrFields
                    RepValue.Value = arrvalores(i)
                    Paramet.Add(RepValue)
                    Reporte.DataDefinition.ParameterFields(strField).ApplyCurrentValues(Paramet)
                    i += 1
                Next
            End If
            If strForm.Length > 0 Then
                i = 0
                arrFieldsF = strForm(posicion).Split("|")
                arrvaloresF = strValueForm(posicion).Split("|")
                For Each strField In arrFieldsF
                    crFormulaTextField1 = Reporte.DataDefinition.FormulaFields(strField)
                    crFormulaTextField1.Text = Chr(34) & arrvaloresF(i) & Chr(34)
                    i += 1
                Next
            End If
            TabPage1 = New System.Windows.Forms.TabPage
            Me.TabPage1.Text = reportename
            ReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer
            TabControl1.TabPages.Add(TabPage1)
            TabPage1.Controls.Add(ReportViewer1)

            Config_RViewer(ReportViewer1)
            If Reporte.SummaryInfo.ReportAuthor Is Nothing Then
                Reporte.SetDataSource(obDataset)
            Else
                Reporte.SetDataSource(obDataset.Tables(Reporte.SummaryInfo.ReportAuthor))
                If Reporte.Subreports.Count > 0 Then
                    For Each subRpt As CrystalDecisions.CrystalReports.Engine.ReportDocument In Reporte.Subreports
                        If Reporte.Database.Tables(0).Name = subRpt.Database.Tables(0).Name Then
                            subRpt.SetDataSource(obDataset.Tables(Reporte.SummaryInfo.ReportAuthor))
                        End If
                    Next
                End If
            End If
            ReportViewer1.ReportSource = Reporte
            ReportViewer1.Zoom(1)

        Catch Exp As Exception  'LoadSaveReportException
            MessageBox.Show(Exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Paramet = Nothing
            RepValue = Nothing
        End Try
    End Sub

    Private Sub frmreportes_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F3 Then
            Dim frm As New frmTables(Datasetobj)
            frm.Show(Me)
        ElseIf e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub


    Private Sub frmreportes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ii As Integer
        For ii = 0 To 20
            If Not IsNothing(titulorep(ii)) Then
                If Len(titulorep(ii).ToString) > 0 Then
                    Repo(titulorep(ii), namereporte(ii), ii)
                    'Else
                    '    Exit For
                End If
            End If
        Next


    End Sub
    Private Sub Config_RViewer(ByVal sender As CrystalDecisions.Windows.Forms.CrystalReportViewer)
        sender.ActiveViewIndex = -1
        sender.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        'sender.DisplayGroupTree = True
        sender.DisplayStatusBar = True
        sender.DisplayToolbar = True
        sender.Location = New System.Drawing.Point(7, 3)
        sender.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        sender.Height = TabControl1.Height - 9
        sender.Width = TabControl1.Width - 14
        sender.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        sender.ActiveViewIndex = -1
        sender.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        'sender.DisplayGroupTree = False
        sender.DisplayStatusBar = True
        sender.ShowZoomButton = True
        sender.ShowRefreshButton = True
        sender.ShowPrintButton = True
        sender.DisplayToolbar = True
        sender.ShowPageNavigateButtons = True
        sender.ShowTextSearchButton = True
        'sender.Location = New System.Drawing.Point(7, 6)

    End Sub

    Private Sub frmreportes_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        'If Not Me.WindowState = FormWindowState.Minimized Then
        '    Dim objac As Integer
        '    Dim x As Object
        '    TabControl1.Size = Me.Size
        '    Me.Refresh()
        '    For objac = 0 To TabControl1.TabCount - 1
        '        For Each x In TabControl1.TabPages(objac).Controls
        '            x.height = TabControl1.Height - (x.Top * 7)
        '            x.Width = TabControl1.Width - (x.Left * 2)
        '        Next
        '    Next
        'End If

    End Sub


    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged

    End Sub
End Class