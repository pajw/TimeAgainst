Public Class AppContext
    Inherits ApplicationContext

#Region " Storage "

    Private WithEvents Tray As NotifyIcon
    Private WithEvents Menu As ContextMenuStrip
    Private WithEvents mnuDisplayForm As ToolStripMenuItem
    Private WithEvents mnuSep As ToolStripSeparator
    Private WithEvents mnuExit As ToolStripMenuItem

#End Region

#Region " Constructor "

    Public Sub New()
        'init menu
        mnuDisplayForm = New ToolStripMenuItem("Display form")
        mnuSep = New ToolStripSeparator()
        mnuExit = New ToolStripMenuItem("Exit")
        Menu = New ContextMenuStrip
        Menu.Items.AddRange(New ToolStripItem() {mnuDisplayForm, mnuSep, mnuExit})

        'init tray
        Tray = New NotifyIcon
        Tray.Icon = My.Resources.TrayIcon
        Tray.ContextMenuStrip = Menu
        Tray.Text = "Time Keeper"

        Tray.Visible = True
    End Sub

#End Region

#Region " Event Handlers "

    Private Sub AppContext_ThreadExit(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles Me.ThreadExit
        Tray.Visible = True
    End Sub

    Private Sub mnuDisplayForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles mnuDisplayForm.Click
        ShowMenu()
    End Sub

    Private Sub mnuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles mnuExit.Click
        ExitApplication()
    End Sub

    Private Sub Tray_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles Tray.DoubleClick
        ShowMenu()
    End Sub

#End Region
End Class
