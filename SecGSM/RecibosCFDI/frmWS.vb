Public Class frmWS
    Dim gral As wsLocalGral.gralService
    Dim srv As wsLocalSrv.srvService
    Private Sub frmWS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim var As Object
        srv = New wsLocalSrv.srvService
        var = srv.GetInfo("tblClientes|1=1")
        MsgBox(var)
    End Sub

    Private Sub cmd1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd1.Click
        Dim cs As New clsWs
        Dim rs As clsWs.LoginTypeResult
        rs = cs.fxLogin(p101.Text, p102.Text, p103.Text, p104.Text, p105.Text, p106.Text)
        With txtResultado
            .Clear()
            .AppendText("CertOK=" & rs.CertOK & vbCrLf)
            .AppendText("ClienteOK=" & rs.ClienteOK & vbCrLf)
            .AppendText("MustContact=" & rs.MustContact & vbCrLf)
            .AppendText("WsError=" & rs.WsError & vbCrLf)
            .AppendText("WsResponseCli=" & rs.WsResponseCli & vbCrLf)
            .AppendText("WsResponseCert=" & rs.WsResponseCert & vbCrLf)
            .AppendText("--- Mensajes ---" & vbCrLf)
            For Each strMsgs As String In rs.Msgs
                .AppendText(strMsgs & vbCrLf)
            Next
        End With
    End Sub

   
    Private Sub cmd2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd2.Click
        Dim cs As New clsWs
        Dim rs As clsWs.SignTypeResult
        rs = cs.Sign_Cancel(p201.Text, p202.Text, p203.Text, p204.Text, p205.Text, p206.Text, p207.Text, p208.Text, p209.Text, p210.Text, p211.Text, p212.Text, p213.Text, IIf(p214.Text = "O", clsWs.TypeIO.O, clsWs.TypeIO.I), IIf(p215.Text = "P", clsWs.TypeIP.P, clsWs.TypeIP.I), IIf(p216.Text = "C", clsWs.TypeTC.C, clsWs.TypeTC.T), IIf(p217.Text = "I", clsWs.TypeIET.I, IIf(p217.Text = "E", clsWs.TypeIET.E, clsWs.TypeIET.E)), p218.Text, p219.Text, p220.Text, p221.Text, p222.Text)
        With txtResultado
            .Clear()
            .AppendText("CertOK=" & rs.CertOK & vbCrLf)
            .AppendText("ClienteOK=" & rs.ClienteOK & vbCrLf)
            .AppendText("MustContact=" & rs.MustContact & vbCrLf)
            .AppendText("TimbresOK=" & rs.TimbresOK & vbCrLf)
            .AppendText("WsError=" & rs.WsError & vbCrLf)
            .AppendText("WsResponseCli=" & rs.WsResponseCli & vbCrLf)
            .AppendText("WsResponseCert=" & rs.WsResponseCert & vbCrLf)
            .AppendText("WsResponseTimbres=" & rs.WsResponseTimbres & vbCrLf)
        End With
    End Sub

    Private Sub cmd3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd3.Click
        Dim cs As New clsWs
        Dim valor As Integer
        valor = cs.UpdateCliente(p301.Text, p302.Text, p303.Text, p304.Text, p305.Text, p306.Text, p307.Text)
        txtResultado.Text = valor & " Rows affected"
    End Sub

    Private Sub cmd4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd4.Click
        Dim cs As New clsWs
        Dim valor As Integer
        valor = cs.UpdateCLUS(p401.Text, p402.Text, p403.Text, p404.Text, p405.Text, p406.Text, p407.Text, p408.Text, p409.Text, p410.Text, p411.Text, p412.Text, p413.Text, p414.Text, p415.Text)
        txtResultado.Text = valor & " Rows affected"
    End Sub

    Private Sub cmd5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd5.Click
        Dim cs As New clsWs
        Dim valor As Integer
        valor = cs.UpdateMsgs(p501.Text, p502.Text, p503.Text, p504.Text, p505.Text, p506.Text)
        txtResultado.Text = valor & " Rows affected"
    End Sub

    Private Sub cmd6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd6.Click
        Dim cs As New clsWs
        Dim valor As Integer
        valor = cs.UpdateCatTimbres(p601.Text, p602.Text, p603.Text, p604.Text, p605.Text, p606.Text, p607.Text, p608.Text)
        txtResultado.Text = valor & " Rows affected"
    End Sub

    Private Sub cmd7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd7.Click
        Dim cs As New clsWs
        Dim valor As String
        valor = cs.GetInfo(p701.Text, p702.Text)
        txtResultado.Text = valor
        Dim xd As System.Xml.XmlDocument = New System.Xml.XmlDocument
        xd.LoadXml(valor)
        Dim xmlReader As System.Xml.XmlReader = New System.Xml.XmlNodeReader(xd)
        If xd.ChildNodes.Count > 0 Then
            Dim dts As New DataSet
            dts.ReadXml(xmlReader)
            If dts.Tables.Count > 0 Then
                GridGetInfo.DataSource = dts.Tables(0)
                MsgBox("Se encontraron " & dts.Tables(0).Rows.Count & " Registros", MsgBoxStyle.Information, "AVISO")
            Else
                GridGetInfo.DataSource = Nothing
                MsgBox("No se encontraron registros", MsgBoxStyle.Information, "AVISO")
            End If
        Else
            GridGetInfo.DataSource = Nothing
            MsgBox("No se encontraron registros", MsgBoxStyle.Information, "AVISO")
        End If
    End Sub
End Class