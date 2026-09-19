using NUnit.Framework;
using UnityEngine;

using FernandoPFH_Essentials_Runtime;

public class Vector3Extension
{
    [Test]
    public void MultiplyElementsTest()
    {
        Vector3 testVector = new (3f,4f,5f);
        Vector3 multiplyVector = new (2f,2.5f,5f);
        Vector3 resultVector = new (6f,10f,25f);
        Assert.AreEqual(testVector.MultiplyElements(multiplyVector),resultVector);
    }
}
