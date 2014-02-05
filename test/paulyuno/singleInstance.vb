Imports Microsoft.VisualBasic

Public Class singleInstance : Inherits ApplicationServices.WindowsFormsApplicationBase
    Public Shared Function foo()
        Return My.Application.CommandLineArgs
    End Function
End Class
