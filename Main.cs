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

namespace RDR2LoadingMenu
{
    public class Main : IPlugin
    {
        public void Dispose()
        {

        }

        public void Init(object gameInstance)
        {
            new Harmony("RDR2LoadingMenu").PatchAll(Assembly.GetExecutingAssembly());
        }

        public void Update()
        {

        }
    }
}

