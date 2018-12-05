VERSION 5.00
Begin VB.Form frmSettings 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "超级马里奥游戏集合器 - 选项设定"
   ClientHeight    =   5280
   ClientLeft      =   2760
   ClientTop       =   3750
   ClientWidth     =   5805
   Icon            =   "frmSettings.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   5280
   ScaleWidth      =   5805
   ShowInTaskbar   =   0   'False
   Visible         =   0   'False
   Begin VB.TextBox Text9 
      BeginProperty Font 
         Name            =   "微软雅黑"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   270
      Left            =   960
      TabIndex        =   34
      Top             =   2880
      Width           =   4095
   End
   Begin VB.TextBox Text7 
      BeginProperty Font 
         Name            =   "微软雅黑"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   270
      Left            =   960
      TabIndex        =   33
      Top             =   2520
      Width           =   4095
   End
   Begin VB.TextBox Text6 
      BeginProperty Font 
         Name            =   "微软雅黑"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   270
      Left            =   960
      TabIndex        =   32
      Top             =   2160
      Width           =   4095
   End
   Begin VB.TextBox Text5 
      BeginProperty Font 
         Name            =   "微软雅黑"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   270
      Left            =   960
      TabIndex        =   31
      Top             =   1800
      Width           =   4095
   End
   Begin VB.TextBox Text4 
      BeginProperty Font 
         Name            =   "微软雅黑"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   270
      Left            =   960
      TabIndex        =   30
      Top             =   1440
      Width           =   4095
   End
   Begin VB.TextBox Text3 
      BeginProperty Font 
         Name            =   "微软雅黑"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   270
      Left            =   960
      TabIndex        =   29
      Top             =   1080
      Width           =   4095
   End
   Begin VB.TextBox Text2 
      BeginProperty Font 
         Name            =   "微软雅黑"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   270
      Left            =   960
      TabIndex        =   28
      Top             =   720
      Width           =   4095
   End
   Begin VB.Timer Timer1 
      Interval        =   1
      Left            =   5280
      Top             =   0
   End
   Begin VB.Frame Frame2 
      Caption         =   "程序设置"
      BeginProperty Font 
         Name            =   "微软雅黑"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   1335
      Left            =   120
      TabIndex        =   14
      Top             =   3240
      Width           =   5535
      Begin VB.TextBox Text8 
         BackColor       =   &H8000000F&
         BorderStyle     =   0  'None
         Enabled         =   0   'False
         BeginProperty Font 
            Name            =   "微软雅黑"
            Size            =   15.75
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H8000000D&
         Height          =   855
         Left            =   3120
         MultiLine       =   -1  'True
         TabIndex        =   24
         Text            =   "frmSettings.frx":000C
         Top             =   360
         Visible         =   0   'False
         Width           =   2295
      End
      Begin VB.CheckBox Check1 
         Caption         =   "启动时程序自动载入上次的游戏"
         BeginProperty Font 
            Name            =   "微软雅黑"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   8
         Top             =   360
         Width           =   3855
      End
      Begin VB.CheckBox Check4 
         Caption         =   "开机自动启动"
         BeginProperty Font 
            Name            =   "微软雅黑"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   9
         Top             =   720
         Width           =   2775
      End
      Begin VB.Label Check2S 
         AutoSize        =   -1  'True
         Caption         =   "0"
         Height          =   180
         Left            =   4560
         TabIndex        =   22
         Top             =   720
         Visible         =   0   'False
         Width           =   90
      End
      Begin VB.Label Check1S 
         AutoSize        =   -1  'True
         Caption         =   "0"
         Height          =   180
         Left            =   4560
         TabIndex        =   21
         Top             =   360
         Visible         =   0   'False
         Width           =   90
      End
   End
   Begin VB.CommandButton Command2 
      Caption         =   "默认"
      BeginProperty Font 
         Name            =   "微软雅黑"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   1560
      TabIndex        =   10
      Top             =   4800
      Width           =   1215
   End
   Begin VB.Frame Frame1 
      Caption         =   "模拟器路径设定"
      BeginProperty Font 
         Name            =   "微软雅黑"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   3135
      Left            =   120
      TabIndex        =   12
      Top             =   120
      Width           =   5535
      Begin VB.TextBox Text1 
         BeginProperty Font 
            Name            =   "微软雅黑"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   270
         Left            =   840
         TabIndex        =   27
         Top             =   240
         Width           =   4095
      End
      Begin VB.CommandButton Command9 
         Caption         =   "..."
         BeginProperty Font 
            Name            =   "微软雅黑"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   5040
         TabIndex        =   26
         Top             =   1320
         Width           =   375
      End
      Begin VB.CommandButton Command8 
         Caption         =   "..."
         BeginProperty Font 
            Name            =   "微软雅黑"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   5040
         TabIndex        =   7
         Top             =   2760
         Width           =   375
      End
      Begin VB.CommandButton Command7 
         Caption         =   "..."
         BeginProperty Font 
            Name            =   "微软雅黑"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   5040
         TabIndex        =   6
         Top             =   2400
         Width           =   375
      End
      Begin VB.CommandButton Command6 
         Caption         =   "..."
         BeginProperty Font 
            Name            =   "微软雅黑"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   5040
         TabIndex        =   5
         Top             =   2040
         Width           =   375
      End
      Begin VB.CommandButton Command5 
         Caption         =   "..."
         BeginProperty Font 
            Name            =   "微软雅黑"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   5040
         TabIndex        =   4
         Top             =   1680
         Width           =   375
      End
      Begin VB.CommandButton Command4 
         Caption         =   "..."
         BeginProperty Font 
            Name            =   "微软雅黑"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   5040
         TabIndex        =   3
         Top             =   960
         Width           =   375
      End
      Begin VB.CommandButton Command3 
         Caption         =   "..."
         BeginProperty Font 
            Name            =   "微软雅黑"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   5040
         TabIndex        =   2
         Top             =   600
         Width           =   375
      End
      Begin VB.CommandButton Command1 
         Caption         =   "..."
         BeginProperty Font 
            Name            =   "微软雅黑"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   5040
         TabIndex        =   1
         Top             =   240
         Width           =   375
      End
      Begin VB.Label Label9 
         AutoSize        =   -1  'True
         Caption         =   "VB:"
         BeginProperty Font 
            Name            =   "微软雅黑"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   25
         Top             =   1320
         Width           =   285
      End
      Begin VB.Label Label7 
         AutoSize        =   -1  'True
         BackStyle       =   0  'Transparent
         Caption         =   "NGC/Wii:"
         BeginProperty Font 
            Name            =   "微软雅黑"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   20
         Top             =   2760
         Width           =   795
      End
      Begin VB.Label Label6 
         AutoSize        =   -1  'True
         BackStyle       =   0  'Transparent
         Caption         =   "N64:"
         BeginProperty Font 
            Name            =   "微软雅黑"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   19
         Top             =   2040
         Width           =   405
      End
      Begin VB.Label Label5 
         AutoSize        =   -1  'True
         BackStyle       =   0  'Transparent
         Caption         =   "NDS:"
         BeginProperty Font 
            Name            =   "微软雅黑"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   18
         Top             =   2400
         Width           =   435
      End
      Begin VB.Label Label4 
         AutoSize        =   -1  'True
         BackStyle       =   0  'Transparent
         Caption         =   "GBA:"
         BeginProperty Font 
            Name            =   "微软雅黑"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   17
         Top             =   1680
         Width           =   420
      End
      Begin VB.Label Label3 
         AutoSize        =   -1  'True
         BackStyle       =   0  'Transparent
         Caption         =   "GB/GBC:"
         BeginProperty Font 
            Name            =   "微软雅黑"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   16
         Top             =   960
         Width           =   750
      End
      Begin VB.Label Label1 
         AutoSize        =   -1  'True
         BackStyle       =   0  'Transparent
         Caption         =   "FC:"
         BeginProperty Font 
            Name            =   "微软雅黑"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   15
         Top             =   240
         Width           =   255
      End
      Begin VB.Label Label2 
         AutoSize        =   -1  'True
         BackStyle       =   0  'Transparent
         Caption         =   "SFC:"
         BeginProperty Font 
            Name            =   "微软雅黑"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   13
         Top             =   600
         Width           =   360
      End
   End
   Begin VB.CommandButton CancelButton 
      Caption         =   "取消"
      BeginProperty Font 
         Name            =   "微软雅黑"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   4440
      TabIndex        =   11
      Top             =   4800
      Width           =   1215
   End
   Begin VB.CommandButton OKButton 
      Caption         =   "保存设置"
      BeginProperty Font 
         Name            =   "微软雅黑"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   3000
      TabIndex        =   0
      Top             =   4800
      Width           =   1215
   End
   Begin VB.Label Label8 
      Caption         =   "Label8"
      BeginProperty Font 
         Name            =   "微软雅黑"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   240
      TabIndex        =   23
      Top             =   4680
      Visible         =   0   'False
      Width           =   855
   End
