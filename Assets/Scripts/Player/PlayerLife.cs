using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public Slider healthSlider;
    public Image damageImage;
    public Image healingImage;
    public float fullHealth = 20;
    public float damageFlashSpeed = 2; //higher = shorter damage flash
    public float healingFlashSpeed = 4;
    public float deathGameOverDelay = 2; // seconds to wait after death before display game over screen

    private Animator anim;
    private PlayerMovement playMove; 
    private float currentHealth;

    private bool isDead;
    private bool isHurt;
    private bool isHealing;

    private Color originalDamageColor;
    private Color originalHealingColor;

    void Start()
    {
        anim = GetComponent<Animator>();
        playMove = GetComponent<PlayerMovement>();

        healthSlider.maxValue = fullHealth;
        currentHealth = fullHealth;
        healthSlider.value = currentHealth;

        damageImage.gameObject.SetActive(true);
        originalDamageColor = damageImage.color;
        damageImage.color = Color.clear;

        healingImage.gameObject.SetActive(true);
        originalHealingColor = healingImage.color;
        healingImage.color = Color.clear;
    }

    void Update()
    {
        damageImage.color = isHurt ? originalDamageColor : Color.Lerp(damageImage.color, Color.clear, damageFlashSpeed * Time.deltaTime);
        healingImage.color = isHealing ? originalHealingColor : Color.Lerp(healingImage.color, Color.clear, healingFlashSpeed * Time.deltaTime);

        isHurt = false;
        isHealing = false;
    }

    public void TakeDamage(float amount)
    {
        isHurt = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        if (currentHealth > fullHealth)
        {
            currentHealth = fullHealth;
        }
        else
        {
            isHealing = true;
        }

        healthSlider.value = currentHealth;
    }

    private void Die()
    {
        anim.SetTrigger("die");
        playMove.Die();
    }
}
