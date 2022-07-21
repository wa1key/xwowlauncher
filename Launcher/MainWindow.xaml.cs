using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using Launcher.Pages;
using License = Launcher.Pages.License;

namespace Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Helper.MainFrame = MainFrame;
            Topmost = true;
            Activate();
            Topmost = false;
        }


        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Btn_close(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MainPage());
            // var bg = Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
            //                           "\\az_bg3.mp4");
            // if (!File.Exists(bg))
            // {
            //     var uri = new Uri("/Resources/az_bg2.mp4", UriKind.Relative);
            //     var info = Application.GetResourceStream(uri);
            //     using var memoryStream = new MemoryStream();
            //     info.Stream.CopyTo(memoryStream);
            //     File.WriteAllBytes(bg, memoryStream.ToArray());
            // }
            //
            // MyMediaElement.Source = new Uri(bg);
            //
            //
            //
            // MyMediaElement.MediaEnded += Mp_MediaEnded;
            //
            // MyMediaElement.Play();
        }

        // private void Mp_MediaEnded(object sender, RoutedEventArgs e)
        // {
        //     (sender as MediaElement)?.Stop();
        //     (sender as MediaElement)?.Play();
        // }

        private void MainFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            var thicknessAnimation = new ThicknessAnimation
            {
                From = new Thickness(8, 0, 0, 0), To = new Thickness(0, 0, 0, 0),
                Duration = new Duration(TimeSpan.FromMilliseconds(300))
            };
            var opt = new DoubleAnimation
            {
                From = 0, To = 1,
                Duration = new Duration(TimeSpan.FromMilliseconds(300))
            };
            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(MarginProperty));
            Storyboard.SetTargetProperty(opt, new PropertyPath(OpacityProperty));

            var myStoryboard = new Storyboard();
            myStoryboard.Children.Add(thicknessAnimation);
            myStoryboard.Children.Add(opt);
            myStoryboard.Begin((e.Content as Page)!);
        }

        private void Az_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/C start https://www.azerothcore.org/",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                Process.Start(startInfo);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Clipboard.SetText("https://www.azerothcore.org/");
                MessageBox.Show("网址已复制到剪贴板", "剪贴板", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void License_Click(object sender, RoutedEventArgs e)
        {
            if (Helper.MainFrame.Content.GetType().ToString() != "Launcher.Pages.License")
                Helper.MainFrame.Navigate(new License(false));
        }

        private void Btn_min(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MainWindow_OnClosing(object? sender, CancelEventArgs e)
        {
            if (Helper.MainFrame.Content.GetType().ToString() != "Launcher.Pages.Closing")
                Helper.MainFrame.Navigate(new Closing());
            if (!Helper.IsClose)
                e.Cancel = true;
        }

        private void launcher_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/C start https://github.com/wa1key/xwowlauncher",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                Process.Start(startInfo);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Clipboard.SetText("https://github.com/wa1key/xwowlauncher");
                MessageBox.Show("网址已复制到剪贴板", "剪贴板", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}