using System;
using SmartAIChatBot;
using SmartAIChatBot.Core;
using Learner = SmartAIChatBot.Core.Learner;

class Program
{
    static void Main()
    {
        Console.WriteLine("🤖 Smart Self-Learning AI - Type 'exit' to quit.");

        var learner = new Learner();

        while (true)
        {
            Console.Write("\nYou: ");
            string userInput = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(userInput)) continue;
            if (userInput.ToLower() == "exit") break;

            if (learner.TryHandleFacts(userInput, out string factResponse))
            {
                Console.WriteLine("AI: " + factResponse);
                continue;
            }

            string response = learner.GetResponse(userInput);

            if (string.IsNullOrEmpty(response))
            {
                Console.Write("AI 🤖: I don't know that. Can you teach me? \nYour reply: ");
                string newResponse = Console.ReadLine()?.Trim();

                if (!string.IsNullOrEmpty(newResponse))
                {
                    learner.Learn(userInput, newResponse);
                    Console.WriteLine("AI: Got it! I’ll remember that. 🧠");
                }
            }
            else
            {
                Console.WriteLine("AI: " + response);
            }
        }

        Console.WriteLine("👋 AI: Goodbye!");
    }
}
