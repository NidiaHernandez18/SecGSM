
Public Class Cblowfish
    Private aKey() As Byte
    Private strCipher As String  ' Used to store ciphertext
    Private blnCrypt As Boolean = True
    Private blf_P(17) As Integer
    Private blf_S(3, 255) As Integer
    Private Const ncROUNDS As Integer = 16
    Private Const ncMAXKEYLEN As Integer = 56
    Private aDecTab(255) As Long
    Private aEncTab(63) As Byte
    Private Const OFFSET_4 As Double = 4294967296.0#
    Private Const MAXINT_4 As Double = 2147483647
    Private CLAVE As String = "GSMPTOPZS"
    '
    ' Show key
    Public Function Encrypt(ByRef strData As String) As String
        Dim Strcipher As String
        SetKeyEncrypt()
        Strcipher = cv_HexFromBytes(aKey)

        Strcipher = blf_StringEnc(strData)
        Encrypt = cv_HexFromString(Strcipher)
        Return Encrypt

    End Function

    Public Function Decrypt(ByVal strpData As String) As String
        Dim strPlain As String
        SetKeyEncrypt()
        strPlain = Trim(strpData)
        strPlain = cv_StringFromHex(strPlain)
        Decrypt = blf_StringDec(strPlain)

    End Function



    Private Function blf_StringEnc(ByRef strData As String) As String
      
        Dim strIn As String
        Dim strOut As String
        Dim nLen As Long
        Dim sPad As String
        Dim nPad As Long
        Dim nBlocks As Long
        Dim i As Long
        Dim aBytes() As Byte
        Dim sBlock As New Compatibility.VB6.FixedLengthString(8)
        Dim iIndex As Long

        ' Pad data string to multiple of 8 bytes
        nLen = Len(strData)
        nPad = ((nLen \ 8) + 1) * 8 - nLen
        sPad = New String(Chr(nPad), nPad) ' Pad with # of pads (1-8)
        strIn = strData & sPad
        ' Calc number of 8-byte blocks
        nLen = Len(strIn)
        nBlocks = nLen \ 8
        ' Allocate output string here so we can use Mid$ below
        strOut = New String(" ", nLen)

        ' Work through string in blocks of 8 bytes
        iIndex = 0
        For i = 1 To nBlocks
            sBlock.Value = Mid(strIn, iIndex + 1, 8)
            ' Convert to bytes
            aBytes = System.Text.Encoding.Default.GetBytes(sBlock.Value)
            ' Encrypt the Block
            blf_EncryptBytes(aBytes)
            ' Convert back to a string
            sBlock.Value = System.Text.Encoding.Default.GetString(aBytes)
            ' Copy to output string
            Mid(strOut, iIndex + 1, 8) = sBlock.Value
            iIndex = iIndex + 8
        Next

        Return strOut
    End Function


    Private Sub blf_LoadArrays()
        LoadOriginalBlowfishArrays()
        Dim i As Long
        Dim j As Long
        For i = 0 To 3
            For j = 0 To 255
                If blf_S(i, j) = 0 Then
                    MsgBox("Zero value in S" & i & "," & j & ")")
                End If
            Next
        Next
    End Sub

    Private Function blf_F(ByRef x As Long) As Long
        Dim C, a, b, d As Byte
        Dim y As Long
        uwSplit(x, a, b, C, d)
        y = uw_WordAdd(blf_S(0, a), blf_S(1, b))
        y = y Xor blf_S(2, C)
        y = uw_WordAdd(y, blf_S(3, d))
        blf_F = y
    End Function

    Private Sub blf_EncipherBlock(ByRef xL As Long, ByRef xR As Long)
        Dim i As Long
        Dim temp As Long
        For i = 0 To ncROUNDS - 1
            xL = xL Xor blf_P(i)
            xR = blf_F(xL) Xor xR
            temp = xL
            xL = xR
            xR = temp
        Next
        temp = xL
        xL = xR
        xR = temp
        xR = xR Xor blf_P(ncROUNDS)
        xL = xL Xor blf_P(ncROUNDS + 1)
    End Sub

    Private Sub blf_DecipherBlock(ByRef xL As Long, ByRef xR As Long)
        Dim i As Long
        Dim temp As Long
        For i = ncROUNDS + 1 To 2 Step -1
            xL = xL Xor blf_P(i)
            xR = blf_F(xL) Xor xR
            temp = xL
            xL = xR
            xR = temp
        Next
        temp = xL
        xL = xR
        xR = temp
        xR = xR Xor blf_P(1)
        xL = xL Xor blf_P(0)
    End Sub

    Private Sub blf_Initialise(ByRef aKey() As Byte, ByRef nKeyBytes As Long)
        Dim j, i, K As Long
        Dim wDataL, wData, wDataR As Long
        blf_LoadArrays() ' Initialise P and S arrays
        j = 0
        For i = 0 To (ncROUNDS + 2 - 1)
            wData = &H0S
            For K = 0 To 3
                wData = uw_ShiftLeftBy8(wData) Or aKey(j)
                j = j + 1
                If j >= nKeyBytes Then j = 0
            Next K
            blf_P(i) = blf_P(i) Xor wData
        Next i
        wDataL = &H0S
        wDataR = &H0S
        For i = 0 To (ncROUNDS + 2 - 1) Step 2
            blf_EncipherBlock(wDataL, wDataR)
            blf_P(i) = wDataL
            blf_P(i + 1) = wDataR
        Next i
        For i = 0 To 3
            For j = 0 To 255 Step 2
                blf_EncipherBlock(wDataL, wDataR)
                blf_S(i, j) = wDataL
                blf_S(i, j + 1) = wDataR
            Next j
        Next i
    End Sub

    Private Function blf_Key(ByRef aKey() As Byte, ByRef nKeyLen As Long) As Boolean
        Dim blfKey As Boolean = False
        If nKeyLen < 0 Or nKeyLen > ncMAXKEYLEN Then
            Return blfKey
            Exit Function
        End If
        blf_Initialise(aKey, nKeyLen)
        blfKey = True
        Return blfKey
    End Function

    Private Function blf_KeyInit(ByRef aKey() As Byte) As Boolean
        ' Added Version 5: Replacement for blf_Key to avoid specifying keylen
        Dim nKeyLen As Long
        Dim blfKeyInit As Boolean = False
        nKeyLen = UBound(aKey) - LBound(aKey) + 1
        If nKeyLen < 0 Or nKeyLen > ncMAXKEYLEN Then
            Return blfKeyInit
            Exit Function
        End If
        blf_Initialise(aKey, nKeyLen)
        blfKeyInit = True
        Return blfKeyInit
    End Function

    Private Sub blf_EncryptBytes(ByRef aBytes() As Byte)
        Dim wordL, wordR As Long
        ' Convert to 2 x words
        wordL = uwJoin(aBytes(0), aBytes(1), aBytes(2), aBytes(3))
        wordR = uwJoin(aBytes(4), aBytes(5), aBytes(6), aBytes(7))
        ' Encrypt it
        blf_EncipherBlock(wordL, wordR)
        ' Put back into bytes
        uwSplit(wordL, aBytes(0), aBytes(1), aBytes(2), aBytes(3))
        uwSplit(wordR, aBytes(4), aBytes(5), aBytes(6), aBytes(7))
    End Sub

    Private Sub blf_DecryptBytes(ByRef aBytes() As Byte)
        ' aBytes() must be 8 bytes long

        Dim wordL, wordR As Long
        ' Convert to 2 x words
        wordL = uwJoin(aBytes(0), aBytes(1), aBytes(2), aBytes(3))
        wordR = uwJoin(aBytes(4), aBytes(5), aBytes(6), aBytes(7))
        ' Decrypt it
        blf_DecipherBlock(wordL, wordR)
        ' Put back into bytes
        uwSplit(wordL, aBytes(0), aBytes(1), aBytes(2), aBytes(3))
        uwSplit(wordR, aBytes(4), aBytes(5), aBytes(6), aBytes(7))
    End Sub

    ' Version 5 note: These functions blf_Enc() and blf_Dec() are
    ' probably redundant now.
    ' See improved versions of blf_StringEnc and blf_StringDec

    Private Sub blf_Enc(ByRef awData() As Long, ByRef nWords As Long)
        ' Version 5: Changed Integer counters to Long
        Dim i As Long
        For i = 0 To nWords - 1 Step 2
            blf_EncipherBlock(awData(i), awData(i + 1))
        Next i
    End Sub

    Private Sub blf_Dec(ByRef awData() As Long, ByRef nWords As Long)
        ' Version 5: Changed Integer counters to Long
        Dim i As Long
        For i = 0 To nWords - 1 Step 2
            blf_DecipherBlock(awData(i), awData(i + 1))
        Next i
    End Sub

    Private Function blf_ByteCrypt(ByRef bytData As Byte(), ByVal blnEncrypt As Boolean) As Byte()

        Dim nLen As Long
        Dim nPad As Long
        Dim nBlocks As Long
        Dim h, i As Long
        Dim j, k As Long
        Dim sBlock(7) As Byte
        Dim iIndex As Long

        'If ENCRYPTING, Pad data string to multiple of 8 bytes
        nLen = bytData.Length
        If blnEncrypt = True Then
            nPad = ((nLen \ 8) + 1) * 8 - nLen
            If nLen Mod 8 <> 0 Then
                ReDim Preserve bytData(bytData.Length + nPad - 1)
                For h = 1 To nPad
                    bytData(nLen + h - 1) = nPad
                Next
                nLen = bytData.Length
            End If
        End If
        nBlocks = nLen \ 8

        Dim strOut(nLen - 1) As Byte
        ' Work through string in blocks of 8 bytes
        iIndex = 0
        For i = 1 To nBlocks
            For j = 0 To 7
                sBlock(j) = bytData(iIndex + j)
            Next
            If blnEncrypt = True Then
                blf_EncryptBytes(sBlock)
            Else
                blf_DecryptBytes(sBlock)
            End If

            For k = 0 To 7
                strOut(k + iIndex) = sBlock(k)
            Next

            iIndex = iIndex + 8
        Next
        'If DECRYPTING, remove padding
        If blnEncrypt = False Then
            nPad = strOut(strOut.Length - 1)
            If nPad > 7 Then nPad = 0
            ReDim Preserve strOut(strOut.Length - nPad - 1)
        End If

        Return strOut
    End Function

    Private Function blf_StringRaw(ByRef strData As String, ByRef bEncrypt As Boolean) As String
        ' New function added version 5.
        ' Encrypts or decrypts strData without padding according to current key.
        ' Similar to blf_StringEnc and blf_StringDec, but does not add padding
        ' and ignores trailing odd bytes.
        Dim strIn As String
        Dim strOut As String
        Dim nLen As Long
        Dim nBlocks As Long
        Dim i As Long
        Dim aBytes() As Byte
        Dim sBlock As New Compatibility.VB6.FixedLengthString(8)
        Dim iIndex As Long
        strIn = strData
        nLen = Len(strIn)
        nBlocks = nLen \ 8
        strOut = New String(" ", nLen)
        iIndex = 0
        For i = 1 To nBlocks
            sBlock.Value = Mid(strIn, iIndex + 1, 8)
            aBytes = System.Text.Encoding.Default.GetBytes(sBlock.Value)
            If bEncrypt Then
                blf_EncryptBytes(aBytes)
            Else
                blf_DecryptBytes(aBytes)
            End If
            sBlock.Value = System.Text.Encoding.Default.GetString(aBytes)
            Mid(strOut, iIndex + 1, 8) = sBlock.Value
            iIndex = iIndex + 8
        Next

        Return strOut
    End Function


    Private Function blf_StringDec(ByRef strData As String) As String
        ' Decrypts ciphertext strData and removes RFC 2630 padding
        ' Returns decrypted string.
        ' Requires key and boxes to be already set up.
        ' Version 5. Completely revised.

        Dim strIn As String
        Dim strOut As String
        Dim nLen As Long
        Dim nPad As Long
        Dim nBlocks As Long
        Dim i As Long
        Dim aBytes() As Byte
        Dim sBlock As New Compatibility.VB6.FixedLengthString(8)
        Dim iIndex As Long

        strIn = strData
        ' Calc number of 8-byte blocks
        nLen = Len(strIn)
        nBlocks = nLen \ 8
        ' Allocate output string here so we can use Mid$ below
        strOut = New String(" ", nLen)

        ' Work through string in blocks of 8 bytes
        iIndex = 0
        For i = 1 To nBlocks
            sBlock.Value = Mid(strIn, iIndex + 1, 8)
            ' Convert to bytes
            aBytes = System.Text.Encoding.Default.GetBytes(sBlock.Value)
            ' Encrypt the block
            blf_DecryptBytes(aBytes)
            ' Convert back to a string
            sBlock.Value = System.Text.Encoding.Default.GetString(aBytes)
            ' Copy to output string
            Mid(strOut, iIndex + 1, 8) = sBlock.Value
            iIndex = iIndex + 8
        Next

        ' Strip padding, if valid
        nPad = Asc(Microsoft.VisualBasic.Right(strOut, 1))
        If nPad > 8 Then nPad = 0
        strOut = Microsoft.VisualBasic.Left(strOut, nLen - nPad)

        Return strOut
    End Function

    Private Function PadString(ByRef strData As String) As String
        ' Pad data string to next multiple of 8 bytes as per RFC 2630
        Dim nLen As Long
        Dim sPad As String
        Dim nPad As Long
        nLen = Len(strData)
        nPad = ((nLen \ 8) + 1) * 8 - nLen
        sPad = New String(Chr(nPad), nPad) ' Pad with # of pads (1-8)
        PadString = strData & sPad
    End Function

    Private Function UnpadString(ByRef strData As String) As String
        Dim nLen As Long
        Dim nPad As Long
        UnpadString = ""
        nLen = Len(strData)
        If nLen = 0 Then Exit Function
        ' Get # of padding bytes from last char
        nPad = Asc(Microsoft.VisualBasic.Right(strData, 1))
        If nPad > 8 Then nPad = 0 ' In case invalid
        UnpadString = Microsoft.VisualBasic.Left(strData, nLen - nPad)
    End Function

  
    Private Function cv_BytesFromHex(ByVal sInputHex As String) As Object
        ' Returns array of bytes from hex string in big-endian order
        ' E.g. sHex="FEDC80" will return array {&HFE, &HDC, &H80}
        Dim i As Long
        Dim M As Long
        Dim aBytes() As Byte
        If Len(sInputHex) Mod 2 <> 0 Then
            sInputHex = "0" & sInputHex
        End If

        M = Len(sInputHex) \ 2
        ReDim aBytes(M - 1)

        For i = 0 To M - 1
            aBytes(i) = Val("&H" & Mid(sInputHex, i * 2 + 1, 2))
        Next

        cv_BytesFromHex = Compatibility.VB6.CopyArray(aBytes)
        Return cv_BytesFromHex
    End Function

    Private Function cv_BytesFromString(ByRef strInput As String) As Byte()
        ' Converts string <strInput> of ascii chars to array of bytes
        ' str may contain chars of any value between 0 and 255.
        ' E.g. strInput="abc." will return array {&H61, &H62, &H63, &H2E}
        Dim aBytes() As Byte
        aBytes = System.Text.Encoding.Default.GetBytes(strInput)
        Return aBytes
    End Function

    Private Function cv_WordsFromHex(ByVal sHex As String) As Long()
        ' Converts string <sHex> with hex values into array of words (long ints)
        ' E.g. "fedcba9876543210" will be converted into {&HFEDCBA98, &H76543210}
        Const ncLEN As Integer = 8
        Dim i As Long
        Dim nWords As Long
        Dim aWords() As Long

        nWords = Len(sHex) \ ncLEN
        ReDim aWords(nWords - 1)
        For i = 0 To nWords - 1
            aWords(i) = Val("&H" & Mid(sHex, i * ncLEN + 1, ncLEN))
        Next
        Return aWords

    End Function

    Private Function cv_HexFromWords(ByRef aWords As Object) As String
        ' Converts array of words (Longs) into a hex string
        ' E.g. {&HFEDCBA98, &H76543210} will be converted to "FEDCBA9876543210"
        Const ncLEN As Integer = 8
        Dim i As Long
        Dim nWords As Long
        Dim sHex As New Compatibility.VB6.FixedLengthString(ncLEN)
        Dim iIndex As Long
        Dim s As String
        cv_HexFromWords = ""
        If Not IsArray(aWords) Then
            Exit Function
        End If

        nWords = UBound(aWords) - LBound(aWords) + 1
        s = New String(" ", nWords * ncLEN)
        iIndex = 0
        For i = 0 To nWords - 1
            sHex.Value = Hex(aWords(i))
            sHex.Value = New String("0", ncLEN - Len(sHex.Value)) & sHex.Value
            Mid(s, iIndex + 1, ncLEN) = sHex.Value
            iIndex = iIndex + ncLEN
        Next
        Return s
    End Function

    Private Function cv_HexFromBytes(ByRef aBytes() As Byte) As String
        ' Returns hex string from array of bytes
        ' E.g. aBytes() = {&HFE, &HDC, &H80} will return "FEDC80"
        Dim i As Long
        Dim iIndex As Long
        Dim s As String
        s = New String(" ", (UBound(aBytes) - LBound(aBytes) + 1) * 2)
        iIndex = 0
        For i = LBound(aBytes) To UBound(aBytes)
            Mid(s, iIndex + 1, 2) = HexFromByte(aBytes(i))
            iIndex = iIndex + 2
        Next
        Return s
    End Function

    Private Function cv_HexFromString(ByRef str_Renamed As String) As String
        ' Converts string <str> of ascii chars to string in hex format
        ' str may contain chars of any value between 0 and 255.
        ' E.g. "abc." will be converted to "6162632E"
        Dim byt As Byte
        Dim i As Long
        Dim n As Long
        Dim iIndex As Long
        Dim sHex As String

        n = Len(str_Renamed)
        sHex = New String(" ", n * 2)
        iIndex = 0
        For i = 1 To n
            byt = CByte(Asc(Mid(str_Renamed, i, 1)) And &HFFS)
            Mid(sHex, iIndex + 1, 2) = HexFromByte(byt)
            iIndex = iIndex + 2
        Next
        Return sHex

    End Function

    Private Function cv_StringFromHex(ByRef strHex As String) As String
        ' Converts string <strHex> in hex format to string of ascii chars
        ' with value between 0 and 255.
        ' E.g. "6162632E" will be converted to "abc."
        Dim i As Long
        Dim nBytes As Long
        Dim s As String
        nBytes = Len(strHex) \ 2
        s = New String(" ", nBytes)
        For i = 0 To nBytes - 1
            Mid(s, i + 1, 1) = Chr(Val("&H" & Mid(strHex, i * 2 + 1, 2)))
        Next
        Return s
    End Function

    Private Function cv_GetHexByte(ByVal sInputHex As String, ByRef iIndex As Long) As Byte
        ' Extracts iIndex'th byte from hex string (starting at 1)
        ' E.g. cv_GetHexByte("fecdba98", 3) will return &HBA
        Dim i As Long
        Dim b As Byte
        i = 2 * iIndex
        If i > Len(sInputHex) Or i <= 0 Then
            b = 0
        Else
            b = Val("&H" & Mid(sInputHex, i - 1, 2))
        End If
        Return b
    End Function

    Private Function RandHexByte() As String
        '   Returns a random byte as a 2-digit hex string
        Static stbInit As Boolean
        Dim s As String
        If Not stbInit Then
            Randomize()
            stbInit = True
        End If
        s = HexFromByte(CByte((Rnd() * 256) And &HFFS))
        Return s
    End Function

    Private Function HexFromByte(ByVal x As Object) As String
        x = x And &HFFS
        Dim s As String
        If x < 16 Then
            s = "0" & Hex(x)
        Else
            s = Hex(x)
        End If
        Return s
    End Function

    Private Function testBytesHex() As Byte()
        Dim aBytes() As Byte
        aBytes = cv_BytesFromHex("FEDC80")
        Return aBytes
    End Function

    Private Function testWordsHex() As Long()
        Dim aWords As Long()
        aWords = cv_WordsFromHex("FEDCBA9876543210")
        Return aWords
    End Function

    Private Function EncodeStr64(ByRef sInput As String) As String
        ' Return radix64 encoding of string of binary values
        ' Does not insert CRLFs. Just returns one long string,
        ' so it's up to the user to add line breaks or other formatting.
        ' Version 4: Use Byte array and StrConv - much faster
        Dim abOutput() As Byte ' Version 4: now a Byte array
        Dim sLast As String
        Dim b(3) As Byte ' Version 4: Now 3 not 2
        Dim j As Long
        Dim nLen, i, nQuants As Long
        Dim iIndex As Long
        sLast = ""
        EncodeStr64 = ""
        nLen = Len(sInput)
        nQuants = nLen \ 3
        iIndex = 0
        MakeEncTab()
        If (nQuants > 0) Then
            ReDim abOutput(nQuants * 4 - 1)
            ' Now start reading in 3 bytes at a time
            For i = 0 To nQuants - 1
                For j = 0 To 2
                    b(j) = Asc(Mid(sInput, (i * 3) + j + 1, 1))
                Next
                EncodeQuantumB(b)
                abOutput(iIndex) = b(0)
                abOutput(iIndex + 1) = b(1)
                abOutput(iIndex + 2) = b(2)
                abOutput(iIndex + 3) = b(3)
                iIndex = iIndex + 4
            Next
            EncodeStr64 = System.Text.Encoding.Default.GetString(abOutput)
        End If
        Select Case nLen Mod 3
            Case 0
                sLast = ""
            Case 1
                b(0) = Asc(Mid(sInput, nLen, 1))
                b(1) = 0
                b(2) = 0
                EncodeQuantumB(b)
                sLast = System.Text.Encoding.Default.GetString(b)
                sLast = Microsoft.VisualBasic.Left(sLast, 2) & "=="
            Case 2
                b(0) = Asc(Mid(sInput, nLen - 1, 1))
                b(1) = Asc(Mid(sInput, nLen, 1))
                b(2) = 0
                EncodeQuantumB(b)
                sLast = System.Text.Encoding.Default.GetString(b)
                sLast = Microsoft.VisualBasic.Left(sLast, 3) & "="
        End Select
        Return EncodeStr64 & sLast
    End Function

    Private Function DecodeStr64(ByRef sEncoded As String) As String
        ' Return string of decoded binary values given radix64 string
        ' Ignores any chars not in the 64-char subset
        ' Version 4: Use Byte array and StrConv - much faster
        Dim abDecoded() As Byte 'Version 4: Now a Byte array
        Dim d(3) As Byte
        Dim C As Integer ' NB Integer to catch -1 value
        Dim di As Integer
        Dim i As Long
        Dim nLen As Long
        Dim iIndex As Long
        DecodeStr64 = ""
        nLen = Len(sEncoded)
        If nLen < 4 Then
            Exit Function
        End If
        ReDim abDecoded(((nLen \ 4) * 3) - 1) 'Version 4: Now base zero
        iIndex = 0 ' Version 4: Changed to base 0
        di = 0
        MakeDecTab()
        ' Read in each char in turn
        For i = 1 To Len(sEncoded)
            C = CByte(Asc(Mid(sEncoded, i, 1)))
            C = aDecTab(C)
            If C >= 0 Then
                d(di) = CByte(C) ' Version 3.1: add CByte()
                di = di + 1
                If di = 4 Then
                    abDecoded(iIndex) = SHL2(d(0)) Or (SHR4(d(1)) And &H3S)
                    iIndex = iIndex + 1
                    abDecoded(iIndex) = SHL4(d(1) And &HFS) Or (SHR2(d(2)) And &HFS)
                    iIndex = iIndex + 1
                    abDecoded(iIndex) = SHL6(d(2) And &H3S) Or d(3)
                    iIndex = iIndex + 1
                    If d(3) = 64 Then
                        iIndex = iIndex - 1
                        abDecoded(iIndex) = 0
                    End If
                    If d(2) = 64 Then
                        iIndex = iIndex - 1
                        abDecoded(iIndex) = 0
                    End If
                    di = 0
                End If
            End If
        Next i
        DecodeStr64 = System.Text.Encoding.Default.GetString(abDecoded)
        Return Microsoft.VisualBasic.Left(DecodeStr64, iIndex)
    End Function

    Private Sub EncodeQuantumB(ByRef b() As Byte)
        ' Expects at least 4 bytes in b, i.e. Dim b(3) As Byte
        Dim b2, b0, b1, b3 As Byte
        b0 = SHR2(b(0)) And &H3FS
        b1 = SHL4(b(0) And &H3S) Or (SHR4(b(1)) And &HFS)
        b2 = SHL2(b(1) And &HFS) Or (SHR6(b(2)) And &H3S)
        b3 = b(2) And &H3FS
        b(0) = aEncTab(b0)
        b(1) = aEncTab(b1)
        b(2) = aEncTab(b2)
        b(3) = aEncTab(b3)
    End Sub

    Private Function MakeDecTab() As Object
        ' Set up Radix 64 decoding table
        Dim t As Integer
        Dim C As Integer
        MakeDecTab = ""
        For C = 0 To 255
            aDecTab(C) = -1
        Next
        t = 0
        For C = Asc("A") To Asc("Z")
            aDecTab(C) = t
            t = t + 1
        Next
        For C = Asc("a") To Asc("z")
            aDecTab(C) = t
            t = t + 1
        Next
        For C = Asc("0") To Asc("9")
            aDecTab(C) = t
            t = t + 1
        Next
        C = Asc("+")
        aDecTab(C) = t
        t = t + 1
        C = Asc("/")
        aDecTab(C) = t
        t = t + 1
        C = Asc("=") ' flag for the byte-deleting char
        aDecTab(C) = t ' should be 64
    End Function

    Private Function MakeEncTab() As Object
        ' Set up Radix 64 encoding table in bytes
        Dim i As Integer
        Dim C As Integer
        i = 0
        MakeEncTab = ""
        For C = Asc("A") To Asc("Z")
            aEncTab(i) = C
            i = i + 1
        Next
        For C = Asc("a") To Asc("z")
            aEncTab(i) = C
            i = i + 1
        Next
        For C = Asc("0") To Asc("9")
            aEncTab(i) = C
            i = i + 1
        Next
        C = Asc("+")
        aEncTab(i) = C
        i = i + 1
        C = Asc("/")
        aEncTab(i) = C
        i = i + 1
    End Function

    ' Version 3: ShiftLeft and ShiftRight functions improved.
    Private Function SHL2(ByVal bytValue As Byte) As Byte
        ' Shift 8-bit value to left by 2 bits
        ' i.e. VB equivalent of "bytValue << 2" in C
        SHL2 = (bytValue * &H4S) And &HFFS
    End Function

    Private Function SHL4(ByVal bytValue As Byte) As Byte
        ' Shift 8-bit value to left by 4 bits
        ' i.e. VB equivalent of "bytValue << 4" in C
        SHL4 = (bytValue * &H10S) And &HFFS
    End Function

    Private Function SHL6(ByVal bytValue As Byte) As Byte
        ' Shift 8-bit value to left by 6 bits
        ' i.e. VB equivalent of "bytValue << 6" in C
        SHL6 = (bytValue * &H40S) And &HFFS
    End Function

    Private Function SHR2(ByVal bytValue As Byte) As Byte
        ' Shift 8-bit value to right by 2 bits
        ' i.e. VB equivalent of "bytValue >> 2" in C
        SHR2 = bytValue \ &H4S
    End Function

    Private Function SHR4(ByVal bytValue As Byte) As Byte
        ' Shift 8-bit value to right by 4 bits
        ' i.e. VB equivalent of "bytValue >> 4" in C
        SHR4 = bytValue \ &H10S
    End Function

    Private Function SHR6(ByVal bytValue As Byte) As Byte
        ' Shift 8-bit value to right by 6 bits
        ' i.e. VB equivalent of "bytValue >> 6" in C
        SHR6 = bytValue \ &H40S
    End Function

    Private Function uwJoin(ByRef a As Byte, ByRef b As Byte, ByRef C As Byte, ByRef d As Byte) As Long
        ' Added Version 5: replacement for uw_WordJoin
        ' Join 4 x 8-bit bytes into one 32-bit word a.b.c.d
        Dim lngOUT As Long
        lngOUT = ((a And &H7FS) * &H1000000) Or (b * &H10000) Or (CLng(C) * &H100S) Or d
        If a And &H80S Then
            lngOUT = lngOUT Or &H80000000
        End If
        Return lngOUT
    End Function

    Private Sub uwSplit(ByVal w As Long, ByRef a As Byte, ByRef b As Byte, ByRef C As Byte, ByRef d As Byte)
        ' Added Version 5: replacement for uw_WordSplit
        ' Split 32-bit word w into 4 x 8-bit bytes
        a = CByte(((w And &HFF000000) \ &H1000000) And &HFFS)
        b = CByte(((w And &HFF0000) \ &H10000) And &HFFS)
        C = CByte(((w And &HFF00S) \ &H100S) And &HFFS)
        d = CByte((w And &HFFS) And &HFFS)
    End Sub

    '
    Private Function uw_ShiftLeftBy8(ByRef wordX As Long) As Long
        ' Shift 32-bit long value to left by 8 bits
        ' i.e. VB equivalent of "wordX << 8" in C
        ' Avoiding problem with sign bit
        Dim SLB As Long
        SLB = (wordX And &H7FFFFF) * &H100S
        If (wordX And &H800000) <> 0 Then
            SLB = SLB Or &H80000000
        End If
        Return SLB
    End Function
    Private Function uw_WordAdd(ByRef wordA As Long, ByRef wordB As Long) As Long
        ' Adds words A and B avoiding overflow
        Dim myUnsigned As Double
        Dim WA As Long
        myUnsigned = LongToUnsigned(wordA) + LongToUnsigned(wordB)
        ' Cope with overflow
        If myUnsigned > OFFSET_4 Then
            myUnsigned = myUnsigned - OFFSET_4
        End If
        WA = UnsignedToLong(myUnsigned)
        Return WA
    End Function

    Private Function uw_WordSub(ByRef wordA As Long, ByRef wordB As Long) As Long
        ' Subtract words A and B avoiding underflow
        Dim myUnsigned As Double
        Dim WS As Long
        myUnsigned = LongToUnsigned(wordA) - LongToUnsigned(wordB)
        ' Cope with underflow
        If myUnsigned < 0 Then
            myUnsigned = myUnsigned + OFFSET_4
        End If
        WS = UnsignedToLong(myUnsigned)
        Return WS
    End Function

    '****************************************************
    ' These two functions from Microsoft Article Q189323
    ' "HOWTO: convert between Signed and Unsigned Numbers"
    Private Function UnsignedToLong(ByRef value As Double) As Long
        Dim UTL As Long
        If value < 0 Or value >= OFFSET_4 Then Error (6) ' Overflow
        If value <= MAXINT_4 Then
            UTL = value
        Else
            UTL = value - OFFSET_4
        End If
        Return UTL
    End Function

    Private Function LongToUnsigned(ByRef value As Long) As Double
        Dim LTU As Double
        If value < 0 Then
            LTU = value + OFFSET_4
        Else
            LTU = value
        End If
        Return LTU
    End Function

    Public Sub SetKeyEncrypt()
        aKey = cv_BytesFromHex(CLAVE)      ' In hex format
        blf_KeyInit(aKey)                             ' Initialise key
    End Sub


    Private Sub LoadOriginalBlowfishArrays()
        blf_P(0) = &H243F6A88
        blf_P(1) = &H85A308D3
        blf_P(2) = &H13198A2E
        blf_P(3) = &H3707344
        blf_P(4) = &HA4093822
        blf_P(5) = &H299F31D0
        blf_P(6) = &H82EFA98
        blf_P(7) = &HEC4E6C89
        blf_P(8) = &H452821E6
        blf_P(9) = &H38D01377
        blf_P(10) = &HBE5466CF
        blf_P(11) = &H34E90C6C
        blf_P(12) = &HC0AC29B7
        blf_P(13) = &HC97C50DD
        blf_P(14) = &H3F84D5B5
        blf_P(15) = &HB5470917
        blf_P(16) = &H9216D5D9
        blf_P(17) = &H8979FB1B

        blf_S(0, 0) = &HD1310BA6
        blf_S(0, 1) = &H98DFB5AC
        blf_S(0, 2) = &H2FFD72DB
        blf_S(0, 3) = &HD01ADFB7
        blf_S(0, 4) = &HB8E1AFED
        blf_S(0, 5) = &H6A267E96
        blf_S(0, 6) = &HBA7C9045
        blf_S(0, 7) = &HF12C7F99
        blf_S(0, 8) = &H24A19947
        blf_S(0, 9) = &HB3916CF7
        blf_S(0, 10) = &H801F2E2
        blf_S(0, 11) = &H858EFC16
        blf_S(0, 12) = &H636920D8
        blf_S(0, 13) = &H71574E69
        blf_S(0, 14) = &HA458FEA3
        blf_S(0, 15) = &HF4933D7E
        blf_S(0, 16) = &HD95748F
        blf_S(0, 17) = &H728EB658
        blf_S(0, 18) = &H718BCD58
        blf_S(0, 19) = &H82154AEE
        blf_S(0, 20) = &H7B54A41D
        blf_S(0, 21) = &HC25A59B5
        blf_S(0, 22) = &H9C30D539
        blf_S(0, 23) = &H2AF26013
        blf_S(0, 24) = &HC5D1B023
        blf_S(0, 25) = &H286085F0
        blf_S(0, 26) = &HCA417918
        blf_S(0, 27) = &HB8DB38EF
        blf_S(0, 28) = &H8E79DCB0
        blf_S(0, 29) = &H603A180E
        blf_S(0, 30) = &H6C9E0E8B
        blf_S(0, 31) = &HB01E8A3E
        blf_S(0, 32) = &HD71577C1
        blf_S(0, 33) = &HBD314B27
        blf_S(0, 34) = &H78AF2FDA
        blf_S(0, 35) = &H55605C60
        blf_S(0, 36) = &HE65525F3
        blf_S(0, 37) = &HAA55AB94
        blf_S(0, 38) = &H57489862
        blf_S(0, 39) = &H63E81440
        blf_S(0, 40) = &H55CA396A
        blf_S(0, 41) = &H2AAB10B6
        blf_S(0, 42) = &HB4CC5C34
        blf_S(0, 43) = &H1141E8CE
        blf_S(0, 44) = &HA15486AF
        blf_S(0, 45) = &H7C72E993
        blf_S(0, 46) = &HB3EE1411
        blf_S(0, 47) = &H636FBC2A
        blf_S(0, 48) = &H2BA9C55D
        blf_S(0, 49) = &H741831F6
        blf_S(0, 50) = &HCE5C3E16
        blf_S(0, 51) = &H9B87931E
        blf_S(0, 52) = &HAFD6BA33
        blf_S(0, 53) = &H6C24CF5C
        blf_S(0, 54) = &H7A325381
        blf_S(0, 55) = &H28958677
        blf_S(0, 56) = &H3B8F4898
        blf_S(0, 57) = &H6B4BB9AF
        blf_S(0, 58) = &HC4BFE81B
        blf_S(0, 59) = &H66282193
        blf_S(0, 60) = &H61D809CC
        blf_S(0, 61) = &HFB21A991
        blf_S(0, 62) = &H487CAC60
        blf_S(0, 63) = &H5DEC8032
        blf_S(0, 64) = &HEF845D5D
        blf_S(0, 65) = &HE98575B1
        blf_S(0, 66) = &HDC262302
        blf_S(0, 67) = &HEB651B88
        blf_S(0, 68) = &H23893E81
        blf_S(0, 69) = &HD396ACC5
        blf_S(0, 70) = &HF6D6FF3
        blf_S(0, 71) = &H83F44239
        blf_S(0, 72) = &H2E0B4482
        blf_S(0, 73) = &HA4842004
        blf_S(0, 74) = &H69C8F04A
        blf_S(0, 75) = &H9E1F9B5E
        blf_S(0, 76) = &H21C66842
        blf_S(0, 77) = &HF6E96C9A
        blf_S(0, 78) = &H670C9C61
        blf_S(0, 79) = &HABD388F0
        blf_S(0, 80) = &H6A51A0D2
        blf_S(0, 81) = &HD8542F68
        blf_S(0, 82) = &H960FA728
        blf_S(0, 83) = &HAB5133A3
        blf_S(0, 84) = &H6EEF0B6C
        blf_S(0, 85) = &H137A3BE4
        blf_S(0, 86) = &HBA3BF050
        blf_S(0, 87) = &H7EFB2A98
        blf_S(0, 88) = &HA1F1651D
        blf_S(0, 89) = &H39AF0176
        blf_S(0, 90) = &H66CA593E
        blf_S(0, 91) = &H82430E88
        blf_S(0, 92) = &H8CEE8619
        blf_S(0, 93) = &H456F9FB4
        blf_S(0, 94) = &H7D84A5C3
        blf_S(0, 95) = &H3B8B5EBE
        blf_S(0, 96) = &HE06F75D8
        blf_S(0, 97) = &H85C12073
        blf_S(0, 98) = &H401A449F
        blf_S(0, 99) = &H56C16AA6
        blf_S(0, 100) = &H4ED3AA62
        blf_S(0, 101) = &H363F7706
        blf_S(0, 102) = &H1BFEDF72
        blf_S(0, 103) = &H429B023D
        blf_S(0, 104) = &H37D0D724
        blf_S(0, 105) = &HD00A1248
        blf_S(0, 106) = &HDB0FEAD3
        blf_S(0, 107) = &H49F1C09B
        blf_S(0, 108) = &H75372C9
        blf_S(0, 109) = &H80991B7B
        blf_S(0, 110) = &H25D479D8
        blf_S(0, 111) = &HF6E8DEF7
        blf_S(0, 112) = &HE3FE501A
        blf_S(0, 113) = &HB6794C3B
        blf_S(0, 114) = &H976CE0BD
        blf_S(0, 115) = &H4C006BA
        blf_S(0, 116) = &HC1A94FB6
        blf_S(0, 117) = &H409F60C4
        blf_S(0, 118) = &H5E5C9EC2
        blf_S(0, 119) = &H196A2463
        blf_S(0, 120) = &H68FB6FAF
        blf_S(0, 121) = &H3E6C53B5
        blf_S(0, 122) = &H1339B2EB
        blf_S(0, 123) = &H3B52EC6F
        blf_S(0, 124) = &H6DFC511F
        blf_S(0, 125) = &H9B30952C
        blf_S(0, 126) = &HCC814544
        blf_S(0, 127) = &HAF5EBD09
        blf_S(0, 128) = &HBEE3D004
        blf_S(0, 129) = &HDE334AFD
        blf_S(0, 130) = &H660F2807
        blf_S(0, 131) = &H192E4BB3
        blf_S(0, 132) = &HC0CBA857
        blf_S(0, 133) = &H45C8740F
        blf_S(0, 134) = &HD20B5F39
        blf_S(0, 135) = &HB9D3FBDB
        blf_S(0, 136) = &H5579C0BD
        blf_S(0, 137) = &H1A60320A
        blf_S(0, 138) = &HD6A100C6
        blf_S(0, 139) = &H402C7279
        blf_S(0, 140) = &H679F25FE
        blf_S(0, 141) = &HFB1FA3CC
        blf_S(0, 142) = &H8EA5E9F8
        blf_S(0, 143) = &HDB3222F8
        blf_S(0, 144) = &H3C7516DF
        blf_S(0, 145) = &HFD616B15
        blf_S(0, 146) = &H2F501EC8
        blf_S(0, 147) = &HAD0552AB
        blf_S(0, 148) = &H323DB5FA
        blf_S(0, 149) = &HFD238760
        blf_S(0, 150) = &H53317B48
        blf_S(0, 151) = &H3E00DF82
        blf_S(0, 152) = &H9E5C57BB
        blf_S(0, 153) = &HCA6F8CA0
        blf_S(0, 154) = &H1A87562E
        blf_S(0, 155) = &HDF1769DB
        blf_S(0, 156) = &HD542A8F6
        blf_S(0, 157) = &H287EFFC3
        blf_S(0, 158) = &HAC6732C6
        blf_S(0, 159) = &H8C4F5573
        blf_S(0, 160) = &H695B27B0
        blf_S(0, 161) = &HBBCA58C8
        blf_S(0, 162) = &HE1FFA35D
        blf_S(0, 163) = &HB8F011A0
        blf_S(0, 164) = &H10FA3D98
        blf_S(0, 165) = &HFD2183B8
        blf_S(0, 166) = &H4AFCB56C
        blf_S(0, 167) = &H2DD1D35B
        blf_S(0, 168) = &H9A53E479
        blf_S(0, 169) = &HB6F84565
        blf_S(0, 170) = &HD28E49BC
        blf_S(0, 171) = &H4BFB9790
        blf_S(0, 172) = &HE1DDF2DA
        blf_S(0, 173) = &HA4CB7E33
        blf_S(0, 174) = &H62FB1341
        blf_S(0, 175) = &HCEE4C6E8
        blf_S(0, 176) = &HEF20CADA
        blf_S(0, 177) = &H36774C01
        blf_S(0, 178) = &HD07E9EFE
        blf_S(0, 179) = &H2BF11FB4
        blf_S(0, 180) = &H95DBDA4D
        blf_S(0, 181) = &HAE909198
        blf_S(0, 182) = &HEAAD8E71
        blf_S(0, 183) = &H6B93D5A0
        blf_S(0, 184) = &HD08ED1D0
        blf_S(0, 185) = &HAFC725E0
        blf_S(0, 186) = &H8E3C5B2F
        blf_S(0, 187) = &H8E7594B7
        blf_S(0, 188) = &H8FF6E2FB
        blf_S(0, 189) = &HF2122B64
        blf_S(0, 190) = &H8888B812
        blf_S(0, 191) = &H900DF01C
        blf_S(0, 192) = &H4FAD5EA0
        blf_S(0, 193) = &H688FC31C
        blf_S(0, 194) = &HD1CFF191
        blf_S(0, 195) = &HB3A8C1AD
        blf_S(0, 196) = &H2F2F2218
        blf_S(0, 197) = &HBE0E1777
        blf_S(0, 198) = &HEA752DFE
        blf_S(0, 199) = &H8B021FA1
        blf_S(0, 200) = &HE5A0CC0F
        blf_S(0, 201) = &HB56F74E8
        blf_S(0, 202) = &H18ACF3D6
        blf_S(0, 203) = &HCE89E299
        blf_S(0, 204) = &HB4A84FE0
        blf_S(0, 205) = &HFD13E0B7
        blf_S(0, 206) = &H7CC43B81
        blf_S(0, 207) = &HD2ADA8D9
        blf_S(0, 208) = &H165FA266
        blf_S(0, 209) = &H80957705
        blf_S(0, 210) = &H93CC7314
        blf_S(0, 211) = &H211A1477
        blf_S(0, 212) = &HE6AD2065
        blf_S(0, 213) = &H77B5FA86
        blf_S(0, 214) = &HC75442F5
        blf_S(0, 215) = &HFB9D35CF
        blf_S(0, 216) = &HEBCDAF0C
        blf_S(0, 217) = &H7B3E89A0
        blf_S(0, 218) = &HD6411BD3
        blf_S(0, 219) = &HAE1E7E49
        blf_S(0, 220) = &H250E2D
        blf_S(0, 221) = &H2071B35E
        blf_S(0, 222) = &H226800BB
        blf_S(0, 223) = &H57B8E0AF
        blf_S(0, 224) = &H2464369B
        blf_S(0, 225) = &HF009B91E
        blf_S(0, 226) = &H5563911D
        blf_S(0, 227) = &H59DFA6AA
        blf_S(0, 228) = &H78C14389
        blf_S(0, 229) = &HD95A537F
        blf_S(0, 230) = &H207D5BA2
        blf_S(0, 231) = &H2E5B9C5
        blf_S(0, 232) = &H83260376
        blf_S(0, 233) = &H6295CFA9
        blf_S(0, 234) = &H11C81968
        blf_S(0, 235) = &H4E734A41
        blf_S(0, 236) = &HB3472DCA
        blf_S(0, 237) = &H7B14A94A
        blf_S(0, 238) = &H1B510052
        blf_S(0, 239) = &H9A532915
        blf_S(0, 240) = &HD60F573F
        blf_S(0, 241) = &HBC9BC6E4
        blf_S(0, 242) = &H2B60A476
        blf_S(0, 243) = &H81E67400
        blf_S(0, 244) = &H8BA6FB5
        blf_S(0, 245) = &H571BE91F
        blf_S(0, 246) = &HF296EC6B
        blf_S(0, 247) = &H2A0DD915
        blf_S(0, 248) = &HB6636521
        blf_S(0, 249) = &HE7B9F9B6
        blf_S(0, 250) = &HFF34052E
        blf_S(0, 251) = &HC5855664
        blf_S(0, 252) = &H53B02D5D
        blf_S(0, 253) = &HA99F8FA1
        blf_S(0, 254) = &H8BA4799
        blf_S(0, 255) = &H6E85076A

        blf_S(1, 0) = &H4B7A70E9
        blf_S(1, 1) = &HB5B32944
        blf_S(1, 2) = &HDB75092E
        blf_S(1, 3) = &HC4192623
        blf_S(1, 4) = &HAD6EA6B0
        blf_S(1, 5) = &H49A7DF7D
        blf_S(1, 6) = &H9CEE60B8
        blf_S(1, 7) = &H8FEDB266
        blf_S(1, 8) = &HECAA8C71
        blf_S(1, 9) = &H699A17FF
        blf_S(1, 10) = &H5664526C
        blf_S(1, 11) = &HC2B19EE1
        blf_S(1, 12) = &H193602A5
        blf_S(1, 13) = &H75094C29
        blf_S(1, 14) = &HA0591340
        blf_S(1, 15) = &HE4183A3E
        blf_S(1, 16) = &H3F54989A
        blf_S(1, 17) = &H5B429D65
        blf_S(1, 18) = &H6B8FE4D6
        blf_S(1, 19) = &H99F73FD6
        blf_S(1, 20) = &HA1D29C07
        blf_S(1, 21) = &HEFE830F5
        blf_S(1, 22) = &H4D2D38E6
        blf_S(1, 23) = &HF0255DC1
        blf_S(1, 24) = &H4CDD2086
        blf_S(1, 25) = &H8470EB26
        blf_S(1, 26) = &H6382E9C6
        blf_S(1, 27) = &H21ECC5E
        blf_S(1, 28) = &H9686B3F
        blf_S(1, 29) = &H3EBAEFC9
        blf_S(1, 30) = &H3C971814
        blf_S(1, 31) = &H6B6A70A1
        blf_S(1, 32) = &H687F3584
        blf_S(1, 33) = &H52A0E286
        blf_S(1, 34) = &HB79C5305
        blf_S(1, 35) = &HAA500737
        blf_S(1, 36) = &H3E07841C
        blf_S(1, 37) = &H7FDEAE5C
        blf_S(1, 38) = &H8E7D44EC
        blf_S(1, 39) = &H5716F2B8
        blf_S(1, 40) = &HB03ADA37
        blf_S(1, 41) = &HF0500C0D
        blf_S(1, 42) = &HF01C1F04
        blf_S(1, 43) = &H200B3FF
        blf_S(1, 44) = &HAE0CF51A
        blf_S(1, 45) = &H3CB574B2
        blf_S(1, 46) = &H25837A58
        blf_S(1, 47) = &HDC0921BD
        blf_S(1, 48) = &HD19113F9
        blf_S(1, 49) = &H7CA92FF6
        blf_S(1, 50) = &H94324773
        blf_S(1, 51) = &H22F54701
        blf_S(1, 52) = &H3AE5E581
        blf_S(1, 53) = &H37C2DADC
        blf_S(1, 54) = &HC8B57634
        blf_S(1, 55) = &H9AF3DDA7
        blf_S(1, 56) = &HA9446146
        blf_S(1, 57) = &HFD0030E
        blf_S(1, 58) = &HECC8C73E
        blf_S(1, 59) = &HA4751E41
        blf_S(1, 60) = &HE238CD99
        blf_S(1, 61) = &H3BEA0E2F
        blf_S(1, 62) = &H3280BBA1
        blf_S(1, 63) = &H183EB331
        blf_S(1, 64) = &H4E548B38
        blf_S(1, 65) = &H4F6DB908
        blf_S(1, 66) = &H6F420D03
        blf_S(1, 67) = &HF60A04BF
        blf_S(1, 68) = &H2CB81290
        blf_S(1, 69) = &H24977C79
        blf_S(1, 70) = &H5679B072
        blf_S(1, 71) = &HBCAF89AF
        blf_S(1, 72) = &HDE9A771F
        blf_S(1, 73) = &HD9930810
        blf_S(1, 74) = &HB38BAE12
        blf_S(1, 75) = &HDCCF3F2E
        blf_S(1, 76) = &H5512721F
        blf_S(1, 77) = &H2E6B7124
        blf_S(1, 78) = &H501ADDE6
        blf_S(1, 79) = &H9F84CD87
        blf_S(1, 80) = &H7A584718
        blf_S(1, 81) = &H7408DA17
        blf_S(1, 82) = &HBC9F9ABC
        blf_S(1, 83) = &HE94B7D8C
        blf_S(1, 84) = &HEC7AEC3A
        blf_S(1, 85) = &HDB851DFA
        blf_S(1, 86) = &H63094366
        blf_S(1, 87) = &HC464C3D2
        blf_S(1, 88) = &HEF1C1847
        blf_S(1, 89) = &H3215D908
        blf_S(1, 90) = &HDD433B37
        blf_S(1, 91) = &H24C2BA16
        blf_S(1, 92) = &H12A14D43
        blf_S(1, 93) = &H2A65C451
        blf_S(1, 94) = &H50940002
        blf_S(1, 95) = &H133AE4DD
        blf_S(1, 96) = &H71DFF89E
        blf_S(1, 97) = &H10314E55
        blf_S(1, 98) = &H81AC77D6
        blf_S(1, 99) = &H5F11199B
        blf_S(1, 100) = &H43556F1
        blf_S(1, 101) = &HD7A3C76B
        blf_S(1, 102) = &H3C11183B
        blf_S(1, 103) = &H5924A509
        blf_S(1, 104) = &HF28FE6ED
        blf_S(1, 105) = &H97F1FBFA
        blf_S(1, 106) = &H9EBABF2C
        blf_S(1, 107) = &H1E153C6E
        blf_S(1, 108) = &H86E34570
        blf_S(1, 109) = &HEAE96FB1
        blf_S(1, 110) = &H860E5E0A
        blf_S(1, 111) = &H5A3E2AB3
        blf_S(1, 112) = &H771FE71C
        blf_S(1, 113) = &H4E3D06FA
        blf_S(1, 114) = &H2965DCB9
        blf_S(1, 115) = &H99E71D0F
        blf_S(1, 116) = &H803E89D6
        blf_S(1, 117) = &H5266C825
        blf_S(1, 118) = &H2E4CC978
        blf_S(1, 119) = &H9C10B36A
        blf_S(1, 120) = &HC6150EBA
        blf_S(1, 121) = &H94E2EA78
        blf_S(1, 122) = &HA5FC3C53
        blf_S(1, 123) = &H1E0A2DF4
        blf_S(1, 124) = &HF2F74EA7
        blf_S(1, 125) = &H361D2B3D
        blf_S(1, 126) = &H1939260F
        blf_S(1, 127) = &H19C27960
        blf_S(1, 128) = &H5223A708
        blf_S(1, 129) = &HF71312B6
        blf_S(1, 130) = &HEBADFE6E
        blf_S(1, 131) = &HEAC31F66
        blf_S(1, 132) = &HE3BC4595
        blf_S(1, 133) = &HA67BC883
        blf_S(1, 134) = &HB17F37D1
        blf_S(1, 135) = &H18CFF28
        blf_S(1, 136) = &HC332DDEF
        blf_S(1, 137) = &HBE6C5AA5
        blf_S(1, 138) = &H65582185
        blf_S(1, 139) = &H68AB9802
        blf_S(1, 140) = &HEECEA50F
        blf_S(1, 141) = &HDB2F953B
        blf_S(1, 142) = &H2AEF7DAD
        blf_S(1, 143) = &H5B6E2F84
        blf_S(1, 144) = &H1521B628
        blf_S(1, 145) = &H29076170
        blf_S(1, 146) = &HECDD4775
        blf_S(1, 147) = &H619F1510
        blf_S(1, 148) = &H13CCA830
        blf_S(1, 149) = &HEB61BD96
        blf_S(1, 150) = &H334FE1E
        blf_S(1, 151) = &HAA0363CF
        blf_S(1, 152) = &HB5735C90
        blf_S(1, 153) = &H4C70A239
        blf_S(1, 154) = &HD59E9E0B
        blf_S(1, 155) = &HCBAADE14
        blf_S(1, 156) = &HEECC86BC
        blf_S(1, 157) = &H60622CA7
        blf_S(1, 158) = &H9CAB5CAB
        blf_S(1, 159) = &HB2F3846E
        blf_S(1, 160) = &H648B1EAF
        blf_S(1, 161) = &H19BDF0CA
        blf_S(1, 162) = &HA02369B9
        blf_S(1, 163) = &H655ABB50
        blf_S(1, 164) = &H40685A32
        blf_S(1, 165) = &H3C2AB4B3
        blf_S(1, 166) = &H319EE9D5
        blf_S(1, 167) = &HC021B8F7
        blf_S(1, 168) = &H9B540B19
        blf_S(1, 169) = &H875FA099
        blf_S(1, 170) = &H95F7997E
        blf_S(1, 171) = &H623D7DA8
        blf_S(1, 172) = &HF837889A
        blf_S(1, 173) = &H97E32D77
        blf_S(1, 174) = &H11ED935F
        blf_S(1, 175) = &H16681281
        blf_S(1, 176) = &HE358829
        blf_S(1, 177) = &HC7E61FD6
        blf_S(1, 178) = &H96DEDFA1
        blf_S(1, 179) = &H7858BA99
        blf_S(1, 180) = &H57F584A5
        blf_S(1, 181) = &H1B227263
        blf_S(1, 182) = &H9B83C3FF
        blf_S(1, 183) = &H1AC24696
        blf_S(1, 184) = &HCDB30AEB
        blf_S(1, 185) = &H532E3054
        blf_S(1, 186) = &H8FD948E4
        blf_S(1, 187) = &H6DBC3128
        blf_S(1, 188) = &H58EBF2EF
        blf_S(1, 189) = &H34C6FFEA
        blf_S(1, 190) = &HFE28ED61
        blf_S(1, 191) = &HEE7C3C73
        blf_S(1, 192) = &H5D4A14D9
        blf_S(1, 193) = &HE864B7E3
        blf_S(1, 194) = &H42105D14
        blf_S(1, 195) = &H203E13E0
        blf_S(1, 196) = &H45EEE2B6
        blf_S(1, 197) = &HA3AAABEA
        blf_S(1, 198) = &HDB6C4F15
        blf_S(1, 199) = &HFACB4FD0
        blf_S(1, 200) = &HC742F442
        blf_S(1, 201) = &HEF6ABBB5
        blf_S(1, 202) = &H654F3B1D
        blf_S(1, 203) = &H41CD2105
        blf_S(1, 204) = &HD81E799E
        blf_S(1, 205) = &H86854DC7
        blf_S(1, 206) = &HE44B476A
        blf_S(1, 207) = &H3D816250
        blf_S(1, 208) = &HCF62A1F2
        blf_S(1, 209) = &H5B8D2646
        blf_S(1, 210) = &HFC8883A0
        blf_S(1, 211) = &HC1C7B6A3
        blf_S(1, 212) = &H7F1524C3
        blf_S(1, 213) = &H69CB7492
        blf_S(1, 214) = &H47848A0B
        blf_S(1, 215) = &H5692B285
        blf_S(1, 216) = &H95BBF00
        blf_S(1, 217) = &HAD19489D
        blf_S(1, 218) = &H1462B174
        blf_S(1, 219) = &H23820E00
        blf_S(1, 220) = &H58428D2A
        blf_S(1, 221) = &HC55F5EA
        blf_S(1, 222) = &H1DADF43E
        blf_S(1, 223) = &H233F7061
        blf_S(1, 224) = &H3372F092
        blf_S(1, 225) = &H8D937E41
        blf_S(1, 226) = &HD65FECF1
        blf_S(1, 227) = &H6C223BDB
        blf_S(1, 228) = &H7CDE3759
        blf_S(1, 229) = &HCBEE7460
        blf_S(1, 230) = &H4085F2A7
        blf_S(1, 231) = &HCE77326E
        blf_S(1, 232) = &HA6078084
        blf_S(1, 233) = &H19F8509E
        blf_S(1, 234) = &HE8EFD855
        blf_S(1, 235) = &H61D99735
        blf_S(1, 236) = &HA969A7AA
        blf_S(1, 237) = &HC50C06C2
        blf_S(1, 238) = &H5A04ABFC
        blf_S(1, 239) = &H800BCADC
        blf_S(1, 240) = &H9E447A2E
        blf_S(1, 241) = &HC3453484
        blf_S(1, 242) = &HFDD56705
        blf_S(1, 243) = &HE1E9EC9
        blf_S(1, 244) = &HDB73DBD3
        blf_S(1, 245) = &H105588CD
        blf_S(1, 246) = &H675FDA79
        blf_S(1, 247) = &HE3674340
        blf_S(1, 248) = &HC5C43465
        blf_S(1, 249) = &H713E38D8
        blf_S(1, 250) = &H3D28F89E
        blf_S(1, 251) = &HF16DFF20
        blf_S(1, 252) = &H153E21E7
        blf_S(1, 253) = &H8FB03D4A
        blf_S(1, 254) = &HE6E39F2B
        blf_S(1, 255) = &HDB83ADF7

        blf_S(2, 0) = &HE93D5A68
        blf_S(2, 1) = &H948140F7
        blf_S(2, 2) = &HF64C261C
        blf_S(2, 3) = &H94692934
        blf_S(2, 4) = &H411520F7
        blf_S(2, 5) = &H7602D4F7
        blf_S(2, 6) = &HBCF46B2E
        blf_S(2, 7) = &HD4A20068
        blf_S(2, 8) = &HD4082471
        blf_S(2, 9) = &H3320F46A
        blf_S(2, 10) = &H43B7D4B7
        blf_S(2, 11) = &H500061AF
        blf_S(2, 12) = &H1E39F62E
        blf_S(2, 13) = &H97244546
        blf_S(2, 14) = &H14214F74
        blf_S(2, 15) = &HBF8B8840
        blf_S(2, 16) = &H4D95FC1D
        blf_S(2, 17) = &H96B591AF
        blf_S(2, 18) = &H70F4DDD3
        blf_S(2, 19) = &H66A02F45
        blf_S(2, 20) = &HBFBC09EC
        blf_S(2, 21) = &H3BD9785
        blf_S(2, 22) = &H7FAC6DD0
        blf_S(2, 23) = &H31CB8504
        blf_S(2, 24) = &H96EB27B3
        blf_S(2, 25) = &H55FD3941
        blf_S(2, 26) = &HDA2547E6
        blf_S(2, 27) = &HABCA0A9A
        blf_S(2, 28) = &H28507825
        blf_S(2, 29) = &H530429F4
        blf_S(2, 30) = &HA2C86DA
        blf_S(2, 31) = &HE9B66DFB
        blf_S(2, 32) = &H68DC1462
        blf_S(2, 33) = &HD7486900
        blf_S(2, 34) = &H680EC0A4
        blf_S(2, 35) = &H27A18DEE
        blf_S(2, 36) = &H4F3FFEA2
        blf_S(2, 37) = &HE887AD8C
        blf_S(2, 38) = &HB58CE006
        blf_S(2, 39) = &H7AF4D6B6
        blf_S(2, 40) = &HAACE1E7C
        blf_S(2, 41) = &HD3375FEC
        blf_S(2, 42) = &HCE78A399
        blf_S(2, 43) = &H406B2A42
        blf_S(2, 44) = &H20FE9E35
        blf_S(2, 45) = &HD9F385B9
        blf_S(2, 46) = &HEE39D7AB
        blf_S(2, 47) = &H3B124E8B
        blf_S(2, 48) = &H1DC9FAF7
        blf_S(2, 49) = &H4B6D1856
        blf_S(2, 50) = &H26A36631
        blf_S(2, 51) = &HEAE397B2
        blf_S(2, 52) = &H3A6EFA74
        blf_S(2, 53) = &HDD5B4332
        blf_S(2, 54) = &H6841E7F7
        blf_S(2, 55) = &HCA7820FB
        blf_S(2, 56) = &HFB0AF54E
        blf_S(2, 57) = &HD8FEB397
        blf_S(2, 58) = &H454056AC
        blf_S(2, 59) = &HBA489527
        blf_S(2, 60) = &H55533A3A
        blf_S(2, 61) = &H20838D87
        blf_S(2, 62) = &HFE6BA9B7
        blf_S(2, 63) = &HD096954B
        blf_S(2, 64) = &H55A867BC
        blf_S(2, 65) = &HA1159A58
        blf_S(2, 66) = &HCCA92963
        blf_S(2, 67) = &H99E1DB33
        blf_S(2, 68) = &HA62A4A56
        blf_S(2, 69) = &H3F3125F9
        blf_S(2, 70) = &H5EF47E1C
        blf_S(2, 71) = &H9029317C
        blf_S(2, 72) = &HFDF8E802
        blf_S(2, 73) = &H4272F70
        blf_S(2, 74) = &H80BB155C
        blf_S(2, 75) = &H5282CE3
        blf_S(2, 76) = &H95C11548
        blf_S(2, 77) = &HE4C66D22
        blf_S(2, 78) = &H48C1133F
        blf_S(2, 79) = &HC70F86DC
        blf_S(2, 80) = &H7F9C9EE
        blf_S(2, 81) = &H41041F0F
        blf_S(2, 82) = &H404779A4
        blf_S(2, 83) = &H5D886E17
        blf_S(2, 84) = &H325F51EB
        blf_S(2, 85) = &HD59BC0D1
        blf_S(2, 86) = &HF2BCC18F
        blf_S(2, 87) = &H41113564
        blf_S(2, 88) = &H257B7834
        blf_S(2, 89) = &H602A9C60
        blf_S(2, 90) = &HDFF8E8A3
        blf_S(2, 91) = &H1F636C1B
        blf_S(2, 92) = &HE12B4C2
        blf_S(2, 93) = &H2E1329E
        blf_S(2, 94) = &HAF664FD1
        blf_S(2, 95) = &HCAD18115
        blf_S(2, 96) = &H6B2395E0
        blf_S(2, 97) = &H333E92E1
        blf_S(2, 98) = &H3B240B62
        blf_S(2, 99) = &HEEBEB922
        blf_S(2, 100) = &H85B2A20E
        blf_S(2, 101) = &HE6BA0D99
        blf_S(2, 102) = &HDE720C8C
        blf_S(2, 103) = &H2DA2F728
        blf_S(2, 104) = &HD0127845
        blf_S(2, 105) = &H95B794FD
        blf_S(2, 106) = &H647D0862
        blf_S(2, 107) = &HE7CCF5F0
        blf_S(2, 108) = &H5449A36F
        blf_S(2, 109) = &H877D48FA
        blf_S(2, 110) = &HC39DFD27
        blf_S(2, 111) = &HF33E8D1E
        blf_S(2, 112) = &HA476341
        blf_S(2, 113) = &H992EFF74
        blf_S(2, 114) = &H3A6F6EAB
        blf_S(2, 115) = &HF4F8FD37
        blf_S(2, 116) = &HA812DC60
        blf_S(2, 117) = &HA1EBDDF8
        blf_S(2, 118) = &H991BE14C
        blf_S(2, 119) = &HDB6E6B0D
        blf_S(2, 120) = &HC67B5510
        blf_S(2, 121) = &H6D672C37
        blf_S(2, 122) = &H2765D43B
        blf_S(2, 123) = &HDCD0E804
        blf_S(2, 124) = &HF1290DC7
        blf_S(2, 125) = &HCC00FFA3
        blf_S(2, 126) = &HB5390F92
        blf_S(2, 127) = &H690FED0B
        blf_S(2, 128) = &H667B9FFB
        blf_S(2, 129) = &HCEDB7D9C
        blf_S(2, 130) = &HA091CF0B
        blf_S(2, 131) = &HD9155EA3
        blf_S(2, 132) = &HBB132F88
        blf_S(2, 133) = &H515BAD24
        blf_S(2, 134) = &H7B9479BF
        blf_S(2, 135) = &H763BD6EB
        blf_S(2, 136) = &H37392EB3
        blf_S(2, 137) = &HCC115979
        blf_S(2, 138) = &H8026E297
        blf_S(2, 139) = &HF42E312D
        blf_S(2, 140) = &H6842ADA7
        blf_S(2, 141) = &HC66A2B3B
        blf_S(2, 142) = &H12754CCC
        blf_S(2, 143) = &H782EF11C
        blf_S(2, 144) = &H6A124237
        blf_S(2, 145) = &HB79251E7
        blf_S(2, 146) = &H6A1BBE6
        blf_S(2, 147) = &H4BFB6350
        blf_S(2, 148) = &H1A6B1018
        blf_S(2, 149) = &H11CAEDFA
        blf_S(2, 150) = &H3D25BDD8
        blf_S(2, 151) = &HE2E1C3C9
        blf_S(2, 152) = &H44421659
        blf_S(2, 153) = &HA121386
        blf_S(2, 154) = &HD90CEC6E
        blf_S(2, 155) = &HD5ABEA2A
        blf_S(2, 156) = &H64AF674E
        blf_S(2, 157) = &HDA86A85F
        blf_S(2, 158) = &HBEBFE988
        blf_S(2, 159) = &H64E4C3FE
        blf_S(2, 160) = &H9DBC8057
        blf_S(2, 161) = &HF0F7C086
        blf_S(2, 162) = &H60787BF8
        blf_S(2, 163) = &H6003604D
        blf_S(2, 164) = &HD1FD8346
        blf_S(2, 165) = &HF6381FB0
        blf_S(2, 166) = &H7745AE04
        blf_S(2, 167) = &HD736FCCC
        blf_S(2, 168) = &H83426B33
        blf_S(2, 169) = &HF01EAB71
        blf_S(2, 170) = &HB0804187
        blf_S(2, 171) = &H3C005E5F
        blf_S(2, 172) = &H77A057BE
        blf_S(2, 173) = &HBDE8AE24
        blf_S(2, 174) = &H55464299
        blf_S(2, 175) = &HBF582E61
        blf_S(2, 176) = &H4E58F48F
        blf_S(2, 177) = &HF2DDFDA2
        blf_S(2, 178) = &HF474EF38
        blf_S(2, 179) = &H8789BDC2
        blf_S(2, 180) = &H5366F9C3
        blf_S(2, 181) = &HC8B38E74
        blf_S(2, 182) = &HB475F255
        blf_S(2, 183) = &H46FCD9B9
        blf_S(2, 184) = &H7AEB2661
        blf_S(2, 185) = &H8B1DDF84
        blf_S(2, 186) = &H846A0E79
        blf_S(2, 187) = &H915F95E2
        blf_S(2, 188) = &H466E598E
        blf_S(2, 189) = &H20B45770
        blf_S(2, 190) = &H8CD55591
        blf_S(2, 191) = &HC902DE4C
        blf_S(2, 192) = &HB90BACE1
        blf_S(2, 193) = &HBB8205D0
        blf_S(2, 194) = &H11A86248
        blf_S(2, 195) = &H7574A99E
        blf_S(2, 196) = &HB77F19B6
        blf_S(2, 197) = &HE0A9DC09
        blf_S(2, 198) = &H662D09A1
        blf_S(2, 199) = &HC4324633
        blf_S(2, 200) = &HE85A1F02
        blf_S(2, 201) = &H9F0BE8C
        blf_S(2, 202) = &H4A99A025
        blf_S(2, 203) = &H1D6EFE10
        blf_S(2, 204) = &H1AB93D1D
        blf_S(2, 205) = &HBA5A4DF
        blf_S(2, 206) = &HA186F20F
        blf_S(2, 207) = &H2868F169
        blf_S(2, 208) = &HDCB7DA83
        blf_S(2, 209) = &H573906FE
        blf_S(2, 210) = &HA1E2CE9B
        blf_S(2, 211) = &H4FCD7F52
        blf_S(2, 212) = &H50115E01
        blf_S(2, 213) = &HA70683FA
        blf_S(2, 214) = &HA002B5C4
        blf_S(2, 215) = &HDE6D027
        blf_S(2, 216) = &H9AF88C27
        blf_S(2, 217) = &H773F8641
        blf_S(2, 218) = &HC3604C06
        blf_S(2, 219) = &H61A806B5
        blf_S(2, 220) = &HF0177A28
        blf_S(2, 221) = &HC0F586E0
        blf_S(2, 222) = &H6058AA
        blf_S(2, 223) = &H30DC7D62
        blf_S(2, 224) = &H11E69ED7
        blf_S(2, 225) = &H2338EA63
        blf_S(2, 226) = &H53C2DD94
        blf_S(2, 227) = &HC2C21634
        blf_S(2, 228) = &HBBCBEE56
        blf_S(2, 229) = &H90BCB6DE
        blf_S(2, 230) = &HEBFC7DA1
        blf_S(2, 231) = &HCE591D76
        blf_S(2, 232) = &H6F05E409
        blf_S(2, 233) = &H4B7C0188
        blf_S(2, 234) = &H39720A3D
        blf_S(2, 235) = &H7C927C24
        blf_S(2, 236) = &H86E3725F
        blf_S(2, 237) = &H724D9DB9
        blf_S(2, 238) = &H1AC15BB4
        blf_S(2, 239) = &HD39EB8FC
        blf_S(2, 240) = &HED545578
        blf_S(2, 241) = &H8FCA5B5
        blf_S(2, 242) = &HD83D7CD3
        blf_S(2, 243) = &H4DAD0FC4
        blf_S(2, 244) = &H1E50EF5E
        blf_S(2, 245) = &HB161E6F8
        blf_S(2, 246) = &HA28514D9
        blf_S(2, 247) = &H6C51133C
        blf_S(2, 248) = &H6FD5C7E7
        blf_S(2, 249) = &H56E14EC4
        blf_S(2, 250) = &H362ABFCE
        blf_S(2, 251) = &HDDC6C837
        blf_S(2, 252) = &HD79A3234
        blf_S(2, 253) = &H92638212
        blf_S(2, 254) = &H670EFA8E
        blf_S(2, 255) = &H406000E0

        blf_S(3, 0) = &H3A39CE37
        blf_S(3, 1) = &HD3FAF5CF
        blf_S(3, 2) = &HABC27737
        blf_S(3, 3) = &H5AC52D1B
        blf_S(3, 4) = &H5CB0679E
        blf_S(3, 5) = &H4FA33742
        blf_S(3, 6) = &HD3822740
        blf_S(3, 7) = &H99BC9BBE
        blf_S(3, 8) = &HD5118E9D
        blf_S(3, 9) = &HBF0F7315
        blf_S(3, 10) = &HD62D1C7E
        blf_S(3, 11) = &HC700C47B
        blf_S(3, 12) = &HB78C1B6B
        blf_S(3, 13) = &H21A19045
        blf_S(3, 14) = &HB26EB1BE
        blf_S(3, 15) = &H6A366EB4
        blf_S(3, 16) = &H5748AB2F
        blf_S(3, 17) = &HBC946E79
        blf_S(3, 18) = &HC6A376D2
        blf_S(3, 19) = &H6549C2C8
        blf_S(3, 20) = &H530FF8EE
        blf_S(3, 21) = &H468DDE7D
        blf_S(3, 22) = &HD5730A1D
        blf_S(3, 23) = &H4CD04DC6
        blf_S(3, 24) = &H2939BBDB
        blf_S(3, 25) = &HA9BA4650
        blf_S(3, 26) = &HAC9526E8
        blf_S(3, 27) = &HBE5EE304
        blf_S(3, 28) = &HA1FAD5F0
        blf_S(3, 29) = &H6A2D519A
        blf_S(3, 30) = &H63EF8CE2
        blf_S(3, 31) = &H9A86EE22
        blf_S(3, 32) = &HC089C2B8
        blf_S(3, 33) = &H43242EF6
        blf_S(3, 34) = &HA51E03AA
        blf_S(3, 35) = &H9CF2D0A4
        blf_S(3, 36) = &H83C061BA
        blf_S(3, 37) = &H9BE96A4D
        blf_S(3, 38) = &H8FE51550
        blf_S(3, 39) = &HBA645BD6
        blf_S(3, 40) = &H2826A2F9
        blf_S(3, 41) = &HA73A3AE1
        blf_S(3, 42) = &H4BA99586
        blf_S(3, 43) = &HEF5562E9
        blf_S(3, 44) = &HC72FEFD3
        blf_S(3, 45) = &HF752F7DA
        blf_S(3, 46) = &H3F046F69
        blf_S(3, 47) = &H77FA0A59
        blf_S(3, 48) = &H80E4A915
        blf_S(3, 49) = &H87B08601
        blf_S(3, 50) = &H9B09E6AD
        blf_S(3, 51) = &H3B3EE593
        blf_S(3, 52) = &HE990FD5A
        blf_S(3, 53) = &H9E34D797
        blf_S(3, 54) = &H2CF0B7D9
        blf_S(3, 55) = &H22B8B51
        blf_S(3, 56) = &H96D5AC3A
        blf_S(3, 57) = &H17DA67D
        blf_S(3, 58) = &HD1CF3ED6
        blf_S(3, 59) = &H7C7D2D28
        blf_S(3, 60) = &H1F9F25CF
        blf_S(3, 61) = &HADF2B89B
        blf_S(3, 62) = &H5AD6B472
        blf_S(3, 63) = &H5A88F54C
        blf_S(3, 64) = &HE029AC71
        blf_S(3, 65) = &HE019A5E6
        blf_S(3, 66) = &H47B0ACFD
        blf_S(3, 67) = &HED93FA9B
        blf_S(3, 68) = &HE8D3C48D
        blf_S(3, 69) = &H283B57CC
        blf_S(3, 70) = &HF8D56629
        blf_S(3, 71) = &H79132E28
        blf_S(3, 72) = &H785F0191
        blf_S(3, 73) = &HED756055
        blf_S(3, 74) = &HF7960E44
        blf_S(3, 75) = &HE3D35E8C
        blf_S(3, 76) = &H15056DD4
        blf_S(3, 77) = &H88F46DBA
        blf_S(3, 78) = &H3A16125
        blf_S(3, 79) = &H564F0BD
        blf_S(3, 80) = &HC3EB9E15
        blf_S(3, 81) = &H3C9057A2
        blf_S(3, 82) = &H97271AEC
        blf_S(3, 83) = &HA93A072A
        blf_S(3, 84) = &H1B3F6D9B
        blf_S(3, 85) = &H1E6321F5
        blf_S(3, 86) = &HF59C66FB
        blf_S(3, 87) = &H26DCF319
        blf_S(3, 88) = &H7533D928
        blf_S(3, 89) = &HB155FDF5
        blf_S(3, 90) = &H3563482
        blf_S(3, 91) = &H8ABA3CBB
        blf_S(3, 92) = &H28517711
        blf_S(3, 93) = &HC20AD9F8
        blf_S(3, 94) = &HABCC5167
        blf_S(3, 95) = &HCCAD925F
        blf_S(3, 96) = &H4DE81751
        blf_S(3, 97) = &H3830DC8E
        blf_S(3, 98) = &H379D5862
        blf_S(3, 99) = &H9320F991
        blf_S(3, 100) = &HEA7A90C2
        blf_S(3, 101) = &HFB3E7BCE
        blf_S(3, 102) = &H5121CE64
        blf_S(3, 103) = &H774FBE32
        blf_S(3, 104) = &HA8B6E37E
        blf_S(3, 105) = &HC3293D46
        blf_S(3, 106) = &H48DE5369
        blf_S(3, 107) = &H6413E680
        blf_S(3, 108) = &HA2AE0810
        blf_S(3, 109) = &HDD6DB224
        blf_S(3, 110) = &H69852DFD
        blf_S(3, 111) = &H9072166
        blf_S(3, 112) = &HB39A460A
        blf_S(3, 113) = &H6445C0DD
        blf_S(3, 114) = &H586CDECF
        blf_S(3, 115) = &H1C20C8AE
        blf_S(3, 116) = &H5BBEF7DD
        blf_S(3, 117) = &H1B588D40
        blf_S(3, 118) = &HCCD2017F
        blf_S(3, 119) = &H6BB4E3BB
        blf_S(3, 120) = &HDDA26A7E
        blf_S(3, 121) = &H3A59FF45
        blf_S(3, 122) = &H3E350A44
        blf_S(3, 123) = &HBCB4CDD5
        blf_S(3, 124) = &H72EACEA8
        blf_S(3, 125) = &HFA6484BB
        blf_S(3, 126) = &H8D6612AE
        blf_S(3, 127) = &HBF3C6F47
        blf_S(3, 128) = &HD29BE463
        blf_S(3, 129) = &H542F5D9E
        blf_S(3, 130) = &HAEC2771B
        blf_S(3, 131) = &HF64E6370
        blf_S(3, 132) = &H740E0D8D
        blf_S(3, 133) = &HE75B1357
        blf_S(3, 134) = &HF8721671
        blf_S(3, 135) = &HAF537D5D
        blf_S(3, 136) = &H4040CB08
        blf_S(3, 137) = &H4EB4E2CC
        blf_S(3, 138) = &H34D2466A
        blf_S(3, 139) = &H115AF84
        blf_S(3, 140) = &HE1B00428
        blf_S(3, 141) = &H95983A1D
        blf_S(3, 142) = &H6B89FB4
        blf_S(3, 143) = &HCE6EA048
        blf_S(3, 144) = &H6F3F3B82
        blf_S(3, 145) = &H3520AB82
        blf_S(3, 146) = &H11A1D4B
        blf_S(3, 147) = &H277227F8
        blf_S(3, 148) = &H611560B1
        blf_S(3, 149) = &HE7933FDC
        blf_S(3, 150) = &HBB3A792B
        blf_S(3, 151) = &H344525BD
        blf_S(3, 152) = &HA08839E1
        blf_S(3, 153) = &H51CE794B
        blf_S(3, 154) = &H2F32C9B7
        blf_S(3, 155) = &HA01FBAC9
        blf_S(3, 156) = &HE01CC87E
        blf_S(3, 157) = &HBCC7D1F6
        blf_S(3, 158) = &HCF0111C3
        blf_S(3, 159) = &HA1E8AAC7
        blf_S(3, 160) = &H1A908749
        blf_S(3, 161) = &HD44FBD9A
        blf_S(3, 162) = &HD0DADECB
        blf_S(3, 163) = &HD50ADA38
        blf_S(3, 164) = &H339C32A
        blf_S(3, 165) = &HC6913667
        blf_S(3, 166) = &H8DF9317C
        blf_S(3, 167) = &HE0B12B4F
        blf_S(3, 168) = &HF79E59B7
        blf_S(3, 169) = &H43F5BB3A
        blf_S(3, 170) = &HF2D519FF
        blf_S(3, 171) = &H27D9459C
        blf_S(3, 172) = &HBF97222C
        blf_S(3, 173) = &H15E6FC2A
        blf_S(3, 174) = &HF91FC71
        blf_S(3, 175) = &H9B941525
        blf_S(3, 176) = &HFAE59361
        blf_S(3, 177) = &HCEB69CEB
        blf_S(3, 178) = &HC2A86459
        blf_S(3, 179) = &H12BAA8D1
        blf_S(3, 180) = &HB6C1075E
        blf_S(3, 181) = &HE3056A0C
        blf_S(3, 182) = &H10D25065
        blf_S(3, 183) = &HCB03A442
        blf_S(3, 184) = &HE0EC6E0E
        blf_S(3, 185) = &H1698DB3B
        blf_S(3, 186) = &H4C98A0BE
        blf_S(3, 187) = &H3278E964
        blf_S(3, 188) = &H9F1F9532
        blf_S(3, 189) = &HE0D392DF
        blf_S(3, 190) = &HD3A0342B
        blf_S(3, 191) = &H8971F21E
        blf_S(3, 192) = &H1B0A7441
        blf_S(3, 193) = &H4BA3348C
        blf_S(3, 194) = &HC5BE7120
        blf_S(3, 195) = &HC37632D8
        blf_S(3, 196) = &HDF359F8D
        blf_S(3, 197) = &H9B992F2E
        blf_S(3, 198) = &HE60B6F47
        blf_S(3, 199) = &HFE3F11D
        blf_S(3, 200) = &HE54CDA54
        blf_S(3, 201) = &H1EDAD891
        blf_S(3, 202) = &HCE6279CF
        blf_S(3, 203) = &HCD3E7E6F
        blf_S(3, 204) = &H1618B166
        blf_S(3, 205) = &HFD2C1D05
        blf_S(3, 206) = &H848FD2C5
        blf_S(3, 207) = &HF6FB2299
        blf_S(3, 208) = &HF523F357
        blf_S(3, 209) = &HA6327623
        blf_S(3, 210) = &H93A83531
        blf_S(3, 211) = &H56CCCD02
        blf_S(3, 212) = &HACF08162
        blf_S(3, 213) = &H5A75EBB5
        blf_S(3, 214) = &H6E163697
        blf_S(3, 215) = &H88D273CC
        blf_S(3, 216) = &HDE966292
        blf_S(3, 217) = &H81B949D0
        blf_S(3, 218) = &H4C50901B
        blf_S(3, 219) = &H71C65614
        blf_S(3, 220) = &HE6C6C7BD
        blf_S(3, 221) = &H327A140A
        blf_S(3, 222) = &H45E1D006
        blf_S(3, 223) = &HC3F27B9A
        blf_S(3, 224) = &HC9AA53FD
        blf_S(3, 225) = &H62A80F00
        blf_S(3, 226) = &HBB25BFE2
        blf_S(3, 227) = &H35BDD2F6
        blf_S(3, 228) = &H71126905
        blf_S(3, 229) = &HB2040222
        blf_S(3, 230) = &HB6CBCF7C
        blf_S(3, 231) = &HCD769C2B
        blf_S(3, 232) = &H53113EC0
        blf_S(3, 233) = &H1640E3D3
        blf_S(3, 234) = &H38ABBD60
        blf_S(3, 235) = &H2547ADF0
        blf_S(3, 236) = &HBA38209C
        blf_S(3, 237) = &HF746CE76
        blf_S(3, 238) = &H77AFA1C5
        blf_S(3, 239) = &H20756060
        blf_S(3, 240) = &H85CBFE4E
        blf_S(3, 241) = &H8AE88DD8
        blf_S(3, 242) = &H7AAAF9B0
        blf_S(3, 243) = &H4CF9AA7E
        blf_S(3, 244) = &H1948C25C
        blf_S(3, 245) = &H2FB8A8C
        blf_S(3, 246) = &H1C36AE4
        blf_S(3, 247) = &HD6EBE1F9
        blf_S(3, 248) = &H90D4F869
        blf_S(3, 249) = &HA65CDEA0
        blf_S(3, 250) = &H3F09252D
        blf_S(3, 251) = &HC208E69F
        blf_S(3, 252) = &HB74E6132
        blf_S(3, 253) = &HCE77E25B
        blf_S(3, 254) = &H578FDFE3
        blf_S(3, 255) = &H3AC372E6
    End Sub

End Class
