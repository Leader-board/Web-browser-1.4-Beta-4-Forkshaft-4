<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class help
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.OnlineHelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReturnToIndexToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.OnlineHelpToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReturnToIndexToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.MenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnlineHelpToolStripMenuItem, Me.ReturnToIndexToolStripMenuItem, Me.RefreshToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 24)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(273, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'OnlineHelpToolStripMenuItem
        '
        Me.OnlineHelpToolStripMenuItem.Name = "OnlineHelpToolStripMenuItem"
        Me.OnlineHelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.OnlineHelpToolStripMenuItem.Text = "Back"
        '
        'ReturnToIndexToolStripMenuItem
        '
        Me.ReturnToIndexToolStripMenuItem.Name = "ReturnToIndexToolStripMenuItem"
        Me.ReturnToIndexToolStripMenuItem.Size = New System.Drawing.Size(62, 20)
        Me.ReturnToIndexToolStripMenuItem.Text = "Forward"
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(58, 20)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser1.Location = New System.Drawing.Point(0, 48)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(273, 339)
        Me.WebBrowser1.TabIndex = 1
        '
        'MenuStrip2
        '
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnlineHelpToolStripMenuItem1, Me.ReturnToIndexToolStripMenuItem1})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Size = New System.Drawing.Size(273, 24)
        Me.MenuStrip2.TabIndex = 2
        Me.MenuStrip2.Text = "MenuStrip2"
        '
        'OnlineHelpToolStripMenuItem1
        '
        Me.OnlineHelpToolStripMenuItem1.Name = "OnlineHelpToolStripMenuItem1"
        Me.OnlineHelpToolStripMenuItem1.Size = New System.Drawing.Size(82, 20)
        Me.OnlineHelpToolStripMenuItem1.Text = "Online Help"
        '
        'ReturnToIndexToolStripMenuItem1
        '
        Me.ReturnToIndexToolStripMenuItem1.Name = "ReturnToIndexToolStripMenuItem1"
        Me.ReturnToIndexToolStripMenuItem1.Size = New System.Drawing.Size(99, 20)
        Me.ReturnToIndexToolStripMenuItem1.Text = "Return to index"
        '
        'help
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(273, 387)
        Me.Controls.Add(Me.WebBrowser1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MinimizeBox = False
        Me.Name = "help"
        Me.Text = "Help Center"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents OnlineHelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReturnToIndexToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip2 As System.Windows.Forms.MenuStrip
    Friend WithEvents OnlineHelpToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReturnToIndexToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
End Class
