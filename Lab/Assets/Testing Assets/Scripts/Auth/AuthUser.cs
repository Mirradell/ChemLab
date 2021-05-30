using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AuthUser : MonoBehaviour
{
	[SerializeField]
	private TMP_InputField InName, InSurname;

	public void StartGame()
	{
		if (InName.text != "" && InSurname.text != "")
		{
			PlayerPrefs.SetString("GameName", InSurname.text + " " + InName.text);
			PlayerPrefs.Save();
			SceneManager.LoadScene(1);
		}
	}
}
