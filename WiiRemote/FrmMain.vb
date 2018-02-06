Imports System.ComponentModel
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
    Private pulsAnterior As String = ""
    Private WithEvents p_wiimote As CL_WIIMOTE
    Private workerState As Boolean = False
    Private bateria As Decimal
    Private dteInicio As DateTime = DateTime.Now
    Dim lngTiempoTranscurrido As Long = 0
    Private unoPulsado As Boolean = False


    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tmBateria.Start()
        p_wiimote = New CL_WIIMOTE(Me)
        If (Not p_wiimote.connectat) Then
            p_wiimote.mostrar_missatge("No s'ha detectat cap WiiMote\\prem <intro> per a finalitzar", "")
            Me.Close()
        End If
    End Sub
    Private Sub gestio_event_wiimote(ByVal tipus As String, ByVal valor As Object) Handles p_wiimote.event_del_wiimote
        Dim x_cursor As Double
        Dim y_cursor As Double

        If (tipus = CL_WIIMOTE.WII_EVENT_BOTO) Then
            GestionarBotons(valor)
            pulsAnterior = valor

        End If

        With p_wiimote.COMANDAMENT.WiimoteState.IRState
            .Mode = WiimoteLib.IRMode.Extended
            x_cursor = (.RawMidpoint.X / 1024) * screenWidth
            y_cursor = (.RawMidpoint.Y / 640) * screenHeight
            Cursor.Position = New Point(screenWidth - x_cursor, y_cursor)
        End With



    End Sub


    Sub GestionarBotons(ByVal result As String)
        Dim dteFinal As DateTime = DateTime.Now
        lngTiempoTranscurrido = DateDiff(DateInterval.Second, dteInicio, dteFinal)
        'If result IsNot "1" AndAlso pulsAnterior = "1" Then
        '    mouse_event(&H4, 0, 0, 0, 0)
        'End If

        Select Case result
            Case "AMUNT"
                If (lngTiempoTranscurrido * 1000) >= 22 Then
                    dteInicio = DateTime.Now
                    SendKeys.SendWait("{UP}")
                End If

            Case "AVALL"
                If (lngTiempoTranscurrido * 1000) >= 22 Then
                    dteInicio = DateTime.Now
                    SendKeys.SendWait("{DOWN}")
                End If

            Case "DRETA"
                If (lngTiempoTranscurrido * 1000) >= 22 Then
                    dteInicio = DateTime.Now
                    SendKeys.SendWait("{RIGHT}")
                End If
            Case "ESQUERRA"
                If (lngTiempoTranscurrido * 1000) >= 22 Then
                    dteInicio = DateTime.Now
                    SendKeys.SendWait("{LEFT}")
                End If
            Case "MES"
                If (lngTiempoTranscurrido * 1000) >= 600 Then
                    dteInicio = DateTime.Now
                    SendKeys.SendWait("^{ADD}")
                End If


            Case "MENYS"
                If (lngTiempoTranscurrido * 1000) >= 600 Then
                    dteInicio = DateTime.Now
                    SendKeys.SendWait("^{SUBTRACT}")
                End If


            Case "HOME"
                If (lngTiempoTranscurrido * 1000) >= 300 Then
                    dteInicio = DateTime.Now
                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
                    mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
                End If

            Case "A"
                If (lngTiempoTranscurrido * 1000) >= 500 Then
                    dteInicio = DateTime.Now
                    mouse_event(&H2, 0, 0, 0, 0)
                    mouse_event(&H4, 0, 0, 0, 0)
                End If


            Case "B"
                If (lngTiempoTranscurrido * 1000) >= 500 Then
                    dteInicio = DateTime.Now
                    mouse_event(&H2, 0, 0, 0, 0)
                    mouse_event(&H4, 0, 0, 0, 0)
                    mouse_event(&H2, 0, 0, 0, 0)
                    mouse_event(&H4, 0, 0, 0, 0)
                End If
            Case "1"
                'If (lngTiempoTranscurrido * 1000) >= 500 AndAlso Not pulsAnterior = "1" Then
                '    dteInicio = DateTime.Now
                '    mouse_event(&H2, 0, 0, 0, 0)
                '    unoPulsado = True
                'End If
                If (lngTiempoTranscurrido * 1000) >= 500 AndAlso Not unoPulsado Then
                    dteInicio = DateTime.Now
                    mouse_event(&H2, 0, 0, 0, 0)
                    unoPulsado = True
                End If
                If (lngTiempoTranscurrido * 1000) >= 500 AndAlso unoPulsado Then
                    dteInicio = DateTime.Now
                    mouse_event(&H4, 0, 0, 0, 0)
                    unoPulsado = False
                End If
            Case "2"
                If (lngTiempoTranscurrido * 1000) >= 500 Then
                    dteInicio = DateTime.Now
                    Me.Close()
                End If
        End Select

    End Sub

    Sub GestionarLedsBateria()
        bateria = Math.Round(p_wiimote.COMANDAMENT.WiimoteState.Battery, 2)

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

    Private Sub bgWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgWorker.DoWork
        GestionarBotons(sender.ToString)
    End Sub

    Private Sub bgWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgWorker.RunWorkerCompleted
        workerState = False
    End Sub

    Private Sub tmBateria_Tick(sender As Object, e As EventArgs) Handles tmBateria.Tick
        GestionarLedsBateria()
        tmBateria.Interval = 60000

    End Sub

    Private Sub ModoAhorroToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModoAhorroToolStripMenuItem.Click
        If Not ModoAhorroToolStripMenuItem.Checked Then
            p_wiimote.COMANDAMENT.SetLEDs(0)
            tmBateria.Stop()
            ModoAhorroToolStripMenuItem.Checked = True
        Else
            tmBateria.Interval = 100
            tmBateria.Start()
            ModoAhorroToolStripMenuItem.Checked = False

        End If

    End Sub
End Class
