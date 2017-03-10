Partial Public Class SlowLoadingControl

    ''' <summary>
    ''' Add a whole bunch of text boxes to the wrap panel so that it will take a long time to load
    ''' </summary>
    Private Sub WrapPanel_Initialized(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For i = 1 To 4000
            panel.Children.Add(New TextBox With {.Text = "Hello"})
        Next
    End Sub
End Class
