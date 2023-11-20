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
            ModHooks.Instance.AfterSavegameLoadHook += AfterSaveGameLoad;
            ModHooks.Instance.NewGameHook += AddComponent;
            ModHooks.Instance.LanguageGetHook += LangGet;
        }


        public override string GetVersion(){
            return FileVersionInfo.GetVersionInfo(Assembly.GetAssembly(typeof(DoubleAnyRad)).Location).FileVersion;
        }

        private static string LangGet(string key, string sheettitle)
		{
			return key switch
			{
				"ABSOLUTE_RADIANCE_SUPER" => "Any", 
				"GG_S_RADIANCE" => "God of meme.", 
				"GODSEEKER_RADIANCE_STATUE" => "Ok.", 
				_ => global::Language.Language.GetInternal(key, sheettitle), 
			};
		}

        private static void AfterSaveGameLoad(SaveGameData data) {
            AddComponent();
        }

        private static void AddComponent() {
            GameManager.instance.gameObject.AddComponent<AbsFinder>();
        }

        public void Unload() {
            ModHooks.Instance.AfterSavegameLoadHook -= AfterSaveGameLoad;
            ModHooks.Instance.NewGameHook -= AddComponent;
            ModHooks.Instance.LanguageGetHook -= LangGet;
            GameManager instance = GameManager.instance;
            AbsFinder absFinder = ((instance != null) ? instance.gameObject.GetComponent<AbsFinder>() : null);
            if (!(absFinder == null))
            {
                Object.Destroy(absFinder);
            }
        }
    }
}