using System;
using System.IO;
using IniParser;
using IniParser.Model;

namespace GithubAutomation.Selenium.Configuration;

public static class Configuration
{
    public static readonly IniData ParsedData;

    static Configuration()
    {
        try
        {
            var parser = new FileIniDataParser();
            ParsedData = parser.ReadFile(AppDomain.CurrentDomain.BaseDirectory + "Config.cfg");
        }
        catch (FileNotFoundException)
        {
            throw new Exception("Could not find configuration file at: " + AppContext.BaseDirectory);
        }
    }
}