using System;

namespace Pinyin4net
{
    public static class TextHelper
    {
        /**
       * @param hanyuPinyinWithToneNumber
       * @return Hanyu Pinyin string without tone number
       */
        public static String ExtractToneNumber(String hanyuPinyinWithToneNumber)
        {
            return hanyuPinyinWithToneNumber.Substring(hanyuPinyinWithToneNumber.Length - 1);
        }

        /**
       * @param hanyuPinyinWithToneNumber
       * @return only tone number
       */
        public static String ExtractPinyinString(String hanyuPinyinWithToneNumber)
        {
            return hanyuPinyinWithToneNumber.Substring(0, hanyuPinyinWithToneNumber.Length - 1);
        }
    }
}