End
Attribute VB_Name = "frmSettings"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim A As String

Private Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpString As Any, ByVal lpFileName As String) As Long
Private Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Long, ByVal lpFileName As String) As Long
'以下关于开机启动
Private Declare Function ShellAbout Lib "shell32.dll" Alias "ShellAboutA" (ByVal hwnd As Long, ByVal szApp As String, ByVal szOtherStuff As String, ByVal hIcon As Long) As Long

Const HKEY_CURRENT_USER = &H80000001
Const REG_SZ = 1
Private Declare Function RegSetValue Lib "advapi32.dll" Alias "RegSetValueA" (ByVal hKey As Long, ByVal lpSubKey As String, ByVal dwType As Long, ByVal lpData As String, ByVal cbData As Long) As Long
Private Declare Function RegCreateKey& Lib "advapi32.dll" Alias "RegCreateKeyA" (ByVal hKey&, ByVal lpszSubKey$, lphKey&)

Private Declare Function RegCloseKey Lib "advapi32.dll" (ByVal hKey As Long) As Long
Private Declare Function RegSetValueEx Lib "advapi32.dll" Alias "RegSetValueExA" (ByVal hKey As Long, ByVal lpValueName As String, ByVal Reserved As Long, ByVal dwType As Long, lpData As Any, ByVal cbData As Long) As Long


