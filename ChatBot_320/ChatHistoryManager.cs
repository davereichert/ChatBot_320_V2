using ChatBot_320;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

public static class ChatHistoryManager
{
    private const string HistoryFilePath = "chatHistory.json";

    public static void SaveChatHistory(IEnumerable<ChatMessage> messages)
    {
        var json = JsonConvert.SerializeObject(messages);
        File.WriteAllText(HistoryFilePath, json);
    }

    public static List<ChatMessage> LoadChatHistory()
    {
        if (!File.Exists(HistoryFilePath))
            return new List<ChatMessage>();

        var json = File.ReadAllText(HistoryFilePath);
        return JsonConvert.DeserializeObject<List<ChatMessage>>(json);
    }

    public static void ClearChatHistory()
    {
        if (File.Exists(HistoryFilePath))
            File.Delete(HistoryFilePath);
    }
}
