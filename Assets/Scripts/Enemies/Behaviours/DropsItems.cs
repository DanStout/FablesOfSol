using UnityEngine;
using System.Collections;

public class DropsItems : MonoBehaviour
{
    public float destroyAfterSeconds = 2;
    public GameObject[] droppableItems;
    public ParticleSystem deathParticleSystem;
    private float itemHeightOffset; //used to make sure the item isn't half in the ground
    private GameObject chosenItem;

    void Start()
    {
        var itemIndex = Random.Range(0, droppableItems.Length); //Range is [Inclusive, Exclusive) (No -1 necessary)
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
