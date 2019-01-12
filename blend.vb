Public Class blend

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text = "" Then
            TextBox1.Text = "0"
        ElseIf TextBox1.Text <= "80" Then
            TrackBar1.Value = TextBox1.Text
        Else
        End If
    End Sub
    Private Sub TrackBar1_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar1.ValueChanged
        TextBox1.Text = TrackBar1.Value
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub blend_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.blender = 0 Then
            TextBox1.Text = 0
            TrackBar1.Value = TextBox1.Text
        Else
            TextBox1.Text = 100 - (My.Settings.blender * 100)
            TrackBar1.Value = TextBox1.Text
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Make sure that the blender is between 0-80!
        If TextBox1.Text = "" Or TextBox1.Text > 80 Then
            Dim error8 As Integer
            error8 = MsgBox("Select a blender value from 0-80% - Error 8", MsgBoxStyle.Critical, "Error 8")
        ElseIf TrackBar1.Value = 0 Then
            Form1.Opacity = 1
            My.Settings.blender = 0
            Me.Close()
        Else
            My.Settings.blender = (100 - TrackBar1.Value) / 100
            Form1.Opacity = My.Settings.blender
            Me.Close()
        End If
    End Sub
End Class