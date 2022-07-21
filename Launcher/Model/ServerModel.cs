using System.Collections.Generic;

namespace Launcher.Model;

public class ServerModel : ViewModelBase
{
    public string Name { get; set; }
    public string Ip { get; set; }
    public bool UseCdn { get; set; }

}