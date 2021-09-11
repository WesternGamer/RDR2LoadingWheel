using HarmonyLib;
using Sandbox.Graphics;
using Sandbox.Graphics.GUI;
using System;
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
        private static float m_rotationSpeed;

        private static float m_rotatingAngle;

        private static float m_wheelScale;

        private static string m_texture;

        private static Vector2 m_textureSize;

        public static bool Prefix(MyGuiControlRotatingWheel __instance, float __0, float __1, float ___m_rotationSpeed, float ___m_rotatingAngle, float ___m_wheelScale, string ___m_texture, Vector2 ___m_textureSize)
        {
            float transitionAlpha = __0;
            float backgroundTransitionAlpha = __1;
            m_rotationSpeed = ___m_rotationSpeed;
            m_rotatingAngle = ___m_rotatingAngle;
            m_wheelScale = ___m_wheelScale;
            m_texture = ___m_texture;
            m_textureSize = ___m_textureSize;

            m_texture = @"D:\Games\steamapps\common\SpaceEngineers\Bin64\frame_0_delay-0.02s.png";
            m_textureSize = MyRenderProxy.GetTextureSize(m_texture);

            m_textureSize = DivideVector(m_textureSize, 2f);

            Vector2 positionAbsolute = __instance.GetPositionAbsolute();
            Color color = new Color(transitionAlpha * new Color(0, 0, 0, 80).ToVector4());
            DrawWheel(positionAbsolute + MyGuiConstants.SHADOW_OFFSET, m_wheelScale, color, m_rotatingAngle, m_rotationSpeed);
            Color color2 = MyGuiControlBase.ApplyColorMaskModifiers(__instance.ColorMask, __instance.Enabled, transitionAlpha);
            DrawWheel(positionAbsolute, m_wheelScale, color2, m_rotatingAngle, m_rotationSpeed);

            void DrawWheel(Vector2 position, float scale, Color internalColor, float rotationAngle, float rotationSpeed)
            {
                Vector2 normalizedSize = MyGuiManager.GetNormalizedSize(m_textureSize, scale);
                MyGuiManager.DrawSpriteBatch(m_texture, position, normalizedSize, internalColor, MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_CENTER, false, false, null, rotationAngle, __instance.ManualRotationUpdate ? 0f : rotationSpeed);
            }

            Vector2 DivideVector(Vector2 vector, float float1)
            {
                float vectorX = vector.X;
                float vectorY = vector.Y;

                float vectorXResult = vectorX / float1;
                float vectorYResult = vectorY / float1;

                Vector2 result = new Vector2(vectorXResult, vectorYResult);

                return result;
            }

            return false;
        }

        public override void Update()
        {
            bool lastUpadated = false;
            base.Update();
            if (lastUpadated)
            {
                lastUpadated = false;
                return;
            }


        }


    }

    [HarmonyPatch(typeof(MyGuiControlRotatingWheel), "UpdateRotation")]
    public class Patch_LoadingRotation : MyGuiControlRotatingWheel
    {
        private static float m_rotationSpeed;

        private static float m_rotatingAngle;

        private static float m_wheelScale;

        private static string m_texture;

        private static Vector2 m_textureSize;

        public static bool Prefix(MyGuiControlRotatingWheel __instance, float ___m_rotationSpeed, float ___m_rotatingAngle, float ___m_wheelScale, string ___m_texture, Vector2 ___m_textureSize)
        {
            m_rotationSpeed = ___m_rotationSpeed;
            m_rotatingAngle = ___m_rotatingAngle;
            m_wheelScale = ___m_wheelScale;
            m_texture = ___m_texture;
            m_textureSize = ___m_textureSize;

            m_rotatingAngle = (float)MyEnvironment.TickCount / 1000f * m_rotationSpeed;
            return false;
        }
    }

}
