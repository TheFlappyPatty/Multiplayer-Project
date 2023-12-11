using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControler : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController characterController;
    private Rigidbody PlayerRidg;

    public float MovementSpeed;
    public float MaxMovementSpeed;
    public float gravitymulti;
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
        characterController = gameObject.GetComponent<CharacterController>();
        PlayerRidg = gameObject.GetComponent<Rigidbody>();
        Weapion = CurrentWeapons[0].GetComponent<WeaponScript>();
        Shot = false;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(characterController.isGrounded);

        //PlayerMovement 
        Vector3 MoveDir = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        //Player Jump
        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            MoveDir.y += JumpStrength;
        }
        MoveDir += Physics.gravity * gravitymulti;
        characterController.Move(MoveDir * MovementSpeed * Time.deltaTime);


        //shoots the currently held gun
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if(Shot == false)
            {
            StartCoroutine(Shooting(Weapion.FireRate,Weapion.Damage,Weapion.MuzzleVelocity,Weapion.AmmoType));
            }
        }


        //Locking the mouse and Mouse Controls
        Cursor.lockState = CursorLockMode.Locked;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        gameObject.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * MouseSens, 0));
        PlayerCamera.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * MouseSens, 0, 0));





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
}
