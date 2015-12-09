using UnityEngine;
using System.Collections;

public class DamagingParticleSystem : MonoBehaviour
{
    public float Damage;

    private bool active;
    private ParticleSystem system;

	void Start()
	{
        system = GetComponent<ParticleSystem>();

        system.Stop();
        active = false;
	}

    public void Activate()
    {
        if (!active)
        {
            system.Play();
            active = true;
        }
    }


    public void Deactivate()
    {
        if (active)
        {
            system.Stop();
            active = false;
        }
    }

	public void PlayOnce()
	{
		system.Play();
	}
}
