﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.1
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.1.
'
Namespace wsLocalSrv
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="srvBinding", [Namespace]:="urn:srv")>  _
    Partial Public Class srvService
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private UpdateClienteOperationCompleted As System.Threading.SendOrPostCallback
        
        Private UpdateCLUSOperationCompleted As System.Threading.SendOrPostCallback
        
        Private UpdateMsgsOperationCompleted As System.Threading.SendOrPostCallback
        
        Private UpdateCatTimbresOperationCompleted As System.Threading.SendOrPostCallback
        
        Private GetInfoOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.SecGSM.My.MySettings.Default.SecGSM_wsLocalSrv_srvService
            If (Me.IsLocalFileSystemWebService(Me.Url) = true) Then
                Me.UseDefaultCredentials = true
                Me.useDefaultCredentialsSetExplicitly = false
            Else
                Me.useDefaultCredentialsSetExplicitly = true
            End If
        End Sub
        
        Public Shadows Property Url() As String
            Get
                Return MyBase.Url
            End Get
            Set
                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = true)  _
                            AndAlso (Me.useDefaultCredentialsSetExplicitly = false))  _
                            AndAlso (Me.IsLocalFileSystemWebService(value) = false)) Then
                    MyBase.UseDefaultCredentials = false
                End If
                MyBase.Url = value
            End Set
        End Property
        
        Public Shadows Property UseDefaultCredentials() As Boolean
            Get
                Return MyBase.UseDefaultCredentials
            End Get
            Set
                MyBase.UseDefaultCredentials = value
                Me.useDefaultCredentialsSetExplicitly = true
            End Set
        End Property
        
        '''<remarks/>
        Public Event UpdateClienteCompleted As UpdateClienteCompletedEventHandler
        
        '''<remarks/>
        Public Event UpdateCLUSCompleted As UpdateCLUSCompletedEventHandler
        
        '''<remarks/>
        Public Event UpdateMsgsCompleted As UpdateMsgsCompletedEventHandler
        
        '''<remarks/>
        Public Event UpdateCatTimbresCompleted As UpdateCatTimbresCompletedEventHandler
        
        '''<remarks/>
        Public Event GetInfoCompleted As GetInfoCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:srvAction", RequestNamespace:="urn:srv", ResponseNamespace:="urn:srv")>  _
        Public Function UpdateCliente(ByVal Info As String) As <System.Xml.Serialization.SoapElementAttribute("value")> String
            Dim results() As Object = Me.Invoke("UpdateCliente", New Object() {Info})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub UpdateClienteAsync(ByVal Info As String)
            Me.UpdateClienteAsync(Info, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub UpdateClienteAsync(ByVal Info As String, ByVal userState As Object)
            If (Me.UpdateClienteOperationCompleted Is Nothing) Then
                Me.UpdateClienteOperationCompleted = AddressOf Me.OnUpdateClienteOperationCompleted
            End If
            Me.InvokeAsync("UpdateCliente", New Object() {Info}, Me.UpdateClienteOperationCompleted, userState)
        End Sub
        
        Private Sub OnUpdateClienteOperationCompleted(ByVal arg As Object)
            If (Not (Me.UpdateClienteCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent UpdateClienteCompleted(Me, New UpdateClienteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:srvAction", RequestNamespace:="urn:srv", ResponseNamespace:="urn:srv")>  _
        Public Function UpdateCLUS(ByVal Info As String, <System.Xml.Serialization.SoapElementAttribute(DataType:="base64Binary")> ByVal CLU() As Byte, <System.Xml.Serialization.SoapElementAttribute(DataType:="base64Binary")> ByVal PFX() As Byte) As <System.Xml.Serialization.SoapElementAttribute("value")> String
            Dim results() As Object = Me.Invoke("UpdateCLUS", New Object() {Info, CLU, PFX})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub UpdateCLUSAsync(ByVal Info As String, ByVal CLU() As Byte, ByVal PFX() As Byte)
            Me.UpdateCLUSAsync(Info, CLU, PFX, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub UpdateCLUSAsync(ByVal Info As String, ByVal CLU() As Byte, ByVal PFX() As Byte, ByVal userState As Object)
            If (Me.UpdateCLUSOperationCompleted Is Nothing) Then
                Me.UpdateCLUSOperationCompleted = AddressOf Me.OnUpdateCLUSOperationCompleted
            End If
            Me.InvokeAsync("UpdateCLUS", New Object() {Info, CLU, PFX}, Me.UpdateCLUSOperationCompleted, userState)
        End Sub
        
        Private Sub OnUpdateCLUSOperationCompleted(ByVal arg As Object)
            If (Not (Me.UpdateCLUSCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent UpdateCLUSCompleted(Me, New UpdateCLUSCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:srvAction", RequestNamespace:="urn:srv", ResponseNamespace:="urn:srv")>  _
        Public Function UpdateMsgs(ByVal Info As String) As <System.Xml.Serialization.SoapElementAttribute("value")> String
            Dim results() As Object = Me.Invoke("UpdateMsgs", New Object() {Info})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub UpdateMsgsAsync(ByVal Info As String)
            Me.UpdateMsgsAsync(Info, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub UpdateMsgsAsync(ByVal Info As String, ByVal userState As Object)
            If (Me.UpdateMsgsOperationCompleted Is Nothing) Then
                Me.UpdateMsgsOperationCompleted = AddressOf Me.OnUpdateMsgsOperationCompleted
            End If
            Me.InvokeAsync("UpdateMsgs", New Object() {Info}, Me.UpdateMsgsOperationCompleted, userState)
        End Sub
        
        Private Sub OnUpdateMsgsOperationCompleted(ByVal arg As Object)
            If (Not (Me.UpdateMsgsCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent UpdateMsgsCompleted(Me, New UpdateMsgsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:srvAction", RequestNamespace:="urn:srv", ResponseNamespace:="urn:srv")>  _
        Public Function UpdateCatTimbres(ByVal Info As String) As <System.Xml.Serialization.SoapElementAttribute("value")> String
            Dim results() As Object = Me.Invoke("UpdateCatTimbres", New Object() {Info})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub UpdateCatTimbresAsync(ByVal Info As String)
            Me.UpdateCatTimbresAsync(Info, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub UpdateCatTimbresAsync(ByVal Info As String, ByVal userState As Object)
            If (Me.UpdateCatTimbresOperationCompleted Is Nothing) Then
                Me.UpdateCatTimbresOperationCompleted = AddressOf Me.OnUpdateCatTimbresOperationCompleted
            End If
            Me.InvokeAsync("UpdateCatTimbres", New Object() {Info}, Me.UpdateCatTimbresOperationCompleted, userState)
        End Sub
        
        Private Sub OnUpdateCatTimbresOperationCompleted(ByVal arg As Object)
            If (Not (Me.UpdateCatTimbresCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent UpdateCatTimbresCompleted(Me, New UpdateCatTimbresCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:srvAction", RequestNamespace:="urn:srv", ResponseNamespace:="urn:srv")>  _
        Public Function GetInfo(ByVal Info As String) As <System.Xml.Serialization.SoapElementAttribute("value")> String
            Dim results() As Object = Me.Invoke("GetInfo", New Object() {Info})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub GetInfoAsync(ByVal Info As String)
            Me.GetInfoAsync(Info, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub GetInfoAsync(ByVal Info As String, ByVal userState As Object)
            If (Me.GetInfoOperationCompleted Is Nothing) Then
                Me.GetInfoOperationCompleted = AddressOf Me.OnGetInfoOperationCompleted
            End If
            Me.InvokeAsync("GetInfo", New Object() {Info}, Me.GetInfoOperationCompleted, userState)
        End Sub
        
        Private Sub OnGetInfoOperationCompleted(ByVal arg As Object)
            If (Not (Me.GetInfoCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent GetInfoCompleted(Me, New GetInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
        
        Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
            If ((url Is Nothing)  _
                        OrElse (url Is String.Empty)) Then
                Return false
            End If
            Dim wsUri As System.Uri = New System.Uri(url)
            If ((wsUri.Port >= 1024)  _
                        AndAlso (String.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) = 0)) Then
                Return true
            End If
            Return false
        End Function
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")>  _
    Public Delegate Sub UpdateClienteCompletedEventHandler(ByVal sender As Object, ByVal e As UpdateClienteCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class UpdateClienteCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")>  _
    Public Delegate Sub UpdateCLUSCompletedEventHandler(ByVal sender As Object, ByVal e As UpdateCLUSCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class UpdateCLUSCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")>  _
    Public Delegate Sub UpdateMsgsCompletedEventHandler(ByVal sender As Object, ByVal e As UpdateMsgsCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class UpdateMsgsCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")>  _
    Public Delegate Sub UpdateCatTimbresCompletedEventHandler(ByVal sender As Object, ByVal e As UpdateCatTimbresCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class UpdateCatTimbresCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")>  _
    Public Delegate Sub GetInfoCompletedEventHandler(ByVal sender As Object, ByVal e As GetInfoCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class GetInfoCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
    End Class
End Namespace
