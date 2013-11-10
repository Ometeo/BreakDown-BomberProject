using UnityEngine;
using System.Collections;

public class InterfaceScript : MonoBehaviour {

    [SerializeField]
    private SkillInterfaceScript _passiveSkill;
    public SkillInterfaceScript PassiveSkill
    {
        get { return _passiveSkill; }
        set { _passiveSkill = value; }
    }

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

    [SerializeField]
    private TextMesh _lifePoints;
    public TextMesh LifePoints
    {
        get { return _lifePoints; }
        set { _lifePoints = value; }
    }

    [SerializeField]
    private TextMesh _movementSpeed;
    public TextMesh MovementSpeed
    {
        get { return _movementSpeed; }
        set { _movementSpeed = value; }
    }

    [SerializeField]
    private TextMesh _bombsAvailable;
    public TextMesh BombsAvailable
    {
        get { return _bombsAvailable; }
        set { _bombsAvailable = value; }
    }
}
