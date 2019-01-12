Public Class homepage

    Private Sub homepage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = My.Settings.homepage
        TextBox2.Text = My.Settings.homepage2
        TextBox3.Text = My.Settings.homepage3
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Settings.homepage = TextBox1.Text
        My.Settings.homepage2 = TextBox2.Text
        My.Settings.homepage3 = TextBox3.Text
        My.Settings.Save()
        Me.Close()
    End Sub
End Class