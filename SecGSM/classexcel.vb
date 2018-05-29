Imports System.Text
Imports System.Web
Imports System.Xml
Imports System.Xml.Xsl
Imports System.IO

Public Class classexcel
    Public strHEader As String = ""
    Public strFooter As String = ""
    Public AnexaElaboro As Boolean = False
    Public NewWidthFromColumn As Int16 = 0
    Public Function DaTatabletoexcel(ByVal dtTable As DataTable, ByVal Archivo As String, ByRef rutadearchivo As String, Optional ByVal mostarcolumnas As Boolean = True, Optional ByVal style As String = "") As Boolean
        Dim strXSL As String
        Dim strXSLTempFile As String = ""
        Dim strExcelFile As String
        Dim dsDataSet As DataSet
        Dim objFsXSL As FileStream = Nothing
        Dim objstrWrtXSL As StreamWriter = Nothing
        Dim objFsXML As System.IO.FileStream = Nothing
        Dim objXmlTxtWrt As XmlTextWriter = Nothing
        Dim objStrRdr As StringReader = Nothing
        Dim objXmlTxtRdr As XmlTextReader = Nothing
        Dim objXPath As XPath.XPathDocument = Nothing
        Dim objXslTran As Xsl.XslCompiledTransform
        Dim xslRes As XmlResolver

        Try
            rutadearchivo = ""
            dsDataSet = New DataSet
            dsDataSet.Tables.Add(dtTable.Copy)
            strXSL = CreateXSL(dsDataSet.Tables(0), mostarcolumnas)
            strXSLTempFile = Environ("TEMP") & dtTable.TableName & Now.ToString("MM-dd-yy") & Now.Hour.ToString & Now.Minute.ToString _
                        & Now.Second.ToString & Now.Millisecond.ToString & ".xsl"
            objFsXSL = New FileStream(strXSLTempFile, FileMode.Create)
            objstrWrtXSL = New StreamWriter(objFsXSL)
            objstrWrtXSL.Write(strXSL)
            objstrWrtXSL.Flush()
            objstrWrtXSL.Close()

            strExcelFile = Environ("TEMP") & Archivo & Now.Hour.ToString & Now.Minute.ToString _
                        & Now.Second.ToString & Now.Millisecond.ToString & ".xls"
            objFsXML = New System.IO.FileStream(strExcelFile, System.IO.FileMode.Create)
            objXmlTxtWrt = New XmlTextWriter(objFsXML, System.Text.Encoding.Unicode)
            'Create Xpath Doc to be given as used while doing the XSL Trannsfor
            objStrRdr = New StringReader(dsDataSet.GetXml)
            objXmlTxtRdr = New XmlTextReader(objStrRdr)
            objXPath = New XPath.XPathDocument(objXmlTxtRdr)
            objXslTran = New Xsl.XslCompiledTransform
            If style = "" Then
                objXslTran.Load(strXSLTempFile)
            Else
                objXslTran.Load(style)
            End If
            objXslTran.Transform(objXPath, objXmlTxtWrt)
            rutadearchivo = strExcelFile
            Return True
        Catch exptn As Exception
            MsgBox(exptn.Message)
            Return False
        Finally
            strXSL = Nothing
            strXSLTempFile = Nothing
            dsDataSet = Nothing
            If Not objFsXSL Is Nothing Then
                objFsXSL.Close()
                objFsXSL = Nothing
            End If
            If Not objstrWrtXSL Is Nothing Then
                objstrWrtXSL.Close()
                objstrWrtXSL = Nothing
            End If
            If Not objXmlTxtWrt Is Nothing Then
                objXmlTxtWrt.Close()
                objXmlTxtWrt = Nothing
            End If
            If Not objFsXML Is Nothing Then
                objFsXML.Close()
                objFsXML = Nothing
            End If
            If Not objStrRdr Is Nothing Then
                objStrRdr.Close()
                objStrRdr = Nothing
            End If
            If Not objXmlTxtRdr Is Nothing Then
                objXmlTxtRdr.Close()
                objXmlTxtRdr = Nothing
            End If
            objXPath = Nothing
            objXslTran = Nothing
            xslRes = Nothing
        End Try

    End Function
    Private Function CreateXSL(ByRef dtTable As DataTable, ByVal blnDisplayColumnHeader As Boolean) As String
        Dim sbXSL As StringBuilder

        Try
            sbXSL = New StringBuilder

            sbXSL.Append("<xsl:stylesheet xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"" version=""1.0"">")
            sbXSL.Append("<xsl:template match=""/"">")
            sbXSL.Append("<HTML>")
            sbXSL.Append("<HEAD>")
            'sbXSL.Append("<style type=""""text/css"""">")
            'sbXSL.Append("h1 {color:red}")
            'sbXSL.Append("</style>")
            sbXSL.Append("</HEAD>")
            sbXSL.Append("<BODY>")
            'sbXSL.Append("<h4>" & strHeader & "</h4>")
            sbXSL.Append("<TABLE style=" & Chr(34) & "font-size:8.0pt" & Chr(34) & ">")
            sbXSL.Append("<TR>")
            sbXSL.Append("<Th colspan=" & Chr(34) & dtTable.Columns.Count & Chr(34) & ">")
            sbXSL.Append(strHEader)
            sbXSL.Append("</Th>")
            sbXSL.Append("</TR>")
            sbXSL.Append("<TR style=" & Chr(34) & "background-color:Silver;height:39.0pt" & Chr(34) & ">")

            If blnDisplayColumnHeader = True Then
                Dim i As Int16 = 1
                For Each dcColumn As DataColumn In dtTable.Columns
                    dcColumn.ColumnName = Replace(dcColumn.ColumnName.Replace("(", "_").Replace(")", "_").Replace("$", "").Replace("#", "Num").Replace("%", ".").Replace("/", " ").Replace(" ", "_").Replace(".", "p").Replace(",", "c").Trim(), " ", "_")
                    If NewWidthFromColumn > 0 And i >= NewWidthFromColumn Then
                        sbXSL.Append("<Th style=" & Chr(34) & "text-align:center;vertical-align:justify;font-size:8.0pt;Width:58pt" & Chr(34) & ">")
                    Else
                        sbXSL.Append("<Th style=" & Chr(34) & "font-size:8.0pt" & Chr(34) & ">")
                    End If
                    sbXSL.Append(dcColumn.ColumnName.Replace("_", " "))
                    sbXSL.Append("</Th>")
                    i += 1
                Next
            End If

            sbXSL.Append("</TR>")
            sbXSL.Append("<xsl:for-each select=""NewDataSet/" & dtTable.TableName & """>")
            sbXSL.Append("<TR>")

            For Each dcColumn As DataColumn In dtTable.Columns
                sbXSL.Append("<TD><xsl:value-of select=""")
                sbXSL.Append(dcColumn.ColumnName)
                sbXSL.Append("""/></TD>")
            Next

            sbXSL.Append("</TR>")

            sbXSL.Append("</xsl:for-each>")

            If AnexaElaboro Then
                sbXSL.Append("<tr></tr>")
                sbXSL.Append("<tr></tr>")
                sbXSL.Append("<tr></tr>")
                sbXSL.Append("<tr></tr>")
                sbXSL.Append("<th colspan=" & Chr(34) & "4" & Chr(34) & ">____________________________________________________________</th>")
                sbXSL.Append("<td></td>")
                sbXSL.Append("<td></td>")
                sbXSL.Append("<td></td>")
                sbXSL.Append("<td></td>")
                sbXSL.Append("<td></td>")
                sbXSL.Append("<td></td>")
                sbXSL.Append("<th colspan=" & Chr(34) & "8" & Chr(34) & ">____________________________________________________________</th>")
                sbXSL.Append("<td></td>")
                sbXSL.Append("<tr>")
                sbXSL.Append("<th colspan=" & Chr(34) & "4" & Chr(34) & ">ELABORO</th>")
                sbXSL.Append("<td></td>")
                sbXSL.Append("<td></td>")
                sbXSL.Append("<td></td>")
                sbXSL.Append("<td></td>")
                sbXSL.Append("<td></td>")
                sbXSL.Append("<td></td>")
                sbXSL.Append("<th colspan=" & Chr(34) & "8" & Chr(34) & ">AUTORIZO</th>")
                sbXSL.Append("<td></td>")
                sbXSL.Append("<td></td>")
                sbXSL.Append("</tr>")
            End If

            sbXSL.Append("</TABLE>")
            sbXSL.Append("</BODY>")
            sbXSL.Append("</HTML>")
            sbXSL.Append("</xsl:template>")
            sbXSL.Append("</xsl:stylesheet>")
            Return sbXSL.ToString
        Catch exptn As Exception
            Throw
        Finally
            sbXSL = Nothing
        End Try
    End Function
End Class
