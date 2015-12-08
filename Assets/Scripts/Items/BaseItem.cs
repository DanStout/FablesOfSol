using UnityEngine;
using System.Collections;

public abstract class BaseItem : MonoBehaviour
{
    public Sprite inventoryTile;

    public delegate void QuantityChangedHandler(BaseItem item);
    public event QuantityChangedHandler onQuantityChanged;

    private int _quantity;

    protected virtual void Awake()
    {
        InitialSetup();
    }

    protected virtual void OnLevelWasLoaded()
    {
        InitialSetup();
    }

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

    /// <summary>
    /// Called in both Awake() and OnLevelWasLoaded(), which is necessary to maintain references between scene loads
    /// </summary>
    protected abstract void InitialSetup();
    public abstract void Use();

    public abstract string Name { get; }
}
