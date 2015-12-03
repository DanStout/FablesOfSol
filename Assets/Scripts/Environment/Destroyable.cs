using UnityEngine;
using System.Collections;

public class Destroyable : MonoBehaviour
{
    public int hitsRequired = 2;
    private int hitsLeft;

    void Start()
    {
        hitsLeft = hitsRequired;
    }

    public void TakeHit()
    {
        hitsLeft--;
        if (hitsLeft == 0)
        {
            Destroy(gameObject);
        }
    }

    

}
