using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject DemoText;
    [SerializeField] private GameObject TheoryPanel, MainMenuPanel, LaboratoryPanel;
    [SerializeField] private GameObject ResultsPanel;
    
    private void Start() => 
        ResultsPanel.SetActive(false);

    public void SwitchLanguage(string lang)
    {
        PlayerPrefs.SetString("lang", lang);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void DemoLang()
    {
        DemoText.SetActive(true);
    }

    public void OpenTheoryPanel(bool open)
    {
        if (open)
        {
            TheoryPanel.SetActive(true);
            //MainMenuPanel.SetActive(false);
        }
        else{
            TheoryPanel.SetActive(false);
            //MainMenuPanel.SetActive(true);
        }
    }
    public void OpenLaboratoryPanel(bool open)
    {
        if (open)
        {
            LaboratoryPanel.SetActive(true);
            //MainMenuPanel.SetActive(false);
        }
        else
        {
            LaboratoryPanel.SetActive(false);
            //MainMenuPanel.SetActive(true);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenResults(bool open)
    {
        ResultsPanel.SetActive(open);
    }
}
