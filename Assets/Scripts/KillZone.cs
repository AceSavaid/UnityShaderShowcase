using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    //[SerializeField] string name = "";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            other.gameObject.GetComponent<Player>().Die();
            
        }
    }
}
