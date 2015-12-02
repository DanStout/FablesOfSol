using UnityEngine;
using System.Collections;

public abstract class BaseItem : MonoBehaviour
{
    public Vector3 rotate = new Vector3(0, 150, 0);

    public abstract string Name { get; }
    public abstract void Use();

    protected virtual void Update()
    {
        transform.Rotate(rotate * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter(Collider col)
    {
        var invent = col.GetComponent<PlayerInventory>();
        if (invent != null)
        {
            invent.PickupItem(this);
        }
        Destroy(gameObject);
    }
}
