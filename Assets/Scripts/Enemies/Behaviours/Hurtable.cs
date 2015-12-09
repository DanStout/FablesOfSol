using UnityEngine;
using System.Collections;

public class Hurtable : MonoBehaviour
{
    public float fullHealth = 10;
    public float damageFlashSeconds = 0.5f;
    public Material damagedMaterial;

    protected float currHealth;
    protected Material originalMaterial;
    protected SkinnedMeshRenderer meshRend;

    public delegate void HurtHandler();
    public event HurtHandler onHurt;

    public delegate void DeathHandler();
    public event DeathHandler onDeath;

    protected virtual void Start()
    {
        currHealth = fullHealth;
        meshRend = GetComponentInChildren<SkinnedMeshRenderer>();
        originalMaterial = meshRend.material;

    }

    public virtual void Die()
    {
        if (meshRend != null)
        {
            meshRend.material = originalMaterial;
        }
        Destroy(this);
    }

    public virtual void TakeDamage(float amount)
    {
        StartCoroutine(HurtFlashForTime(damageFlashSeconds));
        currHealth -= amount;
        if (currHealth <= 0)
        {
            if (onDeath == null)
                print("No event attached on {0}'s death..".FormatWith(gameObject.name));
            else
                onDeath();
        }
        else if (onHurt != null)
        {
            onHurt();
        }
    }

    public void SetHealthToLevel(float level)
    {
        currHealth = level;
    }

    protected IEnumerator HurtFlashForTime(float seconds)
    {
        if (meshRend != null)
        {
            meshRend.material = damagedMaterial;
            yield return new WaitForSeconds(seconds);
            meshRend.material = originalMaterial;
        }
    }

	protected void raiseDeathEvent()
	{
		if (onDeath != null)
			onDeath ();
		else 
			print("No event attached on {0}'s death..".FormatWith(gameObject.name));
	}

	protected void raiseOnHurtEvent()
	{
		if (onHurt != null)
			onHurt ();
		else
			print ("No hurt handler event attached...");
	}
}
