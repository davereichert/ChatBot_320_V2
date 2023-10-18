using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace ChatBot_320
{
    public class JsonResponseLoader : IResponseLoader
    {
        public List<KeywordResponse> LoadResponses(string path)
        {
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<KeywordResponse>>(json);
        }
    }
}
