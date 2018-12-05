Attribute VB_Name = "modGlobal"
Option Explicit
Public Const ConfigFile = "\Config.ini"
Public Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpString As Any, ByVal lpFileName As String) As Long
Public Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Long, ByVal lpFileName As String) As Long

'/-----------------
 Public Declare Function ShellExecute Lib "shell32.dll" _
       Alias "ShellExecuteA" _
       (ByVal hwnd As Long, _
       ByVal lpOperation As String, _
       ByVal lpFile As String, _
       ByVal lpParameters As String, _
       ByVal lpDirectory As String, _
       ByVal nShowCmd As Long) As Long
 Public Const SW_HIDE = 0
 Public Const SW_SHOWNORMAL = 1
 Public Const SW_SHOWMINIMIZED = 2
 Public Const SW_SHOWMAXIMIZED = 3
 Public Const SW_MAXIMIZE = 3
 Public Const SW_SHOWNOACTIVATE = 4
 Public Const SW_SHOW = 5
 Public Const SW_MINIMIZE = 6
 Public Const SW_SHOWMINNOACTIVE = 7
 Public Const SW_SHOWNA = 8
 Public Const SW_RESTORE = 9
                                         '-----------------/
Public Declare Function InitCommonControls Lib "Comctl32.dll" () As Long
Public Settings
Public ROMType As String
Public Sub MakeDir()

On Error Resume Next
If Dir(App.Path & "\GAME") = "" Then
    MkDir App.Path & "\GAME"
    MkDir App.Path & "\GAME\FC\"
    MkDir App.Path & "\GAME\FC\ROM"
    MkDir App.Path & "\GAME\SFC\"
    MkDir App.Path & "\GAME\SFC\ROM"
    MkDir App.Path & "\GAME\VB\"
    MkDir App.Path & "\GAME\VB\ROM"
    MkDir App.Path & "\GAME\GBA\"
    MkDir App.Path & "\GAME\GBA\ROM"
    MkDir App.Path & "\GAME\N64\"
    MkDir App.Path & "\GAME\N64\ROM"
    MkDir App.Path & "\GAME\NDS\"
    MkDir App.Path & "\GAME\NDS\ROM"
    MkDir App.Path & "\GAME\WiiGC\"
    MkDir App.Path & "\GAME\WiiGC\ROM"
End If
If Dir(App.Path & "\ROMInfo") = "" Then
    MkDir App.Path & "\ROMInfo"
    MkDir App.Path & "\ROMInfo\FC"
    MkDir App.Path & "\ROMInfo\SFC"
    MkDir App.Path & "\ROMInfo\VB"
    MkDir App.Path & "\ROMInfo\GBA"
    MkDir App.Path & "\ROMInfo\N64"
    MkDir App.Path & "\ROMInfo\NDS"
    MkDir App.Path & "\ROMInfo\WiiGC"
End If
End Sub

Public Sub LoadPic(ROMName As String)
If Dir(App.Path & "\ROMInfo\" & "\" & ROMType & "\" & extractFileName(ROMName) & ".jpg") <> "" Then
    frmMain.Snapshot.Picture = LoadPicture(App.Path & "\ROMInfo\" & "\" & ROMType & "\" & extractFileName(ROMName) & ".jpg")
Else
    '没有图片时
End If
End Sub

Public Sub LoadROMInfo(ROMName As String)
On Error Resume Next
Dim s As String, s1 As String
Dim fs As New FileSystemObject
Dim ts As TextStream
frmMain.tInfomation.Text = ""
If Dir(App.Path & "\ROMInfo\" & "\" & ROMType & "\" & extractFileName(ROMName) & ".txt") = "" Then _
frmMain.tInfomation.Text = "这个文件暂时还没有介绍，请帮忙一同编辑吧！": Exit Sub
If fs.FileExists(App.Path & "\ROMInfo\" & "\" & ROMType & "\" & extractFileName(ROMName) & ".txt") = True Then
    Set ts = fs.OpenTextFile(App.Path & "\ROMInfo\" & "\" & ROMType & "\" & extractFileName(ROMName) & ".txt")
    s = ts.ReadAll
    ts.Close
    Set ts = Nothing
End If

frmMain.tInfomation.Text = s  '<--------

Set fs = Nothing
End Sub
Public Function extractFileName(fi As String) As String
    Dim s As String
    Dim i As Long
    s = fi
    Do While InStr(s, "\")
        s = Mid$(s, InStr(s, "\") + 1)
    Loop
    If InStr(s, ".") Then
        s = Left$(s, InStr(s, ".") - 1)
    End If
    extractFileName = s
End Function
