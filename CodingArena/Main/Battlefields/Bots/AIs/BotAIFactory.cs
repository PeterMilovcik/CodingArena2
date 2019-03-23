using CodingArena.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CodingArena.Main.Battlefields.Bots.AIs
{
    public class BotAIFactory
    {
        public List<IBotAI> CreateBotAIs()
        {
            var result = new List<IBotAI>();
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var dir = Path.Combine(baseDirectory, "Bots");
            if (!Directory.Exists(dir)) throw new DirectoryNotFoundException($"{dir} not found.");
            var files = Directory.GetFiles(dir, "*.dll");
            foreach (var file in files)
            {
                try
                {
                    var assembly = Assembly.Load(File.ReadAllBytes(file));
                    var aiType = FindAIType(assembly);

                    if (aiType != null &&
                        Activator.CreateInstance(aiType) is IBotAI botAI)
                    {
                        result.Add(botAI);
                    }
                }
                catch
                {
                    // ignored
                }
            }
            return result;
        }

        private Type FindAIType(Assembly assembly)
        {
            try
            {
                return assembly.ExportedTypes.FirstOrDefault(IsBotAIType);
            }
            catch (TypeLoadException)
            {
                return null;
            }
        }

        private bool IsBotAIType(Type t) => typeof(IBotAI).IsAssignableFrom(t) && t.IsClass;
    }
}