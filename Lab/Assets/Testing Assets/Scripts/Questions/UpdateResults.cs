using System;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UpdateResults : MonoBehaviour
{
    private string reportDirectoryName = "Report";
    private string reportFileName = "report.csv";

    [SerializeField]
    GameObject Prefab;

    [SerializeField]
    GameObject Content;

    private void Start() => 
        SetResults();

    public void SetResults()
    {
        string dir = GetDirectoryPath();
        if (Directory.Exists(dir))
        {
            foreach (Transform child in Content.transform)
            {
                Destroy(child.gameObject);
            }

            string[] dataInFile = File.ReadAllLines(GetFilePath(), Encoding.UTF8);
            
            //заголовки
            SetTitles(dataInFile);
            
            for (int i = dataInFile.Length - 1; i > 0; i--)
            {
                var temp = dataInFile[i].Split(';');
                var prefab = Instantiate(Prefab.gameObject, Content.transform);

                var i1 = 0;
                foreach (Transform child in prefab.transform)
                {
                    foreach (Transform ch in child.transform)
                        ch.gameObject.GetComponent<Text>().text = temp[i1];

                    i1++;
                }
            }
        }
    }

    private void SetTitles(string[] dataInFile)
    {
        var temp = dataInFile[0].Split(';');
        var prefab = Instantiate(Prefab.gameObject, Content.transform);

        var i1 = 0;
        foreach (Transform child in prefab.transform)
        {
            foreach (Transform ch in child.transform)
                ch.gameObject.GetComponent<Text>().text = temp[i1];

            i1++;
        }
    }

    private string GetDirectoryPath()
    {
        return Application.dataPath + "/" + reportDirectoryName;
    }
    private string GetFilePath()
    {
        return GetDirectoryPath() + "/" + reportFileName;
    }
}
