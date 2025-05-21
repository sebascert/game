using System;
using System.Collections;
using System.Linq;

using UnityEditor;

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

// TODO optional improvement
// have a transition scene, would require a scene manager
class CinematicSystem : GameSystem<CinematicSystem>
{
    [SerializeField]
    private Cinematic[] cinematics;

    public override void Init()
    {
    }

    public IEnumerator PlayCinematic(string cinematic)
    {
        Cinematic foundCinematic = cinematics.FirstOrDefault(cnm => cnm.name == cinematic);
        if (foundCinematic == null)
            throw new Exception($"CinematicSystem: cinematic {name} not found");

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(foundCinematic.scene.name, LoadSceneMode.Additive);
        yield return new WaitUntil(() => asyncLoad is { isDone: true });

        Scene loadedScene = SceneManager.GetSceneByName(foundCinematic.scene.name);

        PlayableDirector director = null;
        foreach (GameObject obj in loadedScene.GetRootGameObjects())
        {
            director = obj.GetComponentInChildren<PlayableDirector>();
            if (director)
                break;
        }

        if (!director)
            throw new Exception("CinematicSystem: cinematic has no PlayableDirector");

        director.Play();
        yield return new WaitUntil(() => director.state != PlayState.Playing);

        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(loadedScene);
        yield return new WaitUntil(() => asyncUnload is { isDone: true });
    }
}

[Serializable]
class Cinematic
{
    public SceneAsset scene;
    public string name;
}