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
		if (meshRend != null) {
			meshRend.material = originalMaterial;
			Destroy (this);
		}
    }

    public void TakeDamage(float amount)
    {
        StartCoroutine(HurtFlashForTime(damageFlashSeconds));
        currHealth -= amount;
        if (currHealth <= 0)
        {
            if (onDeath == null)
            {
                print("No event attached on {0}'s death..".FormatWith(gameObject.name));
            }
            else
                onDeath();
        }
    }

    private IEnumerator HurtFlashForTime(float seconds)
    {
		if(meshRend != null){
        	meshRend.material = damagedMaterial;
        	yield return new WaitForSeconds(seconds);
        	meshRend.material = originalMaterial;
		}
    }
}
