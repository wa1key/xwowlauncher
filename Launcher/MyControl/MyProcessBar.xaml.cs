using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Launcher.MyControl;

public partial class MyProcessBar : UserControl
{
    public MyProcessBar()
    {
        InitializeComponent();
    }


    public void SetValue(double value)
    {
        P1.Width = Width;
        if (ValueCanvas.Clip is RectangleGeometry aa)
        {
            var w = value / 100 * Width;

            //     P2.Visibility = Math.Abs(w - Width) < 0.02 ? Visibility.Collapsed : Visibility.Visible;

            P2.Width = w;

            aa.Rect = new Rect(0, 0, w, 100);
        }
    }

    private void MyProcessBar_OnLoaded(object sender, RoutedEventArgs e)
    {
        SetValue(0);
    }
}