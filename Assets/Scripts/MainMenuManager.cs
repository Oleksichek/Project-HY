using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OnStartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitGame()
    {
        Application.Quit();
    }
}
