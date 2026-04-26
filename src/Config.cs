using System.ComponentModel;
using Nep3ArchipelagoClient.Template.Configuration;
using Reloaded.Mod.Interfaces.Structs;
using System.ComponentModel.DataAnnotations;

namespace Nep3ArchipelagoClient.Configuration;

public class Config : Configurable<Config>
{
    /*
        User Properties:
            - Please put all of your configurable properties here.
    
        By default, configuration saves as "Config.json" in mod user config folder.    
        Need more config files/classes? See Configuration.cs
    
        Available Attributes:
        - Category
        - DisplayName
        - Description
        - DefaultValue

        // Technically Supported but not Useful
        - Browsable
        - Localizable

        The `DefaultValue` attribute is used as part of the `Reset` button in Reloaded-Launcher.
    */

    [DisplayName("AP Server")]
    [Description("Ap Server")]
    [DefaultValue("localhost")]
    public string Server { get; set; } = "localhost";

    [DisplayName("Port")]
    [Description("Port")]
    [DefaultValue(38281)]
    public int Port { get; set; } = 38281;

    [DisplayName("Player")]
    [Description("Player Name")]
    [DefaultValue("Player1")]
    public string Player { get; set; } = "Player1";
}

/// <summary>
/// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
/// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
/// </summary>
public class ConfiguratorMixin : ConfiguratorMixinBase
{
    // 
}
