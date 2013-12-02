using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    [SerializeField]
    private string _disconnectedLevel;
    public string DisconnectedLevel
    {
        get { return _disconnectedLevel; }
        set { _disconnectedLevel = value; }
    }

    void Awake ()
    {
        GameOptionSingleton.Instance.SceneMngScript = this;
        DontDestroyOnLoad(this);
    }

    public void LoadLevel(string level)
    {
        if (Network.isServer)
        {
            Network.RemoveRPCsInGroup(0);
            Network.RemoveRPCsInGroup(1);
        }
        LoadLevel(level, 1);
    }

    void LoadLevel(string level, int levelPrefix)
    {
		Network.SetSendingEnabled(0, false);	
		Network.isMessageQueueRunning = false;

		Network.SetLevelPrefix(levelPrefix);
		Application.LoadLevel(level);

		Network.isMessageQueueRunning = true;
		Network.SetSendingEnabled(0, true);
    }
}
