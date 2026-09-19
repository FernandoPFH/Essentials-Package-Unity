using NUnit.Framework;

using FernandoPFH_Essentials_Runtime;

public class SerializableDictionary
{
    [Test]
    public void GetValueTest()
    {
        SerializableDictionary<string,int> testDictionary = new SerializableDictionary<string,int>
        {
            {"Test", 1},
            {"Test 2", 5},
        };

        Assert.AreEqual(testDictionary["Test"],1);
        Assert.AreEqual(testDictionary["Test 2"],5);
    }

    [Test]
    public void AddValueTest()
    {
        SerializableDictionary<string,int> testDictionary = new SerializableDictionary<string,int>
        {
            {"Test", 1},
        };

        Assert.AreEqual(testDictionary.Count, 1);

        testDictionary.Add("Test 2", 5);

        Assert.AreEqual(testDictionary["Test"],1);
        Assert.AreEqual(testDictionary["Test 2"],5);

        Assert.AreEqual(testDictionary.Count, 2);
    }

    [Test]
    public void RemoveValueTest()
    {
        SerializableDictionary<string,int> testDictionary = new SerializableDictionary<string,int>
        {
            {"Test", 1},
            {"Test 2", 5},
        };

        Assert.AreEqual(testDictionary["Test 2"],5);
        Assert.AreEqual(testDictionary.Count, 2);

        testDictionary.Remove("Test 2");

        Assert.AreEqual(testDictionary["Test"],1);
        Assert.AreEqual(testDictionary.Count, 1);
    }
}
