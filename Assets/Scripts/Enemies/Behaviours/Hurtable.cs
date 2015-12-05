using UnityEngine;
using System.Collections;

public class Hurtable : MonoBehaviour
{
    public float fullHealth = 10;
    public float damageFlashSeconds = 0.5f;
    public Material damagedMaterial;

    private float currHealth;
    private Material originalMaterial;
    private SkinnedMeshRenderer meshRend;

    public delegate void DeathHandler();
    public event DeathHandler onDeath;

	void Start()
	{
        currHealth = fullHealth;
        meshRend = GetComponentInChildren<SkinnedMeshRenderer>();
        originalMaterial = meshRend.material;
	}
	
	public void Die()
    {
        enabled = false;
    }

    public void TakeDamage(float amount)
    {
        print("I've been hurt!");
        StartCoroutine(HurtFlashForTime(damageFlashSeconds));
        currHealth -= amount;
        if (currHealth <= 0)
        {
            onDeath();
        }
    }

    private IEnumerator HurtFlashForTime(float seconds)
    {
        meshRend.material = damagedMaterial;
        yield return new WaitForSeconds(seconds);
        meshRend.material = originalMaterial;
    }
}
