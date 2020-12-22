using System.Linq;
using NUnit.Framework;

namespace Pinyin4net.Tests
{
    [TestFixture]
    public class GwoyeuPinYinTest
    {
        [Test]
        [Description("Test Chinese character input")]
        [TestCase('蔣', "chiang3")]
        [TestCase('介', "chieh4")]
        [TestCase('石', "shih2")]
        public void TestNonChineseCharacter(char ch, string result)
        {
            var expected = PinyinHelper.ToWadeGilesPinyinStringArray(ch).First();
            Assert.AreEqual(expected, result);
        }
    }
}