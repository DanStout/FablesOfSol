using UnityEngine;
using System.Collections;

public interface IItem
{
	void Use();
    void Equip();
    string Name { get; }
}
