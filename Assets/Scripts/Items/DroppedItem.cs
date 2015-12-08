using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BaseItem))]
public class DroppedItem : MonoBehaviour
{
    public Vector3 rotate = new Vector3(0, 150, 0);
    private BaseItem itemScript;

    void Start()
    {
        itemScript = GetComponent<BaseItem>();
    }

	void Update()
    {
        transform.Rotate(rotate * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col)
    {
<<<<<<< HEAD
=======
        print("Triggeded!");
>>>>>>> 5c874fbcd44fe2e8a0e88c9c8658823f3cc96609
        var invent = col.GetComponent<PlayerInventory>();
        if (invent != null)
        {
            invent.PickupItem(itemScript);
            Destroy(gameObject);
        }
    }
}
