using System.Globalization;
using System.Threading;

namespace FernandoPFH_Essentials_Runtime
{
    public static class LocalizationForcer
    {
        public static void ForceCulture(string cultureName="pt-BR")
        {
            CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }
    }
}

// Example of Use
// public static class ForcePtBrLocalization
// {
//     [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
//     public static void RunBeforeSceneLoad()
//     {
//         FernandoPFH_Essentials_Runtime.LocalizationForcer.ForceCulture();
//     }
// }