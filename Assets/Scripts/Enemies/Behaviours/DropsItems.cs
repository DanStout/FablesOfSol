using UnityEngine;
using System.Collections;

public class DropsItems : MonoBehaviour
{
    public float destroyAfterSeconds = 2;
    public GameObject[] droppableItems;
    public ParticleSystem deathParticleSystem;
    private float itemHeightOffset;
    private GameObject chosenItem;

    void Start()
    {
        var itemIndex = Random.Range(0, droppableItems.Length);
        chosenItem = droppableItems[itemIndex];
        itemHeightOffset = chosenItem.GetComponent<Collider>().bounds.max.y / 2;
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

        var pos = transform.position;
        pos.y += itemHeightOffset;
        Instantiate(chosenItem, pos, Quaternion.identity);
    }
}
