using HarmonyLib;
using Sandbox.Game;
using Sandbox.Game.Gui;
using Sandbox.Game.Localization;
using Sandbox.Graphics.GUI;
using System.IO;
using System.Reflection;
using VRage;
using VRage.FileSystem;
using VRage.Input;
using VRage.Plugins;

namespace RDR2LoadingWheel
{
    public class Main : IPlugin
    {
        public void Dispose()
        {
            
        }

        public void Init(object gameInstance)
        {
            string imageFilePath = Path.GetFullPath(Path.Combine(MyFileSystem.ExePath, "LoadingWheel.png"));
            if (!File.Exists(imageFilePath))
            {
                ExtactImage("RDR2LoadingWheel", MyFileSystem.ExePath, "Resources", "LoadingWheel.png");
            }
            // Starts an instance of Harmony
            Harmony harmony = new Harmony("RDR2LoadingWheel");
            // Patches all patches in the plugin.
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public void Update()
        {
#if DEBUG
            if (MyInput.Static.IsNewKeyPressed(MyKeys.F11))
            {
                MyGuiScreenProgress progressScreen = new MyGuiScreenProgress(MyTexts.Get(MySpaceTexts.ProgressScreen_LoadingWorld));
                MyScreenManager.AddScreen(progressScreen);
            }
#endif
        }    

        // Thanks to BetterCoder for the code
        internal static void ExtactImage(string nameSpace, string outDirectory, string internalFilePath, string resourceName)
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            using (Stream s = assembly.GetManifestResourceStream(nameSpace + "." + (internalFilePath == "" ? "" : internalFilePath + ".") + resourceName))
            using (BinaryReader r = new BinaryReader(s))
            using (FileStream fs = new FileStream(outDirectory + "\\" + resourceName, FileMode.OpenOrCreate))
            using (BinaryWriter w = new BinaryWriter(fs))
                w.Write(r.ReadBytes((int)s.Length));
        }
    }
}

