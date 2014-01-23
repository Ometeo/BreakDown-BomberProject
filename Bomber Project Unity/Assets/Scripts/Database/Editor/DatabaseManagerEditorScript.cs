using UnityEngine;
using UnityEditor;

public class DatabaseManagerEditorScript
{
    [MenuItem("Assets/Create/DatabaseManager")]
    public static void CreateAsset()
    {
        ScriptableObjectUtilityScript.CreateAsset<DatabaseManagerScript>();
    }
}