using UnityEngine;
using System.Collections;

public class DungeonManager : MonoBehaviour
{
    public float yMin = -40;
    public GameObject respawnPoint;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < yMin)
            player.transform.position = respawnPoint.transform.position;
    }
}
