using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyRaidFightSystem.Model
{
  
        public static class SaveAndReadPlayer
        {
            public static object Read(Type dataType, string filePath)
            {
                JObject obj = null;
                JsonSerializer jsonSerializer = new JsonSerializer();
                if (File.Exists(filePath + "player.json"))
                {
                    StreamReader sr = new StreamReader(filePath + "player.json");
                    JsonReader jsonReader = new JsonTextReader(sr);
                    obj = jsonSerializer.Deserialize(jsonReader) as JObject;
                    jsonReader.Close();
                    sr.Close();
                }
                return obj.ToObject(dataType);
            }
            public static void Save(object data, string filePath)
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                if (File.Exists(filePath + "player.json")) File.Delete(filePath + "player.json");
                StreamWriter sw = new StreamWriter(filePath + "player.json");
                JsonWriter jsonWriter = new JsonTextWriter(sw);

                jsonSerializer.Serialize(jsonWriter, data);

                jsonWriter.Close();
                sw.Close();

            }


        
    }
}
