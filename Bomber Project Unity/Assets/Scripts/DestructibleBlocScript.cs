using UnityEngine;
using System.Collections;

public class DestructibleBlocScript : MonoBehaviour
{

    [SerializeField]
    private int _nbHP;

    public int NbHP
    {
        get
        {
            return _nbHP;
        }
        set
        {
            _nbHP = value;
        }
    }

    private Animation _destroyAnimation;


    // Use this for initialization
    void Start()
    {
        _destroyAnimation = this.gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (NbHP <= 0)
        {
            _destroyAnimation.Play();
            
            DestroyBloc();
        }
    }

    void DestroyBloc()
    {
        if(this.gameObject.transform.localScale == Vector3.zero)
            Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        NbHP--;
    }
}
