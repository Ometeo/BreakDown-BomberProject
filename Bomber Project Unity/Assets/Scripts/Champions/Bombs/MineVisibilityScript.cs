using UnityEngine;
using System.Collections;

public class MineVisibilityScript : MonoBehaviour {

    [SerializeField]
    private Animation _visibilityAnim;
    public Animation VisibilityAnim
    {
        get { return _visibilityAnim; }
        set { _visibilityAnim = value; }
    }

    private Collider _currentPlayerCollider;
    private int nbCollisionEnter;

    void Start()
    {
        if (Network.isClient)
            enabled = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (nbCollisionEnter == 0)
        {
            NetworkPlayer player = PlayersSingleton.Instance.GetPlayer(col.transform.parent);
            networkView.RPC("ShowMine", player);
            ShowMine();
        }
        nbCollisionEnter++;
    }

    void OnTriggerExit(Collider col)
    {
        nbCollisionEnter--;
        if (nbCollisionEnter == 0)
        {
            NetworkPlayer player = PlayersSingleton.Instance.GetPlayer(col.transform.parent);
            networkView.RPC("HideMine", player);
            HideMine();
        }
    }

    [RPC]
    void ShowMine()
    {
        foreach (AnimationState state in VisibilityAnim.animation)
        {
            state.speed = 1f;
            state.time = Mathf.Clamp(state.time, 0f, state.length);
        }
        VisibilityAnim.Play();
        
    }

    [RPC]
    void HideMine()
    {
        foreach (AnimationState state in VisibilityAnim.animation)
        {
            state.speed = -1f;
            state.time = Mathf.Clamp(state.time, 0f, state.length);
        }
        VisibilityAnim.Play();
    }
}
