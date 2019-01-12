Imports System.Windows.Forms

Public Class firstrun12
    Private Sub firstrun12_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ProgressBar1.Value = "93"
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
        firstrun11.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        firstrun13.Show()
    End Sub
End Class
