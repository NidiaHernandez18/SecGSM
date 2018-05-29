

Module Utils
    Public Declare Function GetVolumeInformation Lib "kernel32" Alias "GetVolumeInformationA" (ByVal lpRootPathName As String, ByVal lpVolumeNameBuffer As String, ByVal nVolumeNameSize As Integer, ByRef lpVolumeSerialNumber As Integer, ByVal lpMaximumComponentLength As Integer, ByVal lpFileSystemFlags As Integer, ByVal lpFileSystemNameBuffer As String, ByVal nFileSystemNameSize As Integer) As Integer

    Public Structure Information
        Dim Validuser As Boolean
        Dim passuser As String
        Dim iduser As String
        Dim nombre As String
        Dim Empresa As String
        Dim sexo As String
        Dim email As String
        Dim pwde As String
    End Structure

    Public Structure Inflllave
        Dim llave As String
        Dim candado As String
        Dim HDD1 As Long
        Dim RFC As String
        Dim PATRONAL As String
        Dim empresas As String
        Dim correcto As Boolean

    End Structure

    Public Personal As New Information
    Public Ejecutarsql As New ClassCone
    Public CanLla As New Inflllave

    Public Function SerieDisk(ByVal strDrive As String) As Long
        Dim Res As Long
        Dim DrvVolumeName$
        Dim VolumeSN As Long
        Dim UnusedStr As String
        Dim UnusedVal1 As Long
        Dim UnusedVal2 As Long
        strDrive = IO.Path.GetPathRoot(strDrive)
        DrvVolumeName$ = Space$(14)
        UnusedStr$ = Space$(32)
        Res = GetVolumeInformation(strDrive, _
        DrvVolumeName$, Len(DrvVolumeName$), VolumeSN&, _
        UnusedVal1&, UnusedVal2&, UnusedStr$, Len(UnusedStr$))
        SerieDisk = Math.Abs(VolumeSN&)
    End Function
End Module
