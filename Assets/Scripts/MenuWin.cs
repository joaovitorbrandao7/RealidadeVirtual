using UnityEngine;
using UnityEngine.SceneManagement; // Necess�rio para carregar cenas
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuWin : MonoBehaviour
{

    public void Menu()
    {

        SceneManager.LoadSceneAsync(0);
    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

}