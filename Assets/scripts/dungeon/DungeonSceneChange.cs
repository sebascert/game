using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DungeonSceneChange : MonoBehaviour
{
    //main menu, sala1, sala2
    public string nextScene = "sala1";
    public float timeDelay = 2f;

    public void CallToNextScene()
    {
        StartCoroutine(ToNextScene());
    }

    private IEnumerator ToNextScene()
    {
        yield return new WaitForSecondsRealtime(timeDelay);
        SceneManager.LoadScene(nextScene);
    }
}