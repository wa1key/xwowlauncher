using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using Launcher.Model;

namespace Launcher.Pages;

public partial class MainPage : Page
{
    public static MainPage? My;


    public MainPage()
    {
        InitializeComponent();
        My = this;
        var serverModel = new ServerModel
        {
            Ip = "127.0.0.1",
            Name = "测试服务器",
        };


        ServerCombox.ItemsSource = Servers;
        Servers.Add(serverModel);


        ServerCombox.SelectedIndex = 0;

        // StartServer(serverModel.Ip);
    }

    public ObservableCollection<ServerModel> Servers = new();


    private void RunWow(object sender, RoutedEventArgs e)
    {
        var screenWidth = ScreenManager.GetScreenWidth();
        var screenHeight = ScreenManager.GetScreenHeight();
        Helper.StartAction(async () =>
        {
            try
            {
                Directory.Delete("cache", true);
            }
            catch (Exception exception)
            {
                // Console.WriteLine(exception);
            }


            if (File.Exists("wtf\\config.wtf"))
            {
                var wtf = await File.ReadAllTextAsync("wtf\\config.wtf", Encoding.UTF8);
                if (wtf.Contains("SET gxResolution \"3840x2160\""))
                {
                    wtf = wtf.Replace("SET gxResolution \"3840x2160\"",
                        "SET gxResolution \"" + screenWidth + "+" + screenHeight + "\"");

                    await File.WriteAllTextAsync("wtf\\config.wtf", wtf, Encoding.UTF8);
                }

                var lines = File.ReadLines("wtf\\config.wtf", Encoding.UTF8);
                var sb = new StringBuilder();
                foreach (var line in lines)
                {
                    if (!line.Contains("gxMultisample"))
                    {
                        sb.AppendLine(line);
                    }
                }

                await File.WriteAllTextAsync("wtf\\config.wtf", sb.ToString(), Encoding.UTF8);
            }


            Console.WriteLine(screenWidth + "x" + screenHeight);

            Dispatcher.Invoke(() =>
            {
                RunWowButton.IsEnabled = false;
                RunWowText.Text = "正在启动";
                if (ServerCombox.SelectedItem is ServerModel server)
                {
                    try
                    {
                        File.WriteAllText("realmlist.wtf", "SET realmlist \"" + server.Ip + "\"");
                        Process.Start("xwow.exe", "mywow");
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                }
            });
            Thread.Sleep(3000);
            Dispatcher.Invoke(() =>
            {
                RunWowButton.IsEnabled = true;
                RunWowText.Text = "启动游戏";
            });
        });
    }

    public class ScreenManager
    {
        /// <summary>
        /// 获取DPI百分比
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public static double GetDpiRatio(Window window)
        {
            var currentGraphics = Graphics.FromHwnd(new WindowInteropHelper(window).Handle);
            return currentGraphics.DpiX / 96;
        }

        public static double GetDpiRatio()
        {
            return GetDpiRatio(Application.Current.MainWindow);
        }

        public static double GetScreenHeight()
        {
            return SystemParameters.PrimaryScreenHeight * GetDpiRatio();
        }

        public static double GetScreenActualHeight()
        {
            return SystemParameters.PrimaryScreenHeight;
        }

        public static double GetScreenWidth()
        {
            return SystemParameters.PrimaryScreenWidth * GetDpiRatio();
        }

        public static double GetScreenActualWidth()
        {
            return SystemParameters.PrimaryScreenWidth;
        }
    }


    async Task HandleClient(TcpClient client, string targetHost, int targetPort)
    {
        using var targetClient = new TcpClient();
        await targetClient.ConnectAsync(targetHost, targetPort);
        await using NetworkStream sourceStream = client.GetStream(), targetStream = targetClient.GetStream();
        await sourceStream.CopyToAsync(targetStream);
    }
}