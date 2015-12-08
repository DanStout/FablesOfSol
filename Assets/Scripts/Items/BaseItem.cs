using UnityEngine;
using System.Collections;

public abstract class BaseItem : MonoBehaviour
{
    public Sprite inventoryTile;

    public abstract void Use();
    public abstract string Name { get; }
}
