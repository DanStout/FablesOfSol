using UnityEngine;
using System.Collections;

/// <summary>
/// Contains information which is of the same type, but has different values for each scene
/// </summary>
public class SceneManager : MonoBehaviour
{
    public AudioClip[] footstepSounds;
	public GameObject titan;
	public GameObject FloeTeleport;

    void Start()
	{
	}
	
	void Update()
	{
		if(titan == null)
		{
			//Open portal to Floe
			FloeTeleport.SetActive(true);
		}
	}
}
