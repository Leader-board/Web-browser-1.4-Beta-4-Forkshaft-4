Imports System.Windows.Forms

Public Class firstrun4

    Private Sub firstrun4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.firstrunmode = "Quick" Then
            ProgressBar1.Value = "43"
        ElseIf My.Settings.firstrunmode = "Full" Then
            ProgressBar1.Value = "25"
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
        firstrun3.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If RadioButton1.Checked = True Then
            My.Settings.searchbase = "Bing"
        ElseIf RadioButton2.Checked = True Then
            My.Settings.searchbase = "Google"
        ElseIf RadioButton3.Checked = True Then
            My.Settings.searchbase = "Yahoo"
        Else
            MsgBox("Error 3 - Select 1 search engine", MsgBoxStyle.Critical, "Error 3")
        End If
        Me.Close()
        firstrun5.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        firstruncancel.Show()
    End Sub
End Class
