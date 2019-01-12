Imports System.Windows.Forms

Public Class firstrun2

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If RadioButton1.Checked = True Then
            My.Settings.ribbonmenuverify = "Ribbon"
        ElseIf RadioButton2.Checked = True Then
            My.Settings.ribbonmenuverify = "Menu"
        End If
        My.Settings.Save()
        If My.Settings.firstrunmode = "Quick" Then
            firstrun3.ProgressBar1.Value = "23"
        ElseIf My.Settings.firstrunmode = "Full" Then
            firstrun3.ProgressBar1.Value = "15"
        End If
        spla.loader.Value = "6"
        spla.Label3.Text = spla.loader.Value
        firstrun3.Show()
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.Close()
        firstruncancel.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        firstrun.Show()
        Me.Close()
    End Sub

    Private Sub firstrun2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.firstrunmode = "Quick" Then
            ProgressBar1.Value = "15"
        ElseIf My.Settings.firstrunmode = "Full" Then
            ProgressBar1.Value = "8"
        End If
    End Sub
End Class
