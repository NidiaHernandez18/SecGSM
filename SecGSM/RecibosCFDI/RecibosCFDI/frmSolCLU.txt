Imports System.IO
Public Class frmSolCLU
    Public DirAPP As String
    Private Sub cmdNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNuevo.Click
        Dim frm As New frmNew
        frm.ShowDialog()
        frm.Close()
    End Sub

    Private Sub frmSolCLU_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DirAPP = My.Application.Info.DirectoryPath
    End Sub

    Private Sub frmSolCLU_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim host As String
        Dim LocalHostAddress As String = ""
        host = System.Net.Dns.GetHostName()
        If Not IsNothing(System.Net.Dns.GetHostEntry(host)) Then
            For Each StrIp As System.Net.IPAddress In System.Net.Dns.GetHostEntry(host).AddressList
                If StrIp.AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then
                    LocalHostAddress = StrIp.ToString
                End If
            Next
        End If
        txtEquipo.Text = host
        txtIP.Text = LocalHostAddress
        'Lee archivo de Solicitudes
        If Not IO.Directory.Exists(DirAPP & "\Solicitudes") Then IO.Directory.CreateDirectory(DirAPP & "\Solicitudes")
        If Not IO.File.Exists(DirAPP & "\Solicitudes\Solicitud.xml") Then CreateFileSol(DirAPP)
        Call LeeSolicitudes()
    End Sub
    Private Sub LeeSolicitudes()
        Dim dtb As New DataTable
        dtb.TableName = "Solicitudes"
        dtb.ReadXmlSchema(DirAPP & "\Solicitudes\Solicitud.xsd")
        dtb.ReadXml(DirAPP & "\Solicitudes\Solicitud.xml")
        For Each dtr As DataRow In dtb.Rows
            Dim lvw As Windows.Forms.ListViewItem
            lvw = lvwSolicitudes.Items.Add(dtr!Empresa.ToString)
            With lvw
                .SubItems.Add(dtr!RFC.ToString)
                .SubItems.Add(dtr!Sucursal.ToString)
                .SubItems.Add(dtr!Fecha.ToString)
                Select Case dtr!EstadoAut.ToString
                    Case Is = "OK"
                        .Group = lvwSolicitudes.Groups("Aceptadas")
                    Case Is = "NO"
                        .Group = lvwSolicitudes.Groups("Rechazadas")
                    Case Else
                        .Group = lvwSolicitudes.Groups("EnEspera")
                End Select
            End With
        Next
    End Sub
    Private Sub VerificaSolicitudes()
        Dim dtb As New DataTable
        dtb.TableName = "Solicitudes"
        dtb.ReadXmlSchema(DirAPP & "\Solicitudes\Solicitud.xsd")
        dtb.ReadXml(DirAPP & "\Solicitudes\Solicitud.xml")
        For Each dtr As DataRow In dtb.Rows
            If dtr!EstadoAut.ToString = "" Then
                Dim ws As New clsWs
                Dim log As New clsWs.RequestCLUTypeResult
                Dim strDatos As String
                '$Origen,$Empresa,$Sucursal,$RFC,$Sistema,$IP,$IDRequest
                '$Origen
                strDatos = txtEquipo.Text & "(" & txtIP.Text & ")"
                '$Numero 
                'strDatos &= "|" & txtNumero.Text
                '$Empresa
                strDatos &= "|" & dtr!Empresa.ToString
                '$Sucursal
                strDatos &= "|" & dtr!Sucursal.ToString
                '$RFC
                strDatos &= "|" & dtr!RFC.ToString
                '$NoCertificado
                'strDatos &= "|" & txtNoCSD.Text
                '$Sistema
                strDatos &= "|" & "ePayroll"
                '$Nombre
                'strDatos &= "|" & txtNombre.Text
                '$Usuario
                'strDatos &= "|" & txtUsuario.Text
                '$Password
                'strDatos &= "|" & txtPwdUsuario.Text
                '$Email
                'strDatos &= "|" & txtEmail.Text
                '$Telefono
                'strDatos &= "|" & txtTelefono.Text
                '$SNEquipo
                'strDatos &= "|" & Mid(HDD1 & Space(11), 1, 11)
                '$Equipo
                'strDatos &= "|" & txtEquipo.Text
                '$IP
                strDatos &= "|" & txtIP.Text
                '$IDRequest
                strDatos &= "|" & dtr!IDRequest.ToString

                log = ws.fxCheckLic(strDatos, dtr!RFC.ToString, dtr!Numero.ToString, dtr!Sucursal, Nothing)
                If log.EstadoAut = "OK" Then
                    If LiberaClu(dtr, log.CLU) Then
                        dtr!EstadoAut = "OK"
                    End If
                ElseIf log.EstadoAut = "NO" Then
                    dtr!EstadoAut = "NO"
                Else

                End If
                dtb.AcceptChanges()
            End If
        Next
        dtb.WriteXmlSchema(DirAPP & "\Solicitudes\Solicitud.xsd")
        dtb.WriteXml(DirAPP & "\Solicitudes\Solicitud.xml")
    End Sub

    Private Sub cmdVerificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdVerificar.Click
        lvwSolicitudes.Items.Clear()
        Call VerificaSolicitudes()
        Call LeeSolicitudes()
        MsgBox("Solicitudes verificadas en GSM", MsgBoxStyle.Information, "AVISO")
    End Sub

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        Me.Close()
    End Sub

    Private Sub cmdEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEliminar.Click

    End Sub
End Class