using UnityEngine;
using System.Collections;

public class WaterDamage : MonoBehaviour
{
    public float damage = 1;
    public float damageDelaySeconds = 0.5f;
    private float lastDamageTime;

    void OnTriggerStay(Collider col)
    {
	    var playLife = col.gameObject.GetComponent<PlayerLife>();
	    if (playLife != null && Time.time - lastDamageTime >= damageDelaySeconds)
	    {
	        playLife.TakeDamage(damage);
	        lastDamageTime = Time.time;
	    }
    }
}
