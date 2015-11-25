using UnityEngine;
using System.Collections;

public class DungeonManager : MonoBehaviour
{
    public float yMin = -40;
    public Transform respawnPoint;
    private GameObject player;
    private GameObject playerAndCam;
    private CameraControl camCtrl;
    private PlayerControl playCtrl;
    private PlayerLife playLife;
    public float fallDamage = 2;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camCtrl = Camera.main.GetComponent<CameraControl>();
        playCtrl = player.GetComponent<PlayerControl>();
        playLife = player.GetComponent<PlayerLife>();
    }

    void Update()
    {
        if (player.transform.position.y < yMin)
        {
            playCtrl.DoIgnoreNextFall = true;
            player.transform.position = respawnPoint.position;
            camCtrl.RecenterOnPlayer();
            playLife.TakeDamage(fallDamage);
        }
    }
}
