using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AutoCreateSceneData
{
    private static string[] ScenePrefabPath = {"Assets/Resources/Scenes" };
    private static string[] SceneSOFolderPath = { "Assets/Resources/GameData/SceneData" };
    private static string SceneSOStr = "Assets/Resources/GameData/SceneData/{0}.asset";
    [MenuItem("额外工具/Auto Create SceneData_SO")]
    public static void RefreshSceneData()
    {
        DeleteSceneSO();
        var allGuids = GetAllPrefab();
        CreateAllSceneData(allGuids);
        Debug.Log("生成场景数据成功");
    }

    private static void CreateAllSceneData(string[] allGuids)
    {
        foreach (var guid in allGuids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var obj = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
            int pointIndex = path.IndexOf('.');
            var pathWithoutFormat = path.Remove(pointIndex);
            pathWithoutFormat = pathWithoutFormat.Replace("Assets/Resources/", "");
            CreateSceneData(obj, pathWithoutFormat);
        }
    }

    private static string[] GetAllPrefab()
    {
        return AssetDatabase.FindAssets("t:Prefab", ScenePrefabPath);

    }

    private static void DeleteSceneSO()
    {
        var guids = AssetDatabase.FindAssets("t:ScriptableObject", SceneSOFolderPath);
        foreach(var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            AssetDatabase.DeleteAsset(path);
        }
    }

    private static void CreateSceneData(GameObject scene, string path)
    {
        GetSceneMsg(scene, out var id, out var grounds, out var obstacles);
        CreateSO(scene.name, id, path, grounds, obstacles);
    }

    private static void GetSceneMsg(GameObject obj,out int id, out List<Ground> grounds, out List<Obstacle> obstacles)
    {
        var scene = obj.GetComponent<BattleSceneDataBase>();
        scene.GetSceneObjectSetMsgs(out id,out grounds, out obstacles);
    }

    private static void CreateSO(string name, int id, string path,List<Ground> grounds, List<Obstacle> obstacles)
    {
        var sceneSO = ScriptableObject.CreateInstance<SceneData_SO>();
        sceneSO.grounds = grounds;
        sceneSO.obstacles = obstacles;
        sceneSO.path = path;
        sceneSO.id = id;
        AssetDatabase.CreateAsset(sceneSO, string.Format(SceneSOStr, name));
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
