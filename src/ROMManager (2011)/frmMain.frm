VERSION 5.00
Begin VB.Form frmMain 
   Caption         =   "Nintendo Game Manager"
   ClientHeight    =   6600
   ClientLeft      =   120
   ClientTop       =   405
   ClientWidth     =   8505
   Icon            =   "frmMain.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   6600
   ScaleWidth      =   8505
   StartUpPosition =   2  '��Ļ����
   Begin VB.CommandButton Command2 
      BackColor       =   &H00E0E0E0&
      Caption         =   "����"
      BeginProperty Font 
         Name            =   "΢���ź�"
         Size            =   9.75
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   855
      Left            =   7560
      Style           =   1  'Graphical
      TabIndex        =   8
      Top             =   5160
      Width           =   855
   End
   Begin VB.CommandButton cAdd 
      BackColor       =   &H00E0E0E0&
      Caption         =   "�Զ���"
      BeginProperty Font 
         Name            =   "΢���ź�"
         Size            =   9.75
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   735
      Left            =   7560
      Style           =   1  'Graphical
      TabIndex        =   20
      Top             =   2400
      Width           =   855
   End
   Begin VB.CommandButton Command4 
      Caption         =   ">>"
      Height          =   230
      Left            =   8160
      TabIndex        =   23
      Top             =   0
      Width           =   375
   End
   Begin VB.Timer Timer1 
      Interval        =   1
      Left            =   8040
      Top             =   3000
   End
   Begin VB.CommandButton Command3 
      BackColor       =   &H00E0E0E0&
      Caption         =   "ˢ��"
      BeginProperty Font 
         Name            =   "΢���ź�"
         Size            =   9.75
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   735
      Left            =   7560
      Style           =   1  'Graphical
      TabIndex        =   21
      Top             =   1680
      Width           =   855
   End
   Begin VB.Timer Timer2 
      Enabled         =   0   'False
      Interval        =   1
      Left            =   5160
      Top             =   0
   End
   Begin VB.Timer Timer3 
      Interval        =   500
      Left            =   8040
      Top             =   3480
   End
   Begin VB.CommandButton Command1 
      BackColor       =   &H00E0E0E0&
      Caption         =   "����"
      BeginProperty Font 
         Name            =   "΢���ź�"
         Size            =   9.75
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   855
      Left            =   7560
      Style           =   1  'Graphical
      TabIndex        =   7
      Top             =   4320
      Width           =   855
   End
   Begin VB.CommandButton cBrowse 
      BackColor       =   &H00E0E0E0&
      Caption         =   "���"
      BeginProperty Font 
         Name            =   "΢���ź�"
         Size            =   9.75
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   735
      Left            =   7560
      Style           =   1  'Graphical
      TabIndex        =   4
      Top             =   960
      Width           =   855
   End
   Begin VB.CommandButton cRun 
      BackColor       =   &H00E0E0E0&
      Caption         =   "����"
      BeginProperty Font 
         Name            =   "΢���ź�"
         Size            =   9.75
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   735
      Left            =   7560
      Style           =   1  'Graphical
      TabIndex        =   0
      Top             =   240
      Width           =   855
   End
   Begin VB.Frame Frame3 
      Caption         =   "��Ϸ����"
      BeginProperty Font 
         Name            =   "΢���ź�"
         Size            =   10.5
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   3015
      Left            =   120
      TabIndex        =   11
      Top             =   3120
      Width           =   7335
      Begin VB.TextBox Text1 
         BackColor       =   &H00E0E0E0&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "΢���ź�"
            Size            =   15
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H8000000D&
         Height          =   1095
         Left            =   840
         Locked          =   -1  'True
         MultiLine       =   -1  'True
         TabIndex        =   19
         Text            =   "frmMain.frx":3E72
         Top             =   1080
         Visible         =   0   'False
         Width           =   2535
      End
      Begin VB.CommandButton cEdit 
         Caption         =   "�༭"
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
         Left            =   3120
         TabIndex        =   5
         Top             =   2640
         Width           =   735
      End
      Begin VB.TextBox tInfomation 
         BackColor       =   &H00E0E0E0&
         BeginProperty Font 
            Name            =   "΢���ź�"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   2175
         Left            =   120
         Locked          =   -1  'True
         MultiLine       =   -1  'True
         ScrollBars      =   2  'Vertical
         TabIndex        =   12
         TabStop         =   0   'False
         Top             =   360
         Width           =   3735
      End
      Begin VB.Image Snapshot 
         Appearance      =   0  'Flat
         BorderStyle     =   1  'Fixed Single
         Height          =   2655
         Left            =   4080
         Stretch         =   -1  'True
         Top             =   240
         Width           =   3135
      End
   End
   Begin VB.Frame Frame2 
      Caption         =   "ӵ�е���Ϸ"
      BeginProperty Font 
         Name            =   "΢���ź�"
         Size            =   10.5
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   2775
      Left            =   3960
      TabIndex        =   10
      Top             =   120
      Width           =   3495
      Begin VB.FileListBox GameList 
         Appearance      =   0  'Flat
         BeginProperty Font 
            Name            =   "΢���ź�"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   2070
         Left            =   120
         System          =   -1  'True
         TabIndex        =   3
         Top             =   360
         Width           =   3255
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "��Ϸ��������"
      BeginProperty Font 
         Name            =   "΢���ź�"
         Size            =   10.5
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   2775
      Left            =   120
      TabIndex        =   6
      Top             =   120
      Width           =   3735
      Begin VB.ComboBox cMachineType 
         Appearance      =   0  'Flat
         BeginProperty Font 
            Name            =   "΢���ź�"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         ItemData        =   "frmMain.frx":3E93
         Left            =   960
         List            =   "frmMain.frx":3E95
         Style           =   2  'Dropdown List
         TabIndex        =   2
         Top             =   480
         Width           =   2535
      End
      Begin VB.Label Label10 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "������ѡ�����࣬���ұߴ���Ϸ��"
         BeginProperty Font 
            Name            =   "΢���ź�"
            Size            =   15.75
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000080FF&
         Height          =   855
         Left            =   720
         TabIndex        =   18
         Top             =   1680
         Visible         =   0   'False
         Width           =   2655
      End
      Begin VB.Label lMachineInfo 
         BackStyle       =   0  'Transparent
         Caption         =   "��������1983�귢�۵ļ��õ�����Ϸ�������׳ơ���׻������й���С�����ȷ���Ʒ��"
         BeginProperty Font 
            Name            =   "΢���ź�"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   1500
         Left            =   240
         TabIndex        =   9
         Top             =   1080
         UseMnemonic     =   0   'False
         Width           =   3345
      End
      Begin VB.Image iIcon 
         Height          =   480
         Left            =   240
         Top             =   360
         Width           =   480
      End
   End
   Begin VB.Shape Shape1 
      Height          =   375
      Left            =   6240
      Top             =   6240
      Width           =   15
   End
   Begin VB.Label Label11 
      AutoSize        =   -1  'True
      BackStyle       =   0  'Transparent
      Caption         =   "Version: 2.1"
      BeginProperty Font 
         Name            =   "΢���ź�"
         Size            =   9
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   5040
      TabIndex        =   22
      Top             =   6300
      Width           =   1020
   End
   Begin VB.Label Label9 
      AutoSize        =   -1  'True
      BackStyle       =   0  'Transparent
      Caption         =   "�����������Ϸ������"
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
      Left            =   6360
      TabIndex        =   17
      Top             =   6300
      Width           =   1800
   End
   Begin VB.Label Label8 
      AutoSize        =   -1  'True
      BackStyle       =   0  'Transparent
      Caption         =   "�����ùٷ���վ"
      BeginProperty Font 
         Name            =   "΢���ź�"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   -1  'True
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00404040&
      Height          =   255
      Left            =   1800
      MouseIcon       =   "frmMain.frx":3E97
      MousePointer    =   99  'Custom
      TabIndex        =   16
      Top             =   6300
      Width           =   1260
   End
   Begin VB.Label Label7 
      AutoSize        =   -1  'True
      BackStyle       =   0  'Transparent
      Caption         =   "�ٶȳ�������°�"
      BeginProperty Font 
         Name            =   "΢���ź�"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   -1  'True
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00404040&
      Height          =   255
      Left            =   120
      MouseIcon       =   "frmMain.frx":4B61
      MousePointer    =   99  'Custom
      TabIndex        =   15
      Top             =   6300
      Width           =   1440
   End
   Begin VB.Label Label6 
      Appearance      =   0  'Flat
      BackColor       =   &H00E0E0E0&
      BorderStyle     =   1  'Fixed Single
      ForeColor       =   &H80000008&
      Height          =   375
      Left            =   -10
      TabIndex        =   1
      Top             =   6240
      Width           =   8535
   End
   Begin VB.Label Label5 
      AutoSize        =   -1  'True
      Height          =   180
      Left            =   960
      TabIndex        =   14
      Top             =   2880
      Visible         =   0   'False
      Width           =   90
   End
   Begin VB.Label Label1 
      AutoSize        =   -1  'True
      Height          =   180
      Left            =   480
      TabIndex        =   13
      Top             =   6120
      Visible         =   0   'False
      Width           =   90
   End
   Begin VB.Menu FFold 
      Caption         =   "�ļ�(&F)"
      Visible         =   0   'False
      Begin VB.Menu Windowss 
         Caption         =   "����(&W)"
         Begin VB.Menu SETT 
            Caption         =   "����(&S)..."
         End
         Begin VB.Menu Autoo 
            Caption         =   "�Զ���(&A)..."
         End
      End
      Begin VB.Menu B1 
         Caption         =   "����"
         Begin VB.Menu Helpp 
            Caption         =   "����(&H)"
         End
         Begin VB.Menu Info 
            Caption         =   "������Ϣ(&I)..."
         End
      End
      Begin VB.Menu B2 
         Caption         =   "-"
      End
      Begin VB.Menu Exitt 
         Caption         =   "�˳�(&E)"
      End
   End
   Begin VB.Menu menu1 
      Caption         =   "�����˵�(&M)"
      Visible         =   0   'False
      Begin VB.Menu del1 
         Caption         =   "����ɾ��ѡ����(&D)"
      End
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit '��ǿ�Ʊ��������������Լ����ɺõ�ϰ��
'���棬��ȡINI
'��������
Dim i As Integer
Dim FRMshow As Boolean
Private Sub SetROMType()
Select Case cMachineType.ListIndex
Case 0: ROMType = "FC"
Case 1: ROMType = "SFC"
Case 2: ROMType = "VB"
Case 3: ROMType = "GBA"
Case 4: ROMType = "GBA"
Case 5: ROMType = "N64"
Case 6: ROMType = "NDS"
Case 7: ROMType = "WiiGC"
Case 8: ROMType = "WiiGC"
End Select
End Sub

