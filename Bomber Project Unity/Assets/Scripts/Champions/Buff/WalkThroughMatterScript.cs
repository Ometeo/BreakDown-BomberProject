using UnityEngine;
using System.Collections;

public class WalkThroughMatterScript : BuffScript {

    private LayerMask _layerM;
    public LayerMask LayerM
    {
        get { return _layerM; }
        set { _layerM = value; }
    }

    private Transform _transform;

    void Start()
    {
        _transform = this.transform;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Border"))
            return;

        var cpNormal = col.contacts[0].normal;
        cpNormal = new Vector3(Mathf.Round(cpNormal.x), Mathf.Round(cpNormal.y), Mathf.Round(cpNormal.z));
        var forward = -_transform.forward;

        if (cpNormal == forward)
        {
            var tilePosition = new Vector3(Mathf.Round(_transform.position.x), _transform.position.y, Mathf.Round(_transform.position.z)) + _transform.forward * 2;
            if (checkTileForObject(tilePosition))
            {
                _transform.position = tilePosition;
                RemoveBuff();
            }
        }
    }

    private bool checkTileForObject(Vector3 tilePosition)
    {
        Collider[] cols = Physics.OverlapSphere(tilePosition, 0.45f, LayerM);
        return (cols.Length == 0);
    }
}
