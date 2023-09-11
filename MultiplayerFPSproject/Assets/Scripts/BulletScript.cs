using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float Damage;
    public float HeadShotMult;
    public int Muzzelvelocity;
    public void Update()
    {
        transform.Translate(transform.forward.normalized * Muzzelvelocity * Time.deltaTime);
    }
    public void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
