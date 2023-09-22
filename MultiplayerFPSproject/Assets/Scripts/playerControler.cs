using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControler : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody PlayerRidg;

    public float MovementSpeed;
    public float MaxMovementSpeed;
    public bool TouchingGround;
    public float AirDrag;
    public float JumpStrength;
    public GameObject PlayerCamera;
    public float MouseSens;

    [Header("Guns")]
    public GameObject[] CurrentWeapons;
    public GameObject barrel;
    public GameObject ammo;
    public WeaponScript Weapion;
    private bool Shot;
    void Start()
    {
        PlayerRidg = gameObject.GetComponent<Rigidbody>();
        Weapion = CurrentWeapons[0].GetComponent<WeaponScript>();
        Shot = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if(Shot == false)
            {
            StartCoroutine(Shooting(Weapion.FireRate,Weapion.Damage,Weapion.MuzzleVelocity,Weapion.AmmoType));
            }
        }
        //Locking the mouse
        Cursor.lockState = CursorLockMode.Locked;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        //Player Camera Controls
        gameObject.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * MouseSens, 0));
        PlayerCamera.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * MouseSens, 0, 0));


        //Player Jump
        if (Input.GetKeyDown(KeyCode.Space) && TouchingGround)
        {
            PlayerRidg.drag = AirDrag;
            PlayerRidg.AddForce(Vector3.up * JumpStrength, ForceMode.VelocityChange);

            TouchingGround = false;
        }

        //PlayerMovement 
        if (TouchingGround == true)
        {
            if (Input.GetKey(KeyCode.A))
            {
                PlayerRidg.AddForce(-transform.right * MovementSpeed, ForceMode.Force);
            }
            if (Input.GetKey(KeyCode.D))
            {
                PlayerRidg.AddForce(transform.right * MovementSpeed, ForceMode.Force);
            }
            if (Input.GetKey(KeyCode.W))
            {
                PlayerRidg.AddForce(transform.forward * MovementSpeed, ForceMode.Force);
            }
            if (Input.GetKey(KeyCode.S))
            {
                PlayerRidg.AddForce(-transform.forward * MovementSpeed, ForceMode.Force);
            }
        } else {
            if (Input.GetKey(KeyCode.A))
            {
                PlayerRidg.AddForce(-transform.right * MovementSpeed / 5, ForceMode.Force);
            }
            if (Input.GetKey(KeyCode.D))
            {
                PlayerRidg.AddForce(transform.right * MovementSpeed / 5, ForceMode.Force);
            }
            if (Input.GetKey(KeyCode.W))
            {
                PlayerRidg.AddForce(transform.forward * MovementSpeed / 5, ForceMode.Force);
            }
            if (Input.GetKey(KeyCode.S))
            {
                PlayerRidg.AddForce(-transform.forward * MovementSpeed / 5, ForceMode.Force);
            }
        }
        //limits the players speed
        if (PlayerRidg.velocity.magnitude >= MaxMovementSpeed)
        {
            PlayerRidg.velocity = PlayerRidg.velocity.normalized * MaxMovementSpeed;
        }
    }
    public IEnumerator Shooting(int Rpm,float Damage,int MuzzleVelocity,WeaponScript.BulletType Ammotypes)
    {
        Shot = true;
        var Bullet = Instantiate(ammo, barrel.transform.position,transform.rotation, null).GetComponent<BulletScript>();
        Bullet.Damage = Damage;
        Bullet.HeadShotMult = 2;
        Bullet.Muzzelvelocity = MuzzleVelocity;
        Bullet.ShotType = Ammotypes;
       yield return new WaitForSeconds(60 / Rpm);
        Shot = false;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            TouchingGround = true;
            PlayerRidg.drag = 5;
        }
    }
    }
