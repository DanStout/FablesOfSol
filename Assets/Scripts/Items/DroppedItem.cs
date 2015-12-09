using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BaseItem))]
public class DroppedItem : MonoBehaviour
{
    public Vector3 rotate = new Vector3(0, 150, 0);
    public AudioClip pickupSound;
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
        var invent = col.GetComponent<PlayerInventory>();
        if (invent != null)
        {
            invent.PickupItem(itemScript);
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            Destroy(gameObject);
        }
    }
}
