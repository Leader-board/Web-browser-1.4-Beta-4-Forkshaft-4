Imports System.Windows.Forms

Public Class firstrun5

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
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
        End If
        If RadioButton4.Checked = True Then
            My.Settings.tabcolour = "Red"
            With Form1
                .TabPage1.BackColor = Color.Red
                .TabPage2.BackColor = Color.Red
                .TabPage10.BackColor = Color.Red
                .TabPage5.BackColor = Color.Red
                .TabPage4.BackColor = Color.Red
                .TabPage3.BackColor = Color.Red
                .TabPage11.BackColor = Color.Red
            End With
        ElseIf RadioButton5.Checked = True Then
            My.Settings.tabcolour = "Green"
            With Form1
                .TabPage1.BackColor = Color.Green
                .TabPage2.BackColor = Color.Green
                .TabPage10.BackColor = Color.Green
                .TabPage5.BackColor = Color.Green
                .TabPage4.BackColor = Color.Green
                .TabPage3.BackColor = Color.Green
                .TabPage11.BackColor = Color.Green
            End With
        ElseIf RadioButton6.Checked = True Then
            My.Settings.tabcolour = "Blue"
            With Form1
                .TabPage1.BackColor = Color.Blue
                .TabPage2.BackColor = Color.Blue
                .TabPage10.BackColor = Color.Blue
                .TabPage5.BackColor = Color.Blue
                .TabPage4.BackColor = Color.Blue
                .TabPage3.BackColor = Color.Blue
                .TabPage11.BackColor = Color.Blue
            End With
        ElseIf RadioButton7.Checked = True Then
            My.Settings.tabcolour = "Yellow"
            With Form1
                .TabPage1.BackColor = Color.Yellow
                .TabPage2.BackColor = Color.Yellow
                .TabPage10.BackColor = Color.Yellow
                .TabPage5.BackColor = Color.Yellow
                .TabPage4.BackColor = Color.Yellow
                .TabPage3.BackColor = Color.Yellow
                .TabPage11.BackColor = Color.Yellow
            End With
        ElseIf RadioButton8.Checked = True Then
            My.Settings.tabcolour = "White"
            With Form1
                .TabPage1.BackColor = Color.White
                .TabPage2.BackColor = Color.White
                .TabPage10.BackColor = Color.White
                .TabPage5.BackColor = Color.White
                .TabPage4.BackColor = Color.White
                .TabPage3.BackColor = Color.White
                .TabPage11.BackColor = Color.White
            End With
        End If
        Me.Close()
        firstrun6.Show()
    End Sub

    Private Sub firstrun5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.firstrunmode = "Quick" Then
            ProgressBar1.Value = "64"
        ElseIf My.Settings.firstrunmode = "Full" Then
            ProgressBar1.Value = "33"
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
        firstrun4.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        firstruncancel.Show()
    End Sub
End Class
