Public Structure ValCert
    Dim DC As String
    Dim CN As String
    Dim OU As String
    Dim O As String
    Dim street As String
    Dim L As String
    Dim ST As String
    Dim C As String
    Dim UID As String
    Dim SN As String
    Dim d2_5_4_41 As String
    Dim d2_5_4_45 As String
    Dim d2_5_4_5 As String
End Structure
Public Structure InfoCert
    Dim IDSistema As String
    Dim NoVersion As String
    Dim TipoVersion As String
    Dim ValidAfter As DateTime
    Dim ValidBefore As DateTime
    Dim NoSerie As String
    Dim RFC As String
    Dim Nombre As String
    Dim Subject As ValCert
    Dim NoSerieFIEL As String
End Structure
Public Structure Infoequipo
    Dim RFC As String
    Dim RegPatronal As String
    Dim Sistema As String
    Dim certificado As String
    Dim certificado2 As String
    Dim TipoVersion As String
    Dim NoVersion As String
    Dim IDSistema As String
    Dim TipoVersionLlave As String
End Structure
Public Structure Datos_Cert
    Dim IDSistema As String
    Dim NoVersion As String
    Dim TipoVersion As String
    Dim ValidAfter As DateTime
    Dim ValidBefore As DateTime
    Dim NoSerie As String
    Dim RFC As String
    Dim Nombre As String
    Dim NoSerieFiel As String
    Dim _1CXP As Boolean
    Dim _2CHQ As Boolean
    Dim _3CXC As Boolean
    Dim _4CXCi As Boolean
    Dim _5CXCm As Boolean
    Dim _6CxCr As Boolean
    Dim _7CTB As Boolean
    Dim _8INV As Boolean
End Structure
Public Module Cert
    Public Function GetSubjectValues(ByVal str As String) As ValCert
        Dim strValCert As String
        Dim strValores() As String
        Dim Subject As ValCert

        strValCert = X509.CertSubjectName(str, "|")
        strValores = strValCert.Split("|")
        For Each str In strValores
            With Subject
                If str.StartsWith("DC=") Then .DC = str.Replace("DC=", "")
                If str.StartsWith("CN=") Then .CN = str.Replace("CN=", "")
                If str.StartsWith("OU=") Then .OU = str.Replace("OU=", "")
                If str.StartsWith("O=") Then .O = str.Replace("O=", "")
                If str.StartsWith("STREET") Then .street = str.Replace("STREET=", "")
                If str.StartsWith("L=") Then .L = str.Replace("L=", "")
                If str.StartsWith("ST") Then .ST = str.Replace("ST=", "")
                If str.StartsWith("C=") Then .C = str.Replace("C=", "")
                If str.StartsWith("UID=") Then .UID = str.Replace("UID=", "")
                If str.StartsWith("SN=") Then .SN = str.Replace("SN=", "")
                If str.StartsWith("2.5.4.41") Then .d2_5_4_41 = str.Replace("2.5.4.41=", "")
                If str.StartsWith("2.5.4.45") Then .d2_5_4_45 = str.Replace("2.5.4.45=", "")
                If str.StartsWith("2.5.4.5") Then .d2_5_4_5 = str.Replace("2.5.4.5=", "")
            End With
        Next
        Return Subject
    End Function
    Public Function HexAsStringToCharactersAsString(ByVal HexString As String) As String
        'we`re assuming HexString passed is formatted as 2 chars for each individual Hex value
        'ie A = 0A, B=0B
        Dim UB As Integer = HexString.Length - 1
        Dim SB As New System.Text.StringBuilder
        For Idx As Integer = 0 To UB Step 2
            SB.Append(ChrW(System.Convert.ToInt32(HexString.Chars(Idx) & HexString.Chars(Idx + 1), 16)))
        Next
        Return SB.ToString
    End Function

