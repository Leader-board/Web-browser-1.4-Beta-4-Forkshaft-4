Imports System.Windows.Forms

Public Class firstrun3
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
        firstrun2.Show()
    End Sub

    Private Sub firstrun3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.firstrunmode = "Quick" Then
            ProgressBar1.Value = "26"
        ElseIf My.Settings.firstrunmode = "Full" Then
            ProgressBar1.Value = "15"
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
            My.Settings.homepage = TextBox1.Text
            My.Settings.homepage2 = TextBox2.Text
        Me.Close()
        firstrun4.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        firstruncancel.Show()
    End Sub
End Class
