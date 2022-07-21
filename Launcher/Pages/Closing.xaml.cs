using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;

namespace Launcher.Pages;

public partial class Closing : Page
{
    public Closing()
    {
        InitializeComponent();
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


    private void Close_OnClick(object sender, RoutedEventArgs e)
    {
        Helper.IsClose = true;
        Application.Current.MainWindow.Close();
    }
}