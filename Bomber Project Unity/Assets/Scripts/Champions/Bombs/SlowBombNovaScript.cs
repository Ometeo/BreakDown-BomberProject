using UnityEngine;
using System.Collections;

public class SlowBombNovaScript : MonoBehaviour {

    [SerializeField]
    private SlowBombScript _slowBombScr;
    public SlowBombScript SlowBombScr
    {
        get { return _slowBombScr; }
        set { _slowBombScr = value; }
    }

    private Transform _transform;

    void Start()
    {
        _transform = this.transform;
    }

    void Update()
    {
        if (SlowBombScr.BombHasExploded)
        {
            SlowBombScr.TimeExpandingLeft -= Time.deltaTime;
            float sizeValue = Mathf.Clamp(SlowBombScr.TimeToExpand / SlowBombScr.TimeExpandingLeft, 0f, SlowBombScr.MaxRange);
            _transform.localScale = new Vector3(sizeValue, 0.5f, sizeValue);
            if (SlowBombScr.TimeExpandingLeft < 0.5f && SlowBombScr.TimeExpandingLeft > 0f)
            {
                Color c = renderer.material.color;
                renderer.material.color = new Color(c.r, c.g, c.b, SlowBombScr.TimeExpandingLeft);
            }
            else if (SlowBombScr.TimeExpandingLeft <= 0f)
            {
                ApplySlow();
                Destroy(this.gameObject);
            }
        }
    }

    void ApplySlow()
    {
        Collider[] playersCol = Physics.OverlapSphere(this.transform.position, SlowBombScr.MaxRange, SlowBombScr.PlayerLayer);

        foreach (var playerCol in playersCol)
        {
            GameObject player = playerCol.transform.parent.gameObject;
            MovementSpeedBuffScript mvSpeedBuff = player.AddComponent<MovementSpeedBuffScript>();
            mvSpeedBuff.Duration = SlowBombScr.SlowDuration;
            mvSpeedBuff.ChampStatScript = player.GetComponentInChildren<ChampionsStatsScript>();
            mvSpeedBuff.SpeedMultiplier = SlowBombScr.SlowPercentage;
        }
    }
}
