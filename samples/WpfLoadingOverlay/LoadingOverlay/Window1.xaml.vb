Imports System.Windows.Threading

Class Window1

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)

        ' Load an empty tab
        Dim grid As New Grid
        Dim newItem = New TabItem With {.Header = "Slow loading form", .Content = grid}
        tab.Items.Add(newItem)
        tab.SelectedItem = newItem

        ' Make it render the empty tab
        ThreadHelper.FlushWindowsMessageQueue()

        ' Add the loading overlay
        Dim loadingWindow = LoadingOverlayWindow.CreateAsync(grid)

        ' Add the actual content
        grid.Children.Add(New SlowLoadingControl)

        ' Wait for it to finish rendering
        ThreadHelper.FlushWindowsMessageQueue()

        ' Remove the loading overlay
        loadingWindow.Dispatcher.InvokeShutdown()

    End Sub



End Class
