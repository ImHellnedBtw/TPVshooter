using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public abstract class WeaponBase : MonoBehaviour, IUsable
{
    
    public WeaponData ActualWeaponData;
    public Transform MuzzleExit;
    public GameObject Bullet;
    public Transform gunPlace;
    public Image ScopeImage;
    protected Transform Tarrget;
    protected bool usedNow;
    protected Vector3 startPos;
    protected Quaternion startRot;
    protected float waitTimeReload;
    protected float waitTimeShoot;
    protected bool inScope;
    protected int currentNumberOfBullets;
    protected CameraController cameraController;
    protected DefaultCharacter characterInfo;
    protected GameManager gameManager;

    public virtual void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        cameraController = GameObject.Find("Camera Controller").GetComponent<CameraController>();
        characterInfo = CharacterBase.Character.GetComponent<DefaultCharacter>();
        startPos = transform.localPosition;
        startRot = transform.localRotation;
        currentNumberOfBullets = ActualWeaponData.magSize;
    }
    public virtual void Update()
    {
        if(usedNow == false)
        {
            transform.localPosition = startPos;
            transform.localRotation = startRot;
        }else
        {
            transform.position = gunPlace.position;
            transform.LookAt(gunPlace.position + gunPlace.forward);
            gameManager.VisualBullet(currentNumberOfBullets);

        }
    }

    //interface methods
    public void Rearm()
    {
        RearmWeapon();
    }

    public void Reload()
    {
        ReloadWeapon();
    }

    public void Scope()
    {
        UseScope();
    }

    public void Shoot()
    {
        ShootBullet();
    }

    //methods
    public virtual void ShootBullet()
    {
        if (usedNow == true && waitTimeReload < Time.time && currentNumberOfBullets > 0 && waitTimeShoot < Time.time)
        {
            if ((ActualWeaponData.canRunAndShoot == false && characterInfo.running == false) || ActualWeaponData.canRunAndShoot == true)
            {
                GameObject newBullet = Instantiate(Bullet, MuzzleExit.transform.position, MuzzleExit.transform.rotation);
                newBullet.GetComponent<CharacterBullet>().direction = (characterInfo.targetPos.position - MuzzleExit.transform.position).normalized;
                newBullet.GetComponent<CharacterBullet>().damage = ActualWeaponData.damage;
                waitTimeShoot = Time.time + ActualWeaponData.shootingSpeed;
                currentNumberOfBullets--;
            }
        }
    }
    public virtual void UseScope()
    {
        if(ActualWeaponData.canScope == true && usedNow == true) 
        {
            cameraController.ChangeCamera(ActualWeaponData.sniperScope);
            inScope = !inScope;
        }
    }
    public virtual void ReloadWeapon()
    {
        currentNumberOfBullets = ActualWeaponData.magSize;
        waitTimeReload = Time.time + ActualWeaponData.reloadSpeed;
    }
    public virtual void RearmWeapon()
    {
        if (inScope == true)
        {
            cameraController.ChangeCamera(ActualWeaponData.sniperScope);
            inScope = false;
        }
        usedNow = !usedNow;
    }
}
