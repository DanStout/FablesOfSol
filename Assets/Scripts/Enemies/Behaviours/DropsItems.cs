using UnityEngine;
using System.Collections;

public class DropsItems : MonoBehaviour
{
    public float destroyAfterSeconds = 2;
    public GameObject[] droppableItems;
    public ParticleSystem deathParticleSystem;

    public void Die()
    {
        StartCoroutine(DestroyAfterTime());
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(destroyAfterSeconds);
        Destroy(this.gameObject);

        var itemIndex = Random.Range(0, droppableItems.Length);
        var item = droppableItems[itemIndex];

        var system = Instantiate(deathParticleSystem);
        system.transform.position = transform.position;
        system.Play();
        Destroy(system.gameObject, system.duration);

        Instantiate(item, transform.position, Quaternion.identity);
    }
}
