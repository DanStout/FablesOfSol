using UnityEngine;
using System.Collections;

public class PlayerAudio : MonoBehaviour, IStateListener
{
    public AudioClip soundDeath;
    public AudioClip soundJump;
    public AudioClip[] soundHurtGrunts;
    public AudioClip[] soundHealSighs;

    private AudioSource audioSrc;
    private AudioClip[] footstepSounds;
    private int lastSoundIndex;
    private PlayerLife life;

	void Start()
	{
        audioSrc = GetComponent<AudioSource>();
        footstepSounds = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManager>().footstepSounds;
        audioSrc.PlayOneShot(footstepSounds[0]);
        life = GetComponent<PlayerLife>();
        life.onHurt += life_onHurt;
        life.onHeal += life_onHeal;
    }

    void life_onHeal()
    {
        audioSrc.PlayOneShot(soundHealSighs[Random.Range(0, soundHealSighs.Length)]);
    }

    void life_onHurt()
    {
        audioSrc.PlayOneShot(soundHurtGrunts[Random.Range(0, soundHurtGrunts.Length)]);
    }

    void Disable()
    {
        life.onHurt -= life_onHurt;
        life.onHeal -= life_onHeal;
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
