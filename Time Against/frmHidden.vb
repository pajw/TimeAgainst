Imports Time_Against.Main
Public Class frmHidden
    ' Hidden form to display when the program runs, otherwise you get an error
    Public showWindow As Boolean = False

    Protected Overrides Sub SetVisibleCore(value As Boolean)
        MyBase.SetVisibleCore(showWindow)
    End Sub

End Class