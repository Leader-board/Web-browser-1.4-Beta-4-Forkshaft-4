Imports System.Windows.Forms

Public Class firstrun6

    Private Sub firstrun6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.firstrunmode = "Quick" Then
            ProgressBar1.Value = "82"
        ElseIf My.Settings.firstrunmode = "Full" Then
            ProgressBar1.Value = "47"
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If RadioButton1.Checked = True Then
            My.Settings.menucolour = "Red"
            Form1.MenuStrip1.BackColor = Color.Red
            My.Settings.Save()
        ElseIf RadioButton2.Checked = True Then
            My.Settings.menucolour = "Green"
            Form1.MenuStrip1.BackColor = Color.Green
            My.Settings.Save()
        ElseIf RadioButton3.Checked = True Then
            My.Settings.menucolour = "Blue"
            Form1.MenuStrip1.BackColor = Color.Blue
            My.Settings.Save()
        ElseIf RadioButton4.Checked = True Then
            My.Settings.menucolour = "Yellow"
            Form1.MenuStrip1.BackColor = Color.Yellow
            My.Settings.Save()
        ElseIf RadioButton5.Checked = True Then
            My.Settings.menucolour = "White"
            Form1.MenuStrip1.BackColor = Color.White
            My.Settings.Save()
        Else
            Dim error34 As Integer
            error34 = MsgBox("Error 34 - At least 1 colour style should be selected", MsgBoxStyle.Critical, "Error 34")
        End If
        Me.Close()
        firstrun7.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
        firstrun5.Show()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        firstruncancel.Show()
    End Sub
End Class
