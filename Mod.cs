using nights.test.skipintro.Configuration;
using nights.test.skipintro.Template;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.X86;
using Reloaded.Mod.Interfaces;
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
		if (_configuration.SkipToGamemodeInt == -1) {
			_skipToGamemode = _configuration.SkipToGamemode;
		} else {
			_skipToGamemode = (Gamemode)_configuration.SkipToGamemodeInt;
		}

		// hook gameplay transition function
		unsafe {
            _gamemodeTransitionHook = _hooks.CreateHook<GamemodeTransition>(
                GamemodeTransitionImpl, 0x489210
            ).Activate();
        }
	}

	IHook<GamemodeTransition> _gamemodeTransitionHook;
    Gamemode _skipToGamemode;
    bool _skipped = false; // can only skip once, probably unneeded
	unsafe int GamemodeTransitionImpl(void* self) {
        if (!_skipped) {
			// unused, but nice to know
			Gamemode* at = (Gamemode*)((byte*)self+0x8);
			Gamemode* going_to = (Gamemode*)((byte*)self+0x10);

		    if (*going_to == Gamemode.Advertise) {
				*going_to = _skipToGamemode;
				_skipped = true;
		    }
        }

		return _gamemodeTransitionHook.OriginalFunction.Invoke(self);
	}
	[Function(CallingConventions.MicrosoftThiscall)]
	unsafe delegate int GamemodeTransition(void* self);

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