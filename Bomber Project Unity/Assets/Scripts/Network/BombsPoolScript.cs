using UnityEngine;
using System.Collections;

public class BombsPoolScript : MonoBehaviour {

    [SerializeField]
    private Transform[] _bombs;	
    public Transform[] Bombs
    {
      get { return _bombs; }
      set { _bombs = value; }
    }

    private Stack _bombsStack;
    private ArrayList _bombsOnField;
    private Transform _transform;

    void Start()
    {
        _bombsStack = new Stack();
        foreach (Transform bomb in Bombs)
            _bombsStack.Push(bomb);
        _bombsOnField = new ArrayList();
        _transform = this.transform;
    }

    public void PlaceNextBomb(Transform playerTransform, Vector3 bombPosition)
    {
        Transform bomb = (Transform)_bombsStack.Pop();
        _bombsOnField.Add(bomb);

        var champStatsScript = playerTransform.GetComponentInChildren<ChampionsStatsScript>();
        var bombScript = bomb.GetComponent<BombScript>();
        
        bombScript.ExplDirection = champStatsScript.ExplDirection;
        bombScript.PlayerTransform = playerTransform;
        bomb.parent = null;
        bomb.position = bombPosition;
        bomb.gameObject.SetActive(true);
    }

    void Update()
    {
        for (int bombNb = 0; bombNb < _bombsOnField.Count - 1; bombNb++)
        {
            Transform bomb = (Transform) _bombsOnField[bombNb];
            if (!bomb.gameObject.activeSelf)
            {                
                bomb.parent = _transform;
                bomb.position = Vector3.zero;
                BombScript bombScript = bomb.GetComponent<BombScript>();
                bombScript.PlayerTransform.GetComponentInChildren<ChampionsStatsScript>().NbBombs++;
                _bombsStack.Push(bomb);
                _bombsOnField.Remove(bomb);
            }
        }
    }
}
