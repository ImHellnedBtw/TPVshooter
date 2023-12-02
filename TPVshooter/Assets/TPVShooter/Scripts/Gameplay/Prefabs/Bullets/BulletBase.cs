using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBase: MonoBehaviour
{
    [HideInInspector]public Vector3 direction;
    [HideInInspector]public float damage;
    [SerializeField]
    protected ParticleSystem startShoot;
    [SerializeField]
    protected ParticleSystem endShoot;
    protected Rigidbody rb;
    protected Vector3 startPos;
    protected Vector3 endPos;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        startPos = rb.position;
        startShoot.Play();
    }
    private void Update()
    {
        startShoot.transform.position = startPos;
        float speed = 100;
        rb.velocity = direction *speed;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            other.GetComponent<EnemyBase>().health -= damage;
        if (!other.gameObject.CompareTag("Character"))
        {
            endPos = transform.position;
            endShoot.Play();
            StartCoroutine(destroyEffect());
        }
    }
    IEnumerator destroyEffect()
    {
        while (endShoot.isPlaying == true)
        {
            endShoot.transform.position = endPos;
            yield return null;
        }

        Destroy(this.gameObject);
    }
}
