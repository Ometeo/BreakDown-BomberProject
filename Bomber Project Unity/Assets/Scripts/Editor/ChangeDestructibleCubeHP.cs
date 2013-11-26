/* --------------------------Header-------------------------------------
 * File : ChangeDestructibleCubeHP.cs
 * Description : Script that change HP of Destructible Blocs
 * Version : 1.0.1
 * Created Date : 25/11/2013 14:56:46
 * Created by : Jonathan Bihet
 * Modification Date : 26/11/2013 14:26:33
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using UnityEditor;
using System.Collections;

public class ChangeDestructibleCubeHP : EditorWindow
{
    private Object _object;
    private Object _material;

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Change Destructible Cube Health Point :");
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Change Health Points"))
        {
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

        GUILayout.BeginHorizontal();
        GUILayout.Label("Change Destructible Cube Material :");
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Select Material : ");
        GUILayout.EndHorizontal();

        _material = EditorGUILayout.ObjectField(_material, typeof(Material), true);

        if (GUILayout.Button("Change Material"))
        {
            object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
            foreach (object o in obj)
            {
                GameObject g = (GameObject)o;
                if (g.name == "BasicDestructibleBloc" || g.name == "BasicDestructibleBloc(Clone)")
                {
                    g.transform.renderer.material = (Material)_material;
                }
            }
        }
    }

    [MenuItem("ArenaCreator/Destructible HP")]
    public static void ManageLevelsButton()
    {
        EditorWindow.GetWindow(typeof(ChangeDestructibleCubeHP));
    }
}
