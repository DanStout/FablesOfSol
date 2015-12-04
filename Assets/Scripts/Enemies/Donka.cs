using UnityEngine;
using System.Collections;

public class Donka : MonoBehaviour, IEnemy
{
    private ChasesPlayer chaser;

    void Start()
    {
        chaser = GetComponent<ChasesPlayer>();
    }

    void Update()
    {
        chaser.MoveTowardsPlayer();
    }

    public void TakeDamage(float amount)
    {
        //throw new System.NotImplementedException();
    }

    public string getMaterial()
    {
        throw new System.NotImplementedException();
    }
}
