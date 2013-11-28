/* --------------------------Header-------------------------------------
 * File : GUITextField.cs
 * Description : Script that create a textfield for gui.
 * Version : 1.0.0
 * Created Date : 27/11/2013 17:55:16
 * Created by : Jonathan Bihet
 * Modification Date : 28/11/2013 14:29:54
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

/// <summary>
/// GUITextField Class, extend from MonoBehaviour
/// </summary>
public class GUITextField : MonoBehaviour
{
    private TextMesh Text;
    private bool Selected = false;

    /// <summary>
    /// The max length of the Text.
    /// </summary>
    [SerializeField]
    private int _maxLength;
    public int MaxLength
    {
        get { return _maxLength; }
        set { _maxLength = value; }
    }


    /// <summary>
    /// Get the text mesh at start.
    /// </summary>
    void Start()
    {
        Text = this.gameObject.GetComponent<TextMesh>();
    }

    /// <summary>
    /// Handle the text writing
    /// </summary>
    void Update()
    {
        //If we wrote something and the field is selected.
        if (Input.inputString != "" && Selected)
        {
            foreach (char c in Input.inputString)
                if (c == "\b"[0]) //Delete
                {
                    if (Text.text.Length != 0)
                        Text.text = Text.text.Substring(0, Text.text.Length - 1);
                }
                else
                {
                    if (c == "\n"[0] || c == "\r"[0]) //jump line
                    {
                        Selected = false;
                        Text.color = Color.white;
                    }
                    else
                    {
                        if (Text.text.Length < MaxLength) //If max length not reached => wrote
                            Text.text += c;
                    }
                }
        }
    }

    /// <summary>
    /// Select the Field on click
    /// </summary>
    void OnMouseUp()
    {
        Selected = true;
        Text.color = Color.red;
    }

    /// <summary>
    /// Deselect the Field when the mouse go away.
    /// </summary>
    void OnMouseExit()
    {
        Selected = false;
        Text.color = Color.white;
    }

    /// <summary>
    /// Return the text of the field.
    /// </summary>
    /// <returns>Text value.</returns>
    public string GetText()
    {
        return Text.text;
    }
}
