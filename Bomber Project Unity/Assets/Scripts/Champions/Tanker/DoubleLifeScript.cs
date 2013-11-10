using UnityEngine;
using System.Collections;

public class DoubleLifeScript : SkillScript {

    void Awake()
    {
        ChampStatsScript.DefaultLifePoint += 1;
    }
}
