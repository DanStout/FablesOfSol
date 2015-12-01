using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public float fullHealth = 20;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed; //higher = shorter damage flash
    public float deathGameOverDelay = 2; // seconds to wait after death before display game over screen

    private Animator anim;
    private float currentHealth;
    private bool isDead;
    private bool isHurt;
    private Color originalColor;
    private PlayerMovement playMove; 

    void Start()
    {
        damageImage.gameObject.SetActive(true);
        originalColor = damageImage.color;
        damageImage.color = Color.clear;
        healthSlider.maxValue = fullHealth;
        currentHealth = fullHealth;
        //currentHealth = PlayerPrefs.GetFloat(Constants.Prefs_Health, fullHealth);
        healthSlider.value = currentHealth;
        anim = GetComponent<Animator>();
        playMove = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (isHurt)
        {
            damageImage.color = originalColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        isHurt = false;
    }

    public void TakeDamage(float amount)
    {
        isHurt = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        PlayerPrefs.SetFloat(Constants.Prefs_Health, currentHealth);

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    private void Die()
    {
        anim.SetTrigger("die");
        playMove.Die();
    }
}
