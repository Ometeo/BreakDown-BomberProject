/* --------------------------Header-------------------------------------
 * File : ApplySettingsScript.cs
 * Description : 
 * Version : 1.0.0
 * Created Date : 26/11/2013 08:38:03
 * Created by : Jonathan Bihet
 * Modification Date : 27/11/2013 18:38:43
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class ApplySettingsScript : GUIItemScript
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private CaseAndTickScript _fullScreenOption;
    public CaseAndTickScript FullScreenOption
    {
        get { return _fullScreenOption; }
        set { _fullScreenOption = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private ResolutionTextScript _resolutionOption;
    public ResolutionTextScript ResolutionOption
    {
        get { return _resolutionOption; }
        set { _resolutionOption = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private QualityTextScript _qualityOption;
    public QualityTextScript QualityOption
    {
        get { return _qualityOption; }
        set { _qualityOption = value; }
    }

    /// <summary>
    /// Initialize the GUI on start.
    /// </summary>
    void Start()
    {
        InitializeGUI();
    }

    /// <summary>
    /// 
    /// </summary>
    public override void OnMouseDown()
    {
        Resolution res = Screen.resolutions[ResolutionOption.CurrentValue];
        Screen.SetResolution(res.width, res.height, FullScreenOption.Checked);
        
        QualitySettings.SetQualityLevel(QualityOption.CurrentValue);
        
    }
}
