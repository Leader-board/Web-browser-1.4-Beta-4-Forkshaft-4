Imports System.Windows.Forms

Public Class firstrun10

    Private Sub firstrun10_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ProgressBar1.Value = "80"
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
        firstrun9.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        firstrun11.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        firstruncancel.Show()
    End Sub
End Class
