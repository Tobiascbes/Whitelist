using CounterStrikeSharp.API.Modules.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whitelist;

public class Model
{
    public string? WhitelistedIP { get; set; } 
    public string? UserIP { get; set; }
    public string? SteamID { get; set; }
}
