using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SmartAIChatBot.Core
{
    public class Learner
    {
        private Dictionary<string, string> chatMemory;
        private Dictionary<string, string> facts;

        public Learner()
        {
            chatMemory = MemoryManager.LoadChat();
            facts = MemoryManager.LoadFacts();
        }

        public string GetResponse(string input)
        {
            input = input.ToLower().Trim();

            if (chatMemory.TryGetValue(input, out string value))
                return value;

            // Check if it's a "Who is X" question
            var match = Regex.Match(input, @"who is (\w+)", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                string key = match.Groups[1].Value.ToLower();
                if (chatMemory.TryGetValue(key, out string whoResp))
                    return whoResp;
            }

            return null;
        }

        public void Learn(string input, string response)
        {
            chatMemory[input.ToLower()] = response;
            MemoryManager.Save(chatMemory, facts);
        }

        public bool TryHandleFacts(string input, out string response)
        {
            response = null;

            var nameMatch = Regex.Match(input, @"(my name is|mera naam) (\w+)", RegexOptions.IgnoreCase);
            if (nameMatch.Success)
            {
                facts["name"] = nameMatch.Groups[2].Value;
                MemoryManager.Save(chatMemory, facts);
                response = $"Nice to meet you, {facts["name"]}!";
                return true;
            }

            if ((input.Contains("what is my name") || input.Contains("mera naam kya hai")) && facts.ContainsKey("name"))
            {
                response = $"Your name is {facts["name"]}.";
                return true;
            }

            return false;
        }
    }
}
