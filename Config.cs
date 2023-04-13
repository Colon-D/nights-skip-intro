using System.ComponentModel;
using nights.test.skipintro.Template.Configuration;

namespace nights.test.skipintro.Configuration;

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

    [DisplayName("Skip to Gamemode")]
    [Description("Gamemode to skip to when loading Intro (Advertise).")]
    [DefaultValue(Gamemode.MainMenu)]
    public Gamemode SkipToGamemode { get; set; } = Gamemode.MainMenu;

    [DisplayName("Skip to Gamemode integer override")]
    [Description("Override for enum (-1 to not use).")]
    [DefaultValue(-1)]
    public int SkipToGamemodeInt { get; set; } = -1;
}

/// <summary>
/// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
/// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
/// </summary>
public class ConfiguratorMixin : ConfiguratorMixinBase
{
    // 
}
