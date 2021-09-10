using HarmonyLib;
using System.Reflection;
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
            // Starts an instance of Harmony
            Harmony harmony = new Harmony("RDR2LoadingWheel");
            // Patches all patches in the plugin.
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public void Update()
        {
            
        }
    }
}