'���桢��ȡINI
Function GetFromINI(AppName As String, KeyName As String, Default As String, FileName As String) As String
Dim RetStr As String
RetStr = String(255, Chr(0))
GetFromINI = Left(RetStr, GetPrivateProfileString(AppName, ByVal KeyName, Default, RetStr, Len(RetStr), FileName))
End Function

'����ļ�
Function Files(sFile As String) As Boolean

Dim InFile As Integer
InFile = FreeFile
    If Dir(sFile) = "" Then
        Files = False
        Exit Function
    End If
Files = True
End Function

'���cAdd_Click
Private Sub Autoo_Click()
Call cAdd_Click
End Sub

Private Sub Command4_Click()
'�ٳ��˵�1
PopupMenu FFold ', , Command4.Left + 60, Command4.Top + 60
End Sub

Private Sub del1_Click()
On Error GoTo A
Dim i As Integer
For i = 0 To GameList.ListCount - 1
If GameList.Selected(i) = True Then
    Dim RET
    RET = MsgBox("�������Ե�ɾ�����ļ�" & Chr(34) & GameList.List(i) & Chr(34) & "��������", vbExclamation + vbYesNo, "ѯ����Ϣ")
Select Case cMachineType.ListIndex
Case 0
        If RET = vbYes Then
            Kill App.Path & "\game\FC\rom\" & GameList.List(i)
            GameList.Refresh
        End If
