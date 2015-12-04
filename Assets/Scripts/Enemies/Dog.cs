using UnityEngine;
using System.Collections;

public class Dog : MonoBehaviour, IEnemy
{
    public float attackDamage = 5;
    public float maxHealth = 10;
    public float secondsBetweenAttacks = 1;
    public float damageColorDuration = 2;

    public Color damageColor;
    private Color originalColor;

    private float currHealth;
    private PlayerLife playerLife;
    private float lastAttackTime;
    private Animator anim;
    private bool isDead = false;
    private SkinnedMeshRenderer meshRend;

    private DropsItems dropper;
    private ChasesPlayer chaser;

    void Start()
    {
        dropper = GetComponent<DropsItems>();
        chaser = GetComponent<ChasesPlayer>();
        anim = GetComponent<Animator>();

        currHealth = maxHealth;
        meshRend = GetComponentInChildren<SkinnedMeshRenderer>();
        originalColor = meshRend.material.color;
        tag = "enemy"; //For items to detect and deliver damage
    }

    void Update()
    {
        chaser.MoveTowardsPlayer();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (isDead) return;

        var playerLife = hit.gameObject.GetComponent<PlayerLife>();
        if (playerLife != null && Time.time - lastAttackTime > secondsBetweenAttacks)
        {
            anim.SetTrigger("attack");
            if (playerLife.TakeDamage(attackDamage))
            {
                anim.SetTrigger("howl");
            }
            
            lastAttackTime = Time.time;
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        StartCoroutine(HurtFlashForTime(damageColorDuration));
        currHealth -= amount;
        if (currHealth <= 0)
        {
            isDead = true;
            chaser.Die();
            anim.SetBool("isDead", true);
            dropper.Die();
        }
    }

    private IEnumerator HurtFlashForTime(float seconds)
    {
        meshRend.material.color = damageColor;
        yield return new WaitForSeconds(seconds);
        meshRend.material.color = originalColor;
    }

	// Magnet gun uses this to determine how to interact with other gameobjects
	public string getMaterial()
	{
		return "flesh";
	}
    
}
