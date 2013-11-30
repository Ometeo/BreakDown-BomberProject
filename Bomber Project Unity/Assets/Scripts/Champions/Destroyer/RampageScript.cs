using UnityEngine;
using System.Collections;

public class RampageScript : SkillScript {

	void Awake () {
        ChampStatsScript.ExplDirection = new[]{BombScript.ExplosionDirections.Horizontal, BombScript.ExplosionDirections.Vertical, BombScript.ExplosionDirections.DiagonaleDroite, BombScript.ExplosionDirections.DiagonaleGauche};
	}
}
