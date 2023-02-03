using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon", fileName = "Weapon", order = 2)]
public class Weapon : ScriptableObject
{
    public GameObject projectile;
    public float bulletSpeed = 5.0f;
    public float bulletDamage = 2.0f;
    public float ReloadTime = 1.5f;
    
}
