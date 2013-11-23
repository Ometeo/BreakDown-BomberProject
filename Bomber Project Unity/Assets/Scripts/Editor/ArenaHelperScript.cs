using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class ArenaHelperScript : EditorWindow
{
    private Vector2 _arenaSize;
    private Object _groundObject;
    private Object _borderObject;
    private bool _border;

    /// <summary>
    /// 
    /// </summary>
    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        _arenaSize = EditorGUILayout.Vector2Field("Arena Size (without border) : ", _arenaSize);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Ground Object : ");
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        _groundObject = EditorGUILayout.ObjectField(_groundObject, typeof(GameObject), true);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        _border = EditorGUILayout.Toggle(_border);
        GUILayout.EndHorizontal();

        if (_border)
        {
            GUILayout.BeginHorizontal();
            _borderObject = EditorGUILayout.ObjectField(_borderObject, typeof(GameObject), true);
            GUILayout.EndHorizontal();
        }

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Base Arena"))
            CreateBaseArena(_arenaSize, (GameObject)_groundObject, _border, (GameObject)_borderObject);
        GUILayout.EndHorizontal();
    }

    /// <summary>
    /// 
    /// </summary>
    [MenuItem("ArenaCreator/Arena Helper")]
    public static void ManageLevelsButton()
    {
        EditorWindow.GetWindow(typeof(ArenaHelperScript));
    }

    void CreateBaseArena(Vector2 arenaSize, GameObject groundObject, bool border, GameObject borderObject)
    {
        
        if (groundObject != null)
        {       
            if (arenaSize.x % 2 != 0 && arenaSize.y % 2 != 0)
            {
                GameObject ground = new GameObject("Ground");
                for (int i = -(int)(arenaSize.y / 2); i <= (int)(arenaSize.y / 2); i++)
                {
                    GameObject Line = new GameObject("Line");
                    Line.transform.parent = ground.transform;
                    Line.transform.position = new Vector3(0.0f, 0.0f, (float)i);

                    for (int j = -(int)(arenaSize.x / 2); j <= (int)(arenaSize.x / 2); j++)
                    {
                        GameObject GroundBloc = Instantiate(groundObject) as GameObject;
                        //GroundBloc.name = "GroundBloc";
                        GroundBloc.transform.parent = Line.transform;
                        GroundBloc.transform.position = new Vector3((float)j, 0.0f, (float)i);
                    }
                }
                if (border && borderObject != null)
                {
                    GameObject borderParentObject = new GameObject("Border");
                    for (int j = -(int)((arenaSize.x+1) / 2); j <= (int)((arenaSize.x+1) / 2); j++)
                    {
                        GameObject borderBloc = Instantiate(borderObject) as GameObject;
                        borderBloc.transform.parent = borderParentObject.transform;
                        borderBloc.transform.position = new Vector3((float)j, 0.5f, (-(arenaSize.x -1) / 2));
                    }
                    for (int j = -(int)((arenaSize.x + 1) / 2); j <= (int)((arenaSize.x + 1) / 2); j++)
                    {
                        GameObject borderBloc = Instantiate(borderObject) as GameObject;
                        borderBloc.transform.parent = borderParentObject.transform;
                        borderBloc.transform.position = new Vector3((float)j, 0.5f, ((arenaSize.x - 1) / 2));
                    }
                    for (int i = -(int)(arenaSize.y / 2); i <= (int)(arenaSize.y / 2); i++)
                    {
                        GameObject borderBloc = Instantiate(borderObject) as GameObject;
                        borderBloc.transform.parent = borderParentObject.transform;
                        borderBloc.transform.position = new Vector3((-(arenaSize.x +1) / 2), 0.5f, i);
                    }
                    for (int i = -(int)(arenaSize.y / 2); i <= (int)(arenaSize.y / 2); i++)
                    {
                        GameObject borderBloc = Instantiate(borderObject) as GameObject;
                        borderBloc.transform.parent = borderParentObject.transform;
                        borderBloc.transform.position = new Vector3(((arenaSize.x + 1) / 2), 0.5f, i);
                    }

                }

            }
            else
            {
                Debug.Log("Bad Arena Size");
            }
        }
        else
        {
            Debug.Log("Please select an object for the ground!");
        }
        Debug.Log(border.ToString());
        if (border)
            if(borderObject != null)
                Debug.Log(borderObject.ToString());
            else
            {
                Debug.Log("Please select an object for the border!");
            }

    }
}
