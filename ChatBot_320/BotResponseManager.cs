using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Windows;

namespace ChatBot_320
{
    public static class BotResponseManager
    {
        public static Dictionary<string, string> responses;

        static BotResponseManager()
        {
            LoadResponses();
        }

        public static string GetResponse(string userInput)
        {
            if (responses == null)
            {
                LoadResponses();
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

        public static void LoadResponses()
        {
            var path = "botResponses.json";
            if (!File.Exists(path))
            {
                MessageBox.Show("Datei nicht gefunden. Bitte stellen Sie sicher, dass botResponses.json im aktuellen Verzeichnis liegt.");
                return;
            }

            var json = File.ReadAllText(path);
            var items = JsonConvert.DeserializeObject<List<KeywordResponse>>(json);

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

        public class KeywordResponse
        {
            public string Keyword { get; set; }
            public string Response { get; set; }
        }
    }
}
