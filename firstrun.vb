Public Class firstrun
    Dim error94 As Integer

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        spla.loader.Value = "4"
        spla.Label3.Text = spla.loader.Value
        If RadioButton1.Checked = True Then
            My.Settings.firstrunmode = "Quick"
            My.Settings.Save()
            firstrun2.Show()
            firstrun2.ProgressBar1.Value = "15"
            Me.Close()
        ElseIf RadioButton2.Checked = True Then
            My.Settings.firstrunmode = "Full"
            My.Settings.Save()
            firstrun2.Show()
            firstrun2.ProgressBar1.Value = "8"
            Me.Close()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        firstruncancel.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        My.Settings.firstrun = "1"
        My.Settings.Save()
    End Sub
End Class