using UnityEngine;
using System.Collections;

public class ClickSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] clickSounds;

    void Start()
    {
    }

    public void OnClick()
    {
        var index = Random.Range(0, clickSounds.Length);
        var sound = clickSounds[index];
        PlayClipKeepBetweenScenes(sound, transform.position);
    }

    private void PlayClipKeepBetweenScenes(AudioClip clip, Vector3 position)
    {
        var obj = new GameObject("Persistent Audio Obj");
        obj.AddComponent<DontDestroy>();
        obj.transform.position = position;
        var src = obj.AddComponent<AudioSource>();
        src.clip = clip;
        src.Play();
        Destroy(obj, clip.length);
    }
}
