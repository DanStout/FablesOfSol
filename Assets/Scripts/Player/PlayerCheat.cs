using UnityEngine;
using System.Collections;

public class PlayerCheat : MonoBehaviour
{
    public GameObject[] prefabsToSpawnOnCheat;

	void Start()
	{
	
	}
	
	void Update()
	{
	    if (Input.GetButtonDown("Cheat"))
        {
            SpawnCheatItems();
        }
	}

    private void SpawnCheatItems()
    {
        foreach(var item in prefabsToSpawnOnCheat)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }
    }
}
