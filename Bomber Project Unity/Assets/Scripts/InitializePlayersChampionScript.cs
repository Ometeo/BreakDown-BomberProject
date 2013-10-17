using UnityEngine;
using System.Collections;

public class InitializePlayersChampionScript : MonoBehaviour {

    [SerializeField]
    private MeshRenderer _bodyMeshRenderer;
    public MeshRenderer BodyMeshRenderer
    {
        get { return _bodyMeshRenderer; }
        set { _bodyMeshRenderer = value; }
    }
    

    [SerializeField]
    private MeshRenderer _headMeshRenderer;
    public MeshRenderer HeadMeshRenderer
    {
        get { return _headMeshRenderer; }
        set { _headMeshRenderer = value; }
    }

    public void OrderInitializeChampion(Color skinColor)
    {
        if (Network.isServer)
            networkView.RPC("InitializeChampion", RPCMode.AllBuffered, skinColor.r, skinColor.g, skinColor.b);
    }

    [RPC]
    void InitializeChampion(float red, float green, float blue)
    {
        Color skinColor = new Color(red, green, blue);
        BodyMeshRenderer.material.color = skinColor;
        HeadMeshRenderer.material.color = skinColor;
    }
}
