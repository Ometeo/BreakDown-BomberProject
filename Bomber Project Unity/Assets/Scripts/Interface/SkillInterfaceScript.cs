using UnityEngine;
using System.Collections;

public class SkillInterfaceScript : MonoBehaviour {

    [SerializeField]
    private SkillCooldownInterfaceScript _skCdInterfScript;
    public SkillCooldownInterfaceScript SkCdInterfScript
    {
        get { return _skCdInterfScript; }
        set { _skCdInterfScript = value; }
    }

    [SerializeField]
    private MeshRenderer _iconMeshRender;
    public MeshRenderer IconMeshRender
    {
        get { return _iconMeshRender; }
        set { _iconMeshRender = value; }
    }

    public void InitializeSkill(Texture texture, SkillScript skScript)
    {
        IconMeshRender.material.mainTexture = texture;
        SkCdInterfScript.SkScript = skScript;
    }
}
