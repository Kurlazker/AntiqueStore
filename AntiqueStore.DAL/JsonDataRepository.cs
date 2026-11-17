using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using AntiqueStore.Models;

namespace AntiqueStore.DAL
{
    public class JsonDataRepository : IDataRepository
    {
        private readonly string _filePath = "antiques.json";

        public List<Antique> LoadAll()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Antique>();
            }

            string json = File.ReadAllText(_filePath);

            // TypeNameHandling.Auto автоматически сохраняет и восстанавливает тип объекта
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };

            return JsonConvert.DeserializeObject<List<Antique>>(json, settings)
                   ?? new List<Antique>();
        }

        public void SaveAll(List<Antique> items)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(items, settings);
            File.WriteAllText(_filePath, json);
        }
    }
}