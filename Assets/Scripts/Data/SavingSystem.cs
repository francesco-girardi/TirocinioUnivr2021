using System.IO;
using UnityEngine;

namespace Data {

    public static class SavingSystem {

        /// <summary>
        /// Save player datas into a JSON file
        /// </summary>
        /// <param name="playerDatas"></param>
        /// <param name="path"></param>
        public static void PlayerToJSON(PlayerDatas playerDatas, string path) {
            string datas = JsonUtility.ToJson(playerDatas);

            FileStream fileStream = new FileStream(path, FileMode.Create);

            StreamWriter streamWriter = new StreamWriter(fileStream);
            streamWriter.WriteLine(datas);
            streamWriter.Close();

            fileStream.Close();
        }

        /// <summary>
        /// Return an object PlayerDatas that conteins all player info
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static PlayerDatas PlayerFromJSON(string path) {

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader streamReader = new StreamReader(fileStream);
            string data = streamReader.ReadToEnd();
            streamReader.Close();

            fileStream.Close();

            return JsonUtility.FromJson<PlayerDatas>(data);
        }

    }

}