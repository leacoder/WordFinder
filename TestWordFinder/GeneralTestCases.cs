using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WordFinder;
using WordFinder.FinderStrategy;

namespace TestWordFinder
{
    [TestClass]
    public class GeneralTestCases
    {
        [TestMethod]
        public void TestExampleMatrix5x5()
        {
            var wordStream = new string[] { "chill", "wind", "snow", "cold" };
            var matrix = new string[] { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" };
            var wordFinder = new WordFinder.WordFinder(matrix)
            {
                FinderBehaviour = new HorizontalVerticalFinder()
            };
            var result = wordFinder.Find(wordStream) as List<string>;
            
            //Define valid expected result
            var expected = new List<string>() { "chill", "wind", "cold" };

            CollectionAssert.AreEquivalent(expected, result);
        }

        [TestMethod]
        public void TestMatrix20x20()
        {
            var wordStream = new string[] { "chilipeppers", "windsurf", "snowboard", "coldy" };
            var matrix = new string[] { "wsdftyubstxacbmjutew", "icbmjutewwavsaaasdss", "ntewsdvstaafyusdsdcc", "dddtcbsdsdavsacbcbts", "sbbmtecbcbafyusatewy", "ueedavsachilipeppers", "rdcbafyuwwavstvsbted", "favsaavsbteccbdceseb", "tafyusdcestttebtewsc", "mtecdcbtewwaddtcbsdt", "davsbtebstaabbmtecba", "cbmjuteestteeedavsaa", "tewsdvsewwasdcbafwwb", "ddtcbsdcbmjuavsaabte", "bbmtecbtewsdvsfyuese", "eedavsaddtcbsdteweww", "dcbafyubbmtecbvstvta", "avswwavsaaasdsssddww", "wwavsaaasdsssdccbbta", "snowboarddcccbssatsd" };
            var wordFinder = new WordFinder.WordFinder(matrix)
            {
                FinderBehaviour = new HorizontalVerticalFinder()
            };
            var result = wordFinder.Find(wordStream) as List<string>;

            //Define valid expected result
            var expected = new List<string>() { "chilipeppers", "windsurf", "snowboard" };

            CollectionAssert.AreEquivalent(expected, result);
        }

        [TestMethod]
        public void TestMatrix20x20NoWordFound()
        {   
            var wordStream = new string[] { "chilipeppers", "windsurf", "snowboard", "coldy" };
            var matrix = new string[] { "wsdftyubstxacbmjutew", "wsdftyubstxacbmjutew", "wsdftyubstxacbmjutew", "wsdftyubstxacbmjutew", "wsdftyubstxacbmjutew", "wsdftyubstxacbmjutew", "wsdftyubstxacbmjutew", "wwavsaaasdsssdccbbta", "wwavsaaasdsssdccbbta", "wwavsaaasdsssdccbbta", "wsdftyubstxacbmjutew", "wwavsaaasdsssdccbbta", "wsdftyubstxacbmjutew", "wsdftyubstxacbmjutew", "wsdftyubstxacbmjutew", "wwavsaaasdsssdccbbta", "wwavsaaasdsssdccbbta", "wwavsaaasdsssdccbbta", "wwavsaaasdsssdccbbta", "wwavsaaasdsssdccbbta" };

            var wordFinder = new WordFinder.WordFinder(matrix)
            {
                FinderBehaviour = new HorizontalVerticalFinder()
            };

            var result = wordFinder.Find(wordStream);

            //Define valid expected result
            var expected = new List<string>();

            Assert.IsTrue(result.SequenceEqual(expected));
            //CollectionAssert.AreEquivalent(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Matrix can not exceed 64 values")]
        public void TestMatrix20x20LargewordStream()
        {
            var matrix = new string[] { "wsdftyubstxacbmjutew", "icbmjutewwavsaaasdss", "ntewsdvstaafyusdsdcc", "dddtcbsdsdavsacbcbts", "sbbmtecbcbafyusatewy", "ueedavsachilipeppers", "rdcbafyuwwavstvsbted", "favsaavsbteccbdceseb", "tafyusdcestttebtewsc", "mtecdcbtewwaddtcbsdt", "davsbtebstaabbmtecba", "cbmjuteestteeedavsaa", "tewsdvsewwasdcbafwwb", "ddtcbsdcbmjuavsaabte", "bbmtecbtewsdvsfyuese", "eedavsaddtcbsdteweww", "dcbafyubbmtecbvstvta", "avswwavsaaasdsssddww", "wwavsaaasdsssdccbbta", "snowboarddcccbssatsd" };

            string[] wordStream = JsonArrayConvertion("../../../data/longDictionary.json", "dictionary");

            var wordFinder = new WordFinder.WordFinder(matrix)
            {
                FinderBehaviour = new HorizontalVerticalFinder()
            };
            wordFinder.Find(wordStream);
        }

        [TestMethod]
        public void TestMatrix20x20LargeValidwordStream()
        {
            var matrix = new string[] { "wsdftyubstxacbmjutew", "icbmjutewwavsaaasdss", "ntewsdvstaafyusdsdcc", "dddtcbsdsdavsacbcbts", "sbbmtecbcbafyusatewy", "ueedavsachilipeppers", "rdcbafyuwwavstvsbted", "favsaavsbteccbdceseb", "tafyusdcestttebtewsc", "mtecdcbtewwaddtcbsdt", "davsbtebstaabbmtecba", "cbmjuteestteeedavsaa", "tewsdvsewwasdcbafwwb", "ddtcbsdcbmjuavsaabte", "bbmtecbtewsdvsfyuese", "eedavsaddtcbsdteweww", "dcbafyubbmtecbvstvta", "avswwavsaaasdsssddww", "wwavsaaasdsssdccbbta", "snowboarddcccbssatsd" };

            string[] wordStreamArray = JsonArrayConvertion("../../../data/longValidDictionary.json", "dictionary");

            var wordFinder = new WordFinder.WordFinder(matrix)
            {
                FinderBehaviour = new HorizontalVerticalFinder()
            };
            var result = wordFinder.Find(wordStreamArray) as List<string>;

            //Define valid expected result
            var expected = new List<string>() { "chilipeppers", "windsurf", "snowboard" };

            CollectionAssert.AreEquivalent(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException),"Matrix should have less than 64 items")]
        public void TestMatrix100x100()
        {
            var wordStream = new string[] { "chilipeppers", "windsurf", "snowboard", "coldy" };

            var matrixJson = File.ReadAllText("../../../data/Matrix100x100.json");
            JObject matrixObj = JObject.Parse(matrixJson);

            JArray matrixJarray = (JArray)matrixObj["src"];
            string[] matrixArray = matrixJarray.Select(x => (string)x).ToArray();

            var result = new WordFinder.WordFinder(matrixArray).Find(wordStream) as List<string>;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Matrix should have less than 64 items")]
        public void TestMatrix1000x1000()
        {
            var wordStream = new string[] { "chilipeppers", "windsurf", "snowboard", "coldy" };

            string[] matrixArray = JsonArrayConvertion("../../../data/Matrix2050x2050.json", "src");

            var result = new WordFinder.WordFinder(matrixArray).Find(wordStream) as List<string>;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Matrix should have less than 64 items")]
        public void TestMatrix2050x2050()
        {
            var wordStream = new string[] { "chilipeppers", "windsurf", "snowboard", "coldy" };

            string[] matrixArray = JsonArrayConvertion("../../../data/Matrix2050x2050.json", "src");

            var result = new WordFinder.WordFinder(matrixArray).Find(wordStream) as List<string>;
        }

        private static string[] JsonArrayConvertion(string pathToJson, string property)
        {
            var wordStreamJson = File.ReadAllText(pathToJson);

            JObject wordStreamObj = JObject.Parse(wordStreamJson);
            JArray wordStreamJarray = (JArray)wordStreamObj[property];
            string[] wordStreamArray = wordStreamJarray.Select(x => (string)x).ToArray();
            return wordStreamArray;
        }
    }
}
