using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public float fullHealth = 100;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed;
    public Color flashColor = new Color(1, 0, 0, 0.1f);

    private float currentHealth;
    private bool isDead;
    private bool isHurt;

    void Start()
    {
        healthSlider.maxValue = fullHealth;
        currentHealth = fullHealth;
        //currentHealth = PlayerPrefs.GetFloat(Constants.Prefs_Health, fullHealth);
        healthSlider.value = currentHealth;
    }

    void Update()
    {
        if (isHurt)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        isHurt = false;
    }

    public void TakeDamage(float amount)
    {
        print("Took damage: " + amount);
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
