using CommonLib;
using Newtonsoft.Json;
using ObjectInfo;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace CSE_WebEducation
{
    public class ApiClient_System
    {
        public static List<CSE_AllcodeInfo> Allcode_GetAll()
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/system/allcode-get-all", Method.GET);
                IRestResponse response = client.Execute(request);
                return JsonConvert.DeserializeObject<List<CSE_AllcodeInfo>>(response?.Content ?? "");
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
                return null;
            }
        }
    }

    public class JsonSerializer : ISerializer, IDeserializer
    {
        private readonly Newtonsoft.Json.JsonSerializer _serializer;

        public JsonSerializer()
        {
            ContentType = "application/json";
            _serializer = new Newtonsoft.Json.JsonSerializer
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Include,
                DefaultValueHandling = DefaultValueHandling.Include,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Unspecified
            };
        }

        public JsonSerializer(Newtonsoft.Json.JsonSerializer serializer)
        {
            ContentType = "application/json";
            _serializer = serializer;
        }

        public string Serialize(object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    jsonTextWriter.Formatting = Formatting.Indented;
                    jsonTextWriter.QuoteChar = '"';

                    _serializer.Serialize(jsonTextWriter, obj);

                    var result = stringWriter.ToString();
                    return result;
                }
            }
        }

        public string DateFormat { get; set; }
        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string ContentType { get; set; }

        public T Deserialize<T>(RestSharp.IRestResponse response)
        {
            var content = response.Content;

            using (var stringReader = new StringReader(content))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    return _serializer.Deserialize<T>(jsonTextReader);
                }
            }
        }
    }
}
