using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class GetJson : MonoBehaviour
{
    [SerializeField] private string _nameFile;
    public List<Player> Items = new List<Player>();

    public void LoadItem(string path)
    {
        
        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path, Encoding.UTF8);
            Player[] myItem = JsonHelper.FromJson<Player>(jsonString);
            Items.Clear();

            foreach (Player item in myItem)
            {
                Items.Add(item);
            }
        }
        
    }

    private string getPath(string name, string lang)
    {
#if UNITY_EDITOR
        return Application.dataPath + "/StreamingAssets/Language/" + name + "_" + lang + ".json";
#elif UNITY_ANDROID
		return Application.persistentDataPath+"/StreamingAssets/Language/" + name + "_" + lang + ".json";
#elif UNITY_IPHONE
		return Application.persistentDataPath+"/StreamingAssets/Language/" + name + "_" + lang + ".json"";
#else
        return Application.dataPath + "/StreamingAssets/Language/" + name + "_" + lang + ".json";
#endif
    }

}

[Serializable]
public class Player
{
    public string key;
    public string value;
}

