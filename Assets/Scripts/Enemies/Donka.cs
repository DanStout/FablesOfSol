using UnityEngine;
using System.Collections;

public class Donka : MonoBehaviour, IEnemy
{
    private ChasesPlayer chaser;
    private Hurtable hurt;
    private FacesPlayer faces;
    private DropsItems drops;

    public GameObject activateOnDeath;

    void Start()
    {
        chaser = GetComponent<ChasesPlayer>();
        hurt = GetComponent<Hurtable>();
        faces = GetComponent<FacesPlayer>();
        drops = GetComponent<DropsItems>();

        hurt.onDeath += hurt_onDeath;
        tag = "enemy";
    }

    void hurt_onDeath()
    {
        chaser.Die();
        hurt.Die();
        faces.Die();
        drops.Die();

        activateOnDeath.SetActive(true);
    }

    void Update()
    {
        chaser.MoveTowardsPlayer();
    }

    public string getMaterial()
    {
        throw new System.NotImplementedException();
    }
}
