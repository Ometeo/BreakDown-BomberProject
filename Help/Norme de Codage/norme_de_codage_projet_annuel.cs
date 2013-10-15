/*
 *   File : exemple.cs
 *   Description : this file is a test about code standard.
 *   Version : 1.0.1
 *   Created by : Jonathan Bihet
 *   Created Date : 21/02/2013
 *   Modification Date : 21/02/2013
 *   Modified by : Jonathan Bihet 
 */

using UnityEngine;
using System.Collections;


/// <summary>
///  This is a test class.
///  with a method <c>myMethod</c> to do everything when used in <c>Update</c>.
///  The Test class have two args, <c>_foo</c> & <c>_bar</c>.
/// </summary>
/// <remarks>
///   Secret info : this class is useless.
/// </remarks>
public class Test : MonoBehaviour
{
    public Transform _foo;
    public float _bar;

    /// <summary>
    /// Start is called just before any of the Update methods is called
    /// the first time.
    /// </summary>
    void Start()
    {
        //we do nothing.
        _foo = this.transform;
        bar = _foo.x;

    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        int yata;
        if (true)
            yata = MyMethod(yata, yata, yata);
        else
        {
            yata = MyMethod(yata, yata, yata);
            yata = MyMethod(yata, yata, yata);
        }
    }

    /// <summary>
    /// my method to do something very usefull. Using <paramref name="arg" />,
    /// <paramref name="arg2" />, <paramref name="arg3" />.
    /// </summary>
    /// <param name=arg>first arg of my method.</param>
    /// <param name=arg2>first arg of my method.</param>
    /// <param name=arg3>first arg of my method.</param>
    /// <returns>a very usefull value</returns>
    public void MyMethod(int arg, int arg2, int arg3)
    {
        int varOfTheDead;
        varOfTheDead = 0;       
        return varOfTheDead;
    }
}