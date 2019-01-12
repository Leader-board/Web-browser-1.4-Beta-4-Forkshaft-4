<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class htmlcode
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
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.RangeModeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Tab1ModeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Tab2ModeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveHTMLCodeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.Tab3ModeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBox1.Location = New System.Drawing.Point(0, 24)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.ReadOnly = True
        Me.RichTextBox1.Size = New System.Drawing.Size(416, 450)
        Me.RichTextBox1.TabIndex = 0
        Me.RichTextBox1.Text = "(HTML TEXT)"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RangeModeToolStripMenuItem, Me.FileToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(416, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'RangeModeToolStripMenuItem
        '
        Me.RangeModeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Tab1ModeToolStripMenuItem, Me.Tab2ModeToolStripMenuItem, Me.Tab3ModeToolStripMenuItem})
        Me.RangeModeToolStripMenuItem.Name = "RangeModeToolStripMenuItem"
        Me.RangeModeToolStripMenuItem.Size = New System.Drawing.Size(83, 20)
        Me.RangeModeToolStripMenuItem.Text = "RangeMode"
        '
        'Tab1ModeToolStripMenuItem
        '
        Me.Tab1ModeToolStripMenuItem.Name = "Tab1ModeToolStripMenuItem"
        Me.Tab1ModeToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
        Me.Tab1ModeToolStripMenuItem.Text = "Tab1 Mode"
        '
        'Tab2ModeToolStripMenuItem
        '
        Me.Tab2ModeToolStripMenuItem.Name = "Tab2ModeToolStripMenuItem"
        Me.Tab2ModeToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
        Me.Tab2ModeToolStripMenuItem.Text = "Tab2 Mode"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveHTMLCodeToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'SaveHTMLCodeToolStripMenuItem
        '
        Me.SaveHTMLCodeToolStripMenuItem.Name = "SaveHTMLCodeToolStripMenuItem"
        Me.SaveHTMLCodeToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.SaveHTMLCodeToolStripMenuItem.Text = "Save HTML Code"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(358, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Tab1 Code"
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.DefaultExt = "*html|.html"
        Me.SaveFileDialog1.Title = "Save HTML code"
        '
        'Tab3ModeToolStripMenuItem
        '
        Me.Tab3ModeToolStripMenuItem.Name = "Tab3ModeToolStripMenuItem"
        Me.Tab3ModeToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.Tab3ModeToolStripMenuItem.Text = "Tab3 Mode"
        '
        'htmlcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(416, 474)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MinimizeBox = False
        Me.Name = "htmlcode"
        Me.Text = "View HTML Code"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents RangeModeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Tab1ModeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Tab2ModeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveHTMLCodeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Tab3ModeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
