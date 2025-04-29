using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace SmartAIChatBot.Core
{
    public static class MemoryManager
    {
        private static readonly string memoryFile = "chat_memory.json";

        public static Dictionary<string, string> LoadChat()
        {
            if (!File.Exists(memoryFile)) return new Dictionary<string, string>();

            var all = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(File.ReadAllText(memoryFile));
            return all.ContainsKey("chat") ? all["chat"] : new Dictionary<string, string>();
        }

        public static Dictionary<string, string> LoadFacts()
        {
            if (!File.Exists(memoryFile)) return new Dictionary<string, string>();

            var all = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(File.ReadAllText(memoryFile));
            return all.ContainsKey("facts") ? all["facts"] : new Dictionary<string, string>();
        }

        public static void Save(Dictionary<string, string> chat, Dictionary<string, string> facts)
        {
            var data = new Dictionary<string, Dictionary<string, string>>
            {
                { "chat", chat },
                { "facts", facts }
            };

            File.WriteAllText(memoryFile, JsonConvert.SerializeObject(data, Formatting.Indented));
        }
    }
}
