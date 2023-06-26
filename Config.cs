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

	[Category("General")]
	[DisplayName("Skip to GameMode")]
	[Description("GameMode to skip to when loading the game.")]
	[DefaultValue(GeneralGameModes.Title)]
	public GeneralGameModes SkipToGameMode { get; set; } = GeneralGameModes.Title;
	
	[Category("General")]
	[DisplayName("GameMode Game: Dream")]
	[Description("Dream to load when skipping to the Game GameMode.\n" +
		"Does not support skipping to the Mare part of the dream,\n" +
		"or skipping into a Sega Saturn dream.\n" +
		"I would like to add those in the future though.")]
	[DefaultValue(DreamPiaOnly.SpringValley)]
	public DreamPiaOnly SkipToDream { get; set; } = DreamPiaOnly.SpringValley;

	[Category("Debugging")]
	[DisplayName("Skip to GameMode (debugging)")]
	[Description("Many of these do nothing, or crash the game.")]
	[DefaultValue(GameMode.UseGeneralGameMode)]
	public GameMode SkipToGameModeDebugging { get; set; } = GameMode.UseGeneralGameMode;
}

public enum GeneralGameModes {
	Title = 8,
	MainMenu = 10,
	DreamSelect = 11,
	Game = 30,
}

public enum DreamPiaOnly {
	SpringValley = 1,
	SplashGarden = 3,
	MysticForest = 5,
	FrozenBell = 7,
	SoftMuseum = 9,
	StickCanyon = 11,
	TwinSeedsClaris = 13,
	TwinSeedsElliot = 15,
	SpringValleyXmasClaris = 17,
	SpringValleyXmasElliot = 19,
};

// many of these do nothing
public enum GameMode {
	UseGeneralGameMode = -1,
	Unknown = 0, // referenced in 0x55C790
	Progressive = 1,// (black screen)
	MemoryCard = 2, // (black screen)
	MemoryCardSaving = 3, // GameMode_MemoryCard
	MemoryCardReplay = 4, // GameMode_MemoryCard
	Advertise = 5, // GameMode_SegaLogo, game seems to start with this
	CutsceneIntro = 6, // GameMode_Movie
	CutsceneStoryBegin = 7, // GameMode_Demo
	Title = 8, // GameMode_Title
	LoadSelect = 9, // (black screen)
	MainMenu = 10, // GameMode_ModeSelect
	DreamSelect = 11, // GameMode_SelectDiary
	PS2Option = 12, // GameMode_Option
	Gallery = 13, // GameMode_Gallery
	MovieTheatre = 14, // GameMode_MovieTheater
	NightopianCollection = 15, // GameMode_Collection
	GameMode0x10 = 16, // referenced in 0x55C790
	GameMode0x11 = 17, // referenced in 0x55C790
	Quit = 18, // 
	GameCrash = 19, // GameMode_Game
	GameReplay = 20, // GameMode_Game
	CutsceneStoryEnd = 21, // GameMode_Ending
	CutsceneChristmasBegin = 22, // GameMode_Movie
	CutsceneChristmasEnd = 23, // GameMode_XmasEnding
	Hint = 24, // GameMode_Hint
	TrialUnlockFullGame = 25, // GameMode_TrialXBOX360
	PiaOver = 26, // GameMode_GameOver
	MareOver = 27, // GameMode_GameOver
	SizeChanger = 28,
	SizeChanger_2 =  29,
	Game = 30, // GameMode_Game
	MainMenuRedirect = 31,
	Logo_Loading = 32
}

/// <summary>
/// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
/// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
/// </summary>
public class ConfiguratorMixin : ConfiguratorMixinBase
{
	// 
}
