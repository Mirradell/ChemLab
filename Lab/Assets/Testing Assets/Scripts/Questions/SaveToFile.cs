using UnityEngine;
using System.IO;
using System.Text;

public class SaveToFile : MonoBehaviour
{
    private bool coincidences = false;
    public int numQuestions = 14;
    //private string responseResult;

    private void Start()
    {
        //if (PlayerPrefs.HasKey("NumAttempts"))
        //{
        //    responseResult = PlayerPrefs.GetString("NumAttempts");
        //}
        //else
        //{
        //    responseResult = "false";
        //}
        //PlayerPrefs.SetString("GameStart", "False");
        //Save("Денис", 1, "true");
    }

    public void Save(string name, int scenario, string responseResult)
    {
        coincidences = false;
        string filePath = getPath();

        if (PlayerPrefs.GetString("GameStart") != "True")
        {
            string tittled = "Name";
            for (int i = 1; i <= numQuestions; i++)
            {
                tittled = tittled + ";" + i.ToString();
            }
            StreamWriter outStream3 = new StreamWriter(filePath, true, Encoding.UTF8);
            outStream3.WriteLine(tittled);
            outStream3.Close();
        }
        PlayerPrefs.SetString("GameStart", "True");

        string[] dataAsJson = File.ReadAllLines(getPath(), Encoding.UTF8);
        for (int i1 = 0; i1 < dataAsJson.Length; i1++)
        {

            bool result = dataAsJson[i1].StartsWith(name);
            if (result)
            {
                dataAsJson[i1] = ScenarioFilter(dataAsJson[i1], scenario, responseResult);
                coincidences = true;
            }


        }

        File.WriteAllText(filePath, string.Empty);
        StreamWriter outStream2 = new StreamWriter(filePath, true, Encoding.UTF8);
        for (int i2 = 0; i2 < dataAsJson.Length; i2++)
        {

            outStream2.WriteLine(dataAsJson[i2], Encoding.UTF8);

        }
        outStream2.Close();

        if (!coincidences)
        {
            string str = name;
            for (int i = 1; i <= numQuestions; i++)
            {
                if (i == scenario)
                    str = str + ";" + responseResult.ToString();
                else
                    str = str + ";-";
            }
            StreamWriter outStream4 = new StreamWriter(filePath, true, Encoding.UTF8);
            outStream4.WriteLine(str);
            outStream4.Close();
        }
    }

    private string ScenarioFilter(string str, int scenario, string responseResult)
    {
        string[] mass = str.Split(';');

        if ((mass[scenario].ToString() == " " + responseResult.ToString()) || (mass[scenario].ToString() == responseResult.ToString()))
        {
            return str;
        }
        else
        {
            mass[scenario] = responseResult.ToString();
            str = null;
            for (int i = 0; i < mass.Length; i++)
            {
                str = str + mass[i] + ";";
            }
            return str;
        }
    }

    private string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/StreamingAssets/" + "Saved_data.csv";
#elif UNITY_ANDROID
		return Application.persistentDataPath+"/StreamingAssets/Saved_data.csv";
#elif UNITY_IPHONE
		return Application.persistentDataPath+"/StreamingAssets/"+"Saved_data.csv";
#else
        return Application.dataPath + "/StreamingAssets/" + "Saved_data.csv";
#endif
    }

}