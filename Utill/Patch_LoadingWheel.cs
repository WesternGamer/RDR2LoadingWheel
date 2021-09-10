using HarmonyLib;
using Sandbox.Graphics;
using Sandbox.Graphics.GUI;
using System;
using System.Threading.Tasks;
using System.Windows.Threading;
using VRage.Utils;
using VRageMath;
using Color = VRageMath.Color;

namespace RDR2LoadingWheel.Utill
{
    [HarmonyPatch(typeof(MyGuiControlRotatingWheel), "Draw")]
    public class Patch_LoadingWheel : MyGuiControlRotatingWheel
    {
        private static float m_globalRotationSpeed;

        public static bool Prefix(MyGuiControlRotatingWheel __instance, float ___m_wheelScale, float __0, Vector2 ___m_textureSize, float ___m_rotatingAngle, float ___m_rotationSpeed)
        {
            m_globalRotationSpeed = ___m_rotationSpeed;
            Vector2 positionAbsolute = __instance.GetPositionAbsolute();
            Color color = new Color(__0 * new Color(0, 0, 0, 80).ToVector4());
            DrawWheel(___m_textureSize, positionAbsolute + MyGuiConstants.SHADOW_OFFSET, ___m_wheelScale, color, ___m_rotatingAngle, m_globalRotationSpeed);
            Color color2 = MyGuiControlBase.ApplyColorMaskModifiers(__instance.ColorMask, __instance.Enabled, __0);
            DrawWheel(___m_textureSize, positionAbsolute, ___m_wheelScale, color2, ___m_rotatingAngle, m_globalRotationSpeed);
            void DrawWheel(Vector2 texturesize, Vector2 position, float scale, Color color3, float rotationAngle, float rotationSpeed)
            {
                Vector2 normalizedSize = MyGuiManager.GetNormalizedSize(texturesize, scale);
                //The texture will be enbedded in the plugin, this is just for development.  
                MyGuiManager.DrawSpriteBatch(@"D:\Games\steamapps\common\SpaceEngineers\Bin64\frame_0_delay-0.02s.png", position, normalizedSize, color3, MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_CENTER, false, false, null, rotationAngle, __instance.ManualRotationUpdate ? 0f : rotationSpeed);

                
            }
            
            return false;
        }

        public override void Update()
        {
            base.Update();
            m_globalRotationSpeed++;
            DelayAction(2000);
            void DelayAction(int millisecond)
            {
                var timer = new DispatcherTimer();
                timer.Tick += delegate

                {
                    m_globalRotationSpeed = 0;
                    timer.Stop();
                };

                timer.Interval = TimeSpan.FromMilliseconds(millisecond);
                timer.Start();
            }
        }


    }
}
