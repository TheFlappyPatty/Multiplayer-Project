using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float Damage;
    public float HeadShotMult;
    public int Muzzelvelocity;
    public WeaponScript.BulletType ShotType;
    public void Awake()
    {
        StartCoroutine(BulletDecay());
    }
    public void Update()
    {
        transform.Translate(Vector3.forward * Muzzelvelocity * Time.deltaTime);
    }
    public void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
    public IEnumerator BulletDecay()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
