using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectiles : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSpeed = 2f;
    [SerializeField] float fireRate = 1f;
    float fireTimer;
    [SerializeField] bool shootAtPlayer = false;
    [SerializeField] bool shootRange = false;

    private void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate && shootRange)
        {
            Shoot();
            fireTimer -= fireRate;
        }
        
    }

    private void Shoot()
    {
        GameObject b = Instantiate(bullet, gameObject.transform);
        if (shootAtPlayer)
        {
            //b.transform.rotation = Quaternion.Euler( Vector3.RotateTowards(b.transform.position, FindObjectOfType<Player>().transform.position, 180f, 180f));
            
        }
        b.GetComponent<Rigidbody>().AddForce(bulletSpeed * gameObject.transform.forward);
        b.transform.parent = null;
        Destroy(b, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            shootRange = true;
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            shootRange = false;

        }
    }
}
