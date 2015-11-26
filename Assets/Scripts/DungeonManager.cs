using UnityEngine;
using System.Collections;

/// <summary>
/// Manage resetting the player's position if they fall off
/// </summary>
public class DungeonManager : MonoBehaviour
{
    public float yMin = -40;
    public Transform respawnPoint;
    private GameObject player;
    private CameraControl camCtrl;
    private PlayerMovement playMove;
    private PlayerLife playLife;
    public float fallDamage = 2;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camCtrl = Camera.main.GetComponent<CameraControl>();
        playMove = player.GetComponent<PlayerMovement>();
        playLife = player.GetComponent<PlayerLife>();
    }

    void Update()
    {
        if (player.transform.position.y < yMin)
        {
            playMove.DoIgnoreNextFall = true;
            player.transform.position = respawnPoint.position;
            camCtrl.RecenterOnPlayer();
            playLife.TakeDamage(fallDamage);
        }
    }
}
