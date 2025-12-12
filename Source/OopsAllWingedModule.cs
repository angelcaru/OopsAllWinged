using System;
using Microsoft.Xna.Framework;

namespace Celeste.Mod.OopsAllWinged;

public class OopsAllWingedModule : EverestModule {
    public static OopsAllWingedModule Instance { get; private set; }

    public OopsAllWingedModule() {
        Instance = this;
#if DEBUG
        // debug builds use verbose logging
        Logger.SetLogLevel(nameof(OopsAllWingedModule), LogLevel.Verbose);
#else
        // release builds use info logging to reduce spam in log files
        Logger.SetLogLevel(nameof(OopsAllWingedModule), LogLevel.Info);
#endif
    }

    public static void OnBerryCtor(On.Celeste.Strawberry.orig_ctor orig, Strawberry self, EntityData data, Vector2 offset, EntityID gid) {
        data.Values["winged"] = true;
        data.Values["moon"] = false;
        orig(self, data, offset, gid);
    }

    public override void Load() {
        On.Celeste.Strawberry.ctor += OnBerryCtor;
    }

    public override void Unload() {
        On.Celeste.Strawberry.ctor -= OnBerryCtor;
    }
}