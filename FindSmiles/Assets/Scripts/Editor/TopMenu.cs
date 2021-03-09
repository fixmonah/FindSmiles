using UnityEditor;
using UnityEditor.SceneManagement;

public class TopMenu : Editor
{
    [MenuItem("Tools/StartLoadingScene")]
    static void StartInSlowmo()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/LoaderScene.unity");
        UnityEditor.EditorApplication.isPlaying = true;
    }
}
