using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IEnemy
{
    public EnemyData EnemyData;
    public LayerMask EnemyLayerMask;
    public ParticleSystem destroyParticle;
    public GameObject Muzzle;
    public GameObject Bullet;
    protected Animator animator;
    protected GameObject character;
    protected bool agressive;
    protected float waitTimeShoot;
    protected Rigidbody rb;
    [HideInInspector]public float health;
    protected Vector3 lookAt;
    protected float waitTimeRotate;
    protected float randomRotateTime;
    protected bool destroyed;
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        character = CharacterBase.Character.gameObject;
        health = EnemyData.health;
        if (EnemyData.flying == true)
        {
            transform.position = new Vector3(transform.position.x, EnemyData.flyingHeight, transform.position.z);
        }
    }
    public virtual void Update()
    {
        health = Mathf.Clamp(health, 0 , EnemyData.health);
        if (health <= 0 && destroyed == false)
        {
            destroyed = true;
            destroyParticle.Play();
            StartCoroutine(DestroyEffect());
        }

        if(health < EnemyData.health)
            agressive = true;
        Mathf.Clamp(health, 0f, EnemyData.health);
        Vector3 direction = character.transform.position - transform.position;
        if (Physics.Raycast(transform.position,direction, out RaycastHit hit, 999f, EnemyLayerMask))
        {
            if (agressive == false)
            {
                if (hit.collider.gameObject == character && hit.distance < EnemyData.detectDistance)
                {
                    agressive = true;
                }
                else
                {
                    if(EnemyData.movable == true)
                    Patrol();
                }
            }
            else
            {
                lookAt = new Vector3(character.transform.position.x, transform.position.y, character.transform.position.z);
                transform.LookAt(lookAt);
                if (hit.collider.gameObject == character )
                {
                    Attack();
                }
                else if (hit.collider.gameObject != character && EnemyData.movable == true)
                {
                    rb.velocity = transform.forward * EnemyData.speed;
                }
            }

        }

    }
    public void Attack()
    {
        AttackState();
    }

    public void Patrol()
    {
        PatrolState();
    }
    public virtual void AttackState()
    {
        if (waitTimeShoot < Time.time)
        {
            GameObject newBullet = Instantiate(Bullet, Muzzle.transform.position, Muzzle.transform.rotation);
            newBullet.GetComponent<EnemyBullet>().direction = (character.transform.position - Muzzle.transform.position + new Vector3(0,1,0)).normalized;
            newBullet.GetComponent<EnemyBullet>().damage = EnemyData.damage;
            waitTimeShoot = Time.time + EnemyData.shootSpeed;
        }
    }
    public virtual void PatrolState()
    {
        rb.velocity = transform.forward * EnemyData.speed;
        if(randomRotateTime < Time.time)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Random.Range(90, 270), transform.eulerAngles.z);
            randomRotateTime = Time.time + Random.Range(5, 10);
        }

    }
    IEnumerator DestroyEffect()
    {
        while (destroyParticle.isPlaying == true)
        yield return null;
        Destroy(gameObject);
    }
}
