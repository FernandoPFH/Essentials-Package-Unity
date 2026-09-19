using System;
using UnityEngine;

namespace FernandoPFH_Essentials_Runtime
{
    public static class ColorExtension
    {
        private const byte k_MaxByteForOverexposedColor = 191;

        public static void DecomposeHdrColor(this Color linearColorHdr, out Color baseLinearColor, out float exposure)
        {
            Color32 tempBaseLinearColor = new();
            var maxColorComponent = linearColorHdr.maxColorComponent;
            // replicate Photoshops's decomposition behaviour
            if (maxColorComponent == 0f || maxColorComponent <= 1f && maxColorComponent >= 1 / 255f)
            {
                exposure = 0f;

                tempBaseLinearColor.r = (byte)Mathf.RoundToInt(linearColorHdr.r * 255f);
                tempBaseLinearColor.g = (byte)Mathf.RoundToInt(linearColorHdr.g * 255f);
                tempBaseLinearColor.b = (byte)Mathf.RoundToInt(linearColorHdr.b * 255f);
            }
            else
            {
                // calibrate exposure to the max float color component
                var scaleFactor = k_MaxByteForOverexposedColor / maxColorComponent;
                exposure = Mathf.Log(255f / scaleFactor) / Mathf.Log(2f);

                // maintain maximal integrity of byte values to prevent off-by-one errors when scaling up a color one component at a time
                tempBaseLinearColor.r = Math.Min(k_MaxByteForOverexposedColor, (byte)Mathf.CeilToInt(scaleFactor * linearColorHdr.r));
                tempBaseLinearColor.g = Math.Min(k_MaxByteForOverexposedColor, (byte)Mathf.CeilToInt(scaleFactor * linearColorHdr.g));
                tempBaseLinearColor.b = Math.Min(k_MaxByteForOverexposedColor, (byte)Mathf.CeilToInt(scaleFactor * linearColorHdr.b));
            }

            baseLinearColor = (Color)tempBaseLinearColor;
        }

        public static Color OpaqueColor(this Color color)
            => new(color.r,color.g,color.b,1f);

        public static Color MultiplyOpaqueColor(this Color color,float scale)
            => (color * scale).OpaqueColor();

        public static HSLAI ToHSLAI(this Color color)
        {
            color.DecomposeHdrColor(out Color baseLinearColor, out float intensity);
            Color.RGBToHSV(baseLinearColor, out float H, out float S, out float V);
            return new() { H = H, S = S, L = V, A = color.a, I = intensity };
        }
    }
}
