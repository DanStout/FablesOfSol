using UnityEngine;
using System.Collections;

public class HeartItem : BaseItem
{
    public float healAmount = 3;

    private PlayerLife playLife;

    void Start()
    {
        playLife = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
    }

    public override void Use()
    {
        playLife.Heal(healAmount);
    }
}
