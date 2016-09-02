using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour {

	public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
