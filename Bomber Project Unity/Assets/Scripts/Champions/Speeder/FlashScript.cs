using UnityEngine;
using System.Collections;

public class FlashScript : SkillScript {

    [SerializeField]
    private float _buffDuration;
    public float BuffDuration
    {
        get { return _buffDuration; }
        set { _buffDuration = value; }
    }

    [SerializeField]
    private float _speedMultiplier;
    public float SpeedMultiplier
    {
        get { return _speedMultiplier; }
        set { _speedMultiplier = value; }
    }

    protected override void NormalSkill(Transform playerTransform)
    {
        MovementSpeedBuffScript mvSpeedBuff = playerTransform.gameObject.AddComponent<MovementSpeedBuffScript>();
        mvSpeedBuff.ChampStatScript = ChampStatsScript;
        mvSpeedBuff.Duration = BuffDuration;
        mvSpeedBuff.SpeedMultiplier = SpeedMultiplier;
    }
}
