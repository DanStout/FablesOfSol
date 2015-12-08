using UnityEngine;
using System.Collections;

public class HeartItem : BaseItem
{
    public float healAmount = 3;

    private PlayerLife playLife;

    protected override void InitialSetup()
    {
        if (playLife == null)
            playLife = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
    }

    public override void Use()
    {
        playLife.Heal(healAmount);
        Quantity--;
    }

    public override string Name
    {
        get { return "Heart"; }
    }


}
