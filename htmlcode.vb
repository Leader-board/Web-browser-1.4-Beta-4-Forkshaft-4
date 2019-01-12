Public Class htmlcode
    Private Sub htmlcode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RichTextBox1.Text = Form1.WebBrowser2.DocumentText
    End Sub

    Private Sub Tab1ModeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Tab1ModeToolStripMenuItem.Click
        RichTextBox1.Text = Form1.WebBrowser2.DocumentText
        Label1.Text = "Tab1 Code"
    End Sub

    Private Sub Tab2ModeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Tab2ModeToolStripMenuItem.Click
        RichTextBox1.Text = Form1.WebBrowser1.DocumentText
        Label1.Text = "Tab2 Code"
    End Sub

    Private Sub SaveHTMLCodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveHTMLCodeToolStripMenuItem.Click
        SaveFileDialog1.AutoUpgradeEnabled = True
        SaveFileDialog1.AddExtension = True
        SaveFileDialog1.OverwritePrompt = True

    End Sub

    Private Sub Tab3ModeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Tab3ModeToolStripMenuItem.Click
        RichTextBox1.Text = Form1.WebBrowser3.Document.TextContent
    End Sub
End Class