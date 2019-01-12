Public Class webadvanced

    Private Sub webadvanced_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.websitedrag = "Yes" Then
            RadioButton1.Checked = True
        ElseIf My.Settings.websitedrag = "No" Then
            RadioButton2.Checked = True
        End If
        If My.Settings.scripterrors = "Yes" Then
            RadioButton3.Checked = True
        ElseIf My.Settings.scripterrors = "No" Then
            RadioButton4.Checked = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If RadioButton1.Checked = True Then
            My.Settings.websitedrag = "Yes"
            Form1.WebBrowser2.AllowWebBrowserDrop = True
            Form1.WebBrowser1.AllowWebBrowserDrop = True
            Form1.WebBrowser3.AllowDrop = True
        ElseIf RadioButton2.Checked = True Then
            My.Settings.websitedrag = "No"
            Form1.WebBrowser2.AllowWebBrowserDrop = False
            Form1.WebBrowser1.AllowWebBrowserDrop = False
            Form1.WebBrowser3.AllowDrop = False
        End If
        If RadioButton3.Checked = True Then
            My.Settings.scripterrors = "True"
            Form1.WebBrowser2.ScriptErrorsSuppressed = False
            Form1.WebBrowser1.ScriptErrorsSuppressed = False
        ElseIf RadioButton4.Checked = True Then
            My.Settings.scripterrors = "False"
            Form1.WebBrowser2.ScriptErrorsSuppressed = True
            Form1.WebBrowser1.ScriptErrorsSuppressed = True
        End If
    End Sub
End Class