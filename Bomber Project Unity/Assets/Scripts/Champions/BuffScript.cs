using UnityEngine;
using System.Collections;

public abstract class BuffScript : MonoBehaviour {

    private float _duration;
    public float Duration
    {
        get { return _duration; }
        set { _duration = value; }
    }

    void Update()
    {
        if (Duration > 0)
            Duration -= Time.deltaTime;
        else
            RemoveBuff();
    }

    public virtual void RemoveBuff()
    {
        Destroy(this);
    }
}
