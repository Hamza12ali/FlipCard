using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{   
    public GameObject uiContainer;
    bool menu = false;

    public void onClickMenu()
    {
    
        menu = !menu;
        uiContainer.SetActive(true);
    }
    public void onClickResume()
    {
        menu = !menu;
        uiContainer.SetActive(false);
    }
    public void onClickRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void onClickQuit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit(); // Quits the built application
    #endif
    }
}
