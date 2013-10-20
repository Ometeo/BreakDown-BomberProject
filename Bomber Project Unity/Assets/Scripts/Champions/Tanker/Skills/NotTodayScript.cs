using UnityEngine;
using System.Collections;

public class NotTodayScript : SkillScript {
    public override bool useSkill(Transform playerTransform)
    {
        if (IsSkillActivated())
        {
            playerTransform.GetComponentInChildren<ChampionsStatsScript>().LifePoint += 1;
        }
        return false;
    }
}
