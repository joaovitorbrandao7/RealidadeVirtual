using UnityEngine;
using UnityEngine.SceneManagement; // Necess�rio para carregar cenas
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void PlayGame()
    {

        SceneManager.LoadSceneAsync(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}