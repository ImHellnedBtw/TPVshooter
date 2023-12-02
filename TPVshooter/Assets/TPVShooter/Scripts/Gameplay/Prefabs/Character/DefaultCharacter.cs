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
    private GameObject placeForCam;
    [SerializeField]
    private GameObject legsFocal;
    [SerializeField]
    private GameObject back;
    [SerializeField]
    private LayerMask aimLayerMask;

    private int useGun;
    private GameObject currentGun;
    private Animator animator;
    private Rigidbody characterRb;
    private int verticalInput;
    private int horizontalInput;
    private Vector3 lastLegsPos;
    private GameManager gameManager;

    [HideInInspector]public float health;
    [HideInInspector]public bool running;
    [HideInInspector] public Transform targetPos;
    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        health = 100;
        animator = GetComponent<Animator>();
        characterRb = GetComponent<Rigidbody>();
        useGun = 0;
        currentGun = guns[useGun];
        currentGun.GetComponent<IUsable>().Rearm();
        gameManager.VisualWeapon(useGun);
    }
    private void Update()
    {
        health = Mathf.Clamp(health, 0, 100);
        gameManager.VisualHealth(health);
        if (health == 0)
        {
            gameManager.LoseGame();
        }
    }
    public override void InteractWeapon(bool FirstW, bool SecondW, bool ThirdW, bool Reload)
    {
        //choose gun
        if (FirstW == true)
        {
            useGun = 0;
            currentGun.GetComponent<IUsable>().Rearm();
            currentGun = guns[useGun];
            currentGun.GetComponent<IUsable>().Rearm();
        }
        if (SecondW == true)
        {
            useGun = 1;
            currentGun.GetComponent<IUsable>().Rearm();
            currentGun = guns[useGun];
            currentGun.GetComponent<IUsable>().Rearm();
        }
        if (ThirdW == true)
        {
            useGun = 2;
            currentGun.GetComponent<IUsable>().Rearm();
            currentGun = guns[useGun];
            currentGun.GetComponent<IUsable>().Rearm();
        }
        gameManager.VisualWeapon(useGun);

        //reload
        if (Reload == true)
        {
            currentGun.GetComponent<IUsable>().Reload();
            animator.SetTrigger("Reload");
        }
    }
    public override void Aim(float Vertical, float Horizontal)
    {
        transform.Rotate(Vector3.up * Vertical * rotateSpeed);
        placeForCam.transform.Rotate(Vector3.right * -Horizontal * rotateSpeed);
        Vector3 angles = placeForCam.transform.eulerAngles;
        if (placeForCam.transform.eulerAngles.x < 110 && placeForCam.transform.eulerAngles.x > 0)
            placeForCam.transform.localEulerAngles = new Vector3(110, transform.eulerAngles.y, 0f);
        if (placeForCam.transform.eulerAngles.x >290 && placeForCam.transform.eulerAngles.x < 360)
            placeForCam.transform.localEulerAngles = new Vector3(290, transform.eulerAngles.y, 0f);
        placeForCam.transform.eulerAngles = new Vector3(angles.x, transform.eulerAngles.y, 0f);
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
        if(verticalInput != 0 || horizontalInput != 0)
            animator.SetBool("Walk", true);
        else
            animator.SetBool("Walk", false);
        if (Shift == true)
        {
            velocity.z *= 2;
            velocity.x *= 2;
            animator.SetBool("Run", true);
            running = true;
        }
        else
        {
            animator.SetBool("Run", false);
            running = false;
        }

        characterRb.velocity = transform.TransformDirection(velocity);
        Vector3 changedVelocity = new Vector3(velocity.x, velocity.y, -velocity.z);
        if (changedVelocity.x != 0 || changedVelocity.z != 0)
        {
            legsFocal.transform.eulerAngles = new Vector3(0, Vector3.SignedAngle(changedVelocity, transform.forward, transform.up), 0);
            lastLegsPos = legsFocal.transform.eulerAngles;
        }
        else
            legsFocal.transform.eulerAngles = lastLegsPos;

        back.transform.LookAt(targetPos);

    }
    public override void Shoot(bool LeftMouse, bool RightMouse)
    {
        Vector2 screenCenter = new Vector2(Screen.width /2, Screen.height /2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray,out RaycastHit raycastHit , 101f , aimLayerMask ))
        { 
            targetPos.position = raycastHit.point;
        }
        if (LeftMouse == true )
            currentGun.GetComponent<IUsable>().Shoot();
        else
        if (RightMouse == true)
            currentGun.GetComponent<IUsable>().Scope();
    }
}
