Public Class help

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub OnlineHelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnlineHelpToolStripMenuItem.Click
        WebBrowser1.GoBack()
    End Sub

    Private Sub ReturnToIndexToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturnToIndexToolStripMenuItem.Click
        WebBrowser1.GoForward()
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        WebBrowser1.Refresh()
    End Sub
End Class