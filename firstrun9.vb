Imports System.Windows.Forms

Public Class firstrun9
    Private Sub firstrun9_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ProgressBar1.Value = "71"
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
        firstrun8.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        firstruncancel.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        firstrun10.Show()
    End Sub
End Class
