Imports System
Imports System.IO
Imports Microsoft.VisualBasic.ApplicationServices
Imports Time_Against.Timer

Friend Module Main

    Public Property curDir As String = Directory.GetCurrentDirectory()

    NotInheritable Class Program
        Protected Property MainForm As frmHidden
        Private Sub New()
        End Sub
        <STAThread> _
        Public Shared Sub Main(ByVal cmdArgs() As String)
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Dim controller As New SingleInstanceController()

            Try
                controller.checkInterface(cmdArgs)
            Catch ex As ConstraintException
                Debug.WriteLine("Error starting new instance: " & ex.ToString())
                MessageBox.Show("I couldn't find any document numbers. Please check you are on a valid record.")
            End Try
            If (Not File.Exists(curDir & "\instance.pri")) Then ' this is first instance
                File.Create(curDir & "\instance.pri").Dispose()
                controller.createTray()
            End If
            Try
                controller.Run(cmdArgs)
            Catch ex As NoStartupFormException
                If MsgBox("instance.pri found in Time Against directory. /n Would you like to delete it?", MsgBoxStyle.YesNo) Then
                    File.Delete(curDir & "\instance.pri")
                    MsgBox("File deleted! Trying to run the program again.")

                End If
            End Try



        End Sub

        Private Sub create()

        End Sub
    End Class

    Public Class SingleInstanceController
        Inherits WindowsFormsApplicationBase

#Region " Tray Icon Storage "

        Private WithEvents Tray As NotifyIcon
        Private WithEvents Menu As ContextMenuStrip
        Private WithEvents mnuDisplayForm As ToolStripMenuItem
        Private WithEvents mnuSep As ToolStripSeparator
        Private WithEvents mnuExit As ToolStripMenuItem

#End Region

        Public Sub New()
            IsSingleInstance = True
            AddHandler StartupNextInstance, AddressOf this_StartNextInstance
        End Sub

        Public Sub this_StartNextInstance(sender As Object, e As StartupNextInstanceEventArgs)
            'Runs when first instance is open
        End Sub


        Public Sub checkInterface(ByVal cmdArgs() As String)
            Dim serviceCall As Boolean = False
            Dim salesOrder As Boolean = False
            Dim wbs As Boolean = False
            Dim doc As String = ""

            For Each cmd As String In cmdArgs
                If cmd.ToLower = "-sc" Then
                    serviceCall = True
                ElseIf cmd.ToLower = "-so" Then
                    salesOrder = True
                ElseIf cmd.ToLower = "-wbs" Then
                    wbs = True
                ElseIf cmd.ToLower.StartsWith("-d") Then
                    doc = cmd.Remove(0, 2)
                End If
            Next

            If serviceCall Then
                If doc IsNot Nothing Then
                    Dim SC As New Timer
                    SC.startClock(doc)

                End If
            ElseIf salesOrder Then
                'Etc
            ElseIf wbs Then
                'Etc
            Else

                Throw New ConstraintException("No suitable command arguments supplied. Switches are -sc, -so, -wbs document types; -d for doc")
            End If
        End Sub

#Region " Tray Icons "
        Public Sub createTray()
            MainForm = New frmHidden()
            'Initialise tray menu
            mnuDisplayForm = New ToolStripMenuItem("Display form")
            mnuSep = New ToolStripSeparator()
            mnuExit = New ToolStripMenuItem("Exit")
            Menu = New ContextMenuStrip
            Menu.Items.AddRange(New ToolStripItem() {mnuDisplayForm, mnuSep, mnuExit})

            'Initialise tray
            Tray = New NotifyIcon
            Tray.Icon = My.Resources.Resources.TrayIcon
            Tray.ContextMenuStrip = Menu
            Tray.Text = "Time Against"
            Tray.Visible = True
        End Sub

        Private Sub AppContext_ThreadExit(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles Me.Shutdown
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

        ' Tray misc methods
        'todo Make showMenu() showMenu
        Public Sub ShowMenu()
            Dim form As New frmMenu
        End Sub
        Public Sub ExitApplication()
            ' Clean everything up
            File.Delete(curDir & "\instance.pri")
            Tray.Dispose()
            Application.Exit()
        End Sub

#End Region

    End Class

End Module
