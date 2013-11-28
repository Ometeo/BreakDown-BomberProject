using UnityEngine;
using System.Collections;

public class ClassicBombScript : MonoBehaviour {

    [SerializeField]
    LayerMask _everythingButPlayerMask;
    public LayerMask EverythingButPlayerMask
    {
        get { return _everythingButPlayerMask; }
        set { _everythingButPlayerMask = value; }
    }

    [SerializeField]
    private BombsPoolScript _bombsPoolScr;
    public BombsPoolScript BombsPoolScr
    {
        get { return _bombsPoolScr; }
        set { _bombsPoolScr = value; }
    }

    private ChampionsStatsScript _champStatsScr;
    public ChampionsStatsScript ChampStatsScr
    {
        get
        {
            if (_champStatsScr == null)
                ChampStatsScr = GetComponentInChildren<ChampionsStatsScript>();
            return _champStatsScr;
        }
        set { _champStatsScr = value; }
    }

    public bool UseBomb(Transform playerTransform)
    {
        var onGridPos = new Vector3(Mathf.Round(playerTransform.position.x), playerTransform.position.y, Mathf.Round(playerTransform.position.z));
        if (Network.isServer)
        {
            if (BombScript.IsTileEmpty(onGridPos, EverythingButPlayerMask) && ChampStatsScr.NbBombs > 0)
            {
                BombsPoolScr.PlaceNextBomb(playerTransform, onGridPos);
                ChampStatsScr.NbBombs--;
                return true;
            }
            else
                return false;
        }
        ChampStatsScr.NbBombs--;
        BombsPoolScr.PlaceNextBomb(playerTransform, onGridPos);
        return true;
    }
}
