Imports CrystalDecisions.[Shared]
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO

Public Class Classutilidad
    Public Function BuscarEmple(ByRef objtable As DataTable, ByRef objform As Windows.Forms.Form, ByVal valor As String, ByVal Fields As String) As Long
        Dim Odataview As DataView
        Dim NumReg As Long
        Dim cm As Windows.Forms.CurrencyManager
        Dim DtTable As New DataTable
        Dim objv As Object
        Try

            Odataview = objtable.DefaultView
            Odataview.Sort = Fields
            DtTable = objtable 'le asigno el Dataset a un datatable 
            cm = CType(objform.BindingContext(DtTable), Windows.Forms.CurrencyManager)
            objv = Val(valor)
            NumReg = Odataview.Find(objv)
            If (NumReg > Odataview.Table.Rows.Count Or NumReg < 0) Then
                NumReg = -1
            Else
                cm.Position = NumReg
            End If
            BuscarEmple = NumReg
        Catch ex As Exception
            NumReg = -1
        Finally

        End Try
    End Function
    Public Function ExportToPDF(ByVal rpt As ReportDocument, ByVal NombreArchivo As String, ByVal obdataset As DataSet, Optional ByVal strPara As String = "", Optional ByVal StrValPara As String = "", Optional ByVal strFormula As String = "", Optional ByVal strValFormula As String = "") As String
        Dim vFileName As String
        Dim diskOpts As New DiskFileDestinationOptions
        Dim envstring As String
        Dim arrFields(), arrvalores() As String
        Dim arrFieldsF(), arrvaloresF() As String
        Dim strField As String
        Dim Paramet As New CrystalDecisions.Shared.ParameterValues
        Dim RepValue As New CrystalDecisions.Shared.ParameterDiscreteValue
        Dim crFormulaTextField1 As CrystalDecisions.CrystalReports.Engine.FormulaFieldDefinition
        Dim i As Integer = 0
        Try
            If strPara.Length > 0 Then
                arrFields = strPara.Split("|")
                arrvalores = StrValPara.Split("|")
                For Each strField In arrFields
                    RepValue.Value = arrvalores(i)
                    Paramet.Add(RepValue)
                    rpt.DataDefinition.ParameterFields(strField).ApplyCurrentValues(Paramet)
                    i += 1
                Next
            End If
            If strValFormula.Length > 0 Then
                i = 0
                arrFieldsF = strFormula.Split("|")
                arrvaloresF = strValFormula.Split("|")
                For Each strField In arrFieldsF
                    crFormulaTextField1 = rpt.DataDefinition.FormulaFields(strField)
                    crFormulaTextField1.Text = Chr(34) & arrvaloresF(i) & Chr(34)
                    i += 1
                Next
            End If
            
            If rpt.SummaryInfo.ReportAuthor Is Nothing Then
                rpt.SetDataSource(obdataset)
            Else
                rpt.SetDataSource(obdataset.Tables(rpt.SummaryInfo.ReportAuthor))
                If rpt.Subreports.Count > 0 Then
                    For Each subRpt As CrystalDecisions.CrystalReports.Engine.ReportDocument In rpt.Subreports
                        If rpt.Database.Tables(0).Name = subRpt.Database.Tables(0).Name Then
                            subRpt.SetDataSource(obdataset.Tables(rpt.SummaryInfo.ReportAuthor))
                        End If
                    Next
                End If
            End If

            With rpt.ExportOptions
                .ExportDestinationType = ExportDestinationType.DiskFile
                .ExportFormatType = ExportFormatType.PortableDocFormat
            End With
            envstring = Environ("TEMP")
            vFileName = envstring & "\" & NombreArchivo & ".pdf"
            If File.Exists(vFileName) Then File.Delete(vFileName)
            diskOpts.DiskFileName = vFileName
            rpt.ExportOptions.DestinationOptions = diskOpts

            rpt.Export()
        Catch ex As Exception
            Throw ex
        End Try

        Return vFileName
    End Function
    Public Function ExportToPDF(ByVal rpt As ReportDocument, ByVal obdataset As DataSet, Optional ByVal strPara As String = "", Optional ByVal StrValPara As String = "", Optional ByVal strFormula As String = "", Optional ByVal strValFormula As String = "", Optional ByVal NombreArchivo As String = "") As Stream
        Dim PDFStream As Stream
        Dim diskOpts As New DiskFileDestinationOptions
        Dim Paramet As New CrystalDecisions.Shared.ParameterValues
        Dim RepValue As New CrystalDecisions.Shared.ParameterDiscreteValue
        Dim crFormulaTextField1 As CrystalDecisions.CrystalReports.Engine.FormulaFieldDefinition
        Dim arrFields(), arrvalores() As String
        Dim arrFieldsF(), arrvaloresF() As String
        Dim strField As String
        Dim i As Integer = 0
        Try
            If strPara.Length > 0 Then
                arrFields = strPara.Split("|")
                arrvalores = StrValPara.Split("|")
                For Each strField In arrFields
                    RepValue.Value = arrvalores(i)
                    Paramet.Add(RepValue)
                    rpt.DataDefinition.ParameterFields(strField).ApplyCurrentValues(Paramet)
                    i += 1
                Next
            End If
            If strValFormula.Length > 0 Then
                i = 0
                arrFieldsF = strFormula.Split("|")
                arrvaloresF = strValFormula.Split("|")
                For Each strField In arrFieldsF
                    crFormulaTextField1 = rpt.DataDefinition.FormulaFields(strField)
                    crFormulaTextField1.Text = Chr(34) & arrvaloresF(i) & Chr(34)
                    i += 1
                Next
            End If
            'If rpt.SummaryInfo.ReportAuthor Is Nothing Then
            '    rpt.SetDataSource(obdataset)
            'Else
            '    rpt.SetDataSource(obdataset.Tables(rpt.SummaryInfo.ReportAuthor))
            'End If
            If rpt.SummaryInfo.ReportAuthor Is Nothing Then
                rpt.SetDataSource(obdataset)
            Else
                rpt.SetDataSource(obdataset.Tables(rpt.SummaryInfo.ReportAuthor))
                If rpt.Subreports.Count > 0 Then
                    For Each subRpt As CrystalDecisions.CrystalReports.Engine.ReportDocument In rpt.Subreports
                        If rpt.Database.Tables(0).Name = subRpt.Database.Tables(0).Name Then
                            subRpt.SetDataSource(obdataset.Tables(rpt.SummaryInfo.ReportAuthor))
                        End If
                    Next
                End If
            End If
            'rpt.SetDataSource(obdataset)
            If NombreArchivo = "" Then
                PDFStream = rpt.ExportToStream(ExportFormatType.PortableDocFormat)
            Else
                rpt.ExportToDisk(ExportFormatType.PortableDocFormat, NombreArchivo)
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try

        Return PDFStream
    End Function
    Public Function ExportToPDF(ByVal NombreArchivo As String, ByVal rpt As ReportDocument, ByVal obdataset As DataSet, Optional ByVal strPara As String = "", Optional ByVal StrValPara As String = "", Optional ByVal strFormula As String = "", Optional ByVal strValFormula As String = "") As FileStream
        Dim PDFStream As Stream
        Dim diskOpts As New DiskFileDestinationOptions
        Dim Paramet As New CrystalDecisions.Shared.ParameterValues
        Dim RepValue As New CrystalDecisions.Shared.ParameterDiscreteValue
        Dim crFormulaTextField1 As CrystalDecisions.CrystalReports.Engine.FormulaFieldDefinition
        Dim arrFields(), arrvalores() As String
        Dim arrFieldsF(), arrvaloresF() As String
        Dim strField As String
        Dim i As Integer = 0
        Try
            If strPara.Length > 0 Then
                arrFields = strPara.Split("|")
                arrvalores = StrValPara.Split("|")
                For Each strField In arrFields
                    RepValue.Value = arrvalores(i)
                    Paramet.Add(RepValue)
                    rpt.DataDefinition.ParameterFields(strField).ApplyCurrentValues(Paramet)
                    i += 1
                Next
            End If
            If strValFormula.Length > 0 Then
                i = 0
                arrFieldsF = strFormula.Split("|")
                arrvaloresF = strValFormula.Split("|")
                For Each strField In arrFieldsF
                    crFormulaTextField1 = rpt.DataDefinition.FormulaFields(strField)
                    crFormulaTextField1.Text = Chr(34) & arrvaloresF(i) & Chr(34)
                    i += 1
                Next
            End If

            'If rpt.SummaryInfo.ReportAuthor Is Nothing Then
            '    rpt.SetDataSource(obdataset)
            'Else
            '    rpt.SetDataSource(obdataset.Tables(rpt.SummaryInfo.ReportAuthor))
            'End If

            If rpt.SummaryInfo.ReportAuthor Is Nothing Then
                rpt.SetDataSource(obdataset)
            Else
                rpt.SetDataSource(obdataset.Tables(rpt.SummaryInfo.ReportAuthor))
                If rpt.Subreports.Count > 0 Then
                    For Each subRpt As CrystalDecisions.CrystalReports.Engine.ReportDocument In rpt.Subreports
                        If rpt.Database.Tables(0).Name = subRpt.Database.Tables(0).Name Then
                            subRpt.SetDataSource(obdataset.Tables(rpt.SummaryInfo.ReportAuthor))
                        End If
                    Next
                End If
            End If


            rpt.ExportToDisk(ExportFormatType.PortableDocFormat, NombreArchivo)
            ExportToPDF = IO.File.OpenRead(NombreArchivo)
            Return ExportToPDF
        Catch ex As Exception
            Throw ex
        End Try

        Return PDFStream
    End Function

End Class
