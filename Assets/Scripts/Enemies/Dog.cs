using UnityEngine;
using System.Collections;

public class Dog : MonoBehaviour, IEnemy
{
    public float maxHealth = 10;
    public float damageFlashSeconds = 0.5f;

    public Material damagedMaterial;
    private Material originalMaterial;

    private Animator anim;
    private SkinnedMeshRenderer meshRend;
    private DropsItems dropper;
    private ChasesPlayer chaser;
    private FacesPlayer faces;
    private bool isDead = false;
    private float currHealth;

    void Start()
    {
        dropper = GetComponent<DropsItems>();
        chaser = GetComponent<ChasesPlayer>();
        anim = GetComponent<Animator>();
        faces = GetComponent<FacesPlayer>();

        currHealth = maxHealth;
        meshRend = GetComponentInChildren<SkinnedMeshRenderer>();
        originalMaterial = meshRend.material;
        tag = "enemy"; //For items to detect and deliver damage
    }

    void Update()
    {
        chaser.MoveTowardsPlayer();
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        StartCoroutine(HurtFlashForTime(damageFlashSeconds));
        currHealth -= amount;
        if (currHealth <= 0)
        {
            isDead = true;
            anim.SetTrigger("die");
            chaser.Die();
            dropper.Die();
            faces.Die();
        }
    }

    private IEnumerator HurtFlashForTime(float seconds)
    {
        meshRend.material = damagedMaterial;
        yield return new WaitForSeconds(seconds);
        meshRend.material = originalMaterial;
    }

	// Magnet gun uses this to determine how to interact with other gameobjects
	public string getMaterial()
	{
		return "flesh";
	}
    
}
