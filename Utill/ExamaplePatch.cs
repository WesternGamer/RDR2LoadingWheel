using HarmonyLib;
using Sandbox.Graphics;
using Sandbox.Graphics.GUI;
using System;
using System.Drawing;
using VRage.Utils;
using VRageMath;

namespace RDR2LoadingWheel.Utill
{
    //For more info about how harmony works, go to https:harmony.pardeike.net/

    [HarmonyPatch(typeof(MyGuiControlRotatingWheel), "Draw")]
    public class Patch_1
    {
        // The frame images.
        private static string[] Frames;

        // The index of the current frame.
        private static int FrameNum = 0;

        public static void Prefix(MyGuiControlRotatingWheel __instance, float ___m_wheelScale, float __0, float __1, float ___m_rotationSpeed, Vector2 ___m_textureSize, float ___m_rotatingAngle, bool ___ManualRotationUpdate)
        {
            try
            {
                __instance.Draw(__0, __1);
                Frames = new string[42];
                for (int i = 0; i < 42; i++)
                {
                    Frames[i] = "frame_" + i + "_delay-0.02s.png";
                }
                Vector2 positionAbsolute = __instance.GetPositionAbsolute();
                VRageMath.Color color2 = MyGuiControlBase.ApplyColorMaskModifiers(__instance.ColorMask, __instance.Enabled, __0);
                Vector2 normalizedSize = MyGuiManager.GetNormalizedSize(___m_textureSize, ___m_wheelScale);
                MyGuiManager.DrawSpriteBatch(System.Convert.ToString(Frames[FrameNum]), (Vector2)positionAbsolute, normalizedSize, color2, MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_CENTER, false, false, null, ___m_rotatingAngle, ___ManualRotationUpdate ? 0f : ___m_rotationSpeed);
            }
            catch(Exception e)
            {
                MyLog.Default.WriteLine(e.Message);
            }
        }
        //Action after the original method starts.

        public static void Postfix()
        {

        }
    }
}
