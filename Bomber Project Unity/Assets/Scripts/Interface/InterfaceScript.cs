using UnityEngine;
using System.Collections;

public class InterfaceScript : MonoBehaviour {

    [SerializeField]
    private SkillInterfaceScript _skill1;
    public SkillInterfaceScript Skill1
    {
        get { return _skill1; }
        set { _skill1 = value; }
    }

    [SerializeField]
    private SkillInterfaceScript _skill2;
    public SkillInterfaceScript Skill2
    {
        get { return _skill2; }
        set { _skill2 = value; }
    }

    [SerializeField]
    private SkillInterfaceScript _skillUltimate;
    public SkillInterfaceScript SkillUltimate
    {
        get { return _skillUltimate; }
        set { _skillUltimate = value; }
    }
}
