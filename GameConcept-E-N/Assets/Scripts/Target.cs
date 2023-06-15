using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;

public class Target : MonoBehaviour
{
    private float health;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject healthObject;
    [SerializeField] private Transform cam;
    [SerializeField] private GameObject explosionEffect;

    private float maxHealth;

    private void Start()
    {
        health = BeeEnemySettings.Health;
        maxHealth = health;
    }
    private void LateUpdate()
    {
        healthObject.transform.LookAt(cam);
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / maxHealth;
        if(health <= 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0 , maxHealth);

        healthBar.fillAmount = health / maxHealth;
    }

    public void Die()
    {
        GameObject impactGameObject = Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(impactGameObject, 2f);
        Destroy(gameObject);
        Destroy(healthBar.gameObject);
    }

    public void setCamera(Transform cam)
    {
        this.cam = cam;
    }
}