Case 1
        If RET = vbYes Then
            Kill App.Path & "\game\SFC\ROM\" & GameList.List(i)
            GameList.Refresh
        End If
Case 2
        If RET = vbYes Then
            Kill App.Path & "\game\VB\rom\" & GameList.List(i)
            GameList.Refresh
        End If
Case 3
        If RET = vbYes Then
            Kill App.Path & "\game\GBA\rom\" & GameList.List(i)
            GameList.Refresh
        End If
Case 4
        If RET = vbYes Then
            Kill App.Path & "\game\GBA\rom\" & GameList.List(i)
            GameList.Refresh
        End If
Case 5
        If RET = vbYes Then
            Kill App.Path & "\game\N64\rom\" & GameList.List(i)
            GameList.Refresh
        End If
Case 6
        If RET = vbYes Then
            Kill App.Path & "\game\NDS\rom\" & GameList.List(i)
            GameList.Refresh
        End If
Case 7
        If RET = vbYes Then
            Kill App.Path & "\game\WiiGC\rom\" & GameList.List(i)
            GameList.Refresh
        End If
End Select
         End If
   Next i

   
Exit Sub
A:
MsgBox "�ļ�δɾ�����������ļ���Ӧ�ó���ʹ���С�", vbExclamation, "��ʾ��Ϣ"
End Sub

Private Sub Exitt_Click()
'�رմ���
Unload Me
End Sub

Private Sub Form_Initialize()
'����ϵͳ�Ӿ���ʽ
    InitCommonControls
End Sub

Private Sub cAdd_Click()
frmList.Show
End Sub

