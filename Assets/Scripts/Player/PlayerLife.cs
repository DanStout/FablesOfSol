using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public float fullHealth = 20;
    public float deathGameOverDelay = 2; // seconds to wait after death before display game over screen

    private Animator anim;
    private PlayerMovement playMove;
    private float currentHealth;

    public delegate void OnHurtHandler();
    public event OnHurtHandler onHurt;

    public delegate void OnHealHandler();
    public event OnHealHandler onHeal;

    private bool isDead;
    private HealthUI healthUI;

    void Start()
    {
        anim = GetComponent<Animator>();
        playMove = GetComponent<PlayerMovement>();

        healthUI = GameObject.FindGameObjectWithTag("UI").GetComponent<UIController>().healthUI;

        currentHealth = fullHealth;
        healthUI.InitializeSlider(fullHealth, currentHealth);
    }

    /// <summary> Damages the player and returns whether the player died from this hit</summary>
    public bool TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthUI.UpdateHealth(currentHealth);

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Die();
            return true;
        }
        else
        {
            onHurt();
        }

        return false;
    }


    /// <summary>
    /// Heals the player, and returns whether healing did anything
    /// </summary>
    /// <param name="amount">Amount by which to heal</param>
    /// <returns>Whether healing did anything (I.e. whether player was NOT already at full health)</returns>
    public bool Heal(float amount)
    {
        if (currentHealth.Within(fullHealth, 0.001f)) return false; //if they're roughly equal (accounts for floating point math)

        currentHealth += amount;

        if (currentHealth > fullHealth)
        {
            currentHealth = fullHealth;
        }

        healthUI.UpdateHealth(currentHealth);

        onHeal();

        return true;
    }

    public void Die()
    {
        anim.SetTrigger("die");
        playMove.Die();
    }
}
