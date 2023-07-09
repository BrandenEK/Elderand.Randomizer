using BepInEx;
using Elderand.Level;
using Elderand.Randomizer.Debugging;
using Elderand.Randomizer.Items;
using HarmonyLib;
using UnityEngine.SceneManagement;

namespace Elderand.Randomizer
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Main : BaseUnityPlugin
    {
        private static readonly Manager[] _managers = new Manager[]
        {
            new DataLoader(),
            new ItemRandomizer(),
            new Debugger(),
        };
        public static ItemRandomizer ItemRandomizer => _managers[1] as ItemRandomizer;
        public static Debugger Debugger => _managers[2] as Debugger;
        public static DataLoader Data => _managers[0] as DataLoader;

        private static Main _instance;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;

            new Harmony(PluginInfo.PLUGIN_GUID).PatchAll();
            SceneManager.sceneLoaded += OnSceneLoaded;

            Log("Loaded randomizer mod!");
        }

        public static void Log(object message) => _instance.Logger.LogMessage(message);

        public static void LogWarning(object message) => _instance.Logger.LogWarning(message);

        public static void LogError(object message) => _instance.Logger.LogError(message);

        private void OnEnable() => LevelManager.OnLoadFinished += OnLevelLoaded;
        private void OnDisable() => LevelManager.OnLoadFinished -= OnLevelLoaded;

        private void Initialize()
        {
            foreach (Manager manager in _managers)
                manager.Initialize();
        }

        private void Update()
        {
            foreach (Manager manager in _managers)
                manager.Update();
        }

        private void OnLevelLoaded(Room room, DoorDirection direction)
        {
            Log("");
            Log($"Loaded level: {room.name} ({LevelManager.CurrentIndex})");
            Log("");

            foreach (Manager manager in _managers)
                manager.LevelLoaded(room.name);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "MainMenu")
            {
                Initialize();
                SceneManager.sceneLoaded -= OnSceneLoaded;
            }
        }
    }
}
