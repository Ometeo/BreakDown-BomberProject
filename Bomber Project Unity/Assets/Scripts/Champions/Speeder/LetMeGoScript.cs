using UnityEngine;
using System.Collections;

public class LetMeGoScript : SkillScript {

    [SerializeField]
    private float _buffDuration;
    public float BuffDuration
    {
        get { return _buffDuration; }
        set { _buffDuration = value; }
    }

    [SerializeField]
    private LayerMask _layerM;
    public LayerMask LayerM
    {
        get { return _layerM; }
        set { _layerM = value; }
    }

    protected override void NormalSkill(Transform playerTransform)
    {
        WalkThroughMatterScript walkThrMatScript = playerTransform.gameObject.AddComponent<WalkThroughMatterScript>();
        walkThrMatScript.Duration = BuffDuration;
        walkThrMatScript.LayerM = LayerM;
    }
}
