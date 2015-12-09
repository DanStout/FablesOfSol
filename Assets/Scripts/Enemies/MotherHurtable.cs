using UnityEngine;
using System.Collections;

public class MotherHurtable : Hurtable
{

    private int numHits = 0;

    protected override void Start()
    {
        currHealth = fullHealth;
        meshRend = GetComponentInChildren<SkinnedMeshRenderer>();
        originalMaterial = meshRend.material;
        onDeath += Die;
    }

    public override void Die()
    {
        print("Death");

        GetComponentInParent<Animator>().SetTrigger("die");

        if (meshRend != null)
        {
            meshRend.material = originalMaterial;
        }

        GetComponentInParent<DropsItems>().Die();
    }

    public override void TakeDamage(float amount)
    {
        StartCoroutine(HurtFlashForTime(damageFlashSeconds));
        currHealth -= amount;

        if (currHealth <= 0)
            raiseDeathEvent();
        else
            raiseOnHurtEvent();


        print("HIT COUNT: " + numHits);

        //Count number of hits and reduce health drastically after the second hit
        if (numHits < 2)
        {
            //Increment hit count and replace the ice
            numHits++;
            BreakIce ice = gameObject.transform.parent.FindChild("Ice").GetComponent<BreakIce>();
            ice.replaceIce();
        }
        else
        {
            //If this is the third hit, set health to 1.
            SetHealthToLevel(1);

            //Replace ice for the final time
            BreakIce ice = gameObject.transform.parent.FindChild("Ice").GetComponent<BreakIce>();
            ice.replaceIce();
        }
    }
}
