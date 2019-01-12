Imports System.Windows.Forms

Public Class Dialog7
    Dim rplus As Integer
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        ' Make sure thaat the settings are correct
        If CheckBox1.Checked = True Then
            Form1.ToolTip1.Active = True
        Else
            Form1.ToolTip1.Active = False
        End If
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click

        Me.Close()
    End Sub

End Class
