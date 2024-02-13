using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planner
{
    internal class SerDer
    {
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Заметки";
        public static void SerData<T>(List<T> data, string fileName)
        {
            string fullpath = path + "\\" + fileName;
            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(fullpath, json);
        }
        public static List<T> DeserData<T>(string fileName)
        {
            string fullpath = path + "\\" + fileName;
            if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }

            if (!File.Exists(fullpath))
            {

                string jsonContent = "[]";
                File.WriteAllText(fullpath, jsonContent);
            }
            string json = File.ReadAllText(fullpath);
            List<T> data = JsonConvert.DeserializeObject<List<T>>(json);
            return data;
        }
    }
}
