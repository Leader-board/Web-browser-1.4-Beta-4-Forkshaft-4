Imports System.Windows.Forms

Public Class firstrun8

    Private Sub firstrun8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ProgressBar1.Value = "62"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        firstrun9.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        firstruncancel.Show()
    End Sub
End Class