End Module
Public Class ClassCandado
    Public Function mostrarcandado(ByVal coneccion As OleDb.OleDbConnection, ByVal Empresa As Long, ByVal aplicacion As String) As Boolean
        Dim FrmCan As New Frmcandado
        CanLla.correcto = False
        FrmCan.conectar = coneccion
        FrmCan.Empresa = Empresa
        FrmCan.Aplicac = aplicacion
        CanLla.correcto = FrmCan.Verificar()
        If CanLla.correcto = False Then
            FrmCan.ShowDialog()
        End If
        mostrarcandado = CanLla.correcto
    End Function
    Public Function mostrarcandado(ByVal coneccion As SqlClient.SqlConnection, ByVal Empresa As Long, ByVal aplicacion As String) As Boolean
        Dim FrmCan As New Frmcandado
        CanLla.correcto = False
        FrmCan.conectar(vbYes) = coneccion
        FrmCan.Empresa = Empresa
        FrmCan.Aplicac = aplicacion
        CanLla.correcto = FrmCan.Verificar(True)
        If CanLla.correcto = False Then
            FrmCan.ShowDialog()
        End If
        mostrarcandado = CanLla.correcto
    End Function
    Public Function ValidaCert(ByRef datoscert As InfoCert, ByVal strCLU As String, ByVal strFIEL As String, ByVal strNombre As String, ByVal strRFC As String, ByVal strIDSystem As String) As Boolean
        ' CLU  -  Certificado de Licencia de USO
        ' FIEL -  Firma Electronica o Certificado de Sellos Digitales

        ' Certificado Valido
        If X509.CertIsValidNow(strCLU) Then
            datoscert.NoSerie = HexAsStringToCharactersAsString(X509.CertSerialNumber(strCLU))
        Else
            MsgBox("El certificado no es válido" & vbCrLf & "Solicite un nuevo certificado de licencia de uso del sistema", MsgBoxStyle.Exclamation, "AVISO")
            Exit Function
        End If

        ' FIEL o CSD o Certificado 2 Validacion
        Dim Fiel As InfoCert
        If My.Computer.FileSystem.FileExists(strFIEL) Then
            Fiel.Subject = GetSubjectValues(strFIEL)
            If strCLU.Contains("&") Then
                Fiel.RFC = HexAsStringToCharactersAsString(Fiel.Subject.d2_5_4_45.Trim.Replace(" ", ""))
            Else
                Fiel.RFC = Fiel.Subject.d2_5_4_45
            End If
            If InStr(Fiel.RFC, "/") > 0 Then
                Fiel.RFC = Fiel.RFC.Substring(0, InStr(Fiel.RFC, "/") - 1).Trim
            Else
                Fiel.RFC = Fiel.RFC
            End If
            Fiel.Nombre = Fiel.Subject.O
            Fiel.NoSerie = HexAsStringToCharactersAsString(X509.CertSerialNumber(strFIEL))
        Else
            Fiel.RFC = ""
            Fiel.NoSerie = 0
        End If
        ' CERTIFICADO VALIDACION
        Dim qryCert As String
        qryCert = CryptoSysPKI.X509.CertSubjectName(strCLU, "|")

        Dim Datos() As String
        'Dim DatosCert As InfoCert = Nothing
        Datos = qryCert.Split("|")
        For Each Str As String In Datos
            datoscert.Subject = GetSubjectValues(strCLU)
            ' RFC
            datoscert.RFC = datoscert.Subject.O
            If strCLU.Contains("&") Then
                datoscert.RFC = HexAsStringToCharactersAsString(datoscert.RFC.Trim.Replace(" ", ""))
            End If
            ' Nombre o Razon Social
            datoscert.Nombre = datoscert.Subject.CN
            ' NoSerie FIEL
            datoscert.NoSerieFIEL = Fiel.NoSerie

            If Str.StartsWith("OU=") Then
                Dim DatosVer As String() = Str.Split(",")
                ' 1. Verifica IDSystem
                datoscert.IDSistema = DatosVer(0).Replace("OU=", "").Replace(";", "")
                ' 2. Verifica NoVersion
                datoscert.NoVersion = DatosVer(1).Replace("OU=", "").Replace(";", "")
                ' 3. Verifica TipoVersion
                datoscert.TipoVersion = DatosVer(2).Replace("OU=", "").Replace(";", "")

                Dim b() As Byte
                Dim encoding As New System.Text.UTF8Encoding()
                b = encoding.GetBytes(strRFC)

                Dim vALORfINAL As Integer = 0
                Dim i As Byte
                For Each i In b
                    Dim ValTotal As Integer
                    ValTotal = i
                    vALORfINAL += ValTotal
                Next
                vALORfINAL += datoscert.TipoVersion

                datoscert.TipoVersion = ""
                For j As Single = 0 To vALORfINAL.ToString.Length - 1 Step 2
                    Dim hexVal As String
                    Dim ValTotal As Integer
                    hexVal = vALORfINAL.ToString.Substring(j, 2)
                    hexVal = Convert.ToChar(CInt(hexVal))
                    datoscert.TipoVersion &= hexVal
                Next
            ElseIf Str.StartsWith("O=") Then
                datoscert.RFC = Str.Replace("O=", "").Replace(";", "")
            End If
        Next
        ' Verificar el IDSYSTEM
        If Not IsNothing(datoscert) Then
            Dim strTipoVersion As String = Convert.ToString(CInt(datoscert.TipoVersion), 2)
            Dim Version() As Char
            Dim certif As Datos_Cert
            strTipoVersion = strTipoVersion.PadLeft(8, "0")
            Version = strTipoVersion.ToCharArray
            With certif
                ._8INV = IIf(Version(0) = "0", False, True)
                ._7CTB = IIf(Version(1) = "0", False, True)
                ._6CxCr = IIf(Version(2) = "0", False, True)
                ._5CXCm = IIf(Version(3) = "0", False, True)
                ._4CXCi = IIf(Version(4) = "0", False, True)
                ._3CXC = IIf(Version(5) = "0", False, True)
                ._2CHQ = IIf(Version(6) = "0", False, True)
                ._1CXP = IIf(Version(7) = "0", False, True)
            End With

            If strIDSystem <> datoscert.IDSistema Then
                MsgBox("El certificado no es correcto." & vbCrLf & "Indique el certificado de licencia de uso del sistema", MsgBoxStyle.Exclamation, "AVISO")
                Exit Function
            Else
                If certif._3CXC Or certif._4CXCi Or certif._5CXCm Or certif._6CxCr Then
                    If My.Computer.FileSystem.FileExists(strFIEL) Then
                        If Not (strRFC = datoscert.RFC And datoscert.RFC = Fiel.RFC) Then
                            MsgBox("El RFC del certificado de licencia de uso, no coincide con el de la empresa registrada." & vbCrLf & "Indique el certificado de licencia de uso del sistema para: " & strRFC.ToString, MsgBoxStyle.Exclamation, "AVISO")
                            Exit Function
                            'ElseIf Not (strNombre = DatosCert.Nombre And DatosCert.Nombre = Fiel.Nombre) Then
                            '    MsgBox("El Nombre o Razon Social del certificado de licencia de uso no coincide con la empresa registrada." & vbCrLf & "Indique el certificado de licencia de uso correcto para: " & strNombre.ToString, MsgBoxStyle.Exclamation, "AVISO")
                            '    Exit Function
                        Else
                            Return True
                        End If

                    Else
                        MsgBox("El certificado adicional no es accesible" & vbCrLf & "Indique el certificado de identificación adicional del sistema" & strFIEL, MsgBoxStyle.Exclamation, "AVISO")
                    End If
                Else
                    Return True
                End If
            End If
        Else
            Return False
        End If
    End Function
    Public Function ValidaCert(ByRef datoscert As InfoCert, ByVal strCLU As String, ByVal strNombre As String, ByVal strRFC As String, ByVal strIDSystem As String) As Boolean
        ' CLU  -  Certificado de Licencia de USO
        ' FIEL -  Firma Electronica o Certificado de Sellos Digitales

        ' Certificado Valido
        If X509.CertIsValidNow(strCLU) Then

        Else
            MsgBox("El certificado no es válido" & vbCrLf & "Solicite un nuevo certificado de licencia de uso del sistema", MsgBoxStyle.Exclamation, "AVISO")
            Exit Function
        End If


        ' CERTIFICADO VALIDACION
        Dim qryCert As String
        qryCert = CryptoSysPKI.X509.CertSubjectName(strCLU, "|")

        Dim Datos() As String
        'Dim DatosCert As InfoCert = Nothing
        Datos = qryCert.Split("|")
        For Each Str As String In Datos
            datoscert.Subject = GetSubjectValues(strCLU)
            ' RFC
            datoscert.RFC = datoscert.Subject.O
            ' Nombre o Razon Social
            datoscert.Nombre = datoscert.Subject.CN

            If Str.StartsWith("OU=") Then
                Dim DatosVer As String() = Str.Split(",")
                ' 1. Verifica IDSystem
                datoscert.IDSistema = DatosVer(0).Replace("OU=", "").Replace(";", "")
                ' 2. Verifica NoVersion
                datoscert.NoVersion = DatosVer(1).Replace("OU=", "").Replace(";", "")
                ' 3. Verifica TipoVersion
                datoscert.TipoVersion = DatosVer(2).Replace("OU=", "").Replace(";", "")

                Dim b() As Byte
                Dim encoding As New System.Text.UTF8Encoding()
                b = encoding.GetBytes(strRFC)

                Dim vALORfINAL As Integer = 0
                Dim i As Byte
                For Each i In b
                    Dim ValTotal As Integer
                    ValTotal = i
                    vALORfINAL += ValTotal
                Next
                vALORfINAL += datoscert.TipoVersion

                datoscert.TipoVersion = ""
                For j As Single = 0 To vALORfINAL.ToString.Length - 1 Step 2
                    Dim hexVal As String
                    Dim ValTotal As Integer
                    hexVal = vALORfINAL.ToString.Substring(j, 2)
                    hexVal = Convert.ToChar(CInt(hexVal))
                    datoscert.TipoVersion &= hexVal
                Next
            ElseIf Str.StartsWith("O=") Then
                datoscert.RFC = Str.Replace("O=", "").Replace(";", "")
            End If
        Next
        ' Verificar el IDSYSTEM
        If Not IsNothing(datoscert) Then
            Dim strTipoVersion As String = Convert.ToString(CInt(datoscert.TipoVersion), 2)
            Dim Version() As Char
            Dim certif As Datos_Cert
            strTipoVersion = strTipoVersion.PadLeft(8, "0")
            Version = strTipoVersion.ToCharArray
            With certif
                ._8INV = IIf(Version(0) = "0", False, True)
                ._7CTB = IIf(Version(1) = "0", False, True)
                ._6CxCr = IIf(Version(2) = "0", False, True)
                ._5CXCm = IIf(Version(3) = "0", False, True)
                ._4CXCi = IIf(Version(4) = "0", False, True)
                ._3CXC = IIf(Version(5) = "0", False, True)
                ._2CHQ = IIf(Version(6) = "0", False, True)
                ._1CXP = IIf(Version(7) = "0", False, True)
            End With

            If strIDSystem <> datoscert.IDSistema Then
                MsgBox("El certificado no es correcto." & vbCrLf & "Indique el certificado de licencia de uso del sistema", MsgBoxStyle.Exclamation, "AVISO")
                Exit Function
            Else
                Return True
            End If
        Else
            Return False
        End If
    End Function

    Public Function ValidaCertExpoAMMAC(ByRef datoscert As InfoCert, ByVal strCLU As String, ByVal strNombre As String, ByVal strRFC As String, ByVal strIDSystem As String) As Boolean
        ' CLU  -  Certificado de Licencia de USO
        ' FIEL -  Firma Electronica o Certificado de Sellos Digitales

        ' Certificado Valido
        If X509.CertIsValidNow(strCLU) Then
            datoscert.NoSerie = HexAsStringToCharactersAsString(X509.CertSerialNumber(strCLU))
        Else
            MsgBox("El certificado no es válido" & vbCrLf & "Solicite un nuevo certificado de licencia de uso del sistema", MsgBoxStyle.Exclamation, "AVISO")
            Exit Function
        End If

        ' FIEL o CSD o Certificado 2 Validacion
        ' ''Dim Fiel As InfoCert
        ' ''If My.Computer.FileSystem.FileExists(strFIEL) Then
        ' ''    Fiel.Subject = GetSubjectValues(strFIEL)
        ' ''    If strCLU.Contains("&") Then
        ' ''        Fiel.RFC = HexAsStringToCharactersAsString(Fiel.Subject.d2_5_4_45.Trim.Replace(" ", ""))
        ' ''    Else
        ' ''        Fiel.RFC = Fiel.Subject.d2_5_4_45
        ' ''    End If
        ' ''    If InStr(Fiel.RFC, "/") > 0 Then
        ' ''        Fiel.RFC = Fiel.RFC.Substring(0, InStr(Fiel.RFC, "/") - 1).Trim
        ' ''    Else
        ' ''        Fiel.RFC = Fiel.RFC
        ' ''    End If
        ' ''    Fiel.Nombre = Fiel.Subject.O
        ' ''    Fiel.NoSerie = HexAsStringToCharactersAsString(X509.CertSerialNumber(strFIEL))
        ' ''Else
        ' ''    Fiel.RFC = ""
        ' ''    Fiel.NoSerie = 0
        ' ''End If
        ' CERTIFICADO VALIDACION
        Dim qryCert As String
        qryCert = CryptoSysPKI.X509.CertSubjectName(strCLU, "|")

        Dim Datos() As String
        'Dim DatosCert As InfoCert = Nothing
        Datos = qryCert.Split("|")
        For Each Str As String In Datos
            datoscert.Subject = GetSubjectValues(strCLU)
            ' RFC
            datoscert.RFC = datoscert.Subject.O
            If strCLU.Contains("&") Then
                datoscert.RFC = HexAsStringToCharactersAsString(datoscert.RFC.Trim.Replace(" ", ""))
            End If
            ' Nombre o Razon Social
            datoscert.Nombre = datoscert.Subject.CN
            ' NoSerie FIEL
            ' ''datoscert.NoSerieFIEL = Fiel.NoSerie

            If Str.StartsWith("OU=") Then
                Dim DatosVer As String() = Str.Split(",")
                ' 1. Verifica IDSystem
                datoscert.IDSistema = DatosVer(0).Replace("OU=", "").Replace(";", "")
                ' 2. Verifica NoVersion
                datoscert.NoVersion = DatosVer(1).Replace("OU=", "").Replace(";", "")
                ' 3. Verifica TipoVersion
                datoscert.TipoVersion = DatosVer(2).Replace("OU=", "").Replace(";", "")

                Dim b() As Byte
                Dim encoding As New System.Text.UTF8Encoding()
                b = encoding.GetBytes(strRFC)

                Dim vALORfINAL As Integer = 0
                Dim i As Byte
                For Each i In b
                    Dim ValTotal As Integer
                    ValTotal = i
                    vALORfINAL += ValTotal
                Next
                vALORfINAL += datoscert.TipoVersion

                datoscert.TipoVersion = ""
                For j As Single = 0 To vALORfINAL.ToString.Length - 1 Step 2
                    Dim hexVal As String
                    Dim ValTotal As Integer
                    hexVal = vALORfINAL.ToString.Substring(j, 2)
                    hexVal = Convert.ToChar(CInt(hexVal))
                    datoscert.TipoVersion &= hexVal
                Next
            ElseIf Str.StartsWith("O=") Then
                datoscert.RFC = Str.Replace("O=", "").Replace(";", "")
            End If
        Next
        ' Verificar el IDSYSTEM
        If Not IsNothing(datoscert) Then
            Dim strTipoVersion As String = Convert.ToString(CInt(datoscert.TipoVersion), 2)
            Dim Version() As Char
            Dim certif As Datos_Cert
            strTipoVersion = strTipoVersion.PadLeft(8, "0")
            Version = strTipoVersion.ToCharArray
            With certif
                ._8INV = IIf(Version(0) = "0", False, True)
                ._7CTB = IIf(Version(1) = "0", False, True)
                ._6CxCr = IIf(Version(2) = "0", False, True)
                ._5CXCm = IIf(Version(3) = "0", False, True)
                ._4CXCi = IIf(Version(4) = "0", False, True)
                ._3CXC = IIf(Version(5) = "0", False, True)
                ._2CHQ = IIf(Version(6) = "0", False, True)
                ._1CXP = IIf(Version(7) = "0", False, True)
            End With

            If strIDSystem <> datoscert.IDSistema Then
                MsgBox("El certificado no es correcto." & vbCrLf & "Indique el certificado de licencia de uso del sistema", MsgBoxStyle.Exclamation, "AVISO")
                Exit Function
            Else
                If certif._3CXC Or certif._4CXCi Or certif._5CXCm Or certif._6CxCr Then
                    ' ''If My.Computer.FileSystem.FileExists(strFIEL) Then
                    ' ''If Not (strRFC = datoscert.RFC And datoscert.RFC = Fiel.RFC) Then
                    If Not (strRFC = datoscert.RFC) Then
                        MsgBox("El RFC del certificado de licencia de uso, no coincide con el de la empresa registrada." & vbCrLf & "Indique el certificado de licencia de uso del sistema para: " & strRFC.ToString, MsgBoxStyle.Exclamation, "AVISO")
                        Exit Function
                        'ElseIf Not (strNombre = DatosCert.Nombre And DatosCert.Nombre = Fiel.Nombre) Then
                        '    MsgBox("El Nombre o Razon Social del certificado de licencia de uso no coincide con la empresa registrada." & vbCrLf & "Indique el certificado de licencia de uso correcto para: " & strNombre.ToString, MsgBoxStyle.Exclamation, "AVISO")
                        '    Exit Function
                    Else
                        Return True
                    End If

                    ' ''Else
                    ' ''    MsgBox("El certificado adicional no es accesible" & vbCrLf & "Indique el certificado de identificación adicional del sistema" & strFIEL, MsgBoxStyle.Exclamation, "AVISO")
                    ' ''End If
                Else
                    Return True
                End If
            End If
        Else
            Return False
        End If
    End Function
End Class
