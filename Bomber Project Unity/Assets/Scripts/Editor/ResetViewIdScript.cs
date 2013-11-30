using UnityEditor;
using UnityEngine;
using System.Collections;

public class ResetViewIdScript : EditorWindow
{
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 20), "Start Reallocation ID"))
           StartReallocateViewId();
    }
    

    [MenuItem("ArenaCreator/ResetViewId")]
    public static void ResetViewId()
    {
        EditorWindow.GetWindow(typeof(ResetViewIdScript));
    }

    void StartReallocateViewId()
    {
        int cnt = 0;
        Object[] obj = FindObjectsOfType(typeof (GameObject));
        foreach (var o in obj)
        {
            GameObject go = (GameObject)o;
            NetworkView[] nws = go.GetComponents<NetworkView>();
            if (nws != null && nws.Length > 0)
            {
                foreach (var nView in nws)
                {
                    DestroyImmediate(nView);  
                }
                NetworkView nw = go.AddComponent<NetworkView>();
                nw.stateSynchronization = NetworkStateSynchronization.Off;
                nw.observed = null;
                cnt++;
            }
        }
        Debug.Log("Reset " + cnt + " Network Views");
    }
}
