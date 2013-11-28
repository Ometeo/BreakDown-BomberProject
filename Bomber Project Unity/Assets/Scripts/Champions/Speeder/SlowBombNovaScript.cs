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


}
