Public Class search_select

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If RadioButton1.Checked = True Then
            My.Settings.searchbase = "Bing"
            My.Settings.Save()
        ElseIf RadioButton2.Checked = True Then
            My.Settings.searchbase = "Google"
            My.Settings.Save()
        ElseIf RadioButton3.Checked = True Then
            My.Settings.searchbase = "Yahoo"
            My.Settings.Save()
        End If
    End Sub
End Class