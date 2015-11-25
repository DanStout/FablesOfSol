using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public float fullHealth = 20;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed; //higher = shorter damage flash

    private float currentHealth;
    private bool isDead;
    private bool isHurt;
    private Color originalColor;

    void Start()
    {
        originalColor = damageImage.color;
        damageImage.color = Color.clear;
        healthSlider.maxValue = fullHealth;
        currentHealth = fullHealth;
        //currentHealth = PlayerPrefs.GetFloat(Constants.Prefs_Health, fullHealth);
        healthSlider.value = currentHealth;
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
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Time.timeScale = 0;
    }
}
