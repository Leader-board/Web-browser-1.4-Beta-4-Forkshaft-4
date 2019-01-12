Imports System.Windows.Forms

Public Class Dialog11
    Dim error32 As Integer
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
        If RadioButton1.Checked = True Then
            My.Settings.tabtypecheck = "Tab"
            My.Settings.Save()
            Form1.TabControl1.Appearance = TabAppearance.Normal
        ElseIf RadioButton2.Checked Then
            My.Settings.tabtypecheck = "Push"
            Form1.TabControl1.Appearance = TabAppearance.Buttons
            My.Settings.Save()
        ElseIf RadioButton3.Checked = True Then
            My.Settings.tabtypecheck = "FlatPush"
            Form1.TabControl1.Appearance = TabAppearance.FlatButtons
            My.Settings.Save()
        Else
            error32 = MsgBox("Error 32 - Either the Tab , Push or the Flat Push mode should be selected.", MsgBoxStyle.Critical, "Error 32")
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub


End Class
