using System;
using System.Runtime;
using UnityEngine;

public class Gun : MonoBehaviour
{

    private float damage = 10f;
    private float range = 500f;
    private float impactForce = 30f;
    private float fireRate = 2f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;  //stores all of the bullet info
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);    //remove eventually

            Target target = hit.transform.GetComponent<Target>();

            //if object shot is a target(has health)
            if(target != null)
            {
                target.TakeDamage(damage);
            }

            //force if object is hit. Will be important when we add more physics or for small objects.
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            //instantiates and destroys bullet impact effect.
            GameObject impactGameObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGameObject, 2f);
        }
    }
}
