using UnityEngine;
using UnityEngine.UI;

public class AuthUser : MonoBehaviour
{
	[SerializeField]
	private InputField InName, InSurname;

	[SerializeField] private GameObject questionsPanel, menuPanel;

	public void StartGame()
	{
		if (InName.text != "" && InSurname.text != "")
		{
			PlayerPrefs.SetString("GameName", InSurname.text + " " + InName.text);
			PlayerPrefs.Save();

			menuPanel.SetActive(false);
			questionsPanel.SetActive(true);
		}
	}
}
