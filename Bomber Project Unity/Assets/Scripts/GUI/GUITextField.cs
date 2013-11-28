/* --------------------------Header-------------------------------------
 * File : GUITextField.cs
 * Description : 
 * Version : 1.0.0
 * Created Date : 27/11/2013 17:55:16
 * Created by : Jonathan Bihet
 * Modification Date : 27/11/2013 17:55:16
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class GUITextField : MonoBehaviour
{
    public TextMesh Text;
    public bool Selected = false;

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        Text = this.gameObject.GetComponent<TextMesh>();
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if (Input.inputString != "" && Selected)
        {
            foreach (char c in Input.inputString)
                if (c == "\b"[0])
                {
                    if (Text.text.Length != 0)
                        Text.text = Text.text.Substring(0, Text.text.Length - 1);
                }
                else
                    if (c == "\n"[0] || c == "\r"[0]) 
                    {
                        Selected = false;
                    }
                    else
                        Text.text += c;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void OnMouseUp()
    {
        Selected = true;
    }
}
