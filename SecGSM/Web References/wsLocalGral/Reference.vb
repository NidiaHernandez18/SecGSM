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
Namespace wsLocalGral
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="gralBinding", [Namespace]:="urn:gral")>  _
    Partial Public Class gralService
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private fxLoginOperationCompleted As System.Threading.SendOrPostCallback
        
        Private Sign_CancelOperationCompleted As System.Threading.SendOrPostCallback
        
        Private fxSoporteOperationCompleted As System.Threading.SendOrPostCallback
        
        Private fxNewLicOperationCompleted As System.Threading.SendOrPostCallback
        
        Private fxCheckLicOperationCompleted As System.Threading.SendOrPostCallback
        
        Private fxNewKeyOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.SecGSM.My.MySettings.Default.SecGSM_wsLocalGral_gralService
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
        Public Event fxLoginCompleted As fxLoginCompletedEventHandler
        
        '''<remarks/>
        Public Event Sign_CancelCompleted As Sign_CancelCompletedEventHandler
        
        '''<remarks/>
        Public Event fxSoporteCompleted As fxSoporteCompletedEventHandler
        
        '''<remarks/>
        Public Event fxNewLicCompleted As fxNewLicCompletedEventHandler
        
        '''<remarks/>
        Public Event fxCheckLicCompleted As fxCheckLicCompletedEventHandler
        
        '''<remarks/>
        Public Event fxNewKeyCompleted As fxNewKeyCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:gralAction", RequestNamespace:="urn:gral", ResponseNamespace:="urn:gral")>  _
        Public Function fxLogin(ByVal Info As String) As <System.Xml.Serialization.SoapElementAttribute("value")> String
            Dim results() As Object = Me.Invoke("fxLogin", New Object() {Info})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub fxLoginAsync(ByVal Info As String)
            Me.fxLoginAsync(Info, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub fxLoginAsync(ByVal Info As String, ByVal userState As Object)
            If (Me.fxLoginOperationCompleted Is Nothing) Then
                Me.fxLoginOperationCompleted = AddressOf Me.OnfxLoginOperationCompleted
            End If
            Me.InvokeAsync("fxLogin", New Object() {Info}, Me.fxLoginOperationCompleted, userState)
        End Sub
        
        Private Sub OnfxLoginOperationCompleted(ByVal arg As Object)
            If (Not (Me.fxLoginCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent fxLoginCompleted(Me, New fxLoginCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:gralAction", RequestNamespace:="urn:gral", ResponseNamespace:="urn:gral")>  _
        Public Function Sign_Cancel(ByVal Info As String) As <System.Xml.Serialization.SoapElementAttribute("value")> String
            Dim results() As Object = Me.Invoke("Sign_Cancel", New Object() {Info})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub Sign_CancelAsync(ByVal Info As String)
            Me.Sign_CancelAsync(Info, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub Sign_CancelAsync(ByVal Info As String, ByVal userState As Object)
            If (Me.Sign_CancelOperationCompleted Is Nothing) Then
                Me.Sign_CancelOperationCompleted = AddressOf Me.OnSign_CancelOperationCompleted
            End If
            Me.InvokeAsync("Sign_Cancel", New Object() {Info}, Me.Sign_CancelOperationCompleted, userState)
        End Sub
        
        Private Sub OnSign_CancelOperationCompleted(ByVal arg As Object)
            If (Not (Me.Sign_CancelCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent Sign_CancelCompleted(Me, New Sign_CancelCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:gralAction", RequestNamespace:="urn:gral", ResponseNamespace:="urn:gral")>  _
        Public Function fxSoporte(ByVal Info As String) As <System.Xml.Serialization.SoapElementAttribute("value")> String
            Dim results() As Object = Me.Invoke("fxSoporte", New Object() {Info})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub fxSoporteAsync(ByVal Info As String)
            Me.fxSoporteAsync(Info, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub fxSoporteAsync(ByVal Info As String, ByVal userState As Object)
            If (Me.fxSoporteOperationCompleted Is Nothing) Then
                Me.fxSoporteOperationCompleted = AddressOf Me.OnfxSoporteOperationCompleted
            End If
            Me.InvokeAsync("fxSoporte", New Object() {Info}, Me.fxSoporteOperationCompleted, userState)
        End Sub
        
        Private Sub OnfxSoporteOperationCompleted(ByVal arg As Object)
            If (Not (Me.fxSoporteCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent fxSoporteCompleted(Me, New fxSoporteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:gralAction", RequestNamespace:="urn:gral", ResponseNamespace:="urn:gral")>  _
        Public Function fxNewLic(ByVal Info As String, <System.Xml.Serialization.SoapElementAttribute(DataType:="base64Binary")> ByVal CSD() As Byte) As <System.Xml.Serialization.SoapElementAttribute("value")> String
            Dim results() As Object = Me.Invoke("fxNewLic", New Object() {Info, CSD})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub fxNewLicAsync(ByVal Info As String, ByVal CSD() As Byte)
            Me.fxNewLicAsync(Info, CSD, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub fxNewLicAsync(ByVal Info As String, ByVal CSD() As Byte, ByVal userState As Object)
            If (Me.fxNewLicOperationCompleted Is Nothing) Then
                Me.fxNewLicOperationCompleted = AddressOf Me.OnfxNewLicOperationCompleted
            End If
            Me.InvokeAsync("fxNewLic", New Object() {Info, CSD}, Me.fxNewLicOperationCompleted, userState)
        End Sub
        
        Private Sub OnfxNewLicOperationCompleted(ByVal arg As Object)
            If (Not (Me.fxNewLicCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent fxNewLicCompleted(Me, New fxNewLicCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:gralAction", RequestNamespace:="urn:gral", ResponseNamespace:="urn:gral")>  _
        Public Function fxCheckLic(ByVal Info As String) As <System.Xml.Serialization.SoapElementAttribute("value")> String
            Dim results() As Object = Me.Invoke("fxCheckLic", New Object() {Info})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub fxCheckLicAsync(ByVal Info As String)
            Me.fxCheckLicAsync(Info, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub fxCheckLicAsync(ByVal Info As String, ByVal userState As Object)
            If (Me.fxCheckLicOperationCompleted Is Nothing) Then
                Me.fxCheckLicOperationCompleted = AddressOf Me.OnfxCheckLicOperationCompleted
            End If
            Me.InvokeAsync("fxCheckLic", New Object() {Info}, Me.fxCheckLicOperationCompleted, userState)
        End Sub
        
        Private Sub OnfxCheckLicOperationCompleted(ByVal arg As Object)
            If (Not (Me.fxCheckLicCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent fxCheckLicCompleted(Me, New fxCheckLicCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:gralAction", RequestNamespace:="urn:gral", ResponseNamespace:="urn:gral")>  _
        Public Function fxNewKey(ByVal Info As String) As <System.Xml.Serialization.SoapElementAttribute("value")> String
            Dim results() As Object = Me.Invoke("fxNewKey", New Object() {Info})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub fxNewKeyAsync(ByVal Info As String)
            Me.fxNewKeyAsync(Info, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub fxNewKeyAsync(ByVal Info As String, ByVal userState As Object)
            If (Me.fxNewKeyOperationCompleted Is Nothing) Then
                Me.fxNewKeyOperationCompleted = AddressOf Me.OnfxNewKeyOperationCompleted
            End If
            Me.InvokeAsync("fxNewKey", New Object() {Info}, Me.fxNewKeyOperationCompleted, userState)
        End Sub
        
        Private Sub OnfxNewKeyOperationCompleted(ByVal arg As Object)
            If (Not (Me.fxNewKeyCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent fxNewKeyCompleted(Me, New fxNewKeyCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
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
    Public Delegate Sub fxLoginCompletedEventHandler(ByVal sender As Object, ByVal e As fxLoginCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class fxLoginCompletedEventArgs
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
    Public Delegate Sub Sign_CancelCompletedEventHandler(ByVal sender As Object, ByVal e As Sign_CancelCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class Sign_CancelCompletedEventArgs
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
    Public Delegate Sub fxSoporteCompletedEventHandler(ByVal sender As Object, ByVal e As fxSoporteCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class fxSoporteCompletedEventArgs
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
    Public Delegate Sub fxNewLicCompletedEventHandler(ByVal sender As Object, ByVal e As fxNewLicCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class fxNewLicCompletedEventArgs
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
    Public Delegate Sub fxCheckLicCompletedEventHandler(ByVal sender As Object, ByVal e As fxCheckLicCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class fxCheckLicCompletedEventArgs
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
    Public Delegate Sub fxNewKeyCompletedEventHandler(ByVal sender As Object, ByVal e As fxNewKeyCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class fxNewKeyCompletedEventArgs
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
