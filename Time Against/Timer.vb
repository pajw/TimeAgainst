Public Class Timer
    'todo The entire Timer class
    Dim stopwatch As New Stopwatch

    Public Sub startClock(ByVal doc As String)
        Dim created As Date = Now.Date()
        stopwatch.Start()
    End Sub

    Public Sub stopClock(ByVal doc As String)
        Dim elapsed As TimeSpan = stopwatch.Elapsed
    End Sub
End Class
