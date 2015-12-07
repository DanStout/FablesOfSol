using UnityEngine;
using System.Collections;

public class Teleporter : BaseOperable
{
    public string respawnDestinationTag;
    public string sceneToLoad;
    public AudioClip teleportSound;

    private AudioSource audioSrc;

    protected override void Start()
    {
        base.Start();
        audioSrc = GetComponent<AudioSource>();
    }

    public override void Operate()
    {
        audioSrc.PlayOneShot(teleportSound);
        GameManager.RelocatePlayerToTagOnNextLevel(respawnDestinationTag);
        Application.LoadLevel(sceneToLoad);
    }
}
