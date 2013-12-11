using UnityEngine;
using System.Collections;

public class FallScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        Debug.LogError("test1");
        if (Network.isServer)
        {
            Debug.LogError("test2");
            if (col.CompareTag("Player"))
            {
                Debug.LogError("plop");
                col.transform.parent.GetComponentInChildren<ChampionsStatsScript>().LifePoint = 0;
            }
        }
    }

}
