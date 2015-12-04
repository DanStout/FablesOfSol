using UnityEngine;
using System.Collections;

public class Dog : MonoBehaviour, IEnemy
{
    public float maxHealth = 10;
    public float damageColorDuration = 2;

    public Color damageColor;
    private Color originalColor;

    private Animator anim;
    private SkinnedMeshRenderer meshRend;
    private DropsItems dropper;
    private ChasesPlayer chaser;
    private bool isDead = false;
    private float currHealth;

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

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        StartCoroutine(HurtFlashForTime(damageColorDuration));
        currHealth -= amount;
        if (currHealth <= 0)
        {
            isDead = true;
            chaser.Die();
            anim.SetTrigger("die");
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
