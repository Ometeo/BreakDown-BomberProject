using UnityEngine;
using System.Collections;

public class SkillCooldownInterfaceScript : MonoBehaviour {

    [SerializeField]
    private TextMesh _cdText;
    public TextMesh CdText
    {
        get { return _cdText; }
        set { _cdText = value; }
    }

    private SkillScript _skScript;
    public SkillScript SkScript
    {
        get { return _skScript; }
        set { _skScript = value; }
    }

    void FixedUpdate()
    {
        if (SkScript != null)
        {
            var timeBeforeUse = SkScript.TimeBeforeUse();
            if (timeBeforeUse != 0)
                CdText.text = ((int)timeBeforeUse).ToString();
            else
                CdText.text = "";
        }
    }
}
