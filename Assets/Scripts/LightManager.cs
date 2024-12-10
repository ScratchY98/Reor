using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LightManager : MonoBehaviour
{
    [SerializeField] private Light[] lights;
    [SerializeField] private int finishLightCount;
    [SerializeField] private string sceneToLoad;

    private void Start()
    {
        ResetAllLights();
    }

    public void ResetAllLights()
    {
        foreach (Light light in lights)
        {
            light.ResetLight();
        }
    }
    public void UpscaleFinishLightCount()
    {
        finishLightCount++;
        if (finishLightCount == lights.Length)
            StartCoroutine(LoadSceneAsync(sceneToLoad));
    }

    // Coroutine pour charger la scène asynchrone
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }

    }
}
