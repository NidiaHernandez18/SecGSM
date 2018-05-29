Imports ICSharpCode.SharpZipLib

Public Class clsWs
    Private gral As New wsLocalGral.gralService
    Private srv As New wsLocalSrv.srvService
    Private host As String
    Private LocalHostaddress As String
    Public Structure LoginTypeResult
        Dim ClienteOK As Boolean
        Dim CertOK As Boolean
        Dim MustContact As Boolean
        Dim Msgs() As String
        Dim WsResponseCli As String
        Dim WsResponseCert As String
        Dim CLUKindOfAccess As String
        Dim WsError As String
    End Structure
    Public Structure KeyTypeResult
        Dim llaveOK As String
        Dim WsResponse As String
        Dim llave As String
        Dim Msgs() As String
        Dim WsError As String
    End Structure
    Public Structure SignTypeResult
        '$ClienteOK . "|" . $MustContact . "|" . $CertificadoOK . "|"  . $TimbresOK . "|" . $WsResponse . "|" . $WsResponseCert . "|" . $wsResponseTimbres
        Dim ClienteOK As Boolean
        Dim CertOK As Boolean
        Dim TimbresOK As Boolean
        Dim MustContact As Boolean
        Dim WsResponseCli As String
        Dim WsResponseCert As String
        Dim WsResponseTimbres As String
        Dim WsError As String
    End Structure
    Public Structure RequestCLUTypeResult
        Dim ClienteOK As Boolean
        Dim CertOK As Boolean
        Dim WsResponseCert As String
        Dim wsError As Boolean
        Dim IDRequest As String
        Dim EstadoAut As String
        Dim CLU() As Byte
        Dim NoSerieCLU As String
        Dim RFC As String
    End Structure
    '$IO,$IP,$TC,$IET,
    Public Enum TypeIO
        I
        O
    End Enum
    Public Enum TypeIP
        I
        P
    End Enum
    Public Enum TypeTC
        T
        C
    End Enum
    Public Enum TypeIET
        I
        E
        T
    End Enum
    Public Function fxLogin(ByVal RFC As String, ByVal NoCertificado As String, ByVal Sistema As String, ByVal Empresa As String, ByVal Sucursal As String, ByVal Usuario As String) As LoginTypeResult
        'list($Origen,$RFC, $NoCertificado, $Sistema, $Empresa, $Sucursal, $Usuario) = explode('|',$Info);
        Dim strResult As String
        Dim strParametro As String
        Dim AResult() As String
        Dim result As New LoginTypeResult
        Dim separator() As Char = {"|"}
        Try
            strParametro = host & "(" & LocalHostaddress & ")" & "|" & RFC & "|" & NoCertificado & "|" & Sistema & "|" & Empresa & "|" & Sucursal & "|" & Usuario
            strResult = gral.fxLogin(strParametro)
            AResult = strResult.Split(separator, 7)
            'Return $ClienteOK . "|" . $MustContact . "|" . $CertificadoOK . "|" . $WsResponse . "|" . $WsResponseCert . "|" . $Mensajes ;
            result.ClienteOK = IIf(AResult(0) = "SI", True, False)
            result.MustContact = IIf(AResult(1) = "SI", True, False)
            result.CertOK = IIf(AResult(2) = "SI", True, False)
            result.WsResponseCli = AResult(3)
            result.WsResponseCert = AResult(4)
            result.CLUKindOfAccess = AResult(5)
            result.Msgs = AResult(6).Split("|")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "AVISO")
            result.CertOK = False
            result.ClienteOK = False
            result.MustContact = True
            result.Msgs = {"Error Grave"}
            result.WsError = True
        End Try
        Return result
    End Function
    Public Function Sign_Cancel(ByVal RFC As String, ByVal RFCReceptor As String, ByVal SerieCLU As String, ByVal FechaCFDI As DateTime, ByVal Serie As String, ByVal Folio As String, ByVal UUID As String, _
                                ByVal Importe As Decimal, ByVal IVA As Decimal, ByVal RetIVA As Decimal, ByVal RetISR As Decimal, ByVal Neto As Decimal, ByVal FechaTimbrado As DateTime, ByVal IO As TypeIO, _
                                ByVal IP As TypeIP, ByVal TC As TypeTC, ByVal IET As TypeIET, ByVal Sistema As String, ByVal Empresa As String, ByVal Sucursal As String, ByVal Usuario As String, ByVal strXML As String)
        'list($Origen,$RFC,$Receptor,$NoCertificado,$FechaCFDI,$Serie, $Folio,$UUID,$Importe,$IVA,$RetIVA,$RetISR,$Neto,$FechaTimbrado,$IO,$IP,$TC,$IET, $Sistema, $Empresa, $Sucursal, $Usuario, $XML) = explode('|',$Info);
        Dim strResult As String
        Dim strParametro As String
        Dim AResult() As String
        Dim result As New SignTypeResult
        Dim separator() As Char = {"|"}
        Try
            strParametro = host & "(" & LocalHostaddress & ")" & "|" & RFC & "|" & RFCReceptor & "|" & SerieCLU & "|" & FechaCFDI.ToString("yyyy/MM/dd HH:mm:ss") & "|" & Serie & "|" & Folio & "|" & UUID & "|" & Importe & "|" & IVA & "|" & RetIVA & "|" & RetISR & "|" & Neto & "|" & FechaTimbrado.ToString("yyyy/MM/dd HH:mm:ss") & "|" & IO.ToString & "|" & IP.ToString & "|" & TC.ToString & "|" & IET.ToString & "|" & Sistema & "|" & Empresa & "|" & Sucursal & "|" & Usuario & "|" & strXML
            strResult = gral.Sign_Cancel(strParametro)
            '$ClienteOK . "|" . $MustContact . "|" . $CertificadoOK . "|"  . $TimbresOK . "|" . $WsResponse . "|" . $WsResponseCert . "|" . $wsResponseTimbres 
            AResult = strResult.Split(separator, 7)
            result.ClienteOK = IIf(AResult(0) = "SI", True, False)
            result.MustContact = IIf(AResult(1) = "SI", True, False)
            result.CertOK = IIf(AResult(2) = "SI", True, False)
            result.TimbresOK = IIf(AResult(3) = "SI", True, False)
            result.WsResponseCli = AResult(4)
            result.WsResponseCert = AResult(5)
            result.WsResponseTimbres = AResult(6)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "AVISO")
            result.CertOK = False
            result.ClienteOK = False
            result.MustContact = True
            result.TimbresOK = False
            result.WsError = True
        End Try
        Return result
    End Function
    Public Function UpdateCliente(ByVal NED As String, ByVal IDCliente As Integer, ByVal Nombre As String, ByVal RFC As String, ByVal ISOK As Boolean, ByVal MustContact As Boolean, ByVal Observaciones As String) As Integer
        'list($Origen,$NED,$IDCliente,$Nombre,$RFC,$ISOK, $MustContact, $Observaciones)= explode('|',$Info);
        Dim strResult As Integer
        Dim strParametro As String
        Dim separator() As Char = {"|"}
        Try
            strParametro = host & "(" & LocalHostaddress & ")" & "|" & NED & "|" & IDCliente & "|" & Nombre & "|" & RFC & "|" & IIf(ISOK, 1, 0) & "|" & IIf(MustContact, 1, 0) & "|" & Observaciones
            strResult = srv.UpdateCliente(strParametro)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "AVISO")
            strResult = -1
        End Try
        Return strResult
    End Function
    Public Function UpdateCLUS(ByVal NED As String, ByVal IDCLU As Integer, ByVal NoSerie As String, ByVal IDCliente As Integer, ByVal RFC As String, ByVal Sistema As String, ByVal Modulos As String, ByVal ValidoDesde As DateTime, ByVal ValidoHasta As DateTime, ByVal CanAccess As Boolean, ByVal CanSign As Boolean, ByVal CanCancel As Boolean, ByVal KindOFAccess As String, ByVal Estatus As Boolean, ByVal Observaciones As String) As Integer
        ' list($Origen,$NED,$IDCLU,$NoSerie,$IDCliente,$RFC,$Sistema,$Modulos,$ValidoDesde,$ValidoHasta,$CanAccess,$CanSign,$CanCancel,$KindOfAccess,$Estatus,$Observaciones)= explode('|',$Info);
        Dim strResult As Integer
        Dim strParametro As String
        Dim separator() As Char = {"|"}
        Try
            strParametro = host & "(" & LocalHostaddress & ")" & "|" & NED & "|" & IDCLU & "|" & NoSerie & "|" & IDCliente & "|" & RFC & "|" & Sistema & "|" & Modulos & "|" & ValidoDesde.ToString("yyyy/MM/dd HH:mm:ss") & "|" & ValidoHasta.ToString("yyyy/MM/dd HH:mm:ss") & "|" & IIf(CanAccess, 1, 0) & "|" & IIf(CanSign, 1, 0) & "|" & IIf(CanCancel, 1, 0) & "|" & KindOFAccess & "|" & IIf(Estatus, 1, 0) & "|" & Observaciones
            strResult = srv.UpdateCLUS(strParametro, Nothing, Nothing)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "AVISO")
            strResult = -1
        End Try
        Return strResult
    End Function
    Public Function UpdateMsgs(ByVal NED As String, ByVal IDMsgs As Integer, ByVal IDCliente As Integer, ByVal Mensaje As String, ByVal ValidoDesde As DateTime, ByVal ValidoHasta As DateTime) As Integer
        ' list($Origen,$NED,$IDMsgs,$IDCliente,$Mensaje,$ValidoDesde, $ValidoHasta)= explode('|',$Info);
        Dim strResult As Integer
        Dim strParametro As String
        Dim separator() As Char = {"|"}
        Try
            strParametro = host & "(" & LocalHostaddress & ")" & "|" & NED & "|" & IDMsgs & "|" & IDCliente & "|" & Mensaje & "|" & ValidoDesde.ToString("yyyy/MM/dd HH:mm:ss") & "|" & ValidoHasta.ToString("yyyy/MM/dd HH:mm:ss")
            strResult = srv.UpdateMsgs(strParametro)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "AVISO")
            strResult = -1
        End Try
        Return strResult
    End Function
    Public Function UpdateCatTimbres(ByVal NED As String, ByVal IDCatTimbre As Integer, ByVal IDCliente As Integer, ByVal RFC As String, ByVal ContratadoEl As DateTime, ByVal TotalTimbres As Integer, ByVal Estatus As Boolean, ByVal Observaciones As String) As Integer
        'list($Origen,$NED,$IDCatTimbre,$IDCliente,$RFC,$ContratadoEl,$TotalTimbres,$Estatus,$Observaciones)= explode('|',$Info);
        Dim strResult As Integer
        Dim strParametro As String
        Dim separator() As Char = {"|"}
        Try
            strParametro = host & "(" & LocalHostaddress & ")" & "|" & NED & "|" & IDCatTimbre & "|" & IDCliente & "|" & RFC & "|" & ContratadoEl.ToString("yyyy/MM/dd HH:mm:ss") & "|" & TotalTimbres & "|" & IIf(Estatus, 1, 0) & "|" & Observaciones
            strResult = srv.UpdateCatTimbres(strParametro)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "AVISO")
            strResult = -1
        End Try
        Return strResult
    End Function
    Public Function GetInfo(ByVal tabla As String, ByVal Filtros As String) As String
        ' list($Origen,$tabla,$Filtros) = explode ("|",$Info);
        Dim strResult As String
        Dim strParametro As String
        Dim separator() As Char = {"|"}
        Try
            strParametro = host & "(" & LocalHostaddress & ")" & "|" & tabla & "|" & Filtros
            strResult = srv.GetInfo(strParametro)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "AVISO")
            strResult = -1
        End Try
        Return strResult
    End Function
    Public Function fxSoporte(ByVal RFC As String, ByVal NoCertificado As String, ByVal Sistema As String, ByVal Empresa As String, ByVal Sucursal As String, ByVal Usuario As String, ByVal IDTeam As String, ByVal PwdTeam As String, ByVal MensajeDeError As String) As String
        Dim strParametro As String
        Dim result As String = ""
        Dim separator() As Char = {"|"}
        Try
            strParametro = host & "(" & LocalHostaddress & ")" & "|" & RFC & "|" & NoCertificado & "|" & Sistema & "|" & Empresa & "|" & Sucursal & "|" & Usuario & "|" & IDTeam & "|" & PwdTeam & "|" & MensajeDeError
            result = gral.fxSoporte(strParametro)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "AVISO")

        End Try
        Return result
    End Function
    Public Function ByteArrayToString(ByVal ba() As Byte) As String

        Dim hex As String
        hex = BitConverter.ToString(ba)
        Return hex.Replace("-", "")
    End Function
    Public Shared Function StringToByteArray(ByVal hex As String) As Byte()
        Dim NumberChars As Integer = hex.Length / 2
        Dim bytes As Byte() = New Byte(NumberChars - 1) {}
        Using sr = New IO.StringReader(hex)
            For i As Integer = 0 To NumberChars - 1

                bytes(i) = Convert.ToByte(New String(New Char(1) {ChrW(sr.Read()), ChrW(sr.Read())}), 16)
            Next
        End Using
        Return bytes
    End Function
    Public Function fxNewLic(ByVal strParametro As String, ByVal strRFC As String, ByVal strNumero As String, ByVal strSucursal As String, ByVal CSD() As Byte) As RequestCLUTypeResult
        'list($Origen,$RFC, $NoCertificado, $Sistema, $Empresa, $Sucursal, $Usuario) = explode('|',$Info);
        Dim strResult As String
        Dim AResult() As String
        Dim result As New RequestCLUTypeResult
        Dim separator() As Char = {"|"}
        Dim vByteBuffer() As Byte = Nothing
        Dim vByteBufferPFX() As Byte = Nothing
        Try
            strResult = gral.fxNewLic(strParametro, CSD)
            AResult = strResult.Split(separator, 8)
            result.ClienteOK = IIf(AResult(0) = "SI", True, False)
            result.CertOK = IIf(AResult(1) = "SI", True, False)
            result.WsResponseCert = AResult(2).ToString

            result.IDRequest = AResult(3).ToString
            result.RFC = AResult(4).ToString
            result.EstadoAut = AResult(5).ToString
            result.NoSerieCLU = AResult(6).ToString
            If Not AResult(7).ToString = "" Then
                vByteBuffer = StringToByteArray(AResult(7).ToString)
            End If
            result.CLU = vByteBuffer
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "AVISO")
            result.CertOK = False
            result.ClienteOK = False
            'result.MustContact = True
            'result.Msgs = {"Error Grave"}
            result.wsError = True
        End Try
        Return result
    End Function
    Public Function fxCheckLic(ByVal strParametro As String, ByVal strRFC As String, ByVal strNumero As String, ByVal strSucursal As String, ByVal CSD() As Byte) As RequestCLUTypeResult
        'list($Origen,$RFC, $NoCertificado, $Sistema, $Empresa, $Sucursal, $Usuario) = explode('|',$Info);
        Dim strResult As String
        Dim AResult() As String
        Dim result As New RequestCLUTypeResult
        Dim separator() As Char = {"|"}
        Dim vByteBuffer() As Byte = Nothing
        Try
            strResult = gral.fxCheckLic(strParametro)
            AResult = strResult.Split(separator, 8)
            result.ClienteOK = IIf(AResult(0) = "SI", True, False)
            result.CertOK = IIf(AResult(1) = "SI", True, False)
            result.WsResponseCert = AResult(2).ToString

            result.IDRequest = AResult(3).ToString
            result.RFC = AResult(4).ToString
            result.EstadoAut = AResult(5).ToString
            result.NoSerieCLU = AResult(6).ToString
            If Not AResult(7).ToString = "" Then
                vByteBuffer = StringToByteArray(AResult(7).ToString)
            End If
            result.CLU = vByteBuffer
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "AVISO")
            result.CertOK = False
            result.ClienteOK = False
            'result.MustContact = True
            'result.Msgs = {"Error Grave"}
            result.wsError = True
        End Try
        Return result
    End Function

    Public Function fxCheckKey(ByVal strParametro As String) As KeyTypeResult
        'list($Origen,$IDCliente,$Empresa,$Sucursal,$RFC,$Sistema,$Equipo,$SNEquipo,$IP, $candado) = explode('|',$Info);
        Dim strResult As String
        Dim AResult() As String
        Dim result As New KeyTypeResult
        Dim separator() As Char = {"|"}
        Try
            strResult = gral.fxNewKey(strParametro)
            AResult = strResult.Split(separator, 3)
            result.llaveOK = AResult(0).ToString
            result.WsResponse = AResult(1).ToString
            result.llave = AResult(2).ToString

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "AVISO")
            result.llave = ""
            result.llaveOK = "NO"
            result.WsResponse = "ERROR"
            result.Msgs = {"Error Grave"}
            result.WsError = True
        End Try
        Return result
    End Function


 


    'Public Function fxCheckForRequestLic() As LoginTypeResult
    '    'list($Origen,$RFC, $NoCertificado, $Sistema, $Empresa, $Sucursal, $Usuario) = explode('|',$Info);
    '    Dim strResult As Object
    '    Dim AResult() As Object
    '    Dim result As New LoginTypeResult
    '    Dim separator() As Char = {"|"}
    '    Try
    '        strResult = srv.GetInfo("tblsolicituddclu|IFNULL(EstadoAut,'')=''")
    '        AResult = strResult.Split(separator, 6)
    '        'Return $ClienteOK . "|" . $MustContact . "|" . $CertificadoOK . "|" . $WsResponse . "|" . $WsResponseCert . "|" . $Mensajes ;
    '        result.ClienteOK = IIf(AResult(0) = "SI", True, False)
    '        result.MustContact = IIf(AResult(1) = "SI", True, False)
    '        result.CertOK = IIf(AResult(2) = "SI", True, False)
    '        result.WsResponseCli = AResult(3)
    '        result.WsResponseCert = AResult(4)
    '        result.Msgs = AResult(5).Split("|")
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Exclamation, "AVISO")
    '        result.CertOK = False
    '        result.ClienteOK = False
    '        result.MustContact = True
    '        result.Msgs = {"Error Grave"}
    '        result.WsError = True
    '    End Try
    '    Return result
    'End Function
    'Public Function fxSaveForRequestLic(ByVal strParametro As String, ByVal CSD() As Byte) As LoginTypeResult
    '    'list($Origen,$RFC, $NoCertificado, $Sistema, $Empresa, $Sucursal, $Usuario) = explode('|',$Info);
    '    Dim strResult As Object
    '    Dim AResult() As Object
    '    Dim result As New LoginTypeResult
    '    Dim separator() As Char = {"|"}
    '    Try
    '        strResult = gral.fxNewLic(strParametro, CSD)
    '        AResult = strResult.Split(separator, 6)
    '        'Return $ClienteOK . "|" . $MustContact . "|" . $CertificadoOK . "|" . $WsResponse . "|" . $WsResponseCert . "|" . $Mensajes ;
    '        result.ClienteOK = IIf(AResult(0) = "SI", True, False)
    '        result.MustContact = IIf(AResult(1) = "SI", True, False)
    '        result.CertOK = IIf(AResult(2) = "SI", True, False)
    '        result.WsResponseCli = AResult(3)
    '        result.WsResponseCert = AResult(4)
    '        result.Msgs = AResult(5).Split("|")
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Exclamation, "AVISO")
    '        result.CertOK = False
    '        result.ClienteOK = False
    '        result.MustContact = True
    '        result.Msgs = {"Error Grave"}
    '        result.WsError = True
    '    End Try
    '    Return result
    'End Function

    Public Sub New()
        host = System.Net.Dns.GetHostName()
        If Not IsNothing(System.Net.Dns.GetHostEntry(host)) Then
            For Each StrIp As System.Net.IPAddress In System.Net.Dns.GetHostEntry(host).AddressList
                If Not StrIp.IsIPv6LinkLocal Then
                    LocalHostaddress = StrIp.ToString
                End If
            Next

        End If
    End Sub
End Class
