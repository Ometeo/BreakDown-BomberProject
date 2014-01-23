using UnityEngine;
using System.Collections;

public class TimerScript : MonoBehaviour {

    [SerializeField]
    private ReadyScript _rdyScr;
    public ReadyScript RdyScr
    {
        get { return _rdyScr; }
        set { _rdyScr = value; }
    }

    [SerializeField]
    private TextMesh _timerLeftTextMesh;
    public TextMesh LeftTimerTextMesh
    {
        get { return _timerLeftTextMesh; }
        set { _timerLeftTextMesh = value; }
    }

    [SerializeField]
    private TextMesh _timerRightTextMesh;
    public TextMesh RightTimerTextMesh
    {
        get { return _timerRightTextMesh; }
        set { _timerRightTextMesh = value; }
    }

    [SerializeField]
    private float _defaultTimerValue;
    public float DefaultTimerValue
    {
        get { return _defaultTimerValue; }
        set { _defaultTimerValue = value; }
    }

    private float _timer;

    void Start()
    {
        _timer = DefaultTimerValue;
        UpdateTimers();
    }

    void FixedUpdate()
    {
        _timer -= Time.deltaTime;
        UpdateTimers();
        if (_timer <= 0 && Network.isServer)
            RdyScr.StartGame();
    }

    private void UpdateTimers()
    {
        LeftTimerTextMesh.text = ((int)(_timer)).ToString();
        RightTimerTextMesh.text = ((int)(_timer)).ToString();
    }


}
