using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questions : MonoBehaviour
{
    [SerializeField] private GameObject[] QuestionsCanvas;
    [SerializeField] private Color rightColor; 
    [SerializeField] private Color wrongColor; 
    
    private int CountOfQuestions;
    private int i = 0;
    private int _countOfRightAnsw = 0;
    private List<int> selectedValues = new List<int>();

    private void Start()
    {
        CountOfQuestions = QuestionsCanvas.Length;
    }

    public void Answer(bool Flag)
    {
        var image = QuestionsCanvas[i].GetComponentInChildren<Image>();
        var tempColor = image.color;
        image.color = (Flag) ? rightColor : wrongColor;

        if (!Flag)
            QuestionsCanvas[i].GetComponent<RightAnswers>().SetRightAnswer();

        i++;

        var buttons = QuestionsCanvas[i - 1].GetComponentsInChildren<Button>();
        foreach (var but in buttons)
        {
            but.interactable = false;
        }
        StartCoroutine(Timer());

        IEnumerator Timer()
        {
            SaveToFile save = FindObjectOfType<SaveToFile>();
            if (Flag == true)
                _countOfRightAnsw++;
            yield return new WaitForSeconds(3);
            if (QuestionsCanvas[i - 1].GetComponent<Animator>()) {
                QuestionsCanvas[i - 1].GetComponent<Animator>().SetBool("out", true);
                if (save)
                    save.Save(PlayerPrefs.GetString("GameName"), i, Flag.ToString());
                yield return new WaitForSeconds(2);
            }
            QuestionsCanvas[i - 1].SetActive(false);
            if (i < CountOfQuestions)
            {
                image.color = tempColor;

                QuestionsCanvas[i - 1].SetActive(false);
                QuestionsCanvas[i].SetActive(true);

                var newbuttons = QuestionsCanvas[i].GetComponentsInChildren<Button>();
                foreach (var but in newbuttons)
                {
                    but.interactable = true;
                }

                QuestionsCanvas[i].GetComponent<Canvas>().enabled = true;
            }
            
            if(i == CountOfQuestions)
            {
                SaveResults();
            }
        }
    }
    
     public void SaveResults() => 
         CSVManager.AppendToReport(PlayerPrefs.GetString("GameName"), SetScoreString(), "Тестирование");

    public string SetScoreString()
    {
        string finalString;
        finalString = _countOfRightAnsw.ToString() + " из " + CountOfQuestions;
        return finalString;
    }
}