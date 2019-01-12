Imports System.Windows.Forms

Public Class firstrun13

    Private Sub firstrun13_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ProgressBar1.Value = "100"
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
        firstrun12.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        spla.loader.Value = "17"
        spla.Label3.Text = spla.loader.Value
        Me.Close()
        My.Settings.firstrun = "1"
        My.Settings.Save()
        Form1.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        firstruncancel.Show()
    End Sub
End Class
