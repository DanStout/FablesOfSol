using UnityEngine;
using System.Collections;

public interface IEnemy
{
    void TakeDamage(float amount);
	string getMaterial();
}
