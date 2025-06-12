using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuUi : MonoBehaviour
{
    public void LoadGame()
    {
        StartCoroutine(LoadGameScene());
        //blackput effect?
    }

    private IEnumerator LoadGameScene()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene("sala1");
    }

    public void GameExit()
    {
        Debug.Log("Game Exit");
        Application.Quit();
    }
}