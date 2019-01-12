Imports System.Windows.Forms

Public Class firstrun7


    Private Sub firstrun7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.firstrunmode = "Quick" Then
            ProgressBar1.Value = "100"
        ElseIf My.Settings.firstrunmode = "Full" Then
            ProgressBar1.Value = "55"
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If My.Settings.firstrunmode = "Quick" Then
            spla.loader.Value = "17"
            spla.Label3.Text = spla.loader.Value
            My.Settings.firstrun = "1"
            Me.Close()
            Form1.Show()
        ElseIf My.Settings.firstrunmode = "Full" Then
            Me.Close()
            firstrun8.Show()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
        firstrun6.Show()
    End Sub
End Class
