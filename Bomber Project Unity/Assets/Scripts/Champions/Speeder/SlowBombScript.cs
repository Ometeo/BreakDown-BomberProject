using UnityEngine;
using System.Collections;

public class SlowBombScript : MonoBehaviour {

    [SerializeField]
    private Transform _nova;
    public Transform Nova
    {
        get { return _nova; }
        set { _nova = value; }
    }

    [SerializeField]
    private float _timeToLive;
    public float TimeToLive
    {
        get { return _timeToLive; }
        set { _timeToLive = value; }
    }

    private float _timeExpandingLeft;
    public float TimeExpandingLeft
    {
        get { return _timeExpandingLeft; }
        set { _timeExpandingLeft = value; }
    }

    [SerializeField]
    private float _timeToExpand;
    public float TimeToExpand
    {
        get { return _timeToExpand; }
        set { _timeToExpand = value; }
    }

    [SerializeField]
    private float _maxRange;
    public float MaxRange
    {
        get { return _maxRange; }
        set { _maxRange = value; }
    }

    [SerializeField]
    private float _slowDuration;
    public float SlowDuration
    {
        get { return _slowDuration; }
        set { _slowDuration = value; }
    }

    [SerializeField]
    private float _slowPercentage;
    public float SlowPercentage
    {
        get { return _slowPercentage; }
        set { _slowPercentage = value; }
    }

    private bool _bombHasExploded = false;

	void Start () {
        TimeExpandingLeft = TimeToExpand;
        StartCoroutine("BombCountDown");
	}
	
	void Update () {
        if (_bombHasExploded)
        {
            TimeExpandingLeft -= Time.deltaTime;
            float sizeValue = Mathf.Clamp(TimeToExpand / TimeExpandingLeft, 0f, MaxRange);
            Nova.transform.localScale = new Vector3(sizeValue, 0.5f, sizeValue);
            if (TimeExpandingLeft < 0.5f && TimeExpandingLeft > 0f)
            {
                Color c = Nova.renderer.material.color;
                Nova.renderer.material.color = new Color(c.r, c.g, c.b, TimeExpandingLeft);
            }
            else if (TimeExpandingLeft <= 0f)
            {
                Destroy(this.gameObject);
            }
        }
	}

    IEnumerator BombCountDown()
    {
        yield return new WaitForSeconds(TimeToLive);
        Destruction();
    }

    void Destruction()
    {
        Nova.gameObject.SetActive(true);
        _bombHasExploded = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.LogError(col.name);
            GameObject player = col.transform.parent.gameObject;
            MovementSpeedBuffScript mvSpeedBuff = player.AddComponent<MovementSpeedBuffScript>();
            mvSpeedBuff.Duration = SlowDuration;
            mvSpeedBuff.ChampStatScript = player.GetComponentInChildren<ChampionsStatsScript>();
            mvSpeedBuff.SpeedMultiplier = SlowPercentage;
        }
    }
}
