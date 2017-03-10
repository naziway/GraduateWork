Imports System.Threading

Partial Public Class LoadingOverlayWindow

    ''' <summary>
    ''' Launches a loading window in its own UI thread and positions it
    ''' over <c>overlayedElement</c>.
    ''' </summary>
    ''' <returns>A reference to the created window</returns>
    Public Shared Function CreateAsync(ByVal overlayedElement As FrameworkElement) As LoadingOverlayWindow

        ' Get the coordinates where the loading overlay should be shown
        Dim locationFromScreen = overlayedElement.PointToScreen(New Point(0, 0))

        ' Launch window in new thread
        Dim windowLauncher As New AsyncWindowLauncher(overlayedElement, _
                                                      locationFromScreen.X, _
                                                      locationFromScreen.Y, _
                                                      overlayedElement.ActualWidth, _
                                                      overlayedElement.ActualHeight)
        Dim windowThread As New Thread(AddressOf windowLauncher.CreateWindow)
        windowThread.SetApartmentState(ApartmentState.STA)
        windowThread.Start()

        ' Wait until the new thread has created the window
        While windowLauncher.Window Is Nothing
        End While

        ' The window has been created, so return a reference to it
        Return windowLauncher.Window

    End Function

    Private Sub OnWindowClosed(ByVal sender As Object, ByVal args As EventArgs)
        Dispatcher.InvokeShutdown()
    End Sub

    ''' <summary>
    ''' Used create a window in its own thread with a specific size and position.
    ''' </summary>
    Private Class AsyncWindowLauncher

        Private _window As LoadingOverlayWindow
        Private _overlayedElement As FrameworkElement
        Private _x As Double
        Private _y As Double
        Private _width As Double
        Private _height As Double

        Public Sub New(ByVal overlayedElement As FrameworkElement, ByVal x As Double, ByVal y As Double, ByVal width As Double, ByVal height As Double)
            _overlayedElement = overlayedElement
            _x = x
            _y = y
            _width = width
            _height = height
        End Sub

        ''' <summary>
        ''' Create the window and set size/position
        ''' </summary>
        Public Sub CreateWindow()
            _window = New LoadingOverlayWindow

            _window.Left = _x
            _window.Top = _y
            _window.Width = _width
            _window.Height = _height
            _window.Show()

            AddHandler _window.Closed, AddressOf _window.OnWindowClosed

            Windows.Threading.Dispatcher.Run()
        End Sub

        Public Property Window() As LoadingOverlayWindow
            Get
                Return _window
            End Get
            Set(ByVal value As LoadingOverlayWindow)
                _window = value
            End Set
        End Property

    End Class

End Class
