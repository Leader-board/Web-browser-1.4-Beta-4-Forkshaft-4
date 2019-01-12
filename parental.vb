Public Class parental
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text = TextBox2.Text Then
            'Next step
            My.Settings.parentpassword = TextBox1.Text
            My.Settings.parentalload = "Yes"
        Else
            Dim error1 As Integer
            error1 = MsgBox("Error 12 - The passwords do not match")
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.PasswordChar = "*" Then
            TextBox1.PasswordChar = ""
        ElseIf TextBox1.PasswordChar = "" Then
            TextBox1.PasswordChar = "*"
        End If
    End Sub
End Class