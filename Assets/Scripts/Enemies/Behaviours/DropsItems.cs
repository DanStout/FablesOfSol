using UnityEngine;
using System.Collections;

public class DropsItems : MonoBehaviour
{
    public float destroyAfterSeconds = 2;
    public GameObject[] droppableItems;
    public ParticleSystem deathParticleSystem;
    private GameObject chosenItem;
    private Hurtable hurt;

    void Start()
    {
        var itemIndex = Random.Range(0, droppableItems.Length); //Range is [Inclusive, Exclusive) (No -1 necessary)
		chosenItem = droppableItems[itemIndex];
    }

    public void Die()
    {
        StartCoroutine(DestroyAfterTime());
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(destroyAfterSeconds);
        Destroy(this.gameObject);

        var system = Instantiate(deathParticleSystem);
        system.transform.position = transform.position;
        system.Play();
        Destroy(system.gameObject, system.duration);

        var item = Instantiate<GameObject>(chosenItem);
        var itemHeightOffset = item.GetComponentInChildren<Renderer>().bounds.size.y / 2;
        var pos = transform.position;
        pos.y += itemHeightOffset + 0.1f;
        item.transform.position = pos;
    }
}
