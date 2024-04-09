using UnityEngine;
using UnityEngine.SceneManagement;
using System;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

public class InGameUILoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadUI();
    }
    [ContextMenu("Load UI")]
    private void LoadUI()
    {
        if (SceneManager.GetSceneByName("InGameUI").isLoaded)
            return;
        if (Application.isPlaying)
            SceneManager.LoadSceneAsync("Scenes/InGameUI", LoadSceneMode.Additive);
#if UNITY_EDITOR
        else
            EditorSceneManager.OpenScene("Assets/Scenes/InGameUI.unity", OpenSceneMode.Additive);
#endif
    }
    [ContextMenu("Unload UI")]
    private void UnloadUI()
    {
        if (!SceneManager.GetSceneByName("InGameUI").isLoaded)
            return;
        if (Application.isPlaying)
            SceneManager.UnloadSceneAsync("InGameUI");
#if UNITY_EDITOR
        else
            EditorSceneManager.CloseScene(scene:EditorSceneManager.GetSceneByName("InGameUI"), removeScene: true);
#endif
    }
}