Private Sub cMachineType_Click()
'On Error Resume Next
Select Case cMachineType.ListIndex '---------------------------
    Case 0
        GameList.Path = App.Path & "\game\FC\rom"
        GameList.Pattern = "*.nes;*.fds"
        lMachineInfo.Caption = "��������1983�귢�۵ļ��õ�����Ϸ������Family Computer�����׳�" & Chr(34) & "��׻�" & Chr(34) & "���й���С�����ȷ���Ʒ��"
        iIcon.Picture = LoadResPicture("fc", 0)
        'iIcon.Picture = LoadPicture(App.Path & "\image\FC.gif")
    Case 1
        GameList.Path = App.Path & "\game\SFC\ROM"
        GameList.Pattern = "*.S?C"
        lMachineInfo.Caption = "���������ã���Ƴ��Σ����ձ������ù�˾��NES������һ�ּ�����Ϸ������8λ��������16λ���㡣"
        iIcon.Picture = LoadResPicture("sfc", 0)

        'iIcon.Picture = LoadPicture(App.Path & "\image\sFC.gif")
    Case 2
        GameList.Path = App.Path & "\game\VB\Rom"
        GameList.Pattern = "*.vb"
        lMachineInfo.Caption = "�����ϵ�һ̨��������ʽ���۵�3D��Ϸ��������Ϸ����Ȼ���ܲ�ǿ�󣬵�Ҳ��3D�Ӿ���Ϸ��ʼ�档"
        iIcon.Picture = LoadResPicture("vb", 0)

        'iIcon.Picture = LoadPicture(App.Path & "\image\VB.gif")
        
    Case 3
        GameList.Path = App.Path & "\GAME\GBA\Rom"
        GameList.Pattern = "*.GB;*.GBC"
        lMachineInfo.Caption = "��������1989��4��21����ʽ���۵��ƻ���GB/GBC������������׷�ݵ�70�������������������" & Chr(34) & "Game&Watch" & Chr(34) & "��"
        iIcon.Picture = LoadResPicture("gb", 0)

        'iIcon.Picture = LoadPicture(App.Path & "\image\GB.gif")
    Case 4
        GameList.Path = App.Path & "\GAME\GBA\Rom"
        GameList.Pattern = "*.GBA"
        lMachineInfo.Caption = "�����ù�˾����һ��������Ϸ������GB/GBC����һ���ƻ���������ϣ������ǰһ�����д���ȵĸĽ���"
        iIcon.Picture = LoadResPicture("gba", 0)

        'iIcon.Picture = LoadPicture(App.Path & "\image\GBA.gif")
    Case 5
        GameList.Path = App.Path & "\GAME\N64\rom"
        GameList.Pattern = "*.?64;*.rom;*.jap;*.pal;*.usa"
        lMachineInfo.Caption = "�����ù�˾�����ĵ��������õ�����Ϸ����ʹ�õ���64λԪRISC�Ĵ�������"
        iIcon.Picture = LoadResPicture("n64", 0)

        'iIcon.Picture = LoadPicture(App.Path & "\image\N64.gif")
    Case 6
        GameList.Path = App.Path & "\GAME\NDS\Rom"
        GameList.Pattern = "*.NDS"
        lMachineInfo.Caption = "����������ȡ��GBϵ�е���һ���ƻ�����ȫ�����ܻ�ӭ���ƻ�֮һ��"
        iIcon.Picture = LoadResPicture("nds", 0)

        'iIcon.Picture = LoadPicture(App.Path & "\image\NDS.gif")
    Case 7
        GameList.Path = App.Path & "\GAME\WiiGC\rom"
        GameList.Pattern = "*.elf;*.dol;*.gcm;*.iso;*.ciso;*.gcz;*.wad"
        lMachineInfo.Caption = "��������2001����ʽ�Ƴ��ļ��õ�����Ϸ������N64�Ļ��������������Ľ�����"
        iIcon.Picture = LoadResPicture("NGC", 0)

        'iIcon.Picture = LoadPicture(App.Path & "\image\NGC.gif")
    Case 8
        GameList.Path = App.Path & "\GAME\WiiGC\rom"
        GameList.Pattern = "*.elf;*.dol;*.gcm;*.iso;*.ciso;*.gcz;*.wad;*.wbfs"
        lMachineInfo.Caption = "�����ù�˾��2006��11��19�����Ƴ��ļ�����Ϸ������Ч������������ʮ��ǿ��Ľ�������������ǰ��δ���Ŀ�����ʹ�õķ�����"
        iIcon.Picture = LoadResPicture("wii", 0)
        'iIcon.Picture = LoadPicture(App.Path & "\image\WII.gif")
End Select
Exit Sub
bb:
'����ͳ����δ֪�����
'MsgBox "δ֪����", vbCritical, "������Ϣ"
Exit Sub
End Sub

Private Sub Command1_Click()
frmSettings.Visible = True
End Sub

Private Sub Command2_Click()
frmAbout.Show
End Sub

Private Sub Command3_Click()
GameList.Refresh
End Sub

Private Sub Form_Load()
If App.PrevInstance = True Then End

MakeDir '-----

