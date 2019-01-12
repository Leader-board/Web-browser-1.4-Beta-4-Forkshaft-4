Imports Microsoft.VisualBasic.PowerPacks.Printing

REM This is the starting of the program
Public Class Form1
    REM Declare the necessary variables
    Dim checkthefinder As Integer
    Dim racer As Integer
    Dim flick As Integer
    Dim cricket As Integer
    Dim cry As Integer
    Dim maths As Integer
    Public pinapp1 As String
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        ribbonshow()
        ribbonhide()
    End Sub
    REM Start the main part of the program
    Public Sub ribbonshow()
        With Me
            .TabControl1.Show()
            .TabControl1.Visible = True
            .TabPage1.Show()
            .Button5.Hide()
            .TextBox1.Hide()
            .Button6.Show()
            .Button6.Visible = True
            .Button7.Show()
            .Button8.Show()
            .Button9.Show()
            .Button10.Show()
            .Button11.Show()
            .Button13.Show()
            .Button14.Show()
            .Button15.Show()
            .Button16.Show()
            .Button17.Show()
            .RadioButton6.Hide()
            .RadioButton3.Hide()
            .RadioButton4.Hide()
            .Panel2.Enabled = True
            .Panel2.Visible = True
            .PictureBox1.Show()
            .GroupBox13.Visible = False
        End With
    End Sub
    Public Sub ribbonhide()
        With Me
            .TabControl1.Hide()
            .TabControl1.Visible = False
            .TabPage1.Hide()
            .Button5.Show()
            .TextBox1.Show()
            .Button6.Hide()
            .Button6.Visible = False
            .Button7.Hide()
            .Button8.Hide()
            .Button9.Hide()
            .Button10.Hide()
            .Button11.Hide()
            .Button13.Hide()
            .Button14.Hide()
            .Button15.Hide()
            .Button16.Hide()
            .RadioButton3.Show()
            .RadioButton4.Show()
            .Button17.Hide()
            .Panel2.Enabled = True
            .Panel2.Visible = True
            .PictureBox1.Hide()
            .GroupBox13.Visible = False
        End With
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'This command will tell the browser to navigate to the required webpage
        If TextBox1.Text = "command /settings-reset" Then
            My.Settings.Reset()
        Else
            If RadioButton3.Checked = True Then
                WebBrowser2.Navigate(TextBox1.Text)
            ElseIf RadioButton4.Checked = True Then
                WebBrowser1.Navigate(TextBox1.Text)
            ElseIf RadioButton6.Checked = True Then
                WebBrowser3.Navigate(TextBox1.Text)
            End If

        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'This command will tell the browser to go back one page
        If RadioButton3.Checked = True Then
            WebBrowser2.GoBack()
        ElseIf RadioButton4.Checked = True Then
            WebBrowser1.GoBack()
        ElseIf RadioButton6.Checked = True Then
            WebBrowser3.GoBack()
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'This command will tell the browser to go forward one page
        If RadioButton3.Checked = True Then
            WebBrowser2.GoForward()
        ElseIf RadioButton4.Checked = True Then
            WebBrowser1.GoForward()
        ElseIf RadioButton6.Checked = True Then
            WebBrowser3.GoForward()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        'This command will tell the browser to refresh the webpage
        If RadioButton3.Checked = True Then
            WebBrowser2.Refresh()
        ElseIf RadioButton4.Checked = True Then
            WebBrowser1.Refresh()
        ElseIf RadioButton6.Checked = True Then
            WebBrowser3.Refresh()
        End If
    End Sub

    Private Sub GoBackToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoBackToolStripMenuItem.Click
        'This command will tell the browser to go back one page
        If RadioButton3.Checked = True Then
            WebBrowser2.GoBack()
        ElseIf RadioButton4.Checked = True Then
            WebBrowser1.GoBack()
        ElseIf RadioButton6.Checked = True Then
            WebBrowser3.GoBack()
        End If
    End Sub

    Private Sub GoForwardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoForwardToolStripMenuItem.Click
        'This command will tell the browser to go forward one page
        If RadioButton3.Checked = True Then
            WebBrowser2.GoForward()
        ElseIf RadioButton4.Checked = True Then
            WebBrowser1.GoForward()
        ElseIf RadioButton6.Checked = True Then
            WebBrowser3.GoForward()
        End If
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        'This command will refresh the webpage
        If RadioButton3.Checked = True Then
            WebBrowser2.Refresh()
        ElseIf RadioButton4.Checked = True Then
            WebBrowser1.Refresh()
        ElseIf RadioButton6.Checked = True Then
            WebBrowser3.Refresh()
        End If
    End Sub
    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'This command will close the browser
        End
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        'This command will stop the webpage
        If RadioButton3.Checked = True Then
            WebBrowser2.Stop()
        ElseIf RadioButton4.Checked = True Then
            WebBrowser1.Stop()
        ElseIf RadioButton6.Checked = True Then
            WebBrowser3.Stop()
        End If
    End Sub

    Private Sub StopToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopToolStripMenuItem.Click
        'This command will stop the webpage
        If RadioButton3.Checked = True Then
            WebBrowser2.Stop()
        ElseIf RadioButton4.Checked = True Then
            WebBrowser1.Stop()
        ElseIf RadioButton6.Checked = True Then
            WebBrowser3.Stop()
        End If
    End Sub

    REM This is unnecessary code
    REM Comment out this code
    'Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
    'WebBrowser1.FindForm()
    'End Sub
    Private Sub HomeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HomeToolStripMenuItem.Click
        'This command will tell the browser to go to the home page
        If RadioButton3.Checked = True Then
            WebBrowser2.Navigate(My.Settings.homepage)
        ElseIf RadioButton4.Checked = True Then
            WebBrowser1.Navigate(My.Settings.homepage2)
        ElseIf RadioButton6.Checked = True Then
            WebBrowser3.Navigate(My.Settings.homepage3)
        End If
    End Sub
    Private Sub GoToPageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoToPageToolStripMenuItem.Click
        'This command will tell the browser to go to the page that is in the bottom of this button
        If RadioButton3.Checked = True Then
            WebBrowser2.Navigate(ToolStripTextBox1.Text)
        ElseIf RadioButton4.Checked = True Then
            WebBrowser1.Navigate(ToolStripTextBox1.Text)
        ElseIf RadioButton6.Checked = True Then
            WebBrowser3.Navigate(ToolStripTextBox1.Text)
        End If
    End Sub
    Private Sub mnuHelpAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHelpAbout.Click
        'This command will tell the browser to go to the About page
        frmAbout.Show()
    End Sub
    REM This is a function.
    REM Continue
    Public Function PrReady() As Boolean
        'Make sure the user is ready to print
        Dim intIsReady As Integer

        'The user will respond to the following
        'message box when ready for the printing
        intIsReady = MsgBox("Prepare the printer", MsgBoxStyle.OkCancel, "Print")
        '
        If (intIsReady = vbCancel) Then
            PrReady = False
        Else
            PrReady = True
        End If
    End Function

    Private Sub TextBox1_GotFocus(sender As Object, e As EventArgs) Handles TextBox1.GotFocus
        Timer3.Enabled = False
    End Sub
    Private Sub TextBox1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.LostFocus
        'This command will tell the browser that when the Textbox loses focus, add the text inside it to the History box
        Dialog6.ComboBox1.Items.Add(TextBox1.Text)
        ribhistory.Items.Add(TextBox1.Text)
        menuhistory.Items.Add(TextBox1.Text)
        Timer3.Enabled = True
    End Sub
    Private Sub MankYourOwnMessageBoxToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MankYourOwnMessageBoxToolStripMenuItem.Click
        'This command will tell the browser to go to the Make your own message box wizard
        Dialog1.Show()
    End Sub
    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'This command will tell the browser to go to your home page
        WebBrowser2.Navigate(My.Settings.homepage)
    End Sub
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        WebBrowser2.GoBack()
    End Sub
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        WebBrowser2.GoForward()
    End Sub
    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        WebBrowser2.Stop()
    End Sub
    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        WebBrowser2.Refresh()
    End Sub
    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dialog6.Show()
    End Sub
    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dialog7.Show()
    End Sub
    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dialog8.Show()
    End Sub
    Private Sub TipsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dialog7.Show()
    End Sub
    Private Sub HistoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dialog6.Show()
    End Sub
    Private Sub RibbonToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dialog8.Show()
    End Sub
    Private Sub Button12_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = ("Allows you to see the webpages you have seen in the past")
    End Sub
    Private Sub Button12_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = ("Status")
    End Sub
    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dialog1.Show()
    End Sub
    Private Sub Button13_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = ("Helps you make your own message box")
    End Sub
    Private Sub Button13_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = ("Status")
    End Sub
    Private Sub Button16_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = ("Sets options for Tips")
    End Sub
    Private Sub Button16_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = ("Status")
    End Sub
    Private Sub Button17_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = ("Allows you to choose whether you need the Ribbon interface or the Menu interface")
    End Sub
    Private Sub Button14_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = ("Displays info about this application")
    End Sub

    Private Sub Button14_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = ("Status")
    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        End
    End Sub

    Private Sub Button15_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = ("Closes the browser")
    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'This command will tell the browser to clear the browser
        WebBrowser2.Navigate("HTMLPage1.htm")
    End Sub

    Private Sub ClearTheWebpageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearTheWebpageToolStripMenuItem.Click
        'This command will tell the browser to clear the browser
        WebBrowser2.Navigate("HTMLPage1.htm")
    End Sub

    Private Sub PrintAPageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintAPageToolStripMenuItem.Click
        racer = MsgBox("Is the printer on and online?", vbQuestion + vbYesNo, "Are you ready")
        If (racer = 6) Then
            'For printing
        Else
            flick = MsgBox("The printer could not print because you did not allow it to print", vbCritical + vbRetryCancel, "Error")
            If (flick = 5) Then
                cricket = MsgBox("Is the printer on and online?", vbQuestion + vbYesNo, "Are you ready?")
                If (cricket = 6) Then
                    WebBrowser2.Print()
                Else
                    cry = MsgBox("The printer could not print because you did not allow it to print", vbCritical, "Error")
                End If
            Else
                maths = MsgBox("The printer could not print because you did not allow it to print", vbCritical, "Error")
            End If
        End If
    End Sub

    Private Sub Button6_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = ("Goes to the previous webpage")
    End Sub

    Private Sub Button6_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = ("Status")
    End Sub

    Private Sub Button7_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = ("Goes to the next webpage.")
    End Sub

    Private Sub Button7_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = ("Status")
    End Sub

    Private Sub Button8_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = ("Stops the page from being viewed.")
    End Sub

    Private Sub Button8_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = ("Status")
    End Sub

    Private Sub Button9_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = ("Refreshes the page.")
    End Sub

    Private Sub Button9_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = ("Status")
    End Sub

    Private Sub Button10_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = ("Goes to the Search wizard")
    End Sub

    Private Sub Button10_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = ("Status")
    End Sub

    Private Sub Button11_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = ("Loads your Home page.")
    End Sub

    Private Sub Button11_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = ("Status")
    End Sub

    Private Sub Button15_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = ("Status")
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'This part is for the starting part.
        'Applies the setings.
        'Blender settings
        If My.Settings.blender = 0 Then
            Me.Opacity = 1
        Else
            Me.Opacity = My.Settings.blender
        End If
        spla.loader.Value = "20"
        spla.Label3.Text = spla.loader.Value
        'Ribbon/Menu settings.
        If My.Settings.ribbonmenuverify = "Ribbon" Then
            ribbonshow()
        ElseIf My.Settings.ribbonmenuverify = "Menu" Then
            ribbonhide()
        End If
        spla.loader.Value = "34"
        spla.Label3.Text = spla.loader.Value
        'TabMode settings
        If My.Settings.tabtypecheck = "Tab" Then
            TabControl1.Appearance = TabAppearance.Normal
        ElseIf My.Settings.tabtypecheck = "Push" Then
            TabControl1.Appearance = TabAppearance.Buttons
        ElseIf My.Settings.tabtypecheck = "FlatPush" Then
            TabControl1.Appearance = TabAppearance.FlatButtons
        End If
        spla.loader.Value = "42"
        spla.Label3.Text = spla.loader.Value
        'MenuColour settings
        If My.Settings.menucolour = "Red" Then
            MenuStrip1.BackColor = Color.Red
        ElseIf My.Settings.menucolour = "Green" Then
            MenuStrip1.BackColor = Color.Green
        ElseIf My.Settings.menucolour = "Blue" Then
            MenuStrip1.BackColor = Color.Blue
        ElseIf My.Settings.menucolour = "Yellow" Then
            MenuStrip1.BackColor = Color.Yellow
        ElseIf My.Settings.menucolour = "White" Then
            MenuStrip1.BackColor = Color.White
        End If
        spla.loader.Value = "50"
        spla.Label3.Text = spla.loader.Value
        'TabColour settings
        If My.Settings.tabcolour = "Red" Then
            TabPage1.BackColor = Color.Red
            TabPage2.BackColor = Color.Red
            TabPage10.BackColor = Color.Red
            TabPage5.BackColor = Color.Red
            TabPage4.BackColor = Color.Red
            TabPage3.BackColor = Color.Red
            TabPage11.BackColor = Color.Red
        ElseIf My.Settings.tabcolour = "Green" Then
            TabPage1.BackColor = Color.Green
            TabPage2.BackColor = Color.Green
            TabPage10.BackColor = Color.Green
            TabPage5.BackColor = Color.Green
            TabPage4.BackColor = Color.Green
            TabPage3.BackColor = Color.Green
            TabPage11.BackColor = Color.Green
        ElseIf My.Settings.tabcolour = "Blue" Then
            TabPage1.BackColor = Color.Blue
            TabPage2.BackColor = Color.Blue
            TabPage10.BackColor = Color.Blue
            TabPage5.BackColor = Color.Blue
            TabPage4.BackColor = Color.Blue
            TabPage3.BackColor = Color.Blue
            TabPage11.BackColor = Color.Blue
        ElseIf My.Settings.tabcolour = "Yellow" Then
            TabPage1.BackColor = Color.Yellow
            TabPage2.BackColor = Color.Yellow
            TabPage10.BackColor = Color.Yellow
            TabPage5.BackColor = Color.Yellow
            TabPage4.BackColor = Color.Yellow
            TabPage3.BackColor = Color.Yellow
            TabPage11.BackColor = Color.Yellow
        ElseIf My.Settings.tabcolour = "White" Then
            TabPage1.BackColor = Color.White
            TabPage2.BackColor = Color.White
            TabPage10.BackColor = Color.White
            TabPage5.BackColor = Color.White
            TabPage4.BackColor = Color.White
            TabPage5.BackColor = Color.White
            TabPage11.BackColor = Color.White
        End If
        spla.loader.Value = "60"
        spla.Label3.Text = spla.loader.Value
        'For the Advanced Web settings
        'Website dragging
        If My.Settings.websitedrag = "Yes" Then
            WebBrowser2.AllowWebBrowserDrop = True
            WebBrowser1.AllowWebBrowserDrop = True
            WebBrowser3.AllowDrop = True
        ElseIf My.Settings.websitedrag = "No" Then
            WebBrowser2.AllowWebBrowserDrop = False
            WebBrowser1.AllowWebBrowserDrop = False
            WebBrowser3.AllowDrop = False
        End If
        spla.loader.Value = "65"
        spla.Label3.Text = spla.loader.Value
        'Script error suppressing
        If My.Settings.scripterrors = "Yes" Then
            WebBrowser2.ScriptErrorsSuppressed = False
            WebBrowser1.ScriptErrorsSuppressed = False
        ElseIf My.Settings.scripterrors = "No" Then
            WebBrowser2.ScriptErrorsSuppressed = True
            WebBrowser1.ScriptErrorsSuppressed = True
        End If
        spla.loader.Value = "70"
        spla.Label3.Text = spla.loader.Value
        'For the System information tab
        'OS Name
        Label20.Text = My.Computer.Info.OSFullName
        spla.loader.Value = "72"
        spla.Label3.Text = spla.loader.Value
        menuOSName.Text = My.Computer.Info.OSFullName
        spla.loader.Value = "74"
        spla.Label3.Text = spla.loader.Value
        'Version
        Label22.Text = My.Computer.Info.OSVersion
        spla.loader.Value = "77"
        spla.Label3.Text = spla.loader.Value
        'Architecture
        Label24.Text = My.Computer.Info.OSPlatform
        spla.loader.Value = "80"
        spla.Label3.Text = spla.loader.Value
        menubitcount.Text = My.Computer.Info.OSPlatform
        spla.loader.Value = "82"
        spla.Label3.Text = spla.loader.Value
        'Computer name
        Label26.Text = My.Computer.Name
        spla.loader.Value = "84"
        spla.Label3.Text = spla.loader.Value
        menucomputername.Text = My.Computer.Name
        spla.loader.Value = "86"
        spla.Label3.Text = spla.loader.Value
        'Available RAM installed
        Label28.Text = My.Computer.Info.TotalPhysicalMemory \ 1024 \ 1024
        spla.loader.Value = "90"
        spla.Label3.Text = spla.loader.Value
        menuRAM.Text = My.Computer.Info.TotalPhysicalMemory \ 1024 \ 1024
        spla.loader.Value = "92"
        spla.Label3.Text = spla.loader.Value
        'Free RAM
        Label30.Text = My.Computer.Info.AvailablePhysicalMemory
        spla.loader.Value = "94"
        spla.Label3.Text = spla.loader.Value
        'Network Status
        Label37.Text = My.Computer.Network.IsAvailable
        spla.loader.Value = "96"
        spla.Label3.Text = spla.loader.Value
        'User Name
        Label38.Text = My.User.Name + "'s" + "PC"
        spla.loader.Value = "98"
        spla.Label3.Text = spla.loader.Value
        'User role
        Label39.Text = My.Computer.Clock.LocalTime
        spla.loader.Value = "99"
        spla.Label3.Text = spla.loader.Value
        'HomePage
        If My.Settings.homepage = "" Then
        Else
            WebBrowser2.Navigate(My.Settings.homepage)
        End If
        If My.Settings.homepage2 = "" Then
        Else
            WebBrowser1.Navigate(My.Settings.homepage2)
        End If
        spla.loader.Value = "100"
        spla.Label3.Text = spla.loader.Value
        spla.Close()
    End Sub
    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Panel2.Enabled = True
        Panel2.Visible = True
        TabControl3.Visible = False
        Panel2.Show()
        Label4.Text = WebBrowser2.DocumentTitle
        Label42.Text = WebBrowser1.DocumentTitle
        Label40.Text = WebBrowser3.DocumentTitle
    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        TabControl3.Visible = True
    End Sub

    Private Sub Button14_Click_1(sender As Object, e As EventArgs) Handles Button14.Click
        frmAbout.Show()
    End Sub

    Private Sub Button17_Click_1(sender As Object, e As EventArgs) Handles Button17.Click
        Dialog8.Show()
    End Sub
    Private Sub Button18_Click_1(sender As Object, e As EventArgs)
        If RadioButton1.Checked = True Then
            WebBrowser2.Navigate("/HTMLPage1.htm")
        ElseIf RadioButton2.Checked = True Then
            WebBrowser1.Navigate("/HTMLPage1.htm")
        End If
    End Sub

    Private Sub Button16_Click_1(sender As Object, e As EventArgs)
        Dialog7.Show()
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs)
        racer = MsgBox("Is the printer on and online?", vbQuestion + vbYesNo, "Are you ready")
        If (racer = 6) Then
            WebBrowser2.Print()
        Else
            flick = MsgBox("The printer could not print because you did not allow it to print", vbCritical + vbRetryCancel, "Error")
            If (flick = 5) Then
                cricket = MsgBox("Is the printer on and online?", vbQuestion + vbYesNo, "Are you ready?")
                If (cricket = 6) Then
                    WebBrowser2.Print()
                Else
                    cry = MsgBox("The printer could not print because you did not allow it to print", vbCritical, "Error")
                End If
            Else
                maths = MsgBox("The printer could not print because you did not allow it to print", vbCritical, "Error")
            End If
        End If
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        'Commander
        If TextBox2.Text = "command /settings-reset full" Then
            My.Settings.Reset()
            MsgBox("Your settings have been reset. Please restart the browser. The First Run Wizard appears then.")
        ElseIf TextBox2.Text = "command /help" Then
            MsgBox("The Commander lobrary is a set of administrative command-line tasks. You can reset the browser settings or change advanced settings. For more details , refer to the Commander documentation in the Help Center")
        Else
            If RadioButton1.Checked = True Then
                WebBrowser2.Navigate(TextBox2.Text)
            ElseIf RadioButton2.Checked = True Then
                WebBrowser1.Navigate(TextBox2.Text)
            ElseIf RadioButton5.Checked = True Then
                Webbrowser3.Navigate(TextBox2.Text)
            End If
        End If
    End Sub
    Private Sub TextBox2_GotFocus(sender As Object, e As EventArgs) Handles TextBox2.GotFocus
        Timer3.Enabled = False
    End Sub
    Private Sub TextBox2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.LostFocus
        'This command will tell the browser that when the Textbox loses focus, add the text inside it to the History box
        Dialog6.ComboBox1.Items.Add(TextBox2.Text)
        ribhistory.Items.Add(TextBox2.Text)
        menuhistory.Items.Add(TextBox1.Text)
        Timer3.Enabled = True
    End Sub

    Private Sub Button11_Click_1(sender As Object, e As EventArgs) Handles Button11.Click
        If RadioButton1.Checked = True Then
            WebBrowser2.Navigate(My.Settings.homepage)
        ElseIf RadioButton2.Checked = True Then
            WebBrowser1.Navigate(My.Settings.homepage2)
        ElseIf RadioButton5.Checked = True Then
            WebBrowser3.Navigate(My.Settings.homepage3)
        End If
    End Sub

    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click
        If RadioButton1.Checked = True Then
            WebBrowser2.GoBack()
        ElseIf RadioButton2.Checked = True Then
            WebBrowser1.GoBack()
        ElseIf RadioButton5.Checked = True Then
            WebBrowser3.GoBack()
        End If
    End Sub

    Private Sub Button7_Click_1(sender As Object, e As EventArgs) Handles Button7.Click
        If RadioButton1.Checked = True Then
            WebBrowser2.GoForward()
        ElseIf RadioButton2.Checked = True Then
            WebBrowser1.GoForward()
        ElseIf RadioButton5.Checked = True Then
            WebBrowser3.GoForward()
        End If
    End Sub

    Private Sub Button9_Click_1(sender As Object, e As EventArgs) Handles Button9.Click
        If RadioButton1.Checked = True Then
            WebBrowser2.Refresh()
        ElseIf RadioButton2.Checked = True Then
            WebBrowser1.Refresh()
        ElseIf RadioButton5.Checked = True Then
            WebBrowser3.Refresh()
        End If
    End Sub

    Private Sub Button8_Click_1(sender As Object, e As EventArgs) Handles Button8.Click
        If RadioButton1.Checked = True Then
            WebBrowser2.Stop()
        ElseIf RadioButton2.Checked = True Then
            WebBrowser1.Stop()
        ElseIf RadioButton5.Checked = True Then
            WebBrowser3.Stop()
        End If
    End Sub

    Private Sub Button13_Click_1(sender As Object, e As EventArgs) Handles Button13.Click
        Dialog1.Show()
    End Sub
    Private Sub Button21_Click(sender As Object, e As EventArgs)
        Dialog9.Show()
    End Sub
    Private Sub Button12_Click_1(sender As Object, e As EventArgs)
        Dialog6.Show()
    End Sub
    Private Sub Button19_Click_1(sender As Object, e As EventArgs) Handles Button19.Click
        racer = MsgBox("Is the printer on and online?", vbQuestion + vbYesNo, "Are you ready")
        If (racer = 6) Then

        Else
            flick = MsgBox("The printer could not print because you did not allow it to print", vbCritical + vbRetryCancel, "Error")
            If (flick = 5) Then
                cricket = MsgBox("Is the printer on and online?", vbQuestion + vbYesNo, "Are you ready?")
                If (cricket = 6) Then
                    WebBrowser2.Print()
                Else
                    cry = MsgBox("The printer could not print because you did not allow it to print", vbCritical, "Error")
                End If
            Else
                maths = MsgBox("The printer could not print because you did not allow it to print", vbCritical, "Error")
            End If
        End If
    End Sub

    Private Sub Button16_Click_2(sender As Object, e As EventArgs)
        Dialog7.Show()
    End Sub

    Private Sub Button15_Click_1(sender As Object, e As EventArgs) Handles Button15.Click
        End
    End Sub
    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        Dialog11.Show()
    End Sub

    Private Sub MenuColourOptionsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dialog12.Show()
    End Sub
    Private Sub ToolStripTextBox1_LostFocus(sender As Object, e As EventArgs) Handles ToolStripTextBox1.LostFocus
        ribhistory.Items.Add(ToolStripTextBox1.Text)
        Dialog6.ComboBox1.Items.Add(ToolStripTextBox1.Text)
        menuhistory.Items.Add(ToolStripTextBox1.Text)
    End Sub
    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        appstore.Show()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'Placeholder for full update
        'Network status is shifted to the loading sector.
        If Label4.Text = "unknown" Or Label4.Text = "" Then
        Else
            'If Microsoft.Win32.PowerModes.StatusChange Then
            'Dim sleepnotice As Integer
            'sleepnotice = 0
            'Do Until sleepnotice = 2
            'sleepnotice = sleepnotice + 1
            ' Label50.Text = "About to be suspended"
            'MsgBox("Careful - The PC is about to sleep!", MsgBoxStyle.Exclamation, "Suspension warning!")
            'Loop
            'Else
            Label50.Text = "Normal"
            'End If
            Label6.Text = WebBrowser2.Document.Domain
            Label43.Text = WebBrowser1.Document.Domain
            Label41.Text = WebBrowser3.Document.Domain
            'Buggy...for now
            Label8.Text = WebBrowser2.Url.AbsolutePath
            Label44.Text = WebBrowser1.Url.AbsolutePath
            Label51.Text = WebBrowser3.Url.AbsolutePath
            Label10.Text = WebBrowser2.DocumentType
            Label45.Text = WebBrowser1.DocumentType
            Label52.Text = WebBrowser3.Document.ContentType
            Dim ver As String
            ver = WebBrowser2.ProductVersion
            Label16.Text = ver
            menutridentversion.Text = ver
            Label18.Text = WebBrowser2.EncryptionLevel
            Label46.Text = WebBrowser1.EncryptionLevel
            Label53.Text = WebBrowser3.SecurityState
            menudocumentype1.Text = WebBrowser2.DocumentType
            menudocumentype2.Text = WebBrowser1.DocumentType
            menudocumentype3.Text = WebBrowser3.Document.ContentType
            menuencryptlevel1.Text = WebBrowser2.EncryptionLevel
            menuencryptlevel2.Text = WebBrowser1.EncryptionLevel
            menuencryptlevel3.Text = WebBrowser3.SecurityState
            menuwebdomain1.Text = WebBrowser2.Document.Domain
            menuwebdomain2.Text = WebBrowser1.Document.Domain
            menuwebdomain3.Text = WebBrowser3.Document.Domain
            menupagetitle1.Text = WebBrowser2.DocumentTitle
            menupagetitle2.Text = WebBrowser1.DocumentTitle
            menupagetitle3.Text = WebBrowser3.DocumentTitle
            menuwebpath1.Text = WebBrowser2.Url.AbsolutePath
            menuwebpath2.Text = WebBrowser1.Url.AbsolutePath
            menuwebpath3.Text = WebBrowser3.Url.AbsolutePath
            'Free RAM
            Label30.Text = My.Computer.Info.AvailablePhysicalMemory / 1024 / 1024
            menufreeRAM.Text = My.Computer.Info.AvailablePhysicalMemory
            'Free RAM %
            Label33.Text = (Label30.Text / Label28.Text) * 100
            ProgressBar1.Value = Label33.Text
            Timer3.Enabled = True
        End If
    End Sub
    Private Sub Button21_Click_1(sender As Object, e As EventArgs) Handles Button21.Click
        htmlcode.Show()
    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        frmAbout.Show()
    End Sub

    Private Sub PrereleaseAppStoreToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrereleaseAppStoreToolStripMenuItem.Click
        appstore.Show()
    End Sub
    Private Sub RibbonToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles RibbonToolStripMenuItem.Click
        Dialog8.Show()
    End Sub
    Private Sub TipsToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles TipsToolStripMenuItem.Click
        Dialog7.Show()
    End Sub
    Private Sub MenuColourOptionsToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles MenuColourOptionsToolStripMenuItem.Click
        Dialog12.Show()
    End Sub
    Private Sub ViewHTMLCodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewHTMLCodeToolStripMenuItem.Click
        htmlcode.Show()
    End Sub
    Private Sub DisableScriptingErrorsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisableScriptingErrorsToolStripMenuItem.Click
        WebBrowser2.ScriptErrorsSuppressed = True
        WebBrowser1.ScriptErrorsSuppressed = True
    End Sub
    Private Sub Button16_Click_3(sender As Object, e As EventArgs) Handles Button16.Click
        If CheckBox1.Checked = True Then
            ToolTip1.Active = True
        Else
            ToolTip1.Active = False
        End If
    End Sub
    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        Form3.Show()
    End Sub
    
    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
        GroupBox13.Visible = True
    End Sub
    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        If My.Settings.ribbonmenuverify = "Ribbon" Then
            If RadioButton1.Checked = "True" Then
                If My.Settings.searchbase = "Bing" Then
                    WebBrowser2.Navigate(My.Settings.bingsearch + ribbon_search.Text)
                ElseIf My.Settings.searchbase = "Google" Then
                    WebBrowser2.Navigate(My.Settings.googlesearch + ribbon_search.Text)
                ElseIf My.Settings.searchbase = "Yahoo" Then
                    WebBrowser2.Navigate(My.Settings.yahoosearch + ribbon_search.Text)
                End If
            ElseIf RadioButton2.Checked = "True" Then
                If My.Settings.searchbase = "Bing" Then
                    WebBrowser1.Navigate(My.Settings.bingsearch + ribbon_search.Text)
                ElseIf My.Settings.searchbase = "Google" Then
                    WebBrowser1.Navigate(My.Settings.googlesearch + ribbon_search.Text)
                ElseIf My.Settings.searchbase = "Yahoo" Then
                    WebBrowser1.Navigate(My.Settings.yahoosearch + ribbon_search.Text)
                End If
            ElseIf RadioButton5.Checked = True Then
                If My.Settings.searchbase = "Bing" Then
                    WebBrowser3.Navigate(My.Settings.bingsearch + ribbon_search.Text)
                ElseIf My.Settings.searchbase = "Google" Then
                    WebBrowser3.Navigate(My.Settings.googlesearch + ribbon_search.Text)
                ElseIf My.Settings.searchbase = "Yahoo" Then
                    WebBrowser3.Navigate(My.Settings.yahoosearch + ribbon_search.Text)
                End If
            End If
        ElseIf My.Settings.ribbonmenuverify = "Menu" Then
            If RadioButton3.Checked = True Then
                If My.Settings.searchbase = "Bing" Then
                    WebBrowser2.Navigate(My.Settings.bingsearch + ribbon_search.Text)
                ElseIf My.Settings.searchbase = "Google" Then
                    WebBrowser2.Navigate(My.Settings.googlesearch + ribbon_search.Text)
                ElseIf My.Settings.searchbase = "Yahoo" Then
                    WebBrowser2.Navigate(My.Settings.yahoosearch + ribbon_search.Text)
                End If
            ElseIf RadioButton4.Checked = True Then
                If My.Settings.searchbase = "Bing" Then
                    WebBrowser2.Navigate(My.Settings.bingsearch + ribbon_search.Text)
                ElseIf My.Settings.searchbase = "Google" Then
                    WebBrowser2.Navigate(My.Settings.googlesearch + ribbon_search.Text)
                ElseIf My.Settings.searchbase = "Yahoo" Then
                    WebBrowser2.Navigate(My.Settings.yahoosearch + ribbon_search.Text)
                End If
            ElseIf RadioButton6.Checked = True Then
                If My.Settings.searchbase = "Bing" Then
                    WebBrowser3.Navigate(My.Settings.bingsearch + ribbon_search.Text)
                ElseIf My.Settings.searchbase = "Google" Then
                    WebBrowser3.Navigate(My.Settings.googlesearch + ribbon_search.Text)
                ElseIf My.Settings.searchbase = "Yahoo" Then
                    WebBrowser3.Navigate(My.Settings.yahoosearch + ribbon_search.Text)
                End If
            End If
        End If
        ribhistorysearch.Items.Add(ribbon_search.Text)
        menuhistory.Items.Add(ribbon_search.Text)
    End Sub

    Private Sub Button10_Click_2(sender As Object, e As EventArgs) Handles Button10.Click
        GroupBox13.Visible = False
    End Sub

    Private Sub AboutTheBrowserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutTheBrowserToolStripMenuItem.Click
        frmAbout.Show()
    End Sub
    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        If My.Settings.ribbonmenuverify = "Ribbon" Then
            If RadioButton1.Checked = True Then
                TextBox1.Text = WebBrowser2.Document.Domain + WebBrowser2.Url.AbsolutePath
            ElseIf RadioButton2.Checked = True Then
                TextBox1.Text = WebBrowser1.Document.Domain + WebBrowser1.Url.AbsolutePath
            ElseIf RadioButton3.Checked = True Then
                TextBox1.Text = WebBrowser3.Document.Domain + WebBrowser3.Url.AbsolutePath
            End If
        ElseIf My.Settings.ribbonmenuverify = "Menu" Then
            If RadioButton3.Checked = True Then
                TextBox2.Text = WebBrowser2.Document.Domain + WebBrowser2.Url.AbsolutePath
                ToolStripTextBox1.Text = WebBrowser2.Document.Domain + WebBrowser2.Url.AbsolutePath
            ElseIf RadioButton4.Checked = True Then
                TextBox2.Text = WebBrowser1.Document.Domain + WebBrowser1.Url.AbsolutePath
                ToolStripTextBox1.Text = WebBrowser1.Document.Domain + WebBrowser1.Url.AbsolutePath
            ElseIf RadioButton6.Checked = True Then
                TextBox2.Text = WebBrowser3.Document.Domain + WebBrowser3.Url.AbsolutePath
                ToolStripTextBox1.Text = WebBrowser3.Document.Domain + WebBrowser3.Url.AbsolutePath
            End If
        End If
        Label39.Text = My.Computer.Clock.LocalTime
        If My.Settings.ribbonmenuverify = "Ribbon" Then
            If RadioButton1.Checked = True Then
                ToolStripTextBox1.Text = WebBrowser2.StatusText
            ElseIf RadioButton2.Checked = True Then
                ToolStripTextBox1.Text = WebBrowser1.StatusText
            ElseIf RadioButton3.Checked = True Then
                ToolStripTextBox1.Text = WebBrowser3.StatusText
            End If
        ElseIf My.Settings.ribbonmenuverify = "Menu" Then
            If RadioButton3.Checked = True Then
                ToolStripTextBox1.Text = WebBrowser2.StatusText
            ElseIf RadioButton4.Checked = True Then
                ToolStripTextBox1.Text = WebBrowser1.StatusText
            ElseIf RadioButton6.Checked = True Then
                ToolStripTextBox1.Text = WebBrowser3.StatusText
            End If
        End If
        TabPage12.Text = WebBrowser1.DocumentTitle
        TabPage13.Text = WebBrowser2.DocumentTitle
        TabPage14.Text = WebBrowser3.DocumentTitle
    End Sub
    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles btnribfavourites.Click
        ribfavourite.Items.Add(TextBox2.Text)
    End Sub

    Private Sub Button12_Click_2(sender As Object, e As EventArgs)
        blend.Show()
    End Sub

    Private Sub BlenderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BlenderToolStripMenuItem.Click
        blend.Show()
    End Sub
    Private Sub ActivateSearchBarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActivateSearchBarToolStripMenuItem.Click
        GroupBox13.Visible = True
    End Sub
    Private Sub AddFavouritesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddFavouritesToolStripMenuItem.Click
        menufavourties.Items.Add(TextBox1.Text)
        ribfavourite.Items.Add(TextBox1.Text)
    End Sub
    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        ribhistory.Items.Add(TextBox2.Text)
        menuhistory.Items.Add(TextBox2.Text)
    End Sub
    Private Sub Button30_Click_1(sender As Object, e As EventArgs) Handles Button30.Click
        spla.Show()
        spla.loader.Value = 3
        spla.Label3.Text = spla.loader.Value
        My.Settings.firstrun = 0
        Me.Close()
        firstrun.Show()
    End Sub
    Private Sub RerunFirstRunWizardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RerunFirstRunWizardToolStripMenuItem.Click
        spla.Show()
        spla.loader.Value = 3
        spla.Label3.Text = spla.loader.Value
        My.Settings.firstrun = 0
        Me.Close()
        firstrun.Show()
    End Sub

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        homepage.Show()
    End Sub

    Private Sub SetHomePageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetHomePageToolStripMenuItem.Click
        homepage.Show()
    End Sub

    Private Sub CloseToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        End
    End Sub

    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        If RadioButton1.Checked = True Then
            WebBrowser2.Navigate(ribhistory.Text)
        ElseIf RadioButton2.Checked = True Then
            WebBrowser1.Navigate(ribhistory.Text)
        End If
    End Sub

    Private Sub AdvancedWebSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdvancedWebSettingsToolStripMenuItem.Click
        webadvanced.Show()
    End Sub

    Private Sub ChangeSearchBaseEngineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeSearchBaseEngineToolStripMenuItem.Click
        search_select.Show()
    End Sub

    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        webadvanced.Show()
    End Sub

    Private Sub Button12_Click_3(sender As Object, e As EventArgs) Handles Button12.Click
        blend.Show()
    End Sub

    Private Sub ContributorsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContributorsToolStripMenuItem.Click
        contributors.Show()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        WebBrowser2.Navigate(LinkLabel1.Text)
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        search_select.Show()
    End Sub

    Private Sub DisableToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisableToolStripMenuItem.Click
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Fixed3D
    End Sub
    Private Sub EnableToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnableToolStripMenuItem.Click
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
    End Sub
    Private Sub DisableToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DisableToolStripMenuItem1.Click
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
    End Sub
    Private Sub EnableToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EnableToolStripMenuItem1.Click
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
    End Sub
    Private Sub DisableToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles DisableToolStripMenuItem2.Click
        Me.ControlBox = False
    End Sub
    Private Sub EnableToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles EnableToolStripMenuItem2.Click
        Me.ControlBox = True
    End Sub
    Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
    End Sub

    Private Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Fixed3D
    End Sub

    Private Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
    End Sub

    Private Sub Button38_Click(sender As Object, e As EventArgs) Handles Button38.Click
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
    End Sub

    Private Sub Button39_Click(sender As Object, e As EventArgs) Handles Button39.Click
        Me.ControlBox = True
    End Sub
    Private Sub Button40_Click(sender As Object, e As EventArgs) Handles Button40.Click
        Me.ControlBox = False
    End Sub
    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        contributors.Show()
    End Sub
    Private Sub Button41_Click_1(sender As Object, e As EventArgs) Handles Button41.Click
        If RadioButton1.Checked = True Then
            WebBrowser2.Navigate(ribfavourite.Text)
        ElseIf RadioButton2.Checked = True Then
            WebBrowser1.Navigate(ribfavourite.Text)
        ElseIf RadioButton5.Checked = True Then
            WebBrowser3.Navigate(ribfavourite.Text)
        End If
    End Sub
End Class
REM This is the end of the program
REM Do not continue after this line