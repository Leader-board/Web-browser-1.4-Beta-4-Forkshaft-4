Imports System.Windows.Forms


Public Class Dialog4
    Dim checkk As Integer
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.Hide()
        MsgBox(Dialog3.TextBox1.Text, MsgBoxStyle.Critical, Dialog2.TextBox1.Text)
        MsgBox(Dialog3.TextBox1.Text, MsgBoxStyle.Exclamation, Dialog2.TextBox1.Text)
        MsgBox(Dialog3.TextBox1.Text, MsgBoxStyle.Question, Dialog2.TextBox1.Text)
        MsgBox(Dialog3.TextBox1.Text, MsgBoxStyle.Information, Dialog2.TextBox1.Text)
        MsgBox(Dialog3.TextBox1.Text, , Dialog2.TextBox1.Text)
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.Hide()
        Dialog5.Show()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Me.Hide()
        Dialog3.Show()
    End Sub
End Class