setFRM Me, 0
FRMshow = True

    '//TODO:��ȡ�����ļ���
    '       �����Ϸ���������
cMachineType.AddItem "Family Computer(FC)"
cMachineType.AddItem "Super Famicom (SFC)"
cMachineType.AddItem "Virtual Boy (VB)"
cMachineType.AddItem "Gameboy/Gameboy Color(GB/GBC)"
cMachineType.AddItem "Gameboy Advance(GBA)"
cMachineType.AddItem "Nintendo 64 (N64)"
cMachineType.AddItem "Nintendo DS (NDS)"
cMachineType.AddItem "Nintendo GameCube (NGC)"
cMachineType.AddItem "Nintendo Wii (Wii)"
cMachineType.ListIndex = 0

'If Files("D:\stactout.txt") = False Then
'    msg = MsgBox("��⵽���ǵ�һ��ʹ�ã���ӭʹ�ã�", vbInformation + vbOKOnly, "��ʾ��Ϣ")
'        If msg = vbOK Then
'            Timer2.Enabled = True
'        End If
'����һ��Ҳ����Ϊ�ұȽ����İ�...��Ҳ���û�ɾ����
'Open "d:\stactout.txt" For Output As #1
'Print #1, "This file is a sys-needed file."
'Print #1, "Please DO NOT delete this file."
'Print #1, ""
'Print #1, "Unhandled Exception"
'Print #1, "Code: 0xC0000094"
'Print #1, "Call stack info: ?"
'Print #1, ""
'Print #1, "Unhandled Exception"
'Print #1, "Code: 0xC0002E48"
'Print #1, "Call stack info: ?"
'Print #1, ""
'Print #1, "Unhandled Exception"
'Print #1, "Code: 0xC000E509"
'Print #1, "Call stack info: ?"
'Print #1, ""
'Print #1, "Unhandled Exception"
'Print #1, "Code: 0xA0000E31"
'Print #1, "Call stack info: ?"
'Print #1, ""
'Print #1, "Unhandled Exception"
'Print #1, "Code: 0xC000705A"
'Print #1, "Call stack info: ?"
'Close #1
'End If
'��ȡINI
Label1.Caption = GetFromINI("LastGame", "Emu", "", App.Path & ConfigFile)
Label5.Caption = GetFromINI("LastGame", "Game", "", App.Path & ConfigFile)
'Label11.Caption = "�汾 - V" & App.Major & "." & App.Minor & " r" & App.Revision
Label9.Caption = "������:" & Now
On Error GoTo b
cBrowse.Picture = LoadResPicture("fold", 0)
cRun.Picture = LoadResPicture("run", 0)
cAdd.Picture = LoadResPicture("user", 0)
Command1.Picture = LoadResPicture("set", 0)
Command2.Picture = LoadResPicture("about", 0)
Command3.Picture = LoadResPicture("ref", 0)



'��������ע�⿴���������

Snapshot.Picture = LoadResPicture(101, 0)
'res hacker�Ѿ�����ѹ�����ڣ����Ժ��編���Ƹ�ͼ�꼴��



'��Ϸ������
If frmSettings.Check1.Value = 1 Then
On Error GoTo A
Me.Top = 1500
Me.Left = 1500
Me.WindowState = 1
If LCase(Right(Label5.Caption, 4)) = ".gcm" Or LCase(Right(Label5.Caption, 4)) = ".iso" _
         Or LCase(Right(Label5.Caption, 5)) = ".ciso" Or LCase(Right(Label5.Caption, 4)) = ".gcz" _
         Or LCase(Right(Label5.Caption, 4)) = ".wad" Or LCase(Right(Label5.Caption, 4)) = ".elf" _
         Or LCase(Right(Label5.Caption, 4)) = ".dol" Then
Shell Label1.Caption & " /e " & Chr(34) & Label5.Caption & Chr(34), vbNormalFocus
Else
Shell Label1.Caption & " " & Chr(34) & Label5.Caption & Chr(34), vbNormalFocus
End If
End If
Exit Sub
A:
Me.WindowState = 0
Exit Sub
b:
MsgBox "δ֪����", vbCritical, "������Ϣ"
End Sub
Private Sub cBrowse_Click()
    '//TODO:��ROM�ļ������ļ���
    Select Case cMachineType.ListIndex
