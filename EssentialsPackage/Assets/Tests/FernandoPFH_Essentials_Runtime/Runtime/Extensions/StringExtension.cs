using NUnit.Framework;

using FernandoPFH_Essentials_Runtime;
using System.Collections.Generic;

public class StringExtension
{
    [Test]
    public void PopulateTemplateTest()
    {
        string testTemplate = "Hello, {{Entity}}!";
        Dictionary<string,string> testDictionary = new Dictionary<string, string> {{"Entity","World"}};
        string testResult = "Hello, World!";
        Assert.AreEqual(testTemplate.PopulateTemplate(testDictionary),testResult);
    }

    [Test]
    public void ReverseTest()
    {
        string test = "Test";
        string testResult = "tseT";
        Assert.AreEqual(test.Reverse(),testResult);
    }
}
