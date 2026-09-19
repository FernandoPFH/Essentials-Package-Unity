using System.Globalization;
using System.Threading;
using NUnit.Framework;

public class LocalizationForcer
{
    [Test]
    public void LocalizationForcerSimplePasses()
    {
        FernandoPFH_Essentials_Runtime.LocalizationForcer.ForceCulture();
        Assert.AreEqual(Thread.CurrentThread.CurrentCulture,CultureInfo.CreateSpecificCulture("pt-BR"));

        FernandoPFH_Essentials_Runtime.LocalizationForcer.ForceCulture("en-US");
        Assert.AreEqual(Thread.CurrentThread.CurrentCulture,CultureInfo.CreateSpecificCulture("en-US"));
    }
}
