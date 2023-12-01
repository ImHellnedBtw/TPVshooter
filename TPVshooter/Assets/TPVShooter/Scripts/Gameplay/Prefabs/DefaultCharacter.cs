using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCharacter : CharacterBase
{
    [SerializeField]
    private List<GameObject> guns;
    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private GameObject head;
    private int useGun;
    private GameObject currentGun;
    private Animator animator;
    private Rigidbody characterRb;
    private int verticalInput;
    private int horizontalInput;
    private void Start()
    {
        animator = GetComponent<Animator>();
        characterRb = GetComponent<Rigidbody>();
    }
    public override void InteractWeapon(bool FirstW, bool SecondW, bool ThirdW, bool Reload)
    { 
        //choose gun
        if (FirstW == true)
            useGun = 0;
        else
        if (SecondW == true)
            useGun = 1;
        else
        if (ThirdW == true)
            useGun = 2;
        //animator.SetInteger("gunNumber", useGun);
        //currentGun = guns[useGun];

        //reload
        //if(animator.GetCurrentAnimatorStateInfo(1).IsTag("reload") == false)
        //animator.SetBool("Reload", Reload);
    }
    public override void Aim(float Vertical, float Horizontal)
    {
        transform.Rotate(Vector3.up * Vertical * rotateSpeed);
        head.transform.Rotate(Vector3.right * Horizontal * rotateSpeed);
    }
    public override void Jump(bool Jump)
    {
        if(Jump == true)
        characterRb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }
    public override void Move(bool Forward, bool Backward, bool Left, bool Right, bool Shift)
    {
        var velocity = characterRb.velocity;
        horizontalInput = 0;
        if (Backward == true)
            horizontalInput = -1;
        if (Forward == true)
            horizontalInput = 1;
        velocity.z = movementSpeed * horizontalInput;

        verticalInput = 0;
        if (Left == true)
            verticalInput = -1;
        if (Right == true)
            verticalInput = 1;
        velocity.x = movementSpeed * verticalInput;

        if(verticalInput !=0 && horizontalInput !=0)
        {
            velocity.z = movementSpeed * horizontalInput/2;
            velocity.x = movementSpeed * verticalInput/2;
        }
        if (Shift == true)
        {
            velocity.z *= 2;
            velocity.x *= 2;
        }

        characterRb.velocity = transform.TransformDirection(velocity);


    }
    public override void Shoot(bool LeftMouse, bool RightMouse)
    {
        if (LeftMouse == true)
            Debug.Log("THAratatta");
        if (RightMouse == true)
            Debug.Log("Aiming!!!");
    }
}
