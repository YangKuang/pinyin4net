/**
 * Copyright (c) 2012 Yang Kuang
 *
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the
 * "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to
 * permit persons to whom the Software is furnished to do so, subject to
 * the following conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
 * LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
 * OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
 * WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
**/

using System;
using NUnit.Framework;
using Pinyin4net;
using Pinyin4net.Format;
using Pinyin4net.Exceptions;

namespace Pinyin4net.Tests
{
    [TestFixture]
    public class HanyuPinyinTest
    {
        #region Tests

        [Test]
        [Description("Test null input")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullInput()
        {
            PinyinHelper.ToHanyuPinyinStringArray('李', null);
        }
        
        [Test]
        [Description("Test non Chinese character input")]
        [TestCase('A')]
        [TestCase('ガ')]
        [TestCase('ç')]
        [TestCase('匇')]
        public void TestNonChineseCharacter(char ch)
        {
            Assert.IsNull(PinyinHelper.ToHanyuPinyinStringArray(ch));
        }
        
        [Test]
        [Description("Test get hanyupinyin with different VCharType format")]
        //  Simplified Chinese
        [TestCase('吕', HanyuPinyinVCharType.WITH_U_AND_COLON, Result = "lu:3")]
        [TestCase('李', HanyuPinyinVCharType.WITH_U_AND_COLON, Result = "li3")]
        [TestCase('吕', HanyuPinyinVCharType.WITH_V, Result = "lv3")]
        [TestCase('李', HanyuPinyinVCharType.WITH_V, Result = "li3")]
        [TestCase('吕', HanyuPinyinVCharType.WITH_U_UNICODE, Result = "lü3")]
        [TestCase('李', HanyuPinyinVCharType.WITH_U_UNICODE, Result = "li3")]
        //  Traditional Chinese
        [TestCase('呂', HanyuPinyinVCharType.WITH_U_AND_COLON, Result = "lu:3")]
        [TestCase('呂', HanyuPinyinVCharType.WITH_V, Result = "lv3")]
        [TestCase('呂', HanyuPinyinVCharType.WITH_U_UNICODE, Result = "lü3")]
        public string TestVCharType(char ch, HanyuPinyinVCharType vcharType)
        {
            HanyuPinyinOutputFormat format = new HanyuPinyinOutputFormat();
            format.VCharType = vcharType;
            return PinyinHelper.ToHanyuPinyinStringArray(ch, format)[0];
        }

        [Test]
        [Description("Test get hanyupinyin with upper case format")]
        //  Simplified Chinese
        [TestCase('吕', HanyuPinyinVCharType.WITH_U_AND_COLON, Result = "LU:3")]
        [TestCase('李', HanyuPinyinVCharType.WITH_U_AND_COLON, Result = "LI3")]
        [TestCase('吕', HanyuPinyinVCharType.WITH_V, Result = "LV3")]
        [TestCase('李', HanyuPinyinVCharType.WITH_V, Result = "LI3")]
        [TestCase('吕', HanyuPinyinVCharType.WITH_U_UNICODE, Result = "LÜ3")]
        [TestCase('李', HanyuPinyinVCharType.WITH_U_UNICODE, Result = "LI3")]
        //  Traditional Chinese
        [TestCase('呂', HanyuPinyinVCharType.WITH_U_AND_COLON, Result = "LU:3")]
        [TestCase('呂', HanyuPinyinVCharType.WITH_V, Result = "LV3")]
        [TestCase('呂', HanyuPinyinVCharType.WITH_U_UNICODE, Result = "LÜ3")]
        public string TestCaseType(char ch, HanyuPinyinVCharType vcharType)
        {
            HanyuPinyinOutputFormat format = new HanyuPinyinOutputFormat();
            format.CaseType = HanyuPinyinCaseType.UPPERCASE;
            format.VCharType = vcharType;
            return PinyinHelper.ToHanyuPinyinStringArray(ch, format)[0];
        }

        [Test]
        [Description("Test get hanyupinyin with invalid format")]
        [ExpectedException(typeof(InvalidHanyuPinyinFormatException))]
        [TestCase('吕', HanyuPinyinVCharType.WITH_U_AND_COLON)]
        [TestCase('呂', HanyuPinyinVCharType.WITH_U_AND_COLON)]
        public void TestToneMarkWithUAndColon(char ch, HanyuPinyinVCharType vcharType)
        {
            HanyuPinyinOutputFormat format = new HanyuPinyinOutputFormat();
            format.ToneType = HanyuPinyinToneType.WITH_TONE_MARK;
            format.VCharType = vcharType;
            PinyinHelper.ToHanyuPinyinStringArray(ch, format);
        }

        [Test]
        [Description("Test get hanyupinyin with tone mark format")]
        #region Test data
        //  Simplified Chinese
        [TestCase('爸', Result = "bà")]
        [TestCase('波', Result = "bō")]
        [TestCase('苛', Result = "kē")]
        [TestCase('李', Result = "lǐ")]
        [TestCase('露', Result = "lù")]
        [TestCase('吕', Result = "lǚ")]
        [TestCase('来', Result = "lái")]
        [TestCase('背', Result = "bèi")]
        [TestCase('宝', Result = "bǎo")]
        [TestCase('抠', Result = "kōu")]
        [TestCase('虾', Result = "xiā")]
        [TestCase('携', Result = "xié")]
        [TestCase('表', Result = "biǎo")]
        [TestCase('球', Result = "qiú")]
        [TestCase('花', Result = "huā")]
        [TestCase('落', Result = "luò")]
        [TestCase('槐', Result = "huái")]
        [TestCase('徽', Result = "huī")]
        [TestCase('月', Result = "yuè")]
        [TestCase('汗', Result = "hàn")]
        [TestCase('狠', Result = "hěn")]
        [TestCase('邦', Result = "bāng")]
        [TestCase('烹', Result = "pēng")]
        [TestCase('轰', Result = "hōng")]
        [TestCase('天', Result = "tiān")]
        [TestCase('银', Result = "yín")]
        [TestCase('鹰', Result = "yīng")]
        [TestCase('想', Result = "xiǎng")]
        [TestCase('炯', Result = "jiǒng")]
        [TestCase('环', Result = "huán")]
        [TestCase('云', Result = "yún")]
        [TestCase('黄', Result = "huáng")]
        [TestCase('渊', Result = "yuān")]
        [TestCase('儿', Result = "ér")]
        //  Traditional Chinese
        [TestCase('呂', Result = "lǚ")]
        [TestCase('來', Result = "lái")]
        [TestCase('寶', Result = "bǎo")]
        [TestCase('摳', Result = "kōu")]
        [TestCase('蝦', Result = "xiā")]
        [TestCase('攜', Result = "xié")]
        [TestCase('轟', Result = "hōng")]
        [TestCase('銀', Result = "yín")]
        [TestCase('鷹', Result = "yīng")]
        [TestCase('環', Result = "huán")]
        [TestCase('雲', Result = "yún")]
        [TestCase('黃', Result = "huáng")]
        [TestCase('淵', Result = "yuān")]
        [TestCase('兒', Result = "ér")]
        #endregion
        public string TestToneMark(char ch)
        {
            HanyuPinyinOutputFormat format = new HanyuPinyinOutputFormat();
            format.ToneType = HanyuPinyinToneType.WITH_TONE_MARK;
            format.VCharType = HanyuPinyinVCharType.WITH_U_UNICODE;
            return PinyinHelper.ToHanyuPinyinStringArray(ch, format)[0];
        }

        [Test]
        [Description("Test get hanyupinyin with out tone format")]
        #region Test data
        //  Simplified Chinese
        [TestCase('吕', HanyuPinyinVCharType.WITH_U_AND_COLON, HanyuPinyinCaseType.LOWERCASE, Result = "lu:")]
        [TestCase('李', HanyuPinyinVCharType.WITH_U_AND_COLON, HanyuPinyinCaseType.LOWERCASE, Result = "li")]
        [TestCase('吕', HanyuPinyinVCharType.WITH_U_AND_COLON, HanyuPinyinCaseType.UPPERCASE, Result = "LU:")]
        [TestCase('李', HanyuPinyinVCharType.WITH_U_AND_COLON, HanyuPinyinCaseType.UPPERCASE, Result = "LI")]
        [TestCase('吕', HanyuPinyinVCharType.WITH_V, HanyuPinyinCaseType.LOWERCASE, Result = "lv")]
        [TestCase('李', HanyuPinyinVCharType.WITH_V, HanyuPinyinCaseType.LOWERCASE, Result = "li")]
        [TestCase('吕', HanyuPinyinVCharType.WITH_V, HanyuPinyinCaseType.UPPERCASE, Result = "LV")]
        [TestCase('李', HanyuPinyinVCharType.WITH_V, HanyuPinyinCaseType.UPPERCASE, Result = "LI")]
        [TestCase('吕', HanyuPinyinVCharType.WITH_U_UNICODE, HanyuPinyinCaseType.LOWERCASE, Result = "lü")]
        [TestCase('李', HanyuPinyinVCharType.WITH_U_UNICODE, HanyuPinyinCaseType.LOWERCASE, Result = "li")]
        [TestCase('吕', HanyuPinyinVCharType.WITH_U_UNICODE, HanyuPinyinCaseType.UPPERCASE, Result = "LÜ")]
        [TestCase('李', HanyuPinyinVCharType.WITH_U_UNICODE, HanyuPinyinCaseType.UPPERCASE, Result = "LI")]
        //  Traditional Chinese
        [TestCase('呂', HanyuPinyinVCharType.WITH_U_AND_COLON, HanyuPinyinCaseType.LOWERCASE, Result = "lu:")]
        [TestCase('呂', HanyuPinyinVCharType.WITH_U_AND_COLON, HanyuPinyinCaseType.UPPERCASE, Result = "LU:")]
        [TestCase('呂', HanyuPinyinVCharType.WITH_V, HanyuPinyinCaseType.LOWERCASE, Result = "lv")]
        [TestCase('呂', HanyuPinyinVCharType.WITH_V, HanyuPinyinCaseType.UPPERCASE, Result = "LV")]
        [TestCase('呂', HanyuPinyinVCharType.WITH_U_UNICODE, HanyuPinyinCaseType.LOWERCASE, Result = "lü")]
        [TestCase('呂', HanyuPinyinVCharType.WITH_U_UNICODE, HanyuPinyinCaseType.UPPERCASE, Result = "LÜ")]
        #endregion
        public string TestWithoutToneNumber(
            char ch, HanyuPinyinVCharType vcharType, HanyuPinyinCaseType caseType)
        {
            HanyuPinyinOutputFormat format = new HanyuPinyinOutputFormat();
            format.ToneType = HanyuPinyinToneType.WITHOUT_TONE;
            format.VCharType = vcharType;
            format.CaseType = caseType;
            return PinyinHelper.ToHanyuPinyinStringArray(ch, format)[0];
        }

        [Test]
        [Description("Test get hanyupinyin with tone number format")]
        #region Test data
        //  Simplified Chinese
        [TestCase('吕', HanyuPinyinVCharType.WITH_U_AND_COLON, HanyuPinyinCaseType.LOWERCASE, Result = "lu:3")]
        [TestCase('李', HanyuPinyinVCharType.WITH_U_AND_COLON, HanyuPinyinCaseType.LOWERCASE, Result = "li3")]
        [TestCase('吕', HanyuPinyinVCharType.WITH_U_AND_COLON, HanyuPinyinCaseType.UPPERCASE, Result = "LU:3")]
        [TestCase('李', HanyuPinyinVCharType.WITH_U_AND_COLON, HanyuPinyinCaseType.UPPERCASE, Result = "LI3")]
        [TestCase('吕', HanyuPinyinVCharType.WITH_V, HanyuPinyinCaseType.LOWERCASE, Result = "lv3")]
        [TestCase('李', HanyuPinyinVCharType.WITH_V, HanyuPinyinCaseType.LOWERCASE, Result = "li3")]
        [TestCase('吕', HanyuPinyinVCharType.WITH_V, HanyuPinyinCaseType.UPPERCASE, Result = "LV3")]
        [TestCase('李', HanyuPinyinVCharType.WITH_V, HanyuPinyinCaseType.UPPERCASE, Result = "LI3")]
        [TestCase('吕', HanyuPinyinVCharType.WITH_U_UNICODE, HanyuPinyinCaseType.LOWERCASE, Result = "lü3")]
        [TestCase('李', HanyuPinyinVCharType.WITH_U_UNICODE, HanyuPinyinCaseType.LOWERCASE, Result = "li3")]
        [TestCase('吕', HanyuPinyinVCharType.WITH_U_UNICODE, HanyuPinyinCaseType.UPPERCASE, Result = "LÜ3")]
        [TestCase('李', HanyuPinyinVCharType.WITH_U_UNICODE, HanyuPinyinCaseType.UPPERCASE, Result = "LI3")]
        //  Traditional Chinese
        [TestCase('呂', HanyuPinyinVCharType.WITH_U_AND_COLON, HanyuPinyinCaseType.LOWERCASE, Result = "lu:3")]
        [TestCase('呂', HanyuPinyinVCharType.WITH_U_AND_COLON, HanyuPinyinCaseType.UPPERCASE, Result = "LU:3")]
        [TestCase('呂', HanyuPinyinVCharType.WITH_V, HanyuPinyinCaseType.LOWERCASE, Result = "lv3")]
        [TestCase('呂', HanyuPinyinVCharType.WITH_V, HanyuPinyinCaseType.UPPERCASE, Result = "LV3")]
        [TestCase('呂', HanyuPinyinVCharType.WITH_U_UNICODE, HanyuPinyinCaseType.LOWERCASE, Result = "lü3")]
        [TestCase('呂', HanyuPinyinVCharType.WITH_U_UNICODE, HanyuPinyinCaseType.UPPERCASE, Result = "LÜ3")]
        #endregion
        public string TestWithToneNumber(
            char ch, HanyuPinyinVCharType vcharType, HanyuPinyinCaseType caseType)
        {
            HanyuPinyinOutputFormat format = new HanyuPinyinOutputFormat();
            format.ToneType = HanyuPinyinToneType.WITH_TONE_NUMBER;
            format.VCharType = vcharType;
            format.CaseType = caseType;
            return PinyinHelper.ToHanyuPinyinStringArray(ch, format)[0];
        }

        [Test]
        [Category("Simplified Chinese")]
        [Description("Test character with multiple pronounciations")]
        #region Test data
        //  Simplified Chinese
        [TestCase('偻', HanyuPinyinToneType.WITH_TONE_NUMBER, HanyuPinyinVCharType.WITH_U_AND_COLON, HanyuPinyinCaseType.LOWERCASE, Result = new string[] {"lou2", "lu:3"})]
        [TestCase('偻', HanyuPinyinToneType.WITH_TONE_NUMBER, HanyuPinyinVCharType.WITH_U_AND_COLON, HanyuPinyinCaseType.UPPERCASE, Result = new string[] { "LOU2", "LU:3" })]
        [TestCase('偻', HanyuPinyinToneType.WITH_TONE_NUMBER, HanyuPinyinVCharType.WITH_V, HanyuPinyinCaseType.LOWERCASE, Result = new string[] { "lou2", "lv3" })]
        [TestCase('偻', HanyuPinyinToneType.WITH_TONE_NUMBER, HanyuPinyinVCharType.WITH_V, HanyuPinyinCaseType.UPPERCASE, Result = new string[] { "LOU2", "LV3" })]
        [TestCase('偻', HanyuPinyinToneType.WITH_TONE_NUMBER, HanyuPinyinVCharType.WITH_U_UNICODE, HanyuPinyinCaseType.LOWERCASE, Result = new string[] { "lou2", "lü3" })]
        [TestCase('偻', HanyuPinyinToneType.WITH_TONE_NUMBER, HanyuPinyinVCharType.WITH_U_UNICODE, HanyuPinyinCaseType.UPPERCASE, Result = new string[] { "LOU2", "LÜ3" })]

        [TestCase('偻', HanyuPinyinToneType.WITHOUT_TONE, HanyuPinyinVCharType.WITH_U_AND_COLON, HanyuPinyinCaseType.LOWERCASE, Result = new string[] { "lou", "lu:" })]
        [TestCase('偻', HanyuPinyinToneType.WITHOUT_TONE, HanyuPinyinVCharType.WITH_U_AND_COLON, HanyuPinyinCaseType.UPPERCASE, Result = new string[] { "LOU", "LU:" })]
        [TestCase('偻', HanyuPinyinToneType.WITHOUT_TONE, HanyuPinyinVCharType.WITH_V, HanyuPinyinCaseType.LOWERCASE, Result = new string[] { "lou", "lv" })]
        [TestCase('偻', HanyuPinyinToneType.WITHOUT_TONE, HanyuPinyinVCharType.WITH_V, HanyuPinyinCaseType.UPPERCASE, Result = new string[] { "LOU", "LV" })]
        [TestCase('偻', HanyuPinyinToneType.WITHOUT_TONE, HanyuPinyinVCharType.WITH_U_UNICODE, HanyuPinyinCaseType.LOWERCASE, Result = new string[] { "lou", "lü" })]
        [TestCase('偻', HanyuPinyinToneType.WITHOUT_TONE, HanyuPinyinVCharType.WITH_U_UNICODE, HanyuPinyinCaseType.UPPERCASE, Result = new string[] { "LOU", "LÜ" })]

        [TestCase('偻', HanyuPinyinToneType.WITH_TONE_MARK, HanyuPinyinVCharType.WITH_U_UNICODE, HanyuPinyinCaseType.LOWERCASE, Result = new string[] { "lóu", "lǚ" })]
        [TestCase('偻', HanyuPinyinToneType.WITH_TONE_MARK, HanyuPinyinVCharType.WITH_U_UNICODE, HanyuPinyinCaseType.UPPERCASE, Result = new string[] { "LÓU", "LǙ" })]
        //  Traditional Chinese
        [TestCase('僂', HanyuPinyinToneType.WITH_TONE_NUMBER, HanyuPinyinVCharType.WITH_U_AND_COLON, HanyuPinyinCaseType.LOWERCASE, Result = new string[] { "lou2", "lu:3" })]
        [TestCase('僂', HanyuPinyinToneType.WITH_TONE_NUMBER, HanyuPinyinVCharType.WITH_U_AND_COLON, HanyuPinyinCaseType.UPPERCASE, Result = new string[] { "LOU2", "LU:3" })]
        [TestCase('僂', HanyuPinyinToneType.WITH_TONE_NUMBER, HanyuPinyinVCharType.WITH_V, HanyuPinyinCaseType.LOWERCASE, Result = new string[] { "lou2", "lv3" })]
        [TestCase('僂', HanyuPinyinToneType.WITH_TONE_NUMBER, HanyuPinyinVCharType.WITH_V, HanyuPinyinCaseType.UPPERCASE, Result = new string[] { "LOU2", "LV3" })]
        [TestCase('僂', HanyuPinyinToneType.WITH_TONE_NUMBER, HanyuPinyinVCharType.WITH_U_UNICODE, HanyuPinyinCaseType.LOWERCASE, Result = new string[] { "lou2", "lü3" })]
        [TestCase('僂', HanyuPinyinToneType.WITH_TONE_NUMBER, HanyuPinyinVCharType.WITH_U_UNICODE, HanyuPinyinCaseType.UPPERCASE, Result = new string[] { "LOU2", "LÜ3" })]

        [TestCase('僂', HanyuPinyinToneType.WITHOUT_TONE, HanyuPinyinVCharType.WITH_U_AND_COLON, HanyuPinyinCaseType.LOWERCASE, Result = new string[] { "lou", "lu:" })]
        [TestCase('僂', HanyuPinyinToneType.WITHOUT_TONE, HanyuPinyinVCharType.WITH_U_AND_COLON, HanyuPinyinCaseType.UPPERCASE, Result = new string[] { "LOU", "LU:" })]
        [TestCase('僂', HanyuPinyinToneType.WITHOUT_TONE, HanyuPinyinVCharType.WITH_V, HanyuPinyinCaseType.LOWERCASE, Result = new string[] { "lou", "lv" })]
        [TestCase('僂', HanyuPinyinToneType.WITHOUT_TONE, HanyuPinyinVCharType.WITH_V, HanyuPinyinCaseType.UPPERCASE, Result = new string[] { "LOU", "LV" })]
        [TestCase('僂', HanyuPinyinToneType.WITHOUT_TONE, HanyuPinyinVCharType.WITH_U_UNICODE, HanyuPinyinCaseType.LOWERCASE, Result = new string[] { "lou", "lü" })]
        [TestCase('僂', HanyuPinyinToneType.WITHOUT_TONE, HanyuPinyinVCharType.WITH_U_UNICODE, HanyuPinyinCaseType.UPPERCASE, Result = new string[] { "LOU", "LÜ" })]

        [TestCase('僂', HanyuPinyinToneType.WITH_TONE_MARK, HanyuPinyinVCharType.WITH_U_UNICODE, HanyuPinyinCaseType.LOWERCASE, Result = new string[] { "lóu", "lǚ" })]
        [TestCase('僂', HanyuPinyinToneType.WITH_TONE_MARK, HanyuPinyinVCharType.WITH_U_UNICODE, HanyuPinyinCaseType.UPPERCASE, Result = new string[] { "LÓU", "LǙ" })]
        #endregion
        public string[] TestCharWithMultiplePronouciations(
            char ch, HanyuPinyinToneType toneType,
            HanyuPinyinVCharType vcharType, HanyuPinyinCaseType caseType)
        {
            HanyuPinyinOutputFormat format = new HanyuPinyinOutputFormat();
            format.ToneType = toneType;
            format.VCharType = vcharType;
            format.CaseType = caseType;
            return PinyinHelper.ToHanyuPinyinStringArray(ch, format);
        }
        
        #endregion
    }
}
