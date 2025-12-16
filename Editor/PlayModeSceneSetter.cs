using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PlayModeSceneSetter
{
    [MenuItem("Editor Start Scene/Set Current As Start")]
    static void SetStartScene()
    {
        Scene _currScene = SceneManager.GetActiveScene();

        if (_currScene.IsValid() == false || string.IsNullOrEmpty(_currScene.path))
        {
            Debug.Log("Current scene null or not valid");
            EditorSceneManager.playModeStartScene = null;
            return;
        }

        SceneAsset _sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(_currScene.path);
        EditorSceneManager.playModeStartScene = _sceneAsset;
    }

    [MenuItem("Editor Start Scene/Clear")]
    static void ClearStartScene() => EditorSceneManager.playModeStartScene = null;
}
