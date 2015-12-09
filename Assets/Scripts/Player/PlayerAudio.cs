using UnityEngine;
using System.Collections;

public class PlayerAudio : MonoBehaviour, IStateListener
{
    public AudioClip soundDeath;
    public AudioClip soundJump;

    private AudioSource audioSrc;
    private AudioClip[] footstepSounds;
    private int lastSoundIndex;

	void Start()
	{
        audioSrc = GetComponent<AudioSource>();
        footstepSounds = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManager>().footstepSounds;
        audioSrc.PlayOneShot(footstepSounds[0]);
    }

    public void OnFootDown()
    {
        var index = lastSoundIndex;

        if (footstepSounds.Length > 1)
        {
            while (index == lastSoundIndex)
            {
                index = Random.Range(0, footstepSounds.Length);
            }
        }

        audioSrc.PlayOneShot(footstepSounds[index]);

        lastSoundIndex = index;
    }

    public void StateEntered(string name)
    {
        switch(name)
        {
            case "Death":
                audioSrc.PlayOneShot(soundDeath);
                break;
            case "JumpUp":
                audioSrc.PlayOneShot(soundJump);
                break;
        }
    }

    public void StateExited(string name)
    {
    }
}
