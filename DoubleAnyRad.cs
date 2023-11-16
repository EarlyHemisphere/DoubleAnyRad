using System.Diagnostics;
using System.Reflection;
using JetBrains.Annotations;
using Modding;
using UnityEngine;

namespace DoubleAnyRad {
    [UsedImplicitly]
    public class DoubleAnyRad : Mod, ITogglableMod {
        public static DoubleAnyRad instance;

        public DoubleAnyRad() : base("Double Any Radiance") { }

        public override void Initialize() {
            instance = this;

            Log("Initalizing.");
            ModHooks.AfterSavegameLoadHook += AfterSaveGameLoad;
            ModHooks.NewGameHook += AddComponent;
            ModHooks.LanguageGetHook += LangGet;
        }


        public override string GetVersion(){
            return FileVersionInfo.GetVersionInfo(Assembly.GetAssembly(typeof(DoubleAnyRad)).Location).FileVersion;
        }

        private static string LangGet(string key, string sheettitle, string orig) {
            if (key != null) {
                switch (key) {
                    case "ABSOLUTE_RADIANCE_SUPER": return "Any";
                    case "GG_S_RADIANCE": return "God of meme.";
                    case "GODSEEKER_RADIANCE_STATUE": return "Ok.";
                    default: return Language.Language.GetInternal(key, sheettitle);
                }
            }
            return orig;
        }

        private static void AfterSaveGameLoad(SaveGameData data) {
            AddComponent();
        }

        private static void AddComponent() {
            GameManager.instance.gameObject.AddComponent<AbsFinder>();
        }

        public void Unload() {
            ModHooks.AfterSavegameLoadHook -= AfterSaveGameLoad;
            ModHooks.NewGameHook -= AddComponent;
            ModHooks.LanguageGetHook -= LangGet;
            GameManager instance = GameManager.instance;
            AbsFinder absFinder = ((instance != null) ? instance.gameObject.GetComponent<AbsFinder>() : null);
            if (!(absFinder == null))
            {
                Object.Destroy(absFinder);
            }
        }
    }
}