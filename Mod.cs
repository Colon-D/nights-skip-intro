using nights.test.skipintro.Configuration;
using nights.test.skipintro.Template;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.X86;
using Reloaded.Mod.Interfaces;
using System.Runtime.InteropServices;
using CallingConventions = Reloaded.Hooks.Definitions.X86.CallingConventions;
using IReloadedHooks = Reloaded.Hooks.ReloadedII.Interfaces.IReloadedHooks;

namespace nights.test.skipintro;

/// <summary>
/// Your mod logic goes here.
/// </summary>
public class Mod : ModBase // <= Do not Remove.
{
	/// <summary>
	/// Provides access to the mod loader API.
	/// </summary>
	private readonly IModLoader _modLoader;

	/// <summary>
	/// Provides access to the Reloaded.Hooks API.
	/// </summary>
	/// <remarks>This is null if you remove dependency on Reloaded.SharedLib.Hooks in your mod.</remarks>
	private readonly IReloadedHooks? _hooks;

	/// <summary>
	/// Provides access to the Reloaded logger.
	/// </summary>
	private readonly ILogger _logger;

	/// <summary>
	/// Entry point into the mod, instance that created this class.
	/// </summary>
	private readonly IMod _owner;

	/// <summary>
	/// Provides access to this mod's configuration.
	/// </summary>
	private Config _configuration;

	/// <summary>
	/// The configuration of the currently executing mod.
	/// </summary>
	private readonly IModConfig _modConfig;

	public Mod(ModContext context)
	{
		_modLoader = context.ModLoader;
		_hooks = context.Hooks;
		_logger = context.Logger;
		_owner = context.Owner;
		_configuration = context.Configuration;
		_modConfig = context.ModConfig;


		// For more information about this template, please see
		// https://reloaded-project.github.io/Reloaded-II/ModTemplate/

		// If you want to implement e.g. unload support in your mod,
		// and some other neat features, override the methods in ModBase.

		// read from config
		if (_configuration.SkipToGameModeDebugging == GameMode.UseGeneralGameMode) {
			_skipToGameMode = (GameMode)_configuration.SkipToGameMode;
		} else {
			_skipToGameMode = (GameMode)_configuration.SkipToGameModeDebugging;
		}

		// hook gameplay transition function
		unsafe {
			_transitionHook = _hooks.CreateHook<Transition>(
				GameModeTransitionImpl, 0x489210
			).Activate();
		}
	}

	[Function(CallingConventions.MicrosoftThiscall)]
	unsafe delegate int Transition(GameModeManager* self);
	IHook<Transition> _transitionHook;
	GameMode _skipToGameMode;
	bool _skipped = false; // can only skip once, probably unneeded
	unsafe int GameModeTransitionImpl(GameModeManager* gamemode_manager) {
		if (!_skipped) {
			if (gamemode_manager->to == GameMode.Advertise) {
				gamemode_manager->to = _skipToGameMode;

				if (_skipToGameMode == GameMode.Game) {
					GameStateManager* gameStateManager =
						*(GameStateManager**)0x24C4E94;
					gameStateManager->Dream =
						(Dream)_configuration.SkipToDream;

					if (
						gameStateManager->Dream
						>= Dream.SpringValleyXmasClaris
					) {
						DreamType* dreamType = (DreamType*)0x8B13C8;
						*dreamType = DreamType.Christmas;
					}
				}

				_skipped = true;
			}
		}

		return _transitionHook.OriginalFunction.Invoke(gamemode_manager);
	}

	#region Standard Overrides
	public override void ConfigurationUpdated(Config configuration)
	{
		// Apply settings from configuration.
		// ... your code here.
		_configuration = configuration;
		_logger.WriteLine($"[{_modConfig.ModId}] Config Updated: Applying");
	}
	#endregion

	#region For Exports, Serialization etc.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public Mod() { }
#pragma warning restore CS8618
	#endregion
}

[StructLayout(LayoutKind.Explicit)]
public unsafe struct GameModeManager {
	[FieldOffset(0x10)]
	public GameMode to;
}

[StructLayout(LayoutKind.Explicit)]
public unsafe struct GameStateManager {
	[FieldOffset(0x44)]
	public Dream Dream;
}

public enum DreamType {
	BrandNew,
	SegaSaturn,
	Christmas,
}

public enum Dream {
	SpringValley = 1,
	Gillwing,
	SplashGarden,
	Puffy,
	MysticForest,
	Gulpo,
	FrozenBell,
	Clawz,
	SoftMuseum,
	Jackle,
	StickCanyon,
	Reala,
	TwinSeedsClaris,
	WizemanClaris,
	TwinSeedsElliot,
	WizemanElliot,
	SpringValleyXmasClaris,
	GillwingXmasClaris,
	SpringValleyXmasElliot,
	GillwingXmasElliot,
};
