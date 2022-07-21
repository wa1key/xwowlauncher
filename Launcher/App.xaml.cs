using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace Launcher;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App
{
    public App()
    {
        var ps = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
        foreach (var process in ps)
        {
            if (process.Id != Environment.ProcessId)
            {
                process.Kill();
            }
        }

        // 在异常由应用程序引发但未进行处理时发生。主要指的是UI线程。
        DispatcherUnhandledException +=
            App_DispatcherUnhandledException;
        //  当某个异常未被捕获时出现。主要指的是非UI线程
        AppDomain.CurrentDomain.UnhandledException +=
            CurrentDomain_UnhandledException;
    }
    

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        //可以记录日志并转向错误bug窗口友好提示用户
        if (e.ExceptionObject is Exception ex) MessageBox.Show("消息:" + ex.Message + "\r\n" + ex.StackTrace);
    }

    private static void App_DispatcherUnhandledException(object sender,
        DispatcherUnhandledExceptionEventArgs e)
    {
        //可以记录日志并转向错误bug窗口友好提示用户
        e.Handled = true;
        MessageBox.Show("消息:" + e.Exception.Message + "\r\n" + e.Exception.StackTrace, "异常", MessageBoxButton.OK,
            MessageBoxImage.Error);
    }
}