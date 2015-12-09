using UnityEngine;
using System.Collections;

public class Destroyable : MonoBehaviour
{
    public ParticleSystem destroySystem;
    public AudioClip explosionSound;

    public void TakeHit()
    {
        var system = Instantiate(destroySystem);
        system.transform.position = transform.position;
        system.Play();
        Destroy(system.gameObject, system.duration);
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
    }

    

}
