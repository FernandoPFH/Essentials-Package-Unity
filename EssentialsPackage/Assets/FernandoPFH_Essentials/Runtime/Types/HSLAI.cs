using UnityEngine;

namespace FernandoPFH_Essentials_Runtime
{
    public class HSLAI
    {
        public float H;
        public float S;
        public float L;
        public float A;
        public float I;

        public Color ToColor()
        {
            float intensityMultiplier = Mathf.Pow(2, I);

            Color colorWithoutHDR = ToColorWithoutHDR();
            Color colorWithHDR = colorWithoutHDR * intensityMultiplier;

            colorWithHDR.a = A;
            return colorWithHDR;
        }

        public Color ToColorWithoutHDR()
        {
            Color color = Color.HSVToRGB(H, S, L);
            color.a = A;
            return color;
        }
    }
}
