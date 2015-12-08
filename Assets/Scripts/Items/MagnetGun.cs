using UnityEngine;
using System.Collections;

public class MagnetGun : Weapon
{
    private GameObject owner;
    private bool isOn = false;
    private ParticleSystem particleSys;
    private Animator anim;
    public AudioClip activeSound;
    private AudioSource playerAudio;

    void Awake()
    {
        owner = GameObject.FindGameObjectWithTag("Player");
        anim = owner.GetComponent<Animator>();
        playerAudio = owner.GetComponent<AudioSource>();
    }

    void Start()
    {
        ParticleSystem[] systems = owner.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem p in systems)
        {
            if (p.gameObject.name == "MagnetGunParticle")
                particleSys = p;
        }
    }

    public void Update()
    {
        if (!isOn) return;
        RaycastHit hit;
        Vector3 fwd = owner.transform.TransformDirection(Vector3.forward);
        Vector3 pos = new Vector3(0, 1, 0);

        if (Physics.Raycast(owner.transform.position + pos, fwd, out hit, 20))
        {

            Titan titan = isParentTitan(hit.collider.gameObject);
            if (titan != null)
            {
                print("found titan!");
                titan.pullThrum();
                return;
            }

            IMagnetic mag = hit.collider.gameObject.GetComponentInChildren<IMagnetic>();
            if (mag != null)
            {
                Vector3 cur = hit.collider.transform.position;
                if (Vector3.Distance(cur, owner.transform.position) > 3)
                {

                    if (!mag.isHeavierThanPlayer())
                    {
                        hit.collider.transform.position =
                            Vector3.MoveTowards(cur, owner.transform.position, .15f);
                    }
                    else
                    {
                        owner.transform.position = Vector3.MoveTowards(owner.transform.position, cur, .15f);
                    }
                }
            }
        }
    }

    public override void Use()
    {
        print("Using magnet gun");
        //Spawn particle system
        if (particleSys != null && !isOn)
        {
            playerAudio.clip = activeSound;
            playerAudio.Play();

            isOn = true;
            particleSys.enableEmission = true;
            anim.SetBool("gunAttack", true);
        }
        else if (isOn)
        {
            TurnOff();
        }
    }

    //Used during raycast hit to determine if we have hit a child of a magnetic object
    private Titan isParentTitan(GameObject g)
    {
        if (g.name == "ThrumTitan")
            return g.GetComponent<Titan>();
        else if (g.transform.parent != null)
            return isParentTitan(g.transform.parent.gameObject);
        else
            return null;
    }

    public override void Equip()
    {
        owner.GetComponent<PlayerInventory>().Equip(PlayerInventory.WeaponType.MagnetGun);
    }

    public override string Name
    {
        get { return "Magnet Gun"; }
    }


    private void TurnOff()
    {
        playerAudio.Stop();
        isOn = false;
        particleSys.enableEmission = false;
        anim.SetBool("gunAttack", false);
    }

    void OnDisable()
    {
        //TurnOff();
    }
}
