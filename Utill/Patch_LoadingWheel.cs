using HarmonyLib;
using Sandbox.Graphics;
using Sandbox.Graphics.GUI;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using VRage.Library;
using VRage.Utils;
using VRageMath;
using VRageRender;
using Color = VRageMath.Color;

namespace RDR2LoadingWheel.Utill
{
    [HarmonyPatch(typeof(MyGuiControlRotatingWheel), "Draw")]
    public class Patch_LoadingWheel : MyGuiControlRotatingWheel
    {
        private static MyGuiControlRotatingWheel m_instance;

        private static float m_rotationSpeed;

        private static float m_rotatingAngle;

        private static float m_wheelScale;

        private static string m_texture;

        private static Vector2 m_textureSize;

        private static float m_transitionAlpha;

        private static float m_backgroundTransitionAlpha;

        public static bool Prefix(MyGuiControlRotatingWheel __instance, float __0, float __1, float ___m_rotationSpeed, float ___m_rotatingAngle, float ___m_wheelScale, string ___m_texture, Vector2 ___m_textureSize)
        {
            m_instance = __instance;
            m_rotationSpeed = ___m_rotationSpeed;
            m_rotatingAngle = ___m_rotatingAngle;
            m_wheelScale = ___m_wheelScale;
            m_texture = ___m_texture;
            m_textureSize = ___m_textureSize;
            m_transitionAlpha = __0;
            m_backgroundTransitionAlpha = __1;

            //Will be set in an internal resource when other issues are fixed.
            m_texture = @"D:\Games\steamapps\common\SpaceEngineers\Bin64\frame_0_delay-0.02s.png";
            m_textureSize = MyRenderProxy.GetTextureSize(m_texture);

            m_textureSize = DivideVector(m_textureSize, 2f);

            Thread thread1 = new Thread(UpdateWheel);
            thread1.Start();

            return false;
        }

        private static float AddFloat(float float1, float float2)
        {
            float result = float1 + float1;
            return result;
        }

        private static Vector2 DivideVector(Vector2 vector, float float1)
        {
            float vectorX = vector.X;
            float vectorY = vector.Y;

            float vectorXResult = vectorX / float1;
            float vectorYResult = vectorY / float1;

            Vector2 result = new Vector2(vectorXResult, vectorYResult);

            return result;
        }

        private static void UpdateWheel()
        {
            while (m_instance.Visible)
            {
                Vector2 positionAbsolute = m_instance.GetPositionAbsolute();
                Color color = new Color(m_transitionAlpha * new Color(0, 0, 0, 80).ToVector4());
                Color color2 = ApplyColorMaskModifiers(m_instance.ColorMask, m_instance.Enabled, m_transitionAlpha);

                DrawWheel();

                Thread.Sleep(1000);

                void DrawWheel()
                {
                    Vector2 normalizedSize = MyGuiManager.GetNormalizedSize(m_textureSize, m_wheelScale);
                    MyGuiManager.DrawSpriteBatch(m_texture, positionAbsolute + MyGuiConstants.SHADOW_OFFSET, normalizedSize, color, MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_CENTER, false, false, null, m_rotatingAngle, m_instance.ManualRotationUpdate ? 0f : m_rotationSpeed);
                    MyGuiManager.DrawSpriteBatch(m_texture, positionAbsolute, normalizedSize, color2, MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_CENTER, false, false, null, m_rotatingAngle, m_instance.ManualRotationUpdate ? 0f : m_rotationSpeed);

                }
            }
        }



    }

    [HarmonyPatch(typeof(MyGuiControlRotatingWheel), "UpdateRotation")]
    public class Patch_LoadingRotation
    {
        private static float m_rotationSpeed;

        private static float m_rotatingAngle;

        private static float m_wheelScale;

        private static string m_texture;

        private static Vector2 m_textureSize;

        public static bool Prefix(MyGuiControlRotatingWheel __instance, float ___m_rotationSpeed, float ___m_rotatingAngle, float ___m_wheelScale, string ___m_texture, Vector2 ___m_textureSize)
        {
            /*m_rotationSpeed = ___m_rotationSpeed;
            m_rotatingAngle = ___m_rotatingAngle;
            m_wheelScale = ___m_wheelScale;
            m_texture = ___m_texture;
            m_textureSize = ___m_textureSize;
            m_rotatingAngle = (float)MyEnvironment.TickCount / 1000f * m_rotationSpeed;*/
            return false;
        }
    }

}