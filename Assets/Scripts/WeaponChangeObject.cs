using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChangeObject : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            if (weapon)
            {
                other.gameObject.GetComponent<Player>().ChangeWeapon(weapon);
            }
            else
            {
                Debug.LogWarning("No weapon in Weapon Change Orb");
            }
            
        }
    }
}
