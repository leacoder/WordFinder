using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WordFinder;

namespace TestWordFinder
{
    [TestClass]
    public class GeneralTestCases
    {
        [TestMethod]
        public void TestExampleMatrix5x5()
        {
            var dictionary = new string[] { "chill", "wind", "snow", "cold" };
            var src = new string[] { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" };

            var result = new WordFinderWorker(dictionary).Find(src) as List<string>;

            //Define valid expected result
            var expected = new List<string>() { "chill", "wind", "cold" };

            CollectionAssert.AreEquivalent(expected.ToArray(), result.ToArray());
        }

        [TestMethod]
        public void TestMatrix20x20()
        {
            var dictionary = new string[] { "chilipeppers", "windsurf", "snowboard", "coldy" };
            var src = new string[] { "wsdftyubstxacbmjutew", "icbmjutewwavsaaasdss", "ntewsdvstaafyusdsdcc", "dddtcbsdsdavsacbcbts", "sbbmtecbcbafyusatewy", "ueedavsachilipeppers", "rdcbafyuwwavstvsbted", "favsaavsbteccbdceseb", "tafyusdcestttebtewsc", "mtecdcbtewwaddtcbsdt", "davsbtebstaabbmtecba", "cbmjuteestteeedavsaa", "tewsdvsewwasdcbafwwb", "ddtcbsdcbmjuavsaabte", "bbmtecbtewsdvsfyuese", "eedavsaddtcbsdteweww", "dcbafyubbmtecbvstvta", "avswwavsaaasdsssddww", "wwavsaaasdsssdccbbta", "snowboarddcccbssatsd" };

            var result = new WordFinderWorker(dictionary).Find(src) as List<string>;

            //Define valid expected result
            var expected = new List<string>() { "chilipeppers", "windsurf", "snowboard" };

            CollectionAssert.AreEquivalent(expected.ToArray(), result.ToArray());
        }

        [TestMethod]
        public void TestMatrix20x20NoWordFound()
        {
            var dictionary = new string[] { "chilipeppers", "windsurf", "snowboard", "coldy" };
            var src = new string[] { "wsdftyubstxacbmjutew", "wsdftyubstxacbmjutew", "wsdftyubstxacbmjutew", "wsdftyubstxacbmjutew", "wsdftyubstxacbmjutew", "wsdftyubstxacbmjutew", "wsdftyubstxacbmjutew", "wwavsaaasdsssdccbbta", "wwavsaaasdsssdccbbta", "wwavsaaasdsssdccbbta", "wsdftyubstxacbmjutew", "wwavsaaasdsssdccbbta", "wsdftyubstxacbmjutew", "wsdftyubstxacbmjutew", "wsdftyubstxacbmjutew", "wwavsaaasdsssdccbbta", "wwavsaaasdsssdccbbta", "wwavsaaasdsssdccbbta", "wwavsaaasdsssdccbbta", "wwavsaaasdsssdccbbta" };

            var result = new WordFinderWorker(dictionary).Find(src) as List<string>;

            //Define valid expected result
            var expected = new List<string>(){};

            CollectionAssert.AreEquivalent(expected.ToArray(), result.ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Dictionary can not exceed 2048 words.")]
        public void TestMatrix20x20LargeDictionary()
        {
            var src = new string[] { "wsdftyubstxacbmjutew", "icbmjutewwavsaaasdss", "ntewsdvstaafyusdsdcc", "dddtcbsdsdavsacbcbts", "sbbmtecbcbafyusatewy", "ueedavsachilipeppers", "rdcbafyuwwavstvsbted", "favsaavsbteccbdceseb", "tafyusdcestttebtewsc", "mtecdcbtewwaddtcbsdt", "davsbtebstaabbmtecba", "cbmjuteestteeedavsaa", "tewsdvsewwasdcbafwwb", "ddtcbsdcbmjuavsaabte", "bbmtecbtewsdvsfyuese", "eedavsaddtcbsdteweww", "dcbafyubbmtecbvstvta", "avswwavsaaasdsssddww", "wwavsaaasdsssdccbbta", "snowboarddcccbssatsd" };

            string[] dictionaryArray = JsonArrayConvertion("../../../data/longDictionary.json", "dictionary");

            var result = new WordFinderWorker(dictionaryArray).Find(src) as List<string>;
        }

        [TestMethod]
        public void TestMatrix20x20LargeValidDictionary()
        {
            var src = new string[] { "wsdftyubstxacbmjutew", "icbmjutewwavsaaasdss", "ntewsdvstaafyusdsdcc", "dddtcbsdsdavsacbcbts", "sbbmtecbcbafyusatewy", "ueedavsachilipeppers", "rdcbafyuwwavstvsbted", "favsaavsbteccbdceseb", "tafyusdcestttebtewsc", "mtecdcbtewwaddtcbsdt", "davsbtebstaabbmtecba", "cbmjuteestteeedavsaa", "tewsdvsewwasdcbafwwb", "ddtcbsdcbmjuavsaabte", "bbmtecbtewsdvsfyuese", "eedavsaddtcbsdteweww", "dcbafyubbmtecbvstvta", "avswwavsaaasdsssddww", "wwavsaaasdsssdccbbta", "snowboarddcccbssatsd" };

            string[] dictionaryArray = JsonArrayConvertion("../../../data/longValidDictionary.json", "dictionary");

            var result = new WordFinderWorker(dictionaryArray).Find(src) as List<string>;

            //Define valid expected result
            var expected = new List<string>() { "chilipeppers", "windsurf", "snowboard" };

            CollectionAssert.AreEquivalent(expected.ToArray(), result.ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException),"Matrix should have less than 64 items")]
        public void TestMatrix100x100()
        {
            var dictionary = new string[] { "chilipeppers", "windsurf", "snowboard", "coldy" };

            var srcJson = File.ReadAllText("../../../data/Matrix100x100.json");
            JObject srcObj = JObject.Parse(srcJson);

            JArray srcJarray = (JArray)srcObj["src"];
            string[] srcArray = srcJarray.Select(x => (string)x).ToArray();

            var result = new WordFinderWorker(dictionary).Find(srcArray) as List<string>;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Matrix should have less than 64 items")]
        public void TestMatrix1000x1000()
        {
            var dictionary = new string[] { "chilipeppers", "windsurf", "snowboard", "coldy" };

            string[] srcArray = JsonArrayConvertion("../../../data/Matrix2050x2050.json", "src");

            var result = new WordFinderWorker(dictionary).Find(srcArray) as List<string>;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Matrix should have less than 64 items")]
        public void TestMatrix2050x2050()
        {
            var dictionary = new string[] { "chilipeppers", "windsurf", "snowboard", "coldy" };

            string[] srcArray = JsonArrayConvertion("../../../data/Matrix2050x2050.json", "src");

            var result = new WordFinderWorker(dictionary).Find(srcArray) as List<string>;
        }

        private static string[] JsonArrayConvertion(string pathToJson, string property)
        {
            var dictionaryJson = File.ReadAllText(pathToJson);

            JObject dictionaryObj = JObject.Parse(dictionaryJson);
            JArray dictionaryJarray = (JArray)dictionaryObj[property];
            string[] dictionaryArray = dictionaryJarray.Select(x => (string)x).ToArray();
            return dictionaryArray;
        }
    }
}
