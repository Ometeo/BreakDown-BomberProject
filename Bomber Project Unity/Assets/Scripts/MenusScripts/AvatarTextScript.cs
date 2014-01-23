/* --------------------------Header-------------------------------------
 * File : AvatarTextScript.cs
 * Description : 
 * Version : 1.0.0
 * Created Date : 27/11/2013 18:01:29
 * Created by : Jonathan Bihet
 * Modification Date : 27/11/2013 18:01:29
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class AvatarTextScript : MonoBehaviour {

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private string[] _champions;
    public string[] Champions
    {
        get { return _champions; }
        set { _champions = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private int _currentValue = 0;
    public int CurrentValue
    {
        get { return _currentValue; }
        set { _currentValue = value; }
    }

	/// <summary>
	/// 
	/// </summary>
	void Start () {
        DatabaseManagerScript databaseScript = (DatabaseManagerScript)Resources.Load("DatabaseManager", typeof(DatabaseManagerScript));

        string[] champName = new string[databaseScript.Champions.Length];
        for (int numChamp = 0; numChamp < databaseScript.Champions.Length; numChamp++)
            champName[numChamp] = databaseScript.Champions[numChamp].name;
        Champions = champName;
        this.gameObject.GetComponent<TextMesh>().text = Champions[CurrentValue];
	}

    /// <summary>
    /// 
    /// </summary>
    public void Increment()
    {
        if (CurrentValue == Champions.Length - 1)
            CurrentValue = 0;
        else
            CurrentValue++;
        this.gameObject.GetComponent<TextMesh>().text = Champions[CurrentValue];
    }

    /// <summary>
    /// 
    /// </summary>
    public void Decrement()
    {
        if (CurrentValue == 0)
            CurrentValue = Champions.Length - 1;
        else
            CurrentValue--;
        this.gameObject.GetComponent<TextMesh>().text = Champions[CurrentValue];
    }
}
