VERSION 5.00
Begin VB.Form frmList 
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "�Զ��崰��"
   ClientHeight    =   4200
   ClientLeft      =   45
   ClientTop       =   375
   ClientWidth     =   5895
   Icon            =   "frmList.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   4200
   ScaleWidth      =   5895
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  '��Ļ����
   Begin VB.CommandButton cmdCancel 
      Cancel          =   -1  'True
      Caption         =   "�ر�"
      BeginProperty Font 
         Name            =   "΢���ź�"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   435
      Left            =   4920
      TabIndex        =   5
      Top             =   3600
      Width           =   855
   End
   Begin VB.CommandButton Command2 
      BackColor       =   &H00E0E0E0&
      Caption         =   "���"
      BeginProperty Font 
         Name            =   "΢���ź�"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   435
      Left            =   120
      TabIndex        =   3
      Top             =   3600
      Width           =   975
   End
   Begin VB.CommandButton Command1 
      BackColor       =   &H00E0E0E0&
      Caption         =   "�Ƴ�"
      BeginProperty Font 
         Name            =   "΢���ź�"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   435
      Left            =   3840
      TabIndex        =   2
      Top             =   3600
      Width           =   975
   End
   Begin VB.CommandButton cAdd 
      BackColor       =   &H00E0E0E0&
      Caption         =   "���"
      Default         =   -1  'True
      BeginProperty Font 
         Name            =   "΢���ź�"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   435
      Left            =   2760
      TabIndex        =   0
      Top             =   3600
      Width           =   975
   End
   Begin VB.ListBox File1 
      BeginProperty Font 
         Name            =   "΢���ź�"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   3120
      Left            =   120
      TabIndex        =   1
      Top             =   360
      Width           =   5655
   End
   Begin VB.Label Label1 
      AutoSize        =   -1  'True
      Caption         =   "��˫�����б��ڵ���Ϸ��"
      BeginProperty Font 
         Name            =   "΢���ź�"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   120
      TabIndex        =   4
      Top             =   75
      Width           =   2160
   End
End
Attribute VB_Name = "frmList"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit '��ǿ�Ʊ��������������Լ����ɺõ�ϰ��
'�������������
Dim OpenFile As OPENFILENAME
Dim lReturn As Long
Dim sFilter As String

Private Sub cAdd_Click()
'���������
    OpenFile.lStructSize = Len(OpenFile)
    OpenFile.hwndOwner = Me.hwnd
    OpenFile.hInstance = App.hInstance
    sFilter = "����֧�ֵ��ļ�" & Chr(0) & "*.vb;*.?64;*.rom;*.jap;*.pal;*.usa;*.nds;*.GBA;*.GB;*.GBC;*.SFC;*.smc;*.nes;*.zip;*.elf;*.dol;*.gcm;*.iso;*.ciso;*.gcz;*.wad"
    OpenFile.lpstrFilter = sFilter
    OpenFile.nFilterIndex = 1
    OpenFile.lpstrFile = String(257, 0)
    OpenFile.nMaxFile = Len(OpenFile.lpstrFile) - 1
    OpenFile.lpstrFileTitle = OpenFile.lpstrFile
    OpenFile.nMaxFileTitle = OpenFile.nMaxFile
    'OpenFile.lpstrInitialDir = "C:\"
    OpenFile.lpstrTitle = "��"
    OpenFile.flags = 0
    lReturn = GetOpenFileName(OpenFile)
    If lReturn <> 0 Then
    File1.AddItem (OpenFile.lpstrFile)
    End If
End Sub

Private Sub cmdCancel_Click()
Unload Me
End Sub

Private Sub Command1_Click()
'ɾ��ѡ������ȡ�˸�����
On Error Resume Next
   Dim dlll As Integer
   For dlll = 0 To File1.ListCount - 1
      If File1.Selected(dlll) = True Then
      File1.RemoveItem (dlll)
         End If
   Next dlll
End Sub

Private Sub Command2_Click()
'�����
Dim RET
RET = MsgBox("�˲������ɷ��أ�ȷ��Ҫ�����", vbYesNo + vbQuestion, "ѯ����Ϣ")
If RET = vbYes Then
File1.Clear
End If

End Sub

