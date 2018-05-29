Imports ICSharpCode.SharpZipLib

Module modFunciones
    Public Function GetLlaveFromWS(ByVal dtrEmpresa As DataRow, ByVal DatosEquipo As Infoequipo, ByVal strEquipo As String, ByVal strIP As String, ByVal conn As OleDb.OleDbConnection) As Boolean
        Dim recux As New ClassCone
        Dim reccon1 As New Cblowfish
        Dim ws As New clsWs
        Dim Dt As New DataTable
        Dim HDD1 As String
        Dim candado As String
        Dim Security As New Pk1
        Dim SelloValidado As Boolean = False

        CanLla.empresas = dtrEmpresa!Empresa
        CanLla.PATRONAL = dtrEmpresa!RegPatronal.ToString
        CanLla.RFC = dtrEmpresa!RFC.ToString


        Dt = New DataTable
        HDD1 = SerieDisk("C:\")
        'candado = llave and llave = candado
        'candado = Mid(HDD1 & Space(11), 1, 11) & Mid(CanLla.empresas & Space(30), 1, 30) & Mid(dtrEmpresa!RFC.ToString & Space(12), 1, 12) & Mid("ePayRoll".PadRight(3, " "), 1, 3)
        candado = Mid(HDD1 & Space(11), 1, 11) & Mid(dtrEmpresa!Empresa & Space(30), 1, 30) & Mid(dtrEmpresa!RFC & Space(12), 1, 12) & "ePayRoll".PadRight(11, " ")
        candado = Security.Encriptarpaso1(candado)
        Dt = Ejecutarsql.RecDatatable("select * from tbl_certLlave where llave='" & candado & "'", conn)

        If Dt.Rows.Count > 0 Then
            Dim dtr As DataRow
            For Each dtr In Dt.Rows
                If Security.Verify_Sign(dtr!Candado, DatosEquipo.certificado, candado) Then
                    SelloValidado = True
                End If
            Next
        End If
        If Not SelloValidado Then
            Dim strDatosKey As String
            strDatosKey = strEquipo & "(" & strIP & ")"  '$Origen,
            strDatosKey &= "|" & dtrEmpresa!Numero.ToString  '$IDCliente,
            '$Empresa
            strDatosKey &= "|" & dtrEmpresa!Empresa.ToString
            '$Sucursal
            strDatosKey &= "|" & dtrEmpresa!Sucursal.ToString
            '$RFC,
            strDatosKey &= "|" & dtrEmpresa!RFC.ToString
            '$Sistema,
            strDatosKey &= "|" & "ePayroll"
            '$Equipo
            strDatosKey &= "|" & strEquipo
            '$SNEquipo
            strDatosKey &= "|" & Mid(HDD1 & Space(11), 1, 11)
            '$IP
            strDatosKey &= "|" & strIP
            '$candado
            strDatosKey &= "|" & candado

            Dim key As clsWs.KeyTypeResult
            key = ws.fxCheckKey(strDatosKey)


            If key.llaveOK = "SI" Then
                'INSERTA KEY EN DATABASE
                If Security.Verify_Sign(key.llave, DatosEquipo.certificado, candado) Then
                    Ejecutarsql.ExecuteSql("insert into tbl_certLlave(candado,llave)values('" & key.llave & "','" & candado & "')", conn)
                End If
                'extrae la key e inserta y vuelve a verificar.
                Dt = Ejecutarsql.RecDatatable("select * from tbl_certLlave where llave='" & candado & "'", conn)
                If Dt.Rows.Count > 0 Then
                    Dim dtr As DataRow
                    For Each dtr In Dt.Rows
                        If Security.Verify_Sign(dtr!Candado, DatosEquipo.certificado, candado) Then
                            SelloValidado = True
                        End If
                    Next
                End If
            End If

            If Not SelloValidado Then
                Dim frm As New frmCertificado2
                frm.conectar() = conn
                frm.lblCandado.Text = candado
                frm.txtCert.Text = DatosEquipo.certificado.ToString.Trim
                frm.ShowDialog()
                If Not frm.continuar Then
                    Exit Function
                End If
            End If
        End If
        Dt = Nothing
        Return SelloValidado
    End Function
    Public Function RegistraSolicitud(ByVal strDatos As String, ByVal strRutaCSD As String, ByVal strRutaKey As String, ByVal strEstadoAut As String, ByVal IDRequest As String) As Boolean
        Dim dirapp As String = My.Application.Info.DirectoryPath
        Dim ADatos() As String = strDatos.Split("|")

        If Not IO.File.Exists(dirapp & "\Solicitudes\Solicitud.xml") Then CreateFileSol(dirapp)
        Dim dtb As New DataTable
        dtb.TableName = "Solicitudes"
        dtb.ReadXmlSchema(dirapp & "\Solicitudes\Solicitud.xsd")
        dtb.ReadXml(dirapp & "\Solicitudes\Solicitud.xml")

        If dtb.Select("IDRequest=" & IDRequest & "AND Numero =" & ADatos(1) & " AND Sucursal =" & ADatos(3)).Length = 0 Then
            Dim dtr As DataRow
            dtr = dtb.NewRow
            With dtr
                !IDrequest = IDRequest
                !Numero = ADatos(1)
                !Sucursal = ADatos(3)
                !CSD = IO.Path.GetFileName(strRutaCSD)
                !Empresa = ADatos(2)
                !Fecha = Now
                !RFC = ADatos(4)
                !NoCSD = ADatos(5)
                !LlavePrivada = IO.Path.GetFileName(strRutaKey)

                !Nombre = ADatos(7)
                !Usuario = ADatos(8)
                !Passw = ADatos(9)
                !Email = ADatos(10)
                !Tel = ADatos(11)
                !EstadoAut = strEstadoAut
            End With
            dtb.Rows.Add(dtr)
            Dim strRuta As String
            strRuta = CreaPerfil(dtr!RFC.ToString)
            If IO.Directory.Exists(strRuta) Then
                If Not IO.Directory.Exists(strRuta & "\CSD") Then IO.Directory.CreateDirectory(strRuta & "\CSD")
                'Copia CSD
                IO.File.Copy(strRutaCSD, strRuta & "\CSD\" & IO.Path.GetFileName(dtr!noCSD.ToString) & ".cer", True)
                'Copia KEY
                IO.File.Copy(strRutaKey, strRuta & "\CSD\" & IO.Path.GetFileName(dtr!noCSD.ToString) & ".key", True)
                dtb.WriteXmlSchema(dirapp & "\Solicitudes\Solicitud.xsd")
                dtb.WriteXml(dirapp & "\Solicitudes\Solicitud.xml")
                Return True
            Else
                Return False
            End If

        Else
            MsgBox("Ya existe una solicitud para esta empresa", MsgBoxStyle.Exclamation, "AVISO")
            Return False
        End If
    End Function
    Private Function CreaPerfil(ByVal strRFC As String) As String
        Dim strRuta As String
        Dim dirapp As String = My.Application.Info.DirectoryPath
        strRuta = dirapp & "\Data\" & strRFC
        If Not IO.Directory.Exists(strRuta) Then IO.Directory.CreateDirectory(strRuta)
        If IO.Directory.Exists(strRuta) Then
            'Database 
            If Not IO.Directory.Exists(strRuta & "\BD") Then IO.Directory.CreateDirectory(strRuta & "\BD")
            If Not IO.Directory.Exists(strRuta & "\CLU") Then IO.Directory.CreateDirectory(strRuta & "\CLU")
            If Not IO.Directory.Exists(strRuta & "\CSD") Then IO.Directory.CreateDirectory(strRuta & "\CSD")
            If Not IO.Directory.Exists(strRuta & "\PFX") Then IO.Directory.CreateDirectory(strRuta & "\PFX")
            If Not IO.Directory.Exists(strRuta & "\RPT") Then IO.Directory.CreateDirectory(strRuta & "\RPT")
            If Not IO.File.Exists(strRuta & "\RPT\RXPAGO.rpt") Then
                IO.File.Copy(dirapp & "\RPT\RXPAGO.rpt", strRuta & "\RPT\RXPAGO.rpt", True)
            End If
            If Not IO.Directory.Exists(strRuta & "\XML") Then IO.Directory.CreateDirectory(strRuta & "\XML")
            If Not IO.Directory.Exists(strRuta & "\iMONITOR") Then IO.Directory.CreateDirectory(strRuta & "\iMONITOR")
            If Not IO.Directory.Exists(strRuta & "\RESPALDO") Then IO.Directory.CreateDirectory(strRuta & "\RESPALDO")
            If Not IO.Directory.Exists(strRuta & "\EMAIL") Then IO.Directory.CreateDirectory(strRuta & "\EMAIL")
            If IO.File.Exists(strRuta & "\BD\master.mdb") Then
                Return strRuta
            Else
                IO.File.Copy(dirapp & "\Config\master.mdb", strRuta & "\BD\master.mdb", True)
                Return strRuta
            End If
        Else
            Return ""
        End If
    End Function
    Public Sub CreateFileSol(ByVal strRuta As String)
        Dim dtb As New DataTable
        If Not IO.Directory.Exists(strRuta & "\Solicitudes") Then IO.Directory.CreateDirectory(strRuta & "\Solicitudes")
        dtb.TableName = "Solicitudes"
        With dtb.Columns
            .Add("IDRequest", Type.GetType("System.String"))
            .Add("Numero", Type.GetType("System.String"))
            .Add("Sucursal", Type.GetType("System.String"))
            .Add("CSD", Type.GetType("System.String"))
            .Add("Empresa", Type.GetType("System.String"))
            .Add("Fecha", Type.GetType("System.DateTime"))
            .Add("RFC", Type.GetType("System.String"))
            .Add("NOCSD", Type.GetType("System.String"))
            .Add("LlavePrivada", Type.GetType("System.String"))
            .Add("Nombre", Type.GetType("System.String"))
            .Add("Usuario", Type.GetType("System.String"))
            .Add("Passw", Type.GetType("System.String"))
            .Add("Email", Type.GetType("System.String"))
            .Add("Tel", Type.GetType("System.String"))
            .Add("EstadoAut", Type.GetType("System.String"))
        End With
        dtb.WriteXml(strRuta & "\Solicitudes\Solicitud.xml")
        dtb.WriteXmlSchema(strRuta & "\Solicitudes\Solicitud.xsd")
    End Sub
    Public Function LiberaClu(ByVal dtrSolicitud As DataRow, ByVal CLU() As Byte) As Boolean
        If Not IsNothing(CLU) Then
            'source hex string  
            Dim strRuta As String
            Dim strRutaZip As String
            Dim strRutaPFX As String
            Dim DirApp As String = My.Application.Info.DirectoryPath
            Dim Numero As String = dtrSolicitud!Numero.ToString
            Dim Sucursal As String = dtrSolicitud!Sucursal.ToString
            Dim strRFC As String = dtrSolicitud!RFC.ToString
            Dim strRutaFinal As String
            strRutaFinal = CreaPerfil(strRFC)
            If IO.Directory.Exists(strRutaFinal) Then
                strRuta = DirApp & "\Data\clu"
                If Not IO.Directory.Exists(strRuta) Then IO.Directory.CreateDirectory(strRuta)
                If Not IO.Directory.Exists(strRuta & "\" & strRFC) Then IO.Directory.CreateDirectory(strRuta & "\" & strRFC)
                strRutaPFX = DirApp & "\Data\" & dtrSolicitud!rfc.ToString & "\PFX"
                strRutaZip = strRuta & "\" & strRFC & ".zip"

                IO.File.WriteAllBytes(strRutaZip, CLU)
                If IO.File.Exists(strRutaZip) Then
                    'Descomprime archivo zip
                    strRutaZip = UnZIPXML(strRutaZip, strRuta & "\" & strRFC)

                    'RENOMBRA ARCHIVO(s) .cer y .pfx
                    For Each Str As String In IO.Directory.GetFiles(strRuta & "\" & strRFC, "*.cer")
                        'If Str <> strRuta & "\" & strRFC & "\CLU_" & strRFC & "_" & Numero & "_" & Sucursal & ".cer" Then
                        IO.File.Copy(Str, strRuta & "\CLU_" & strRFC & "_" & Numero & "_" & Sucursal & ".cer", True)
                        IO.File.Delete(Str)
                        'End If
                    Next
                    For Each Str As String In IO.Directory.GetFiles(strRuta & "\" & strRFC, "*.pfx")
                        'If Str <> strRuta & "\" & strRFC & "\" & dtrSolicitud!NOCSD.ToString & ".pfx" Then
                        IO.File.Copy(Str, strRuta & "\" & dtrSolicitud!NOCSD.ToString & ".pfx", True)
                        IO.File.Delete(Str)
                        'End If
                    Next
                    If IO.File.Exists(strRuta & "\CLU_" & strRFC & "_" & Numero & "_" & Sucursal & ".cer") And IO.File.Exists(strRuta & "\" & dtrSolicitud!NOCSD.ToString & ".pfx") Then
                        'Copia CLU  y PFX
                        IO.File.Copy(strRuta & "\CLU_" & strRFC & "_" & Numero & "_" & Sucursal & ".cer", strRutaFinal & "\CLU\CLU_" & strRFC & "_" & Numero & "_" & Sucursal & ".cer", True)
                        IO.File.Copy(strRuta & "\" & dtrSolicitud!NOCSD.ToString & ".pfx", strRutaPFX & "\" & dtrSolicitud!NOCSD.ToString & ".pfx", True)
                        Call CreaUsuario(strRutaFinal, dtrSolicitud)
                        Call CreaEmpresa(strRutaFinal, dtrSolicitud)
                        Return True
                    Else
                        MsgBox("El certificado de licencia de uso ó el PFX no es accesible" & vbCrLf & "Indique el certificado de licencia de uso del sistema", MsgBoxStyle.Exclamation, "AVISO")
                    End If
                Else
                    MsgBox("El certificado de licencia de uso no es accesible" & vbCrLf & "Indique el certificado de licencia de uso del sistema", MsgBoxStyle.Exclamation, "AVISO")
                End If
            Else
                MsgBox("El certificado de licencia de uso no es accesible" & vbCrLf & "Indique el certificado de licencia de uso del sistema", MsgBoxStyle.Exclamation, "AVISO")
            End If
        End If
    End Function
    Public Function CreaUsuario(ByVal strRuta As String, ByVal dtrSolicitud As DataRow) As Boolean
        ' Open Connection
        Dim DB As New ClassCone
        Dim conn As OleDb.OleDbConnection
        conn = DB.conectarsql(strRuta & "\BD\master.mdb", "", "gsdmexico", "", ClassCone.Basededatos.Access)
        Dim dtbUsuarios As DataTable
        Dim dadUsuarios As New OleDb.OleDbDataAdapter
        Dim strSQL As String
        Dim enc As New Cblowfish

        ' Inserta info en tabla
        strSQL = "SELECT * FROM tbl_usuarios WHERE usuario='" & dtrSolicitud!usuario.ToString & "'"
        dtbUsuarios = DB.RecDatatable(strSQL, conn, dadUsuarios)
        If dtbUsuarios.Rows.Count = 0 Then
            Dim dtr As DataRow
            dtr = dtbUsuarios.NewRow
            dtr!usuario = dtrSolicitud!Usuario.ToString
            dtr!nombre = dtrSolicitud!Nombre.ToString
            dtr!passw = enc.Encrypt(dtrSolicitud!passw.ToString)
            dtr!cambiarp = 0
            dtr!deshabili = 0
            dtr!expiraf = Now.AddDays(365)
            dtr!expira = 365
            dtr!intenuser = 0
            dtr!intentos = 99
            dtr!bloqueada = 0
            'dtr!fechaentrada = Now
            dtr!email = dtrSolicitud!email.ToString
            dtr!tel = dtrSolicitud!tel.ToString
            dtr!exten = ""
            dtr!passwc = ""
            dtr!parametros = ""
            dtr!series = ""
            dtr!rol = "USER"
            dtr!IDADMIN = ""
            dtr!IDSUPER = ""
            'dtr!foto=""
            dtbUsuarios.Rows.Add(dtr)
            dadUsuarios.Update(dtbUsuarios)

            strSQL = "DELETE FROM tbl_permisos WHERE usuario='" & dtrSolicitud!usuario.ToString & "'"
            DB.ExecuteSql(strSQL, conn)
            strSQL = "INSERT INTO tbl_permisos(Usuario,numero,permiso,impresion) SELECT '" & dtrSolicitud!usuario.ToString & "' as Usuario,numero,Implec as permiso, Implec as impresion FROM tbl_apps"
            DB.ExecuteSql(strSQL, conn)
        Else

        End If
        conn.Close()
        DB = Nothing

    End Function
    Public Function CreaEmpresa(ByVal strruta As String, ByVal dtrSolicitud As DataRow) As Boolean
        ' Open Connection
        Dim DB As New ClassCone
        Dim conn As OleDb.OleDbConnection
        conn = DB.conectarsql(strruta & "\BD\master.mdb", "", "gsdmexico", "", ClassCone.Basededatos.Access)
        Dim dtbEmpresa As DataTable
        Dim dadEmpresa As New OleDb.OleDbDataAdapter
        Dim strSQL As String

        strSQL = "SELECT * FROM tbl_Empresas WHERE numero=" & dtrSolicitud!numero & " and Sucursal=" & dtrSolicitud!Sucursal
        dtbEmpresa = DB.RecDatatable(strSQL, conn, dadEmpresa)
        If dtbEmpresa.Rows.Count = 0 Then
            Dim dtrConfig As DataRow
            dtrConfig = dtbEmpresa.NewRow
            'Datos de la Empresa
            dtrConfig("Numero") = dtrSolicitud!Numero.ToString
            dtrConfig!Sucursal = dtrSolicitud!Sucursal.ToString
            dtrConfig("Empresa") = dtrSolicitud!Empresa.ToString
            dtrConfig("RFC") = dtrSolicitud!RFC.ToString
            dtrConfig!Email = dtrSolicitud!email.ToString

            'Rutas
            'dtrConfig("RutaMaster") = txtCFDXML.Text
            dtrConfig("CFDXML") = strruta & "\XML\"
            dtrConfig("RutaRespaldo") = strruta & "\Respaldo\"
            dtrConfig!RutaCFDI = strruta & "\iMonitor\"

            'Facturacion
            dtrConfig("CertificadoVigente") = strruta & "\CSD\" & dtrSolicitud!NOCSD.ToString & ".cer"
            dtrConfig("noCertificado") = dtrSolicitud!NOCSD.ToString
            dtrConfig("ClavePrivada") = strruta & "\CSD\" & dtrSolicitud!NOCSD.ToString & ".key"
            dtrConfig("FechaInicio") = Now.Date

            dtrConfig!version = "3.2"
            dtrConfig!Decimales = 2
            dtrConfig!Idioma = "Esp"
            dtrConfig!Culture = "es-MX"
            dtbEmpresa.Rows.Add(dtrConfig)
            dadEmpresa.Update(dtbEmpresa)
        Else

        End If
        conn.Close()
        DB = Nothing
    End Function
    Public Function UnZIPXML(ByVal strZIPFile As String, ByVal strExtract As String) As String
        Dim ZipIN As New Zip.ZipInputStream(IO.File.OpenRead(strZIPFile))
        'Dim entry As Zip.ZipEntry
        If strExtract = "" Then
            strExtract = My.Computer.FileSystem.GetParentPath(strZIPFile)
            strExtract &= "\temp" & Format(Now, "yyMMddHHmmss")
        End If

        If Not My.Computer.FileSystem.DirectoryExists(strExtract) Then
            My.Computer.FileSystem.CreateDirectory(strExtract)
        End If


        Try


            Try
                Dim x As New ICSharpCode.SharpZipLib.Zip.FastZip
                x.ExtractZip(strZIPFile, strExtract, "")
                ZipIN.CloseEntry()
                ZipIN.Close()
                'ZipIN.Flush()
                ZipIN.Dispose()
                Return strExtract
            Catch ex As Exception
                Return False
            End Try
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "AVISO")
            Return Nothing
        Finally

        End Try
    End Function
End Module
