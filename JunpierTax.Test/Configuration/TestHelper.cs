using Newtonsoft.Json;
using System;

namespace JunpierTax.Test.Configuration
{
    public static class TestHelper
    {
        public static string ToJson(object obj)
        {
            try
            {
                var serializerSettings = new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.None,
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                };

                var json = JsonConvert.SerializeObject(obj, Formatting.Indented, serializerSettings);
                json = json.Replace(@"\\", @"\");
                json = json.Replace(@"\\", @"\");
                return json;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
