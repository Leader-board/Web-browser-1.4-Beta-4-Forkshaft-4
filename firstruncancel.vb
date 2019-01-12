Imports System.Windows.Forms

Public Class firstruncancel

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub TableLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel1.Paint
        If RadioButton1.Checked = True Then
            Me.Close()
            spla.loader.Value = "17"
            spla.Label3.Text = spla.loader.Value
            Form1.Show()
        ElseIf RadioButton2.Checked = True Then
            Me.Close()
            My.Settings.firstrun = "1"
            spla.loader.Value = "17"
            spla.Label3.Text = spla.loader.Value
            Form1.Show()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        firstrun.Show()
    End Sub
End Class
