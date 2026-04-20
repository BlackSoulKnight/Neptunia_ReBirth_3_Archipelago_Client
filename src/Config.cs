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

    [DisplayName("Float")]
    [Description("This is a floating point number.")]
    [DefaultValue(6.987654F)]
    public float Float { get; set; } = 6.987654F;

    [DisplayName("Enum")]
    [Description("This is an enumerable.")]
    [DefaultValue(SampleEnum.ILoveIt)]
    public SampleEnum Reloaded { get; set; } = SampleEnum.ILoveIt;

    public enum SampleEnum
    {
        [Display(Name = "No Opinion 🤷")]
        NoOpinion,
        [Display(Name = "It's Sucks! 👎")]
        Sucks,
        [Display(Name = "It's mediocre 😐")]
        IsMediocre,
        [Display(Name = "It's okay! 👍")]
        IsOk,
        [Display(Name = "It's cool! 😎")]
        IsCool,
        [Display(Name = "I Love It!!! ❤️🔥")]
        ILoveIt,
    }
    
    [DisplayName("Int Slider")]
    [Description("This is a int that uses a slider control similar to a volume control slider.")]
    [DefaultValue(100)]
    [SliderControlParams(
        minimum: 0.0,
        maximum: 100.0,
        smallChange: 1.0,
        largeChange: 10.0,
        tickFrequency: 10,
        isSnapToTickEnabled: false,
        tickPlacement:SliderControlTickPlacement.BottomRight,
        showTextField: true,
        isTextFieldEditable: true,
        textValidationRegex: "\\d{1-3}")]
    public int IntSlider { get; set; } = 100;

    [DisplayName("Double Slider")]
    [Description("This is a double that uses a slider control without any frills.")]
    [DefaultValue(0.5)]
    [SliderControlParams(minimum: 0.0, maximum: 1.0)]
    public double DoubleSlider { get; set; } = 0.5;

    [DisplayName("File Picker")]
    [Description("This is a sample file picker.")]
    [DefaultValue("")]
    [FilePickerParams(title:"Choose a File to load from")]
    public string File { get; set; } = "";

    [DisplayName("Folder Picker")]
    [Description("Opens a file picker but locked to only allow folder selections.")]
    [DefaultValue("")]
    [FolderPickerParams(
        initialFolderPath: Environment.SpecialFolder.Desktop,
        userCanEditPathText: false,
        title: "Custom Folder Select",
        okButtonLabel: "Choose Folder",
        fileNameLabel: "ModFolder",
        multiSelect: true,
        forceFileSystem: true)]
    public string Folder { get; set; } = "";

    [Display(Order = 0)]
    public int OrderFirst { get; set; }

    [Display(Order = 1)]
    public int OrderSecond { get; set; }

    [Display(Order = 2)]
    public int OrderThird { get; set; }
}

/// <summary>
/// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
/// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
/// </summary>
public class ConfiguratorMixin : ConfiguratorMixinBase
{
    // 
}
