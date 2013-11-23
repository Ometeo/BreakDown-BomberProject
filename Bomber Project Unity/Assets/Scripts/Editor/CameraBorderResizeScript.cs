using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class CameraBorderResizeScript : EditorWindow
{
    private GameObject _arenaBorder;

    /// <summary>
    /// 
    /// </summary>
    void OnGUI()
    {

        bool exist = false;
        object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (object o in obj)
        {
            GameObject g = (GameObject)o;
            if (g.name == "CameraBorderPrefab")
            {
                exist = true;
                _arenaBorder = g;
            }
        }

        if (!exist)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Border Don't Exist => Create Border");
            GUILayout.EndHorizontal();
            CreateBorder();
        }
        else
        {
            GUILayout.BeginHorizontal();
            _arenaBorder.GetComponent<CameraBorderScript>().ArenaSize = EditorGUILayout.Vector2Field("Arena Size", _arenaBorder.GetComponent<CameraBorderScript>().ArenaSize);
            GUILayout.EndHorizontal();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [MenuItem("ArenaCreator/Camera Border Sizer")]
    public static void ManageLevelsButton()
    {
        EditorWindow.GetWindow(typeof(CameraBorderResizeScript));
    }

    /// <summary>
    /// 
    /// </summary>
    public void CreateBorder()
    {
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/CameraViewPrefabs/CameraBorderPrefab.prefab", typeof(GameObject));
        GameObject clone = Instantiate(prefab) as GameObject;
        clone.name = "CameraBorderPrefab";
    }
}
