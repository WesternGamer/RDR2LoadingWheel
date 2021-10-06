using HarmonyLib;
using Sandbox;
using Sandbox.Engine.Multiplayer;
using Sandbox.Engine.Platform;
using Sandbox.Engine.Utils;
using Sandbox.Game;
using Sandbox.Game.Gui;
using Sandbox.Game.Multiplayer;
using Sandbox.Game.World;
using Sandbox.Graphics;
using Sandbox.Graphics.GUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VRage;
using VRage.Audio;
using VRage.Library.Utils;
using VRage.Utils;
using VRageMath;
using VRageRender;
using VRageRender.Messages;

namespace RDR2LoadingMenu.LoadingScreenPatches
{
    [HarmonyPatch(typeof(MyGuiScreenLoading), "RecreateControls")]
    internal class RecreateControlsWheelPatch
    {
        private static void Postfix(MyGuiScreenLoading __instance, MyGuiControlRotatingWheel ___m_wheel)
        {
            ___m_wheel.Visible = false;

            
        }
    }

    [HarmonyPatch(typeof(MyGuiScreenLoading), "DrawInternal")]
    internal class DrawInternalPatch
    {
        public static uint globalVideoID ;

        private static bool Prefix(MyGuiScreenLoading __instance, float ___m_transitionAlpha)
        {
            MyRenderProxy.Settings.RenderThreadHighPriority = true;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            uint videoID = MyRenderProxy.PlayVideo(@"D:\Games\steamapps\common\SpaceEngineers\Content\Videos\Background01_720p.wmv", 0f);
            globalVideoID = videoID;
            MyRenderProxy.UpdateVideo(videoID);
            MyRenderProxy.DrawVideo(videoID, MyGuiManager.GetSafeFullscreenRectangle(), new Color(Vector4.One), MyVideoRectangleFitMode.AutoFit, true);
            return false;
        }
    }


    [HarmonyPatch(typeof(MyGuiScreenLoading), "Draw")]
    internal class DrawPatch
    {
        private static void Postfix(MyGuiScreenLoading __instance, float ___m_transitionAlpha)
        {
            
        }
    }

    [HarmonyPatch(typeof(MyGuiScreenLoading), "UnloadContent")]
    internal class UnloadContentPatch
    {
        private static void Prefix(MyGuiScreenLoading __instance, float ___m_transitionAlpha)
        {
            MyRenderProxy.CloseVideo(DrawInternalPatch.globalVideoID);
            MyRenderProxy.Settings.RenderThreadHighPriority = false;
            Thread.CurrentThread.Priority = ThreadPriority.Normal;
        }
    }

    /*[HarmonyPatch(typeof(MyGuiScreenLoading), "OnClosed")]
    internal class OnClosedPatch
    {
        private static void Prefix(MyGuiScreenLoading __instance, float ___m_transitionAlpha)
        {
            MyRenderProxy.CloseVideo(DrawInternalPatch.globalVideoID);
            MyRenderProxy.Settings.RenderThreadHighPriority = false;
            Thread.CurrentThread.Priority = ThreadPriority.Normal;
        }
    }

    [HarmonyPatch(typeof(MyGuiScreenLoading), "UnloadOnException")]
    internal class UnloadOnExceptionPatch
    {
        private static void Prefix(MyGuiScreenLoading __instance, float ___m_transitionAlpha)
        {
            MyRenderProxy.CloseVideo(DrawInternalPatch.globalVideoID);
            MyRenderProxy.Settings.RenderThreadHighPriority = false;
            Thread.CurrentThread.Priority = ThreadPriority.Normal;
        }
    }

    [HarmonyPatch(typeof(MyGuiScreenLoading), "OnRemoved")]
    internal class OnRemovedPatch
    {
        private static void Prefix(MyGuiScreenLoading __instance, float ___m_transitionAlpha)
        {
            MyRenderProxy.CloseVideo(DrawInternalPatch.globalVideoID);
            MyRenderProxy.Settings.RenderThreadHighPriority = false;
            Thread.CurrentThread.Priority = ThreadPriority.Normal;
        }
    }*/
}