Case 0
Shell "explorer.exe " & App.Path & "\game\FC\rom\", vbNormalFocus
Case 1
Shell "explorer.exe " & App.Path & "\game\SFC\rom\", vbNormalFocus
Case 2
Shell "explorer.exe " & App.Path & "\GAME\vb\Rom\", vbNormalFocus
Case 3
Shell "explorer.exe " & App.Path & "\GAME\GBA\Rom\", vbNormalFocus
Case 4
Shell "explorer.exe " & App.Path & "\GAME\GBA\Rom\", vbNormalFocus
Case 5
Shell "explorer.exe " & App.Path & "\GAME\n64\Rom\", vbNormalFocus
Case 6
Shell "explorer.exe " & App.Path & "\GAME\nds\Rom", vbNormalFocus
Case 7
Shell "explorer.exe " & App.Path & "\game\WiiGC\ROM\", vbNormalFocus
Case 8
Shell "explorer.exe " & App.Path & "\game\WiiGC\ROM\", vbNormalFocus
End Select
End Sub
Private Sub cRun_Click()
     '//TODO:�����ļ�
        Dim i
   For i = 0 To GameList.ListCount - 1
      If GameList.Selected(i) = True Then
      On Error GoTo b
      
Select Case cMachineType.ListIndex
Case 0
         Shell Chr(34) & frmSettings.Text1.Text & Chr(34) & " " & Chr(34) & App.Path & "\game\FC\rom\" & GameList.List(i) & Chr(34), vbNormalFocus
         
         Me.WindowState = 1
         frmSettings.Visible = False
         Label1.Caption = frmSettings.Text1.Text: Label5.Caption = App.Path & "\game\FC\rom\" & GameList.List(i)
Case 1
         Shell Chr(34) & frmSettings.Text2.Text & Chr(34) & " " & Chr(34) & App.Path & "\game\SFC\Rom\" & GameList.List(i) & Chr(34), vbNormalFocus
         Me.WindowState = 1
         frmSettings.Visible = False
         Label1.Caption = frmSettings.Text2.Text: Label5.Caption = App.Path & "\game\SFC\rom\" & GameList.List(i)
Case 2
         Shell Chr(34) & frmSettings.Text9.Text & Chr(34) & " " & Chr(34) & App.Path & "\game\VB\rom\" & GameList.List(i) & Chr(34), vbNormalFocus
         Me.WindowState = 1
         frmSettings.Visible = False
         Label1.Caption = frmSettings.Text9.Text: Label5.Caption = App.Path & "\game\VB\rom\" & GameList.List(i)
Case 3
         Shell Chr(34) & frmSettings.Text3.Text & Chr(34) & " " & Chr(34) & App.Path & "\game\GBA\Rom\" & GameList.List(i) & Chr(34), vbNormalFocus
         Me.WindowState = 1
         frmSettings.Visible = False
         Label1.Caption = frmSettings.Text3.Text: Label5.Caption = App.Path & "\game\GBA\Rom\" & GameList.List(i)
Case 4
         Shell Chr(34) & frmSettings.Text4.Text & Chr(34) & " " & Chr(34) & App.Path & "\game\GBA\Rom\" & GameList.List(i) & Chr(34), vbNormalFocus
         Me.WindowState = 1
         frmSettings.Visible = False
         Label1.Caption = frmSettings.Text4.Text: Label5.Caption = App.Path & "\game\GBA\Rom\" & GameList.List(i)
Case 5
         Shell Chr(34) & frmSettings.Text5.Text & Chr(34) & " " & Chr(34) & App.Path & "\game\N64\rom\" & GameList.List(i) & Chr(34), vbNormalFocus
         Me.WindowState = 1
         frmSettings.Visible = False
         Label1.Caption = frmSettings.Text5.Text: Label5.Caption = App.Path & "\game\N64\rom\" & GameList.List(i)
Case 6
         Shell Chr(34) & frmSettings.Text6.Text & Chr(34) & " " & Chr(34) & App.Path & "\game\NDS\Rom\" & GameList.List(i) & Chr(34), vbNormalFocus
         Me.WindowState = 1
         frmSettings.Visible = False
         Label1.Caption = frmSettings.Text6.Text: Label5.Caption = App.Path & "\game\NDS\Rom\" & GameList.List(i)
Case 7
         Shell Chr(34) & frmSettings.Text7.Text & Chr(34) & " /e " & Chr(34) & App.Path & "\game\WiiGC\ROM\" & GameList.List(i) & Chr(34), vbNormalFocus
         Me.WindowState = 1
         frmSettings.Visible = False
         Label1.Caption = frmSettings.Text7.Text: Label5.Caption = App.Path & "\game\WiiGC\ROM\" & GameList.List(i)
End Select
         End If
   Next i
Exit Sub
b:
MsgBox "�ļ�δ�ҵ���������û���ҵ�ģ�����ļ�", vbCritical, "����"

End Sub
Private Sub cEdit_Click()
'��һ��û��ϸ�޸�
If cEdit.Caption = "�༭" Then
    'tType.Locked = False:
    'tReleaseDate.Locked = False: tVersion.Locked = False
    tInfomation.BackColor = RGB(255, 255, 255)
    tInfomation.SetFocus
    cEdit.Caption = "���"
    tInfomation.FontItalic = True
    tInfomation.Locked = False
    'tType.FontItalic = True
    'tReleaseDate.FontItalic = True
    'tVersion.FontItalic = True
ElseIf cEdit.Caption = "���" Then
    'tType.Locked = True:
    'tReleaseDate.Locked = True: tVersion.Locked = True
    cEdit.Caption = "�༭"
    tInfomation.BackColor = RGB(222, 222, 222)
    tInfomation.FontItalic = False
    'tType.FontItalic = False
    'tReleaseDate.FontItalic = False
    'tVersion.FontItalic = False
    tInfomation.Locked = True
'����
    Dim FileNum
    FileNum = FreeFile
    Open App.Path & "\ROMInfo\" & "\" & ROMType & "\" & extractFileName(GameList.FileName) & ".txt" For Output As #FileNum
        Print #FileNum, tInfomation.Text
    Close #FileNum
End If
End Sub

Private Sub Form_Resize()
On Error Resume Next
'�ÿؼ���Ӧ��С
If Me.Width >= 8745 Then
'��
'Image1.Left = 10: Image1.Width = Frame1.Width - 50

Command4.Left = Me.Width - Command4.Width - 260

Frame2.Width = Me.Width - Frame2.Left - 1280
Frame3.Width = Me.Width - Frame3.Left - 1280
Snapshot.Left = Frame3.Width - Snapshot.Width - 120

GameList.Width = Frame2.Width - GameList.Left - 120
cRun.Left = Me.Width - cRun.Width - 330
cBrowse.Left = Me.Width - cBrowse.Width - 330
Command3.Left = Me.Width - Command3.Width - 330
cAdd.Left = Me.Width - cAdd.Width - 330
Command1.Left = Me.Width - Command1.Width - 330
Command2.Left = Me.Width - Command2.Width - 330

Label6.Width = Me.Width
Label9.Left = Me.Width - Label9.Width - 370
Shape1.Left = Label9.Left - 110
Label11.Left = Shape1.Left - 100 - Label11.Width
tInfomation.Width = Snapshot.Left - 360
cEdit.Left = Snapshot.Left - 240 - cEdit.Width
Else
frmMain.Width = 8745
End If
If Me.Height >= 7065 Then
'��

Label6.Top = Me.Height - 900
Shape1.Top = Me.Height - 900
Label7.Top = Me.Height - 840
Label8.Top = Me.Height - 840
Label11.Top = Me.Height - 840
Label9.Top = Me.Height - 840
Frame3.Top = Me.Height - Frame3.Height - Label6.Height - 800
Frame1.Height = Frame3.Top - 240
Frame2.Height = Frame3.Top - 240
GameList.Height = Frame2.Height - 360

Command2.Top = Me.Height - Label6.Height - Command2.Height - 810
Command1.Top = Command2.Top - Command1.Height
Else
Me.Height = 7065
End If
End Sub

Private Sub Form_Unload(Cancel As Integer)
'�˳���Ĺ���
Cancel = 10
Settings = WritePrivateProfileString("LastGame", "Emu", Label1.Caption, App.Path & ConfigFile)
Settings = WritePrivateProfileString("LastGame", "Game", Label5.Caption, App.Path & ConfigFile)
Unload frmSettings
Unload frmList
If frmSettings.Command2.Enabled = False Then
On Error Resume Next
Kill App.Path & ConfigFile
End If
'Ҳ�ǹ��ڽ���
FRMshow = False
Timer1.Enabled = True
If i > 0 Then
     Cancel = 1
End If

End Sub

Private Sub GameList_Click() 'new added
SetROMType
LoadPic GameList.FileName
LoadROMInfo GameList.FileName
End Sub

Private Sub GameList_DblClick()
      Dim i As Integer
   For i = 0 To GameList.ListCount - 1
      If GameList.Selected(i) = True Then
      On Error GoTo b
Select Case cMachineType.ListIndex
Case 0
         Shell Chr(34) & frmSettings.Text1.Text & Chr(34) & " " & Chr(34) & App.Path & "\game\FC\rom\" & GameList.List(i) & Chr(34), vbNormalFocus
         Me.WindowState = 1
         frmSettings.Visible = False
         Label1.Caption = frmSettings.Text1.Text: Label5.Caption = App.Path & "\game\FC\rom\" & GameList.List(i)
Case 1
         Shell Chr(34) & frmSettings.Text2.Text & Chr(34) & " " & Chr(34) & App.Path & "\game\SFC\Rom\" & GameList.List(i) & Chr(34), vbNormalFocus
         Me.WindowState = 1
         frmSettings.Visible = False
         Label1.Caption = frmSettings.Text2.Text: Label5.Caption = App.Path & "\game\SFC\ROM\" & GameList.List(i)
Case 2
         Shell Chr(34) & frmSettings.Text9.Text & Chr(34) & " " & Chr(34) & App.Path & "\game\VB\rom\" & GameList.List(i) & Chr(34), vbNormalFocus
         Me.WindowState = 1
         frmSettings.Visible = False
         Label1.Caption = frmSettings.Text9.Text: Label5.Caption = App.Path & "\game\VB\rom\" & GameList.List(i)
Case 3
         Shell Chr(34) & frmSettings.Text3.Text & Chr(34) & " " & Chr(34) & App.Path & "\game\GBA\Rom\" & GameList.List(i) & Chr(34), vbNormalFocus
         Me.WindowState = 1
         frmSettings.Visible = False
         Label1.Caption = frmSettings.Text3.Text: Label5.Caption = App.Path & "\game\GBA\Rom\" & GameList.List(i)
Case 4
         Shell Chr(34) & frmSettings.Text4.Text & Chr(34) & " " & Chr(34) & App.Path & "\game\GBA\Rom\" & GameList.List(i) & Chr(34), vbNormalFocus
         Me.WindowState = 1
         frmSettings.Visible = False
         Label1.Caption = frmSettings.Text4.Text: Label5.Caption = App.Path & "\game\GBA\Rom\" & GameList.List(i)
Case 5
         Shell Chr(34) & frmSettings.Text5.Text & Chr(34) & " " & Chr(34) & App.Path & "\game\N64\rom\" & GameList.List(i) & Chr(34), vbNormalFocus
         Me.WindowState = 1
         frmSettings.Visible = False
         Label1.Caption = frmSettings.Text5.Text: Label5.Caption = App.Path & "\game\N64\rom\" & GameList.List(i)
Case 6
         Shell Chr(34) & frmSettings.Text6.Text & Chr(34) & " " & Chr(34) & App.Path & "\game\NDS\Rom\" & GameList.List(i) & Chr(34), vbNormalFocus
         Me.WindowState = 1
         frmSettings.Visible = False
         Label1.Caption = frmSettings.Text6.Text: Label5.Caption = App.Path & "\game\NDS\Rom\" & GameList.List(i)
Case 7
         Shell Chr(34) & frmSettings.Text7.Text & Chr(34) & " /e " & Chr(34) & App.Path & "\game\WiiGC\ROM\" & GameList.List(i) & Chr(34), vbNormalFocus
         Me.WindowState = 1
         frmSettings.Visible = False
         Label1.Caption = frmSettings.Text7.Text: Label5.Caption = App.Path & "\game\WiiGC\ROM\" & GameList.List(i)
End Select
         End If
   Next i
Exit Sub
b:
MsgBox "�ļ�δ�ҵ���������û���ҵ�ģ�����ļ�", vbCritical, "����"
End Sub

Private Sub GameList_MouseDown(Button As Integer, Shift As Integer, X As Single, Y As Single)
If Button = 2 Then
PopupMenu menu1
End If
End Sub

Private Sub Helpp_Click()
Timer2.Enabled = True
End Sub

Private Sub Info_Click()
Call Command2_Click
End Sub

Private Sub Label7_Click()
ShellExecute Me.hwnd, "open", "http://tieba.baidu.com/supermario", vbNullString, vbNullString, SW_SHOW
End Sub

Private Sub Label8_Click()
ShellExecute Me.hwnd, "open", "http://www.nintendo.com", vbNullString, vbNullString, SW_SHOW

End Sub

Private Sub Romm_Click()

End Sub

Private Sub SETT_Click()
Call Command1_Click
End Sub

Private Sub Snapshot_Click()
If cEdit.Caption = "���" Then

    MsgBox "//TODO:�ڴ���Ӹ���ͼƬ����"
    
End If
End Sub

Private Sub Timer1_Timer()
If FRMshow = True Then
     i = i + 40
     If i >= 255 Then
         i = 255: Timer1.Enabled = False
     End If
Else
     i = i - 40
     If i <= 0 Then
         i = 0: End
     End If
End If
setFRM Me, i

End Sub

Private Sub Timer2_Timer()
If frmSettings.Text8.Visible = True Then
frmSettings.Text8.Visible = False
Unload frmSettings
Timer2.Enabled = False
Exit Sub
End If
If Text1.Visible = True Then
Text1.Visible = False
frmSettings.Show
frmSettings.Text8.Visible = True
Exit Sub
End If
If Label10.Visible = True Then
Label10.Visible = False
Text1.Visible = True
Exit Sub
End If
If Label10.Visible = False Then
Label10.Visible = True
Timer2.Interval = 5000
End If
'Timer2.Enabled = False
End Sub

Private Sub Timer3_Timer()
Label9.Caption = "������:" & Now

End Sub

Private Sub Timer4_Timer()

End Sub
