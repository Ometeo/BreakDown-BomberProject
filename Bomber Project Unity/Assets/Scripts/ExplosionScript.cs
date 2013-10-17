using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(DestroyExplosion());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator DestroyExplosion()
    {
        yield return new WaitForSeconds(0.6f);
        Destroy(this.gameObject);
    }
}
