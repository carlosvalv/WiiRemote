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
End Class
