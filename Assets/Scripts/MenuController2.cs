using UnityEngine;
using UnityEngine.SceneManagement; // Necess�rio para carregar cenas
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController2 : MonoBehaviour
{

    public void PlayGame()
    {

        SceneManager.LoadSceneAsync(3);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}