using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public playerControler PlayerControler;
    public BulletType AmmoType;
    public int Damage;
    public int FireRate;
    public int MuzzleVelocity;
    public void OnPickUp()
    {
       PlayerControler = transform.GetComponentInParent<Transform>().GetComponentInParent<playerControler>();
    }
    public enum BulletType
    {
        Standerd,
        Fire,
        Explosive,
        Laser,
        HV,
        AP,
        Crossbowbolt,
        Arrow,
    }
}
