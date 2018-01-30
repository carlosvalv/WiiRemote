Imports ESPAI_NOMS_WIIMOTE

Public Class FrmMain
    Public Declare Auto Function SetCursorPos Lib "User32.dll" (ByVal X As Integer, ByVal Y As Integer) As Long
    Public Declare Auto Function GetCursorPos Lib "User32.dll" (ByRef lpPoint As Point) As Long
    Public Declare Sub mouse_event Lib "user32" Alias "mouse_event" (ByVal dwFlags As Long, ByVal dx As Long, ByVal dy As Long, ByVal cButtons As Long, ByVal dwExtraInfo As Long)

    ' Click esquerre '
    Public Const MOUSEEVENTF_LEFTDOWN = &H2
    Public Const MOUSEEVENTF_LEFTUP = &H4

    ' Click dret '
    Public Const MOUSEEVENTF_RIGHTDOWN = &H8
    Public Const MOUSEEVENTF_RIGHTUP = &H10

    Private WithEvents p_wiimote As CL_WIIMOTE

    Private bateria As Decimal
    Private LED1 As Boolean = False
    Private LED2 As Boolean = False
    Private LED3 As Boolean = False
    Private LED4 As Boolean = False

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        p_wiimote = New CL_WIIMOTE(Me)
        If (p_wiimote.connectat) Then
            p_wiimote.mostrar_missatge("WiiMote detectat i connectat\\prem <A> o <intro> per a continuar", "A")
        Else
            p_wiimote.mostrar_missatge("No s'ha detectat cap WiiMote\\prem <intro> per a finalitzar", "")
            Me.Close()
        End If
    End Sub
    Private Sub gestio_event_wiimote(ByVal tipus As String, ByVal valor As Object) Handles p_wiimote.event_del_wiimote

        bateria = Math.Round(p_wiimote.COMANDAMENT.WiimoteState.Battery, 2)

        'lbBateria.Text = "nivell de bateria " & Math.Round(p_wiimote.COMANDAMENT.WiimoteState.Battery, 2) & " de 100"
        tipus = CL_WIIMOTE.WII_EVENT_BOTO
        If valor = "HOME" Then
            SendKeys.SendWait("^{ESC}")
            Threading.Thread.Sleep(2000)
        End If

        ' Clicks del mouse '
        If valor = "A" Then
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0)
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)
        End If

        If valor = "B" Then
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
        End If

        ' Fletxes '
        If valor = "AMUNT" Then
            SendKeys.SendWait("{UP}")
        End If

        If valor = "AVALL" Then
            SendKeys.SendWait("{DOWN}")
        End If

        If valor = "DRETA" Then
            SendKeys.SendWait("{RIGHT}")
        End If

        If valor = "ESQUERRA" Then
            SendKeys.SendWait("{LEFT}")
        End If

        ' Zoom '
        If valor = "MES" Then
            SendKeys.SendWait("^{ADD}")
        End If

        If valor = "MENYS" Then
            SendKeys.SendWait("^{SUBTRACT}")
        End If

        ' Batería '
        If bateria > 0 And bateria <= 25 Then
            LED1 = True
            LED2 = False
            LED3 = False
            LED4 = False
        End If
        If bateria > 25 And bateria <= 50 Then
            LED1 = True
            LED2 = True
            LED3 = False
            LED4 = False
        End If
        If bateria > 50 And bateria <= 75 Then
            LED1 = True
            LED2 = True
            LED3 = True
            LED4 = False
        End If
        If bateria > 75 And bateria <= 100 Then
            LED1 = True
            LED2 = True
            LED3 = True
            LED4 = True
        End If

        With p_wiimote.COMANDAMENT.WiimoteState.IRState
            Console.Write("IR0 : (" & .IRSensors(0).RawPosition.X.ToString & "," & .IRSensors(0).RawPosition.Y.ToString & ") - " & .IRSensors(0).Size)
            Console.Write("IR1 : (" & .IRSensors(1).RawPosition.X.ToString & "," & .IRSensors(1).RawPosition.Y.ToString & ") - " & .IRSensors(1).Size)
            Console.Write("IR2 : (" & .IRSensors(2).RawPosition.X.ToString & "," & .IRSensors(2).RawPosition.Y.ToString & ") - " & .IRSensors(2).Size)
            Console.Write("IR3 : (" & .IRSensors(3).RawPosition.X.ToString & "," & .IRSensors(3).RawPosition.Y.ToString & ") - " & .IRSensors(3).Size)
            .Mode = WiimoteLib.IRMode.Extended

            'lbMidPoint.Text = "(" & .RawMidpoint.X.ToString & "," & .RawMidpoint.Y.ToString & ")"

            Console.WriteLine("X: " + .RawMidpoint.X)

            Cursor.Position = New Point(Me.Width - .RawMidpoint.X, .RawMidpoint.Y)
        End With


        'With p_wiimote.COMANDAMENT.WiimoteState.AccelState
        '    lbAccel.Text = "Accel·leròmetre : X= " & .Values.X.ToString() & ", Y= " & .Values.Y.ToString() & ", Z= " & .Values.Z.ToString()
        ' End With
    End Sub
End Class
