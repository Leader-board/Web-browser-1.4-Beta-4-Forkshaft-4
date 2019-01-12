Imports System.Windows.Forms

Public Class Dialog12

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
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
            Dim error9 As Integer
            error9 = MsgBox("Error 9 - At least 1 colour style should be selected", MsgBoxStyle.Critical, "Error 9")
        End If
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