Private Sub File1_DblClick()
'�ж��ļ����Ͳ��ö�Ӧ��ģ�������� | ��һ���Լ��������˿��Լ��ԣ��������ø���
On Error GoTo b
Dim A As String
      Dim i As Integer
      For i = 0 To File1.ListCount - 1
      If File1.Selected(i) = True Then
         If LCase(Right(File1.List(i), 4)) = ".nes" Or LCase(Right(File1.List(i), 4)) = ".fds" Then
         Shell Chr(34) & frmSettings.Text1.Text & Chr(34) & " " & Chr(34) & File1.List(i) & Chr(34), vbNormalFocus
         Me.WindowState = 1
         frmMain.WindowState = 1
         frmMain.Label1.Caption = frmSettings.Text1.Text: frmMain.Label5.Caption = File1.List(i)
         ElseIf LCase(Right(File1.List(i), 4)) = ".sfc" Or LCase(Right(File1.List(i), 4)) = ".smc" Then
         Shell Chr(34) & frmSettings.Text2.Text & Chr(34) & " " & Chr(34) & File1.List(i) & Chr(34), vbNormalFocus
         Me.WindowState = 1
         frmMain.WindowState = 1
         frmMain.Label1.Caption = frmSettings.Text2.Text: frmMain.Label5.Caption = File1.List(i)
         ElseIf LCase(Right(File1.List(i), 3)) = ".gb" Or LCase(Right(File1.List(i), 4)) = ".gbc" Then
         Shell Chr(34) & frmSettings.Text3.Text & Chr(34) & " " & Chr(34) & File1.List(i) & Chr(34), vbNormalFocus
         Me.WindowState = 1
         frmMain.WindowState = 1
         frmMain.Label1.Caption = frmSettings.Text3.Text: frmMain.Label5.Caption = File1.List(i)
         ElseIf LCase(Right(File1.List(i), 4)) = ".gba" Then
         Shell Chr(34) & frmSettings.Text4.Text & Chr(34) & " " & Chr(34) & File1.List(i) & Chr(34), vbNormalFocus
         Me.WindowState = 1
         frmMain.WindowState = 1
         frmMain.Label1.Caption = frmSettings.Text4.Text: frmMain.Label5.Caption = File1.List(i)
         ElseIf LCase(Right(File1.List(i), 2)) = "64" Or LCase(Right(File1.List(i), 4)) = ".pal" _
         Or LCase(Right(File1.List(i), 4)) = ".rom" Then
         Shell Chr(34) & frmSettings.Text5.Text & Chr(34) & " " & Chr(34) & File1.List(i) & Chr(34), vbNormalFocus
         Me.WindowState = 1
         frmMain.WindowState = 1
         frmMain.Label1.Caption = frmSettings.Text5.Text: frmMain.Label5.Caption = File1.List(i)
         ElseIf LCase(Right(File1.List(i), 4)) = ".nds" Then
         Shell Chr(34) & frmSettings.Text6.Text & Chr(34) & " " & Chr(34) & File1.List(i) & Chr(34), vbNormalFocus
         Me.WindowState = 1
         frmMain.WindowState = 1
         frmMain.Label1.Caption = frmSettings.Text6.Text: frmMain.Label5.Caption = File1.List(i)
         '������������
         ElseIf LCase(Right(File1.List(i), 4)) = ".gcm" Or LCase(Right(File1.List(i), 4)) = ".iso" _
         Or LCase(Right(File1.List(i), 5)) = ".ciso" Or LCase(Right(File1.List(i), 4)) = ".gcz" _
         Or LCase(Right(File1.List(i), 4)) = ".wad" Or LCase(Right(File1.List(i), 4)) = ".elf" _
         Or LCase(Right(File1.List(i), 4)) = ".dol" Then
         '������������
         Shell Chr(34) & frmSettings.Text7.Text & Chr(34) & " /e " & Chr(34) & File1.List(i) & Chr(34), vbNormalFocus
         Me.WindowState = 1
         frmMain.WindowState = 1
         frmMain.Label1.Caption = frmSettings.Text7.Text: frmMain.Label5.Caption = File1.List(i)
         ElseIf LCase(Right(File1.List(i), 3)) = ".vb" Then
         Shell Chr(34) & frmSettings.Text9.Text & Chr(34) & " " & Chr(34) & File1.List(i) & Chr(34), vbNormalFocus
         Me.WindowState = 1
         frmSettings.Visible = False
         frmMain.Label1.Caption = frmSettings.Text9.Text: frmMain.Label5.Caption = File1.List(i)

         End If
      End If
   Next i
Exit Sub
b:
MsgBox "��ʧ�ܣ��������Ҳ���ģ�����ļ�", vbCritical, "������Ϣ"
End Sub


Private Sub Form_Load()
'������Ϸ�б�
ok:
On Error GoTo errhand
Dim tmp As String

Dim FileNum As Integer 'z
FileNum = FreeFile
Open App.Path & "\Games.lst" For Input As #FileNum
    Do While (Not EOF(FileNum))
        Line Input #FileNum, tmp
        File1.AddItem tmp
    Loop
Close #FileNum
Exit Sub

errhand:
Open App.Path & "\Games.lst" For Output As #FileNum
    '-
Close #FileNum
GoTo ok
Exit Sub
End Sub

Private Sub Form_QueryUnload(Cancel As Integer, UnloadMode As Integer)
'�����б�
On Error Resume Next
Dim i As Integer, j As Integer
Dim A() As String
i = File1.ListCount
ReDim A(i - 1)
For j = 0 To i - 1
A(j) = File1.List(j)
Next j
Open App.Path & "\Games.lst" For Output As #1
For j = 0 To i - 1
Print #1, A(j)
Next j
Close #1
End Sub

