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

    private bool _selected = false;
    private bool _eraseText = false;

    /// <summary>
    /// The name of the Player Prefs key
    /// </summary>
    [SerializeField]
    private string _playerPrefsField;
    public string PlayerPrefsField
    {
        get { return _playerPrefsField; }
        set { _playerPrefsField = value; }
    }

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

    public static GUITextField PreviousField;

    /// <summary>
    /// Get the text mesh at start.
    /// </summary>
    void Start()
    {
        Text = this.gameObject.GetComponent<TextMesh>();
        if (PlayerPrefsField != null && PlayerPrefsField.Length > 0)
        {
            var savedPrefs = PlayerPrefs.GetString(PlayerPrefsField);
            if (savedPrefs.Length > 0)
                Text.text = savedPrefs;
        }
    }

    /// <summary>
    /// Handle the text writing
    /// </summary>
    void Update()
    {
        //If we wrote something and the field is selected.
        if (Input.inputString != "" && _selected)
        {
            if (_eraseText)
            {
                _eraseText = false;
                Text.text = "";
            }
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
                        UnselectField();                        
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
        if (PreviousField != null)
            PreviousField.UnselectField();
        PreviousField = this;

        _eraseText = true;
        _selected = true;
        Text.color = Color.red;
    }

    public void UnselectField()
    {
        _selected = false;
        Text.color = Color.white;
        SavePlayerPrefs();
    }

    /// <summary>
    /// Return the text of the field.
    /// </summary>
    /// <returns>Text value.</returns>
    public string GetText()
    {
        return Text.text;
    }


    void SavePlayerPrefs()
    {
        // TODO: Uncomment these lines when deployable
        /*
        if (PlayerPrefsField != null && PlayerPrefsField.Length > 0)
        {
            PlayerPrefs.SetString(PlayerPrefsField, Text.text);
        }
        */
        if ("PlayerName".Equals(PlayerPrefsField))
            PlayersSingleton.Instance.MyName = Text.text;
    }
}
