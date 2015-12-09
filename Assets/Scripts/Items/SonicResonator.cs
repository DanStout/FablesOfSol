using UnityEngine;
using System.Collections;
using System.Linq;

public class SonicResonator : Weapon, IGun
{
    private GameObject owner;
    private bool isOn = false;
    private ParticleSystem particleSys;
    private Animator anim;
    private AudioSource playerAud;
    public AudioClip activeSound;
    public AudioClip breakIceSound;

    protected override void InitialSetup()
    {
        if (owner == null) owner = GameObject.FindGameObjectWithTag("Player");
        if (playerAud == null) playerAud = owner.GetComponent<AudioSource>();
        if (anim == null) anim = owner.GetComponent<Animator>();

        if (particleSys == null)
        {
            particleSys = owner
                .GetComponentsInChildren<ParticleSystem>()
                .First(x => x.name == "SonicParticle");
        }
    }

    void Update()
    {
        if (!isOn) return;

        RaycastHit hit;
        Vector3 fwd = owner.transform.TransformDirection(Vector3.forward);
        Vector3 pos = new Vector3(0, 3, 0);

        if (Physics.Raycast(owner.transform.position + pos, fwd, out hit, 10))
        {
            if (hit.collider.gameObject.name == "Ice")
            {
                playerAud.PlayOneShot(breakIceSound);
                if (hit.collider.gameObject.GetComponent<BreakIce>() != null)
                {
                    hit.collider.gameObject.GetComponent<BreakIce>().breakIce();
                }
                else
                {
                    hit.collider.gameObject.GetComponent<IceBreak>().breakIce();
                }
            }
        }
    }

    public override void Use()
    {
        if (particleSys != null && !isOn)
        {
            playerAud.clip = activeSound;
            playerAud.Play();

            isOn = true;
            particleSys.enableEmission = true;
            anim.SetBool("gunAttack", true);
        }
        else if (isOn)
        {
            TurnOff();
        }
    }

    public void TurnOff()
    {
        playerAud.Stop();
        isOn = false;
        particleSys.enableEmission = false;
        anim.SetBool("gunAttack", false);
    }

    public override void Equip()
    {
        owner.GetComponent<PlayerInventory>().Equip(PlayerInventory.WeaponType.SonicResonator);
    }

    public override string Name
    {
        get { return "Sonic Resonator"; }
    }
}
