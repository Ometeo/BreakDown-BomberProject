using UnityEditor;
using UnityEngine;

public class RestorePrefabLinksScript : ScriptableWizard
{
    public GameObject prefab;
 
    [MenuItem("Custom/Swap Uniques For Prefab")]
    private static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard("Swap Uniques For Prefab", typeof(RestorePrefabLinksScript), "Swap all");
    }
 
    private void OnWizardCreate()
    {
        Debug.Log("Swap for prefab : gos = " + Selection.gameObjects.Length);
 
        foreach (GameObject go in Selection.gameObjects)
        {
            GameObject newGo = (GameObject) EditorUtility.InstantiatePrefab(prefab);
            newGo.transform.position = go.transform.position;
            newGo.transform.rotation = go.transform.rotation;
            newGo.transform.parent = go.transform.parent;
 
            newGo.name = go.name;
 
            //int index = newGo.name.IndexOf("(Clone)");
            //newGo.name = newGo.name.Substring(0, index);
        }
 
        foreach (GameObject go in Selection.gameObjects)
        {
            DestroyImmediate(go);
        }
    }
 
    private void OnWizardUpdate()
    {
        helpString = "Select ALL the unique objects you want to replace with IDENTICAL prefabs.\nNew go's make on the pos and rotation only\nTODO: Delete old object";
    }
}