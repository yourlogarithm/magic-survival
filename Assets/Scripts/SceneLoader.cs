using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
        
    public static void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public static void ExitGame()
    {
        Application.Quit();
    }
}
