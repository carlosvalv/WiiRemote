Imports ESPAI_NOMS_WIIMOTE

Public Class FrmMain
    Private WithEvents p_wiimote As CL_WIIMOTE

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
        'lbBateria.Text = "nivell de bateria " & Math.Round(p_wiimote.COMANDAMENT.WiimoteState.Battery, 2) & " de 100"
        tipus = CL_WIIMOTE.WII_EVENT_BOTO
        If valor = "HOME" Then
            SendKeys.SendWait("^{ESC}")
            Threading.Thread.Sleep(2000)
        Else

        End If

        With p_wiimote.COMANDAMENT.WiimoteState.IRState
            Console.Write("IR0 : (" & .IRSensors(0).RawPosition.X.ToString & "," & .IRSensors(0).RawPosition.Y.ToString & ") - " & .IRSensors(0).Size)
            Console.Write("IR1 : (" & .IRSensors(1).RawPosition.X.ToString & "," & .IRSensors(1).RawPosition.Y.ToString & ") - " & .IRSensors(1).Size)
            Console.Write("IR2 : (" & .IRSensors(2).RawPosition.X.ToString & "," & .IRSensors(2).RawPosition.Y.ToString & ") - " & .IRSensors(2).Size)
            Console.Write("IR3 : (" & .IRSensors(3).RawPosition.X.ToString & "," & .IRSensors(3).RawPosition.Y.ToString & ") - " & .IRSensors(3).Size)
            .Mode = WiimoteLib.IRMode.Extended

            'lbMidPoint.Text = "(" & .RawMidpoint.X.ToString & "," & .RawMidpoint.Y.ToString & ")"


            x_cursor = (.RawMidpoint.X / 1024) * 1366
            y_cursor = (.RawMidpoint.Y / 640) * 768
            Cursor.Position = New Point(1366 - x_cursor, y_cursor)
        End With


        'With p_wiimote.COMANDAMENT.WiimoteState.AccelState
        '    lbAccel.Text = "Accel·leròmetre : X= " & .Values.X.ToString() & ", Y= " & .Values.Y.ToString() & ", Z= " & .Values.Z.ToString()
        ' End With
    End Sub
End Class
