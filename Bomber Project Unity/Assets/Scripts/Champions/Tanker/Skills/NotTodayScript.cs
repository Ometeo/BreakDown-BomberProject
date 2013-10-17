using UnityEngine;
using System.Collections;

public class NotTodayScript : SkillScript {
    public override void useSkill(Transform playerTransform)
    {
        if (!IsSkillActivated())
            return;

        playerTransform.GetComponentInChildren<ChampionsStatsScript>().LifePoint += 1;
    }
}
