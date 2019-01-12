Public NotInheritable Class spla
    Private Sub spla_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.firstrun = 0 Then
            firstrun.Show()
            loader.Value = "3"
            Label3.Text = loader.Value
        Else
            Form1.Show()
        End If
    End Sub
End Class
