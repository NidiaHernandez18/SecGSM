Imports CryptoSysPKI
Public Class Pk1
    Public Declare Function CNV_HexFilter Lib "diCrPKI.dll" (ByVal strOutput As String, ByVal strInput As String, ByVal nStrLen As Long) As Long
    Public Declare Function TDEA_HexMode Lib "diCrPKI.dll" (ByVal sOutput As String, ByVal sInput As String, ByVal sHexKey As String, ByVal bEncrypt As Boolean, ByVal sMode As String, ByVal sHexIV As String) As Long
    Public Function encriptarcandado(ByVal cadena As String) As String
        encriptarcandado = Encriptarpaso1(cadena)
    End Function

    Public Function encriptarllave(ByVal cadena As String) As String
        encriptarllave = Encriptarpaso2(cadena)
    End Function
    Public Function descriptarllave(ByVal cadena As String) As String
        descriptarllave = Desencriptar(cadena)
    End Function
    Public Function Encriptarpaso1(ByVal cadena As String) As String
        Encriptarpaso1 = Cadenahex(cadena)
    End Function
    Public Function Encriptarpaso2(ByVal cadena As String) As String
        Dim nRet As Long
        Dim sOutput As String
        Dim sKey As String
        sKey = Cadenahex("GRUPO SISTECOM DE MEXICO")
        sOutput = Space(cadena.Length)
        'MsgBox(sOutput)

        nRet = TDEA_HexMode(sOutput, cadena, sKey, True, "CBC", "B36B6BFB6231084E")
        ' MsgBox(nRet)

        Encriptarpaso2 = sOutput
    End Function
    Public Function Encriptar(ByVal cadena As String) As String

        cadena = Cadenahex(cadena)
        Encriptar = Encriptarpaso2(cadena)

    End Function
    Public Function Desencriptar(ByVal cadena As String) As String

        Dim nRet As Long
        Dim sOutput As String
        Dim sInput As String
        Dim sKey As String
        sKey = Cadenahex("GRUPO SISTECOM DE MEXICO")
        sInput = cadena
        sOutput = Space(sInput.Length)
        nRet = TDEA_HexMode(sOutput, sInput, sKey, False, "CBC", "B36B6BFB6231084E")
        Desencriptar = cnvHexdec(sOutput)
    End Function

    Private Function cnvHexdec(ByVal strHex As String) As String
        Dim arrayhex(65, 1) As String
        Dim posicion As Integer, i As Integer, j As Integer
        Dim valor As String
        arrayhex(0, 0) = "32" : arrayhex(0, 1) = "20"
        arrayhex(1, 0) = "33" : arrayhex(1, 1) = "21"
        arrayhex(2, 0) = "34" : arrayhex(2, 1) = "22"
        arrayhex(3, 0) = "35" : arrayhex(3, 1) = "23"
        arrayhex(4, 0) = "36" : arrayhex(4, 1) = "24"
        arrayhex(5, 0) = "37" : arrayhex(5, 1) = "25"
        arrayhex(6, 0) = "38" : arrayhex(6, 1) = "26"
        arrayhex(7, 0) = "39" : arrayhex(7, 1) = "27"
        arrayhex(8, 0) = "40" : arrayhex(8, 1) = "28"
        arrayhex(9, 0) = "41" : arrayhex(9, 1) = "29"
        arrayhex(10, 0) = "42" : arrayhex(10, 1) = "2A"
        arrayhex(11, 0) = "43" : arrayhex(11, 1) = "2B"
        arrayhex(12, 0) = "44" : arrayhex(12, 1) = "2C"
        arrayhex(13, 0) = "45" : arrayhex(13, 1) = "2D"
        arrayhex(14, 0) = "46" : arrayhex(14, 1) = "2E"
        arrayhex(15, 0) = "47" : arrayhex(15, 1) = "2F"
        arrayhex(16, 0) = "48" : arrayhex(16, 1) = "30"
        arrayhex(17, 0) = "49" : arrayhex(17, 1) = "31"
        arrayhex(18, 0) = "50" : arrayhex(18, 1) = "32"
        arrayhex(19, 0) = "51" : arrayhex(19, 1) = "33"
        arrayhex(20, 0) = "52" : arrayhex(20, 1) = "34"
        arrayhex(21, 0) = "53" : arrayhex(21, 1) = "35"
        arrayhex(22, 0) = "54" : arrayhex(22, 1) = "36"
        arrayhex(23, 0) = "55" : arrayhex(23, 1) = "37"
        arrayhex(24, 0) = "56" : arrayhex(24, 1) = "38"
        arrayhex(25, 0) = "57" : arrayhex(25, 1) = "39"
        arrayhex(26, 0) = "58" : arrayhex(26, 1) = "3A"
        arrayhex(27, 0) = "59" : arrayhex(27, 1) = "3B"
        arrayhex(28, 0) = "60" : arrayhex(28, 1) = "3C"
        arrayhex(29, 0) = "61" : arrayhex(29, 1) = "3D"
        arrayhex(30, 0) = "62" : arrayhex(30, 1) = "3E"
        arrayhex(31, 0) = "63" : arrayhex(31, 1) = "3F"


        arrayhex(32, 0) = "64" : arrayhex(32, 1) = "40"
        arrayhex(33, 0) = "65" : arrayhex(33, 1) = "41"
        arrayhex(34, 0) = "66" : arrayhex(34, 1) = "42"
        arrayhex(35, 0) = "67" : arrayhex(35, 1) = "43"
        arrayhex(36, 0) = "68" : arrayhex(36, 1) = "44"
        arrayhex(37, 0) = "69" : arrayhex(37, 1) = "45"
        arrayhex(38, 0) = "70" : arrayhex(38, 1) = "46"
        arrayhex(39, 0) = "71" : arrayhex(39, 1) = "47"
        arrayhex(40, 0) = "72" : arrayhex(40, 1) = "48"
        arrayhex(41, 0) = "73" : arrayhex(41, 1) = "49"
        arrayhex(42, 0) = "74" : arrayhex(42, 1) = "4A"
        arrayhex(43, 0) = "75" : arrayhex(43, 1) = "4B"
        arrayhex(44, 0) = "76" : arrayhex(44, 1) = "4C"
        arrayhex(45, 0) = "77" : arrayhex(45, 1) = "4D"
        arrayhex(46, 0) = "78" : arrayhex(46, 1) = "4E"
        arrayhex(47, 0) = "79" : arrayhex(47, 1) = "4F"


        arrayhex(48, 0) = "80" : arrayhex(48, 1) = "50"
        arrayhex(49, 0) = "81" : arrayhex(49, 1) = "51"
        arrayhex(50, 0) = "82" : arrayhex(50, 1) = "52"
        arrayhex(51, 0) = "83" : arrayhex(51, 1) = "53"
        arrayhex(52, 0) = "84" : arrayhex(52, 1) = "54"
        arrayhex(53, 0) = "85" : arrayhex(53, 1) = "55"
        arrayhex(54, 0) = "86" : arrayhex(54, 1) = "56"
        arrayhex(55, 0) = "87" : arrayhex(55, 1) = "57"
        arrayhex(56, 0) = "88" : arrayhex(56, 1) = "58"
        arrayhex(57, 0) = "89" : arrayhex(57, 1) = "59"
        arrayhex(58, 0) = "90" : arrayhex(58, 1) = "5A"
        arrayhex(59, 0) = "91" : arrayhex(59, 1) = "5B"
        arrayhex(60, 0) = "92" : arrayhex(60, 1) = "5C"
        arrayhex(61, 0) = "93" : arrayhex(61, 1) = "5D"
        arrayhex(62, 0) = "94" : arrayhex(62, 1) = "5E"
        arrayhex(63, 0) = "95" : arrayhex(63, 1) = "5F"
        cnvHexdec = ""
        posicion = 0
        For i = 1 To Len(strHex) Step 2

            For j = 0 To 63
                valor = Trim(Mid(strHex, i, 2))
                If arrayhex(j, 1) = valor Then
                    posicion = j
                    Exit For
                End If
            Next j
            cnvHexdec = cnvHexdec & Chr(Val(arrayhex(posicion, 0)))
        Next
    End Function


    Public Function Cadenahex(ByVal cadena As String) As String
        Dim Recorre As Integer
        Cadenahex = ""
        For Recorre = 1 To Len(cadena)
            Cadenahex = Cadenahex & Hex(Asc(Mid(cadena, Recorre, 1)))
        Next
    End Function
    Public Function Verify_Sign(ByVal strSignatureB64 As String, ByVal strCertFile As String, ByVal CadenaO As String) As Boolean
        '// INPUT: signature in base64 format; signer's X.509 certificate; 
        '// message data that has been signed (or its digest).
        '// OUTPUT: "valid signature" or "invalid signature". 
        Dim sbPublicKey As New System.Text.StringBuilder
        Dim block() As Byte
        Dim keyBytes As Int32
        Dim digest() As Byte
        'Dim newHash As String

        ' Read in Public key from X.509 certificate
        sbPublicKey = rsa.GetPublicKeyFromCert(strCertFile)
        'Debug.Assert(sbPublicKey.Length > 0)
        keyBytes = rsa.KeyBytes(sbPublicKey.ToString())
        'MsgBox("Key length is " & rsa.KeyBits(sbPublicKey.ToString()) & " bits/" & keyBytes & " bytes ")

        'Convert the signature into an array of bytes
        block = System.Convert.FromBase64String(strSignatureB64)
        'MsgBox("Signature block is " & block.Length & " bytes long")

        '// Check the sizes match. If not: "invalid signature" error
        If (block.Length <> keyBytes) Then
            'MsgBox("Firma inválida", MsgBoxStyle.Information, "AVISO")
            Return False
        End If

        '// Decrypt using RSA public key
        block = rsa.RawPublic(block, sbPublicKey.ToString())
        If block.Length = 0 Then
            'MsgBox("Firma inválida", MsgBoxStyle.Information, "AVISO")
            Return False
        End If
        'MsgBox("Decrypted block, EM=" & Cnv.ToHex(block))

        '// METHOD 1. If we have an independently-generated hash value, we extract
        '// the digest from the EMSA-PKCS1-V1_5 encoded block and compare 
        '// the hash values directly.

        '// Decode this block to extract the message digest
        digest = rsa.DecodeDigestForSignature(block)
        If (digest.Length = 0) Then
            'MsgBox("Firma inválida", MsgBoxStyle.Information, "AVISO")
            Return False
        End If
        'MsgBox("Digest=" & Cnv.ToHex(digest))

        '// Compare the hash value hex strings (careful with the case)
        Dim newHash As String
        newHash = Hash.HexFromString(CadenaO, HashAlgorithm.Md5)

        If (String.Compare(newHash, Cnv.ToHex(digest), True) = 0) Then
            'MsgBox("VALID SIGNATURE")
            Return True
        Else
            'MsgBox("ERROR: Invalid signature")
        End If

        '// METHOD 2. If we have the original message data, we create the MD5 message 
        '// digest of that and then do as in method (1) above.
        '// i.e. newHash = Hash.HexFromBytes(message, HashAlgorithm.Md5);

        '// METHOD 3. Instead of decoding the block, we apply the message encoding
        '// to the original message data (or its digest) and compare the encoded blocks
        '// themselves.

        '// Given a message digest (in bytes) create a new EMSA-PKCS1-V1_5 encoded block
        'Dim block2() As Byte
        'block2 = rsa.EncodeDigestForSignature(keyBytes, Cnv.FromHex(newHash), HashAlgorithm.Md5)
        'Console.WriteLine("EM'=\n{0}", Cnv.ToHex(block2))

        ''// Amazingly, .NET does not have a memcmp() function to compare byte arrays directly, 
        ''// so we compare the blocks as hex strings instead
        'If (String.Compare(Cnv.ToHex(block), Cnv.ToHex(block2)) = 0) Then
        '    Console.WriteLine("VALID SIGNATURE")
        'Else
        '    Console.WriteLine("ERROR: Invalid signature")
        'End If
    End Function
End Class
