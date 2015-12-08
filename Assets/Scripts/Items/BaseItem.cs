using UnityEngine;
using System.Collections;

public abstract class BaseItem : MonoBehaviour
{
    public Sprite inventoryTile;

    public delegate void QuantityChangedHandler(BaseItem item);
    public event QuantityChangedHandler onQuantityChanged;

    private int _quantity;

    public int Quantity
    {
        get
        {
            return _quantity;
        }
        set
        {
            _quantity = value;
            if (onQuantityChanged != null)
                onQuantityChanged(this);
        }
    }

    public abstract void Use();
    public abstract string Name { get; }
}
