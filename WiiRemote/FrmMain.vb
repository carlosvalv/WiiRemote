Imports System.Runtime.InteropServices
Imports ESPAI_NOMS_WIIMOTE


Public Class FrmMain
    <DllImport("user32.dll")>
    Private Shared Sub mouse_event(dwFlags As UInteger, dx As UInteger, dy As UInteger, dwData As UInteger, dwExtraInfo As Integer)
    End Sub
    Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
    Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height

    Public Declare Auto Function SetCursorPos Lib "User32.dll" (ByVal X As Integer, ByVal Y As Integer) As Long
    Public Declare Auto Function GetCursorPos Lib "User32.dll" (ByRef lpPoint As Point) As Long

    ' Click esquerre '
    Public Const MOUSEEVENTF_LEFTDOWN = &H2
    Public Const MOUSEEVENTF_LEFTUP = &H4

    ' Click dret '
    Public Const MOUSEEVENTF_RIGHTDOWN = &H8
    Public Const MOUSEEVENTF_RIGHTUP = &H10

    Private WithEvents p_wiimote As CL_WIIMOTE

    Private bateria As Decimal

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
        Dim x_cursor As Double
        Dim y_cursor As Double
        tipus = CL_WIIMOTE.WII_EVENT_BOTO
        Dim gestionarb As Task = GestionarBotons(valor)

        GestionarLedsBateria()


        With p_wiimote.COMANDAMENT.WiimoteState.IRState
            Console.Write("IR0 : (" & .IRSensors(0).RawPosition.X.ToString & "," & .IRSensors(0).RawPosition.Y.ToString & ") - " & .IRSensors(0).Size)
            Console.Write("IR1 : (" & .IRSensors(1).RawPosition.X.ToString & "," & .IRSensors(1).RawPosition.Y.ToString & ") - " & .IRSensors(1).Size)
            Console.Write("IR2 : (" & .IRSensors(2).RawPosition.X.ToString & "," & .IRSensors(2).RawPosition.Y.ToString & ") - " & .IRSensors(2).Size)
            Console.Write("IR3 : (" & .IRSensors(3).RawPosition.X.ToString & "," & .IRSensors(3).RawPosition.Y.ToString & ") - " & .IRSensors(3).Size)
            .Mode = WiimoteLib.IRMode.Extended



            x_cursor = (.RawMidpoint.X / 1024) * 1366
            y_cursor = (.RawMidpoint.Y / 640) * 768
            Cursor.Position = New Point(1366 - x_cursor, y_cursor)
        End With

    End Sub

    Async Function GestionarBotons(ByVal result As String) As Task
        Select Case result
            Case "AMUNT"
                SendKeys.SendWait("{UP}")
                Threading.Thread.Sleep(1000)

            Case "AVALL"
                SendKeys.SendWait("{DOWN}")
                Threading.Thread.Sleep(1000)

            Case "DRETA"
                SendKeys.SendWait("{RIGHT}")
                Threading.Thread.Sleep(1000)

            Case "ESQUERRA"
                SendKeys.SendWait("{LEFT}")
                Threading.Thread.Sleep(1000)

            Case "MES"
                SendKeys.SendWait("^{ADD}")
                Threading.Thread.Sleep(1000)

            Case "MENYS"
                SendKeys.SendWait("^{SUBTRACT}")
                Threading.Thread.Sleep(1000)

            Case "HOME"
                SendKeys.SendWait("^{ESC}")
                Threading.Thread.Sleep(1000)

            Case "A"
                mouse_event(&H2, 0, 0, 0, 0)

                Threading.Thread.Sleep(1000)

                mouse_event(&H4, 0, 0, 0, 0)
            Case "B"
                mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
                mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
                Threading.Thread.Sleep(1000)


        End Select


    End Function

    Async Sub GestionarLedsBateria()
        'bateria = Math.Round(p_wiimote.COMANDAMENT.WiimoteState.Battery, 2)

        If bateria > 0 And bateria <= 25 Then
            p_wiimote.COMANDAMENT.SetLEDs(True, False, False, False)
        ElseIf bateria > 25 And bateria <= 50 Then
            p_wiimote.COMANDAMENT.SetLEDs(True, True, False, False)
        ElseIf bateria > 50 And bateria <= 75 Then
            p_wiimote.COMANDAMENT.SetLEDs(True, True, True, False)
        ElseIf bateria > 75 And bateria <= 100 Then
            p_wiimote.COMANDAMENT.SetLEDs(True, True, True, True)
        End If
    End Sub
    'Sortir
    Private Sub SortirtToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SortirtToolStripMenuItem.Click
        If MessageBox.Show("Vols tancar l'aplicació", "Sortir",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub
End Class
