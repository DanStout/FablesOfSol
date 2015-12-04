using UnityEngine;
using System.Collections;

public class ReceivesParticleDamage : MonoBehaviour
{
    public float damageDelaySeconds = 0.5f;
    private PlayerLife life;
    private float lastDamageTime;

	void Start()
	{
        life = GetComponent<PlayerLife>();
	}
	
	void Update()
	{
	
	}

    void OnParticleCollision(GameObject other)
    {
        if (Time.time - lastDamageTime > damageDelaySeconds)
        {
            var damagingSystem = other.GetComponent<DamagingParticleSystem>();
            if (damagingSystem != null)
            {
                life.TakeDamage(damagingSystem.Damage);
            }
            lastDamageTime = Time.time;
        }
    }
}
