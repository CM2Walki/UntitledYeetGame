using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YeetSpin : MonoBehaviour
{
    public bool Spin;
    // Update is called once per frame
    void Update()
    {
        var newRot = Quaternion.Euler(transform.rotation.x + Random.Range(-180, 180), transform.rotation.y + Random.Range(-180, 180), transform.rotation.z + Random.Range(-180, 180));
        transform.rotation = newRot;
    }
}
