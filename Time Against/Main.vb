Friend Module Main

#Region " Main sub "

    'This runs first.
    Public Sub Main(ByVal cmdArgs() As String)
        Application.EnableVisualStyles()

        'Determine the interface
        Dim serviceCall As Boolean = False
        Dim salesOrder As Boolean = False
        Dim wbs As Boolean = False

        'Using For so there is a possibility to use more than one cmdarg in future
        For Each cmd As String In cmdArgs
            If cmd.ToLower = "-sc" Then
                serviceCall = True
            ElseIf cmd.ToLower = "-so" Then
                salesOrder = True
            ElseIf cmd.ToLower = "-wbs" Then
                wbs = True
            End If
        Next

        If serviceCall Then
            'Code goes here
        ElseIf salesOrder Then
            'Etc
        ElseIf wbs Then
            'Etc
        Else
            Application.Run(New AppContext)

        End If

    End Sub

    Public Sub ExitApplication()
        Application.Exit()
    End Sub

#End Region

#Region " Other Methods "

    Public Sub ShowMenu()
        mainMenu.Show()
    End Sub

#End Region

End Module
