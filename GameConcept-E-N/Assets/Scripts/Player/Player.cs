using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float health;
    private float maxHealth;
    [SerializeField] private GameObject p;
    [SerializeField] private Image healthBar;
    private bool player = true;

    [Header("Damage Overlay")]
    [SerializeField] Image overlay;
    public float duration;
    public float fadeSpeed;
    private float durationTimer;

    private void Awake()
    {
        maxHealth = health;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    void Update()
    {
        if(overlay.color.a > 0)
        {
            if (health < 30) return;
            durationTimer += Time.deltaTime;
            if(durationTimer > duration)
            {
                // fade the image
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }

    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / maxHealth;
        durationTimer = 0.0f;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);

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

    public void Die()
    {
        LevelManager.instance.GameOver();
        p.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public bool getPlayer()
    {
        return player;
    }
}
