using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Launcher;

public static class Helper
{
    public static Frame MainFrame = new();
    public static bool IsClose = false;
    public static void StartAction(Action action)
    {
        var task = new Task(() =>
            {
                try
                {
                    action.Invoke();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        task.Start();
    }
}