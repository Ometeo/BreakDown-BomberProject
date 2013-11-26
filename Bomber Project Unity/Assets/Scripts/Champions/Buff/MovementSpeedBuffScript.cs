using UnityEngine;
using System.Collections;

public class MovementSpeedBuffScript : BuffScript {

    private ChampionsStatsScript _champStatScript;
    public ChampionsStatsScript ChampStatScript
    {
        get { return _champStatScript; }
        set { _champStatScript = value; }
    }

    private float _speedMultiplier;
    public float SpeedMultiplier
    {
        get { return _speedMultiplier; }
        set { _speedMultiplier = value; }
    }

    void Start()
    {
        ChampStatScript.MovementSpeed *= SpeedMultiplier;
    }

    public override void RemoveBuff()
    {
        ChampStatScript.MovementSpeed *= (1 / SpeedMultiplier);
        Destroy(this);
    }
}
