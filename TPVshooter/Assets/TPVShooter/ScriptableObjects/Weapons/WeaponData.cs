using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponData", menuName = "Weapon Data")]
public class WeaponData : ScriptableObject
{
    public bool canRunAndShoot;
    public bool canScope;
    public bool sniperScope;
    public int magSize;
    public float damage;
    public float reloadSpeed;
    public float shootingSpeed;
}
