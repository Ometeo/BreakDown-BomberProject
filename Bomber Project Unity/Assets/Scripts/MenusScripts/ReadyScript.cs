/* --------------------------Header-------------------------------------
 * File : ReadyScript.cs
 * Description : Script for the button that launch the game.
 * Version : 1.0.0
 * Created Date : 27/11/2013 18:36:27
 * Created by : Jonathan Bihet
 * Modification Date : 27/11/2013 18:36:27
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

/// <summary>
/// ReadyScript Class
/// </summary>
public class ReadyScript : GUIItemScript {

	/// <summary>
	/// Initialize the GUI on start.
	/// </summary>
	void Start ()
    {
        InitializeGUI();
	}

    public override void OnMouseUp()
    {
        //Todo : Launch game!
    }
}
