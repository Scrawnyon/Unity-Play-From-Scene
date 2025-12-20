using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PlayModeSceneSetter
{
    #region Editor prefs
    const bool STORE_IN_PREFS = true;
    const string PREFS_PATH_KEY = "EditorPlayFromScenePath";

    [InitializeOnLoadMethod]
    static void OnEditorStart()
    {
        if (STORE_IN_PREFS)
            EditorApplication.delayCall += LoadPrefs;
    }

    static void LoadPrefs()
    {
        string _path = EditorPrefs.GetString(PREFS_PATH_KEY);
        if (_path != "" && AssetDatabase.AssetPathExists(_path))
            SetStartScene(_path);
    }

    static void SavePrefs(string _scenePath) => EditorPrefs.SetString(PREFS_PATH_KEY, _scenePath);
    #endregion

    [MenuItem("Editor Start Scene/Set Current As Start", priority = 0)]
    static void SetCurrentSceneAsStart()
    {
        Scene _currScene = SceneManager.GetActiveScene();

        if (_currScene.IsValid() == false || string.IsNullOrEmpty(_currScene.path))
        {
            Debug.Log("Current scene null or not valid");
            EditorSceneManager.playModeStartScene = null;
            return;
        }

        if (STORE_IN_PREFS)
            SavePrefs(_currScene.path);

        SetStartScene(_currScene.path);
    }

    [MenuItem("Editor Start Scene/Clear", priority = 1)]
    static void ClearStartScene()
    {
        EditorSceneManager.playModeStartScene = null;

        if (STORE_IN_PREFS)
            SavePrefs("");
    }

    static void SetStartScene(string _scenePath)
    {
        if (_scenePath == "")
        {
            EditorSceneManager.playModeStartScene = null;
            return;
        }

        SceneAsset _sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(_scenePath);
        EditorSceneManager.playModeStartScene = _sceneAsset;
    }
}
