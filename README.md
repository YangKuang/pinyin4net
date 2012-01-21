pinyin4net
=============================================

Pinyin4net can help you convert 中文 to zhōng wén.

Getting started
---------------------------------------------

		using Pinyin4net;  

		...  
		string[] pinyinStr = PinyinHelper.ToHanyuPinyinStringArray(someChineseChar);  
		...  


Features
---------------------------------------------

* Convert Chinese to Hanyu Pinyin system.
* Support both Simplified Chinese and Tranditional Chinese.
* Multiple options for output format  
		All uppercase or lowercase  
		Can out put Unicode ü or v or u:  
		With tone numbers (lü3) or tone marks (lǚ) or without tone (lü)  

Acknowledgments 
---------------------------------------------

Most code of pinyin4net is based on the code of project [pinyin4j](http://pinyin4j.sourceforge.net/).   
Thanks [Li Min](http://www.eng.nus.edu.sg/LCEL/people/limin/) for the great code ^_^.

Bug tracker
---------------------------------------------

Have a bug? Please create an issue here on GitHub!  
[https://github.com/YangKuang/pinyin4net/issues](https://github.com/YangKuang/pinyin4net/issues)

Authors
---------------------------------------------

*Yang Kuang(yangkuang2001@126.com)*

License
---------------------------------------------

Copyright 2012 Yang Kuang.  
Licensed under the Apache License, Version 2.0: http://www.apache.org/licenses/LICENSE-2.0 

