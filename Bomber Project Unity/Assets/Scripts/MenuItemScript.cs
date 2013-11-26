using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class MenuItemScript : MonoBehaviour
{
    private string _itemName;
    private Transform[] _itemText;

    [SerializeField]
    private Transform _titleEffectObject;
    public Transform TitleEffectObject
    {
        get { return _titleEffectObject; }
        set { _titleEffectObject = value; }
    }


    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        _itemName = this.transform.name;
        _itemText = this.gameObject.GetComponentsInChildren<Transform>();
        _itemText[2].gameObject.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    void OnMouseEnter()
    {
        _itemText[1].gameObject.SetActive(false);
        _itemText[2].gameObject.SetActive(true);
    }

    /// <summary>
    /// 
    /// </summary>
    void OnMouseExit()
    {
        _itemText[1].gameObject.SetActive(true);
        _itemText[2].gameObject.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    void OnMouseDown()
    {
        if (_itemName == "Quit")
            Application.Quit();
        else if (_itemName == "Title")
        {
            StartCoroutine(TitleEffect());
        }
        else
        {
            //Application.LoadLevel(_itemName);
        }
    }

    IEnumerator TitleEffect()
    {
        TitleEffectObject.gameObject.SetActive(true);
        yield return new WaitForSeconds(10.0f);
        TitleEffectObject.gameObject.SetActive(false);
    }
}
