using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BulletBase
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
            other.GetComponent<DefaultCharacter>().health -= damage;
        if (!other.gameObject.CompareTag("Enemy"))
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
