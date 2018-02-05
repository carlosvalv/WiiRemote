<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMain
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMain))
        Me.Icon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.MenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SortirtToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'Icon
        '
        Me.Icon.ContextMenuStrip = Me.MenuStrip
        Me.Icon.Icon = CType(resources.GetObject("Icon.Icon"), System.Drawing.Icon)
        Me.Icon.Text = "NotifyIcon1"
        Me.Icon.Visible = True
        '
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SortirtToolStripMenuItem})
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(103, 26)
        '
        'SortirtToolStripMenuItem
        '
        Me.SortirtToolStripMenuItem.Name = "SortirtToolStripMenuItem"
        Me.SortirtToolStripMenuItem.Size = New System.Drawing.Size(102, 22)
        Me.SortirtToolStripMenuItem.Text = "Sortir"
        '
        'FrmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(301, 266)
        Me.Name = "FrmMain"
        Me.ShowInTaskbar = False
        Me.Text = "Form1"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.MenuStrip.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Icon As NotifyIcon
    Friend WithEvents MenuStrip As ContextMenuStrip
    Friend WithEvents SortirtToolStripMenuItem As ToolStripMenuItem
End Class
