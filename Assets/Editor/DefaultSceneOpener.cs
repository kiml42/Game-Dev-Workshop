using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class DefaultSceneOpener
{
    const string ScenePath = "Assets/Scenes/SampleScene.unity";

    static DefaultSceneOpener()
    {
        // Run once per Editor session so we don't fight the user if they switch scenes.
        if (!SessionState.GetBool("DefaultSceneOpened", false))
        {
            SessionState.SetBool("DefaultSceneOpened", true);
            EditorApplication.delayCall += OpenDefaultScene;
        }
    }

    static void OpenDefaultScene()
    {
        if (EditorSceneManager.GetActiveScene().path != ScenePath)
            EditorSceneManager.OpenScene(ScenePath);
    }
}
