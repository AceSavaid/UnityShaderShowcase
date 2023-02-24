using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    //[SerializeField] string name = "name";
    [SerializeField] float damage = 1.0f;
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Player>().HurtPlayer(damage);
    }
}
