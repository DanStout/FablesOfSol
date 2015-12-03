using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider healthSlider;
    public Image damageImage;
    public Image healingImage;

    public float damageFlashSpeed = 2;
    public float healingFlashSpeed = 4;

    private Color originalDamageColor;
    private Color originalHealingColor;

    void Start()
    {
        damageImage.gameObject.SetActive(true);
        originalDamageColor = damageImage.color;
        damageImage.color = Color.clear;

        healingImage.gameObject.SetActive(true);
        originalHealingColor = healingImage.color;
        healingImage.color = Color.clear;
    }

    void Update()
    {
        if (damageImage.color != Color.clear)
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, damageFlashSpeed * Time.deltaTime);
        }

        if (healingImage.color != Color.clear)
        {
            healingImage.color = Color.Lerp(healingImage.color, Color.clear, healingFlashSpeed * Time.deltaTime);
        }
    }

    public void InitializeSlider(float maxHealth, float currentHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void UpdateHealth(float newValue)
    {
        var old = healthSlider.value;
        if (newValue > old)
        {
            healingImage.color = originalHealingColor;
        }
        else if (newValue < old)
        {
            damageImage.color = originalDamageColor;
        }

        healthSlider.value = newValue;
    }


}
