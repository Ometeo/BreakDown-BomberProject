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
    public bool BombHasExploded
    {
        get { return _bombHasExploded; }
        set { _bombHasExploded = value; }
    }


    [SerializeField]
    private LayerMask _playerLayer;
    public LayerMask PlayerLayer
    {
        get { return _playerLayer; }
        set { _playerLayer = value; }
    }

	void Start () {
        TimeExpandingLeft = TimeToExpand;
        StartCoroutine("BombCountDown");
	}
	
	void Update () {
        if (BombHasExploded)
        {
            Destroy(this.gameObject);
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
        Nova.parent = null;
        BombHasExploded = true;
    }


}
