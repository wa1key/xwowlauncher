using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace Launcher.Pages;

public partial class License : Page
{
    private readonly bool _gotoUpdate = false;

    public License(bool g)
    {
        InitializeComponent();
        _gotoUpdate = g;
    }

    private void Back_OnClick(object sender, RoutedEventArgs e)
    {
        var azAgree = Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                       "\\az_agree.txt");

        if (!File.Exists(azAgree))
        {
            File.WriteAllText(azAgree, "您已于" + DateTime.Now + "接受了azerothcore启动器免责条款");
        }


            Helper.MainFrame.GoBack();
       
    }

    private void Jujue_OnClick(object sender, RoutedEventArgs e)
    {
        var azAgree = Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                       "\\az_agree.txt");

        if (File.Exists(azAgree))
        {
            File.Delete(azAgree);
        }

        Application.Current.MainWindow.Close();
    }
}