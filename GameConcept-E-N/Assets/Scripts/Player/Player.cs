using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float health;
    private float maxHealth;
    [SerializeField] private Image healthBar;
    private bool player = true;

    private void Awake()
    {
        maxHealth = health;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / maxHealth;
        if (health <= 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth);

        healthBar.fillAmount = health / maxHealth;
    }

    void Die()
    {
        LevelManager.instance.GameOver();
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public bool getPlayer()
    {
        return player;
    }
}
