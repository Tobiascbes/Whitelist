using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using static CounterStrikeSharp.API.Core.Listeners;

namespace Whitelist;

public class Whitelist :  BasePlugin
{
    public override string ModuleName => "Whitelist";
    public override string ModuleVersion => "0.1";
    public override string ModuleAuthor => "Tobias";
    public override string ModuleDescription => "Whitelist plugin for CounterStrikeSharp";

    private IConfiguration _configuration;
    public List<Model> ReadDatabase()
    {
        List<Model> models = new();
        using SqlConnection connection = new(_configuration["connectionString"]);
        {
            connection.Open();
            using SqlCommand command = new("SELECT * FROM Whitelist", connection);
            {
                using SqlDataReader reader = command.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        Model model = new();
                        model.WhitelistedIP = reader.GetString(1);
                        models.Add(model);
                    }
                }
            }
            connection.Close();
        }
        return models;
    }


    public void Listeners_OnClientConnectHandler(string ipAdress)
    {
        if (ReadDatabase().Any(x => x.WhitelistedIP == ipAdress))
        {
            Console.WriteLine("Whitelisted IP connected");
        }
        else
        {
            Console.WriteLine("Non-whitelisted IP connected");
        }
    }
}
