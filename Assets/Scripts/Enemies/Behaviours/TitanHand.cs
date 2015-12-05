using UnityEngine;
using System.Collections;

public class TitanHand : MonoBehaviour
{
    private Titan titan;

    void Start()
    {
        titan = GetComponentInParent<Titan>();
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        var playerLife = col.gameObject.GetComponent<PlayerLife>();
        if (playerLife != null)
        {
            playerLife.TakeDamage(titan.punchDamage);
        }
    }
}
