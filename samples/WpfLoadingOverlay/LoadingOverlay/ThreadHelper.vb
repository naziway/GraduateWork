Imports System.Windows.Threading

Public Class ThreadHelper

    ''' <summary>
    ''' Force rendering
    ''' </summary>
    Public Shared Sub FlushWindowsMessageQueue()
        Application.Current.Dispatcher.Invoke( _
            New Action(AddressOf DummySub), _
            DispatcherPriority.Background, _
            New Object() {})
    End Sub

    Private Shared Sub DummySub()
    End Sub

End Class
