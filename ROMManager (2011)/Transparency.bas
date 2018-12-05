Attribute VB_Name = "Transparency"
Option Explicit
Private Declare Function GetWindowLong Lib "user32.dll" Alias "GetWindowLongA" (ByVal hwnd As Long, ByVal nIndex As Long) As Long
Private Declare Function SetLayeredWindowAttributes Lib "user32.dll" (ByVal hwnd As Long, ByVal crKey As Long, ByVal bAlpha As Byte, ByVal dwFlags As Long) As Long
Private Declare Function SetWindowLong Lib "user32.dll" Alias "SetWindowLongA" (ByVal hwnd As Long, ByVal nIndex As Long, ByVal dwNewLong As Long) As Long

Private Const LWA_ALPHA = &H2
Private Const LWA_COLORKEY = &H1
Private Const GWL_EXSTYLE = -20
Private Const WS_EX_LAYERED = &H80000

Public Sub setFRM(FRM As Form, ByVal limpid As Long) ' 设置窗体透明度
     Call SetWindowLong(FRM.hwnd, GWL_EXSTYLE, GetWindowLong(FRM.hwnd, GWL_EXSTYLE) Or WS_EX_LAYERED)
     Call SetLayeredWindowAttributes(FRM.hwnd, 0, limpid, LWA_ALPHA)     'limpid在0--255之间
End Sub
