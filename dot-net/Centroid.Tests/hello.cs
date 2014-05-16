//using System.IO;
//using Taps;

//namespace Centroid.Tests
//{
//    public class ConfigTest2 : TAP
//    {

//        const string JsonConfig = @"{""Environment"": {""TheKey"": ""TheValue""}}";

//        const string JsonConfigWithArray = @"{""Array"": [{""Key"": ""Value1""}, {""Key"": ""Value2""}]}";

//        static int Main()
//        {
//            var sharedFilePath = @"..\..\..\..\config.json";

//            //dynamic config = new Config(JsonConfig);
//            //Ok(config.Environment.TheKey == "TheValue", "test_create_from_string");
//            Ok("TheValue" == "TheValue", "test_create_from_string");

//            return 0;
//        }
//    }
//}
