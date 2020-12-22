using System;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Pinyin4net
{
    public class PinyinRomanizationTranslator
    {
        public static String ConvertRomanizationSystem(
            String sourcePinyinStr,
            PinyinRomanizationType sourcePinyinSystem,
            PinyinRomanizationType targetPinyinSystem)
        {
            String pinyinString = TextHelper.ExtractPinyinString(sourcePinyinStr);
            String toneNumberStr = TextHelper.ExtractToneNumber(sourcePinyinStr);

            // return value
            String targetPinyinStr = null;
            // find the node of source Pinyin system
            String xpathQuery1 =
                "//" + sourcePinyinSystem.GetDescription<PinyinRomanizationType>() + "[text()='" +
                pinyinString + "']";


            var pinyinMappingDoc = XDocument.Load(
                Assembly.GetExecutingAssembly().GetManifestResourceStream(
                    "Pinyin4net.Resources.pinyin_mapping.xml"));
            var nav = pinyinMappingDoc.CreateNavigator();
            var hanyuNode = nav.SelectSingleNode(xpathQuery1);

            if (null != hanyuNode)
            {
                // find the node of target Pinyin system
                String xpathQuery2 = "../" + targetPinyinSystem.GetDescription<PinyinRomanizationType>() +
                                     "/text()";
                String targetPinyinStrWithoutToneNumber = hanyuNode.SelectSingleNode(xpathQuery2).Value;

                targetPinyinStr = targetPinyinStrWithoutToneNumber + toneNumberStr;
            }

            return targetPinyinStr;
        }
    }
}