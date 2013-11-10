using UnityEngine;
using System.Collections;

public class NotTodayScript : SkillScript {

    protected override void NormalSkill(Transform playerTransform)
    {       
        if (ChampStatsScript.LifePoint < ChampStatsScript.DefaultLifePoint)
            ChampStatsScript.LifePoint += 1;
    }
}
