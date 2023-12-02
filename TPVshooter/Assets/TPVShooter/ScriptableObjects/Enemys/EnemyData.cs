using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public bool movable;
    public bool flying;
    public float flyingHeight;
    public float speed;
    public float shootSpeed;
    public float damage;
    public float health;
    public float detectDistance;
}
