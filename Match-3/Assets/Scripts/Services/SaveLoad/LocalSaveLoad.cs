using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Match3.Assets.Scripts.Services.SaveLoad {
    public class LocalSaveLoad<T> where T : class {
        public static void Save(T data) {
            string serializedData = JsonConvert.SerializeObject(data);

            BinaryFormatter bf = new BinaryFormatter();
            string fileName = GetFileNameByType();
            FileStream file = File.Create(Application.persistentDataPath
              + fileName);
            bf.Serialize(file, serializedData);
            file.Close();
            Debug.Log($"{typeof(T).FullName} saved!");
        }

        public static T Load() {
            string fileName = GetFileNameByType();

            if (File.Exists(Application.persistentDataPath
              + fileName)) {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file =
                  File.Open(Application.persistentDataPath
                  + fileName, FileMode.Open);
                string data = (string)bf.Deserialize(file);
                file.Close();

                Debug.Log("Game data loaded!");

                T result = JsonConvert.DeserializeObject<T>(data);
                return result;
            }
            else {
                Debug.LogError($"{fileName} is no save data!");
                return null;
            }
        }

        private static string GetFileNameByType() {
            string[] typeNameParts = typeof(T).FullName.Split('[', ']');
            string typeName = typeNameParts[(typeNameParts.Length - 1) / 2].Split(',')[0];
            string result = $"/{typeName}.dat";
            return result;
        }
    }
}
