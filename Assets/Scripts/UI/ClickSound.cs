using UnityEngine;
using System.Collections;

public class ClickSound : MonoBehaviour
{
    private AudioSource audSrc;
    public AudioClip[] clickSounds;

    void Start()
    {
        audSrc = GameObject.FindGameObjectWithTag("UI").GetComponent<AudioSource>();
    }

    public void OnClick()
    {
        var index = Random.Range(0, clickSounds.Length);
        var sound = clickSounds[index];
        audSrc.PlayOneShot(sound);
    }
}
