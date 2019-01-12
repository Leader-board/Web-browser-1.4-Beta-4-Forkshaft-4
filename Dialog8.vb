Imports System.Windows.Forms
REM This is the dialog box for Ribbon
Public Class Dialog8
    REM Declare the necessary variables
    Dim dance As Integer
    REM Start the main part of the program
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If RadioButton1.Checked = True Then
            Form1.ribbonshow()
            My.Settings.ribbonmenuverify = "Ribbon"
            My.Settings.Save()
        ElseIf RadioButton2.Checked = True Then
            Form1.ribbonhide()
            My.Settings.ribbonmenuverify = "Menu"
            My.Settings.Save()
        Else
            dance = MsgBox("You must select either the Ribbon or the menu.Error 39", vbCritical, "Error")
        End If
        Me.Close()
    End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.Close()
    End Sub
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        help.Show()
    End Sub
End Class
