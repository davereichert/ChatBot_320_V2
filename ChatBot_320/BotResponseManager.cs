using System.Collections.Generic;
using System;
using System.IO;

namespace ChatBot_320
{

    public class BotResponseManager
    {
        private readonly IResponseLoader responseLoader;
        private readonly IMessageShower messageShower;
        private readonly string filePath;
        private Dictionary<string, string> responses;

        public BotResponseManager(IResponseLoader responseLoader, IMessageShower messageShower, string filePath)
        {
            this.responseLoader = responseLoader;
            this.messageShower = messageShower;
            this.filePath = filePath;
            LoadResponses();

        }

        public string GetResponse(string userInput)
        {
            if (responses == null)
            {
                return "Entschuldigung, ich habe das nicht verstanden.";
            }

            foreach (var keyword in responses.Keys)
            {
                if (userInput.Contains(keyword))
                {
                    return responses[keyword];
                }
            }

            return "Entschuldigung, ich habe das nicht verstanden.";
        }

        public void LoadResponses()
        {

            responses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (!File.Exists(filePath))
            {
                messageShower.show($"Datei nicht gefunden. Bitte stellen Sie sicher, dass {filePath} im aktuellen Verzeichnis liegt.");
                return;
            }

            var items = responseLoader.LoadResponses(filePath);

            responses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var item in items)
            {
                if (!responses.ContainsKey(item.Keyword))
                {
                    responses.Add(item.Keyword, item.Response);
                }
                else
                {
                    // Handle the duplicate key (e.g., log a warning or skip it)
                    Console.WriteLine($"Warning: Duplicate key '{item.Keyword}' found. Skipping.");
                }
            }
        }
    }
        public class KeywordResponse
        {
            public string Keyword { get; set; }
            public string Response { get; set; }
        }
 
    
}