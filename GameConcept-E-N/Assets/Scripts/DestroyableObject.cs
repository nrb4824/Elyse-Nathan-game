using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class DestroyableObject : MonoBehaviour
{
    public GameObject impactEffect;

    public void Die(RaycastHit hit)
    {
        GameObject impactGameObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impactGameObject, 2f);
        Destroy(gameObject);
    }
}