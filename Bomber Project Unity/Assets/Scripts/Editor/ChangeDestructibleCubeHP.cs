/* --------------------------Header-------------------------------------
 * File : ChangeDestructibleCubeHP.cs
 * Description : Script that change HP of Destructible Blocs
 * Version : 1.0.1
 * Created Date : 25/11/2013 14:56:46
 * Created by : Jonathan Bihet
 * Modification Date : 25/11/2013 16:41:53
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using UnityEditor;
using System.Collections;

public class ChangeDestructibleCubeHP : EditorWindow
{
    private Object _object;
    private int _nbObj;
    private bool _counted = false;
    
    void OnGUI()
    {
        _nbObj = 0;
        GUILayout.BeginHorizontal();
        GUILayout.Label("Change Destructible Cube Health Point :");
        GUILayout.EndHorizontal();

            object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
                foreach (object o in obj)
                {
                    GameObject g = (GameObject)o;
                    if (g.name == "BasicDestructibleBloc" || g.name == "BasicDestructibleBloc(Clone)")
                    {
                        if (g.transform.GetComponent<DestructibleBlocScript>().NbHP != 3)
                            g.transform.GetComponent<DestructibleBlocScript>().NbHP = 3;
                    }
                }

    }

    [MenuItem("ArenaCreator/Select All Of One")]
    public static void ManageLevelsButton()
    {
        EditorWindow.GetWindow(typeof(ChangeDestructibleCubeHP));
    }
}