Function GetFromINI(AppName As String, KeyName As String, Default As String, FileName As String) As String
Dim RetStr As String
RetStr = String(255, Chr(0))
GetFromINI = Left(RetStr, GetPrivateProfileString(AppName, ByVal KeyName, Default, RetStr, Len(RetStr), FileName))
End Function

Private Sub CancelButton_Click()
frmMain.Timer2.Enabled = False
Unload Me
End Sub

Private Sub Check1_MouseDown(Button As Integer, Shift As Integer, X As Single, Y As Single)
If Check1.Value = 0 Then
Check1.Value = 1
Check1S.Caption = "1"
Else
Check1.Value = 0
Check1S.Caption = "0"
End If

End Sub

Private Sub Check1_MouseUp(Button As Integer, Shift As Integer, X As Single, Y As Single)
If Check1.Value = 0 Then
Check1.Value = 1
Check1S.Caption = "1"
Else
Check1.Value = 0
Check1S.Caption = "0"
End If

End Sub

Private Sub Check4_Click()
Dim hKey As Long, SubKey As String, Exe As String
If Check4.Value = 0 Then
Check2S.Caption = "0"
Else
Check2S.Caption = "1"
End If
Settings = WritePrivateProfileString("Settings", "AutoRun", Check2S.Caption, App.Path & ConfigFile)
If Check4.Value = 1 Then
SubKey = "Software\Microsoft\Windows\CurrentVersion\Run"
Exe = App.Path & "\" & App.EXEName & ".exe"
RegCreateKey HKEY_CURRENT_USER, SubKey, hKey
RegSetValueEx hKey, "MarioGames", 0, REG_SZ, ByVal Exe, LenB(StrConv(Exe, vbFromUnicode)) + 1
RegCloseKey hKey
ElseIf Check4.Value = 0 Then
SubKey = "Software\Microsoft\Windows\CurrentVersion\Run"
Exe = ""
RegCreateKey HKEY_CURRENT_USER, SubKey, hKey
RegSetValueEx hKey, "MarioGames", 0, REG_SZ, ByVal Exe, LenB(StrConv(Exe, vbFromUnicode)) + 1
RegCloseKey hKey
End If
End Sub

Private Sub Command1_Click()
A = Text1.Text
Dim OpenFile As OPENFILENAME
Dim lReturn As Long
Dim sFilter As String
    OpenFile.lStructSize = Len(OpenFile)
    OpenFile.hwndOwner = Me.hwnd
    OpenFile.hInstance = App.hInstance
    sFilter = "可执行文件 (*.exe)" & Chr(0) & "*.exe"
    OpenFile.lpstrFilter = sFilter
    OpenFile.nFilterIndex = 1
    OpenFile.lpstrFile = String(257, 0)
    OpenFile.nMaxFile = Len(OpenFile.lpstrFile) - 1
    OpenFile.lpstrFileTitle = OpenFile.lpstrFile
    OpenFile.nMaxFileTitle = OpenFile.nMaxFile
    'OpenFile.lpstrInitialDir = "C:\"
    OpenFile.lpstrTitle = "设置模拟器"
    OpenFile.flags = 0
    lReturn = GetOpenFileName(OpenFile)
            Label8.Caption = (OpenFile.lpstrFileTitle)
            If Label8.Caption = App.EXEName & ".exe" Then
            MsgBox "请勿将此程序设置为模拟器。", vbExclamation, "错误信息"
            Exit Sub
            End If
            Text1.Text = (OpenFile.lpstrFile)
If Text1.Text = "" Then Text1.Text = A
End Sub


Private Sub Command2_Click()
On Error Resume Next
Kill App.Path & ConfigFile
MsgBox "默认设置成功，设置将在下次启动程序后生效", vbInformation, "操作成功"
Command2.Enabled = False
End Sub

Private Sub Command3_Click()
A = Text2.Text
Dim OpenFile As OPENFILENAME
Dim lReturn As Long
Dim sFilter As String
    OpenFile.lStructSize = Len(OpenFile)
    OpenFile.hwndOwner = Me.hwnd
    OpenFile.hInstance = App.hInstance
    sFilter = "可执行文件 (*.exe)" & Chr(0) & "*.exe"
    OpenFile.lpstrFilter = sFilter
    OpenFile.nFilterIndex = 1
    OpenFile.lpstrFile = String(257, 0)
    OpenFile.nMaxFile = Len(OpenFile.lpstrFile) - 1
    OpenFile.lpstrFileTitle = OpenFile.lpstrFile
    OpenFile.nMaxFileTitle = OpenFile.nMaxFile
    'OpenFile.lpstrInitialDir = "C:\"
    OpenFile.lpstrTitle = "打开"
    OpenFile.flags = 0
    lReturn = GetOpenFileName(OpenFile)
            Text2.Text = (OpenFile.lpstrFile)
If Text2.Text = "" Then Text2.Text = A

End Sub

Private Sub Command4_Click()
A = Text3.Text
Dim OpenFile As OPENFILENAME
Dim lReturn As Long
Dim sFilter As String
    OpenFile.lStructSize = Len(OpenFile)
    OpenFile.hwndOwner = Me.hwnd
    OpenFile.hInstance = App.hInstance
    sFilter = "可执行文件 (*.exe)" & Chr(0) & "*.exe"
    OpenFile.lpstrFilter = sFilter
    OpenFile.nFilterIndex = 1
    OpenFile.lpstrFile = String(257, 0)
    OpenFile.nMaxFile = Len(OpenFile.lpstrFile) - 1
    OpenFile.lpstrFileTitle = OpenFile.lpstrFile
    OpenFile.nMaxFileTitle = OpenFile.nMaxFile
    'OpenFile.lpstrInitialDir = "C:\"
    OpenFile.lpstrTitle = "打开"
    OpenFile.flags = 0
    lReturn = GetOpenFileName(OpenFile)
            Text3.Text = (OpenFile.lpstrFile)
If Text3.Text = "" Then Text3.Text = A

End Sub

Private Sub Command5_Click()
A = Text5.Text
Dim OpenFile As OPENFILENAME
Dim lReturn As Long
Dim sFilter As String
    OpenFile.lStructSize = Len(OpenFile)
    OpenFile.hwndOwner = Me.hwnd
    OpenFile.hInstance = App.hInstance
    sFilter = "可执行文件 (*.exe)" & Chr(0) & "*.exe"
    OpenFile.lpstrFilter = sFilter
    OpenFile.nFilterIndex = 1
    OpenFile.lpstrFile = String(257, 0)
    OpenFile.nMaxFile = Len(OpenFile.lpstrFile) - 1
    OpenFile.lpstrFileTitle = OpenFile.lpstrFile
    OpenFile.nMaxFileTitle = OpenFile.nMaxFile
    'OpenFile.lpstrInitialDir = "C:\"
    OpenFile.lpstrTitle = "打开"
    OpenFile.flags = 0
    lReturn = GetOpenFileName(OpenFile)
            Text5.Text = (OpenFile.lpstrFile)
If Text5.Text = "" Then Text5.Text = A

End Sub

Private Sub Command6_Click()
A = Text6.Text
Dim OpenFile As OPENFILENAME
Dim lReturn As Long
Dim sFilter As String
    OpenFile.lStructSize = Len(OpenFile)
    OpenFile.hwndOwner = Me.hwnd
    OpenFile.hInstance = App.hInstance
    sFilter = "可执行文件 (*.exe)" & Chr(0) & "*.exe"
    OpenFile.lpstrFilter = sFilter
    OpenFile.nFilterIndex = 1
    OpenFile.lpstrFile = String(257, 0)
    OpenFile.nMaxFile = Len(OpenFile.lpstrFile) - 1
    OpenFile.lpstrFileTitle = OpenFile.lpstrFile
    OpenFile.nMaxFileTitle = OpenFile.nMaxFile
    'OpenFile.lpstrInitialDir = "C:\"
    OpenFile.lpstrTitle = "打开"
    OpenFile.flags = 0
    lReturn = GetOpenFileName(OpenFile)
            Text6.Text = (OpenFile.lpstrFile)
If Text6.Text = "" Then Text5.Text = A

End Sub

Private Sub Command7_Click()
A = Text7.Text
Dim OpenFile As OPENFILENAME
Dim lReturn As Long
Dim sFilter As String
    OpenFile.lStructSize = Len(OpenFile)
    OpenFile.hwndOwner = Me.hwnd
    OpenFile.hInstance = App.hInstance
    sFilter = "可执行文件 (*.exe)" & Chr(0) & "*.exe"
    OpenFile.lpstrFilter = sFilter
    OpenFile.nFilterIndex = 1
    OpenFile.lpstrFile = String(257, 0)
    OpenFile.nMaxFile = Len(OpenFile.lpstrFile) - 1
    OpenFile.lpstrFileTitle = OpenFile.lpstrFile
    OpenFile.nMaxFileTitle = OpenFile.nMaxFile
    'OpenFile.lpstrInitialDir = "C:\"
    OpenFile.lpstrTitle = "打开"
    OpenFile.flags = 0
    lReturn = GetOpenFileName(OpenFile)
            Text7.Text = (OpenFile.lpstrFile)
If Text6.Text = "" Then Text6.Text = A

End Sub

Private Sub Command8_Click()
A = Text9.Text
Dim OpenFile As OPENFILENAME
Dim lReturn As Long
Dim sFilter As String
    OpenFile.lStructSize = Len(OpenFile)
    OpenFile.hwndOwner = Me.hwnd
    OpenFile.hInstance = App.hInstance
    sFilter = "可执行文件 (*.exe)" & Chr(0) & "*.exe"
    OpenFile.lpstrFilter = sFilter
    OpenFile.nFilterIndex = 1
    OpenFile.lpstrFile = String(257, 0)
    OpenFile.nMaxFile = Len(OpenFile.lpstrFile) - 1
    OpenFile.lpstrFileTitle = OpenFile.lpstrFile
    OpenFile.nMaxFileTitle = OpenFile.nMaxFile
    'OpenFile.lpstrInitialDir = "C:\"
    OpenFile.lpstrTitle = "打开"
    OpenFile.flags = 0
    lReturn = GetOpenFileName(OpenFile)
            Text9.Text = (OpenFile.lpstrFile)
If Text9.Text = "" Then Text9.Text = A

End Sub

Private Sub Command9_Click()
A = Text4.Text
Dim OpenFile As OPENFILENAME
Dim lReturn As Long
Dim sFilter As String
    OpenFile.lStructSize = Len(OpenFile)
    OpenFile.hwndOwner = Me.hwnd
    OpenFile.hInstance = App.hInstance
    sFilter = "可执行文件 (*.exe)" & Chr(0) & "*.exe"
    OpenFile.lpstrFilter = sFilter
    OpenFile.nFilterIndex = 1
    OpenFile.lpstrFile = String(257, 0)
    OpenFile.nMaxFile = Len(OpenFile.lpstrFile) - 1
    OpenFile.lpstrFileTitle = OpenFile.lpstrFile
    OpenFile.nMaxFileTitle = OpenFile.nMaxFile
    'OpenFile.lpstrInitialDir = "C:\"
    OpenFile.lpstrTitle = "打开"
    OpenFile.flags = 0
    lReturn = GetOpenFileName(OpenFile)
            Text4.Text = (OpenFile.lpstrFile)
If Text4.Text = "" Then Text4.Text = A


End Sub

Private Sub Form_Load()
If App.PrevInstance = True Then End
Text1.Text = GetFromINI("Emulators", "FC", App.Path & "\GAME\FC\ZYHEmulator\ZYHEMUNT.EXE", App.Path & ConfigFile)
Text2.Text = GetFromINI("Emulators", "SFC", App.Path & "\GAME\SFC\snes9x.exe", App.Path & ConfigFile)
Text3.Text = GetFromINI("Emulators", "GB/GBC", App.Path & "\GAME\GBA\VisualBoyAdvance.exe", App.Path & ConfigFile)
Text4.Text = GetFromINI("Emulators", "GBA", App.Path & "\GAME\GBA\VisualBoyAdvance.exe", App.Path & ConfigFile)
Text5.Text = GetFromINI("Emulators", "N64", App.Path & "\GAME\N64\Project64.exe", App.Path & ConfigFile)
Text6.Text = GetFromINI("Emulators", "NDS", App.Path & "\GAME\NDS\NO$GBA.EXE", App.Path & ConfigFile)
Text7.Text = GetFromINI("Emulators", "NGC/Wii", App.Path & "\GAME\WiiGC\Dolphin.exe", App.Path & ConfigFile)
Text9.Text = GetFromINI("Emulators", "VB", App.Path & "\GAME\VB\VBjin.exe", App.Path & ConfigFile)

Check1S.Caption = GetFromINI("Settings", "LastGame", "0", App.Path & ConfigFile)
Check1.Value = Check1S.Caption
Check2S.Caption = GetFromINI("Settings", "AutoRun", "0", App.Path & ConfigFile)
Check4.Value = Check2S.Caption

frmMain.Show
End Sub

Private Sub Form_QueryUnload(Cancel As Integer, UnloadMode As Integer)
frmMain.Timer2.Enabled = False
Settings = WritePrivateProfileString("Settings", "LastGame", Check1S.Caption, App.Path & ConfigFile)
End Sub

Private Sub OKButton_Click()
'//TODO:把每个模拟器路径分别写进INI
If Command2.Enabled = True Then
Settings = WritePrivateProfileString("Emulators", "FC", Text1.Text, App.Path & ConfigFile)
Settings = WritePrivateProfileString("Emulators", "SFC", Text2.Text, App.Path & ConfigFile)
Settings = WritePrivateProfileString("Emulators", "GB/GBC", Text3.Text, App.Path & ConfigFile)
Settings = WritePrivateProfileString("Emulators", "VB", Text9.Text, App.Path & ConfigFile)
Settings = WritePrivateProfileString("Emulators", "GBA", Text4.Text, App.Path & ConfigFile)
Settings = WritePrivateProfileString("Emulators", "N64", Text5.Text, App.Path & ConfigFile)
Settings = WritePrivateProfileString("Emulators", "NDS", Text6.Text, App.Path & ConfigFile)
Settings = WritePrivateProfileString("Emulators", "NGC/Wii", Text7.Text, App.Path & ConfigFile)
Settings = WritePrivateProfileString("Settings", "LastGame", Check1S.Caption, App.Path & ConfigFile)
Settings = WritePrivateProfileString("Settings", "AutoRun", Check2S.Caption, App.Path & ConfigFile)
End If
frmMain.Timer2.Enabled = False
Unload Me
End Sub

