using UnityEngine;
using System.Collections;

public interface IItem
{
	void init(GameObject owner);
	void Use();
	string getName();
}
