using UnityEngine;
using System.Collections;

public class DogAI : MonoBehaviour, IEnemy
{
    public float speed = 5;
    public float attackDamage = 5;
    public float maxHealth = 10;
    public float attackDelay = 1; //seconds between attacks
    public float rotationSpeed = 5;
    public float sightRange = 15;
    public float attackRange = 1;
    public float downwardForce = -1.5f;
    public float chaseDelay = 1; //seconds between raycasts
    public LayerMask visibleLayers;
    public float damageColorDuration = 2;

    public Color damageColor;
    private Color originalColor;

    private float currHealth;
    private GameObject player;
    private PlayerLife playerLife;
    private float lastAttackTime;
    private CharacterController charControl;
    private bool isChasing = false;
    private Animator anim;
    private bool isDead = false;
    private SkinnedMeshRenderer meshRend;

    void Start()
    {
        currHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        charControl = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        meshRend = GetComponentInChildren<SkinnedMeshRenderer>();
        originalColor = meshRend.material.color;
        tag = "enemy"; //For items to detect and deliver damage
    }

    void Update()
    {
        var playerLoc = player.transform.position;
        var playerDist = Vector3.Distance(transform.position, playerLoc);
        var movement = Vector3.zero;

        if (!isDead && playerDist <= sightRange)
        {
            var posDiff = playerLoc - transform.position;
            posDiff.y = 0;
            var rotVec = Quaternion.LookRotation(posDiff, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotVec, Time.deltaTime * rotationSpeed);


            if (!isChasing)
            {
                isChasing = CanSeePlayer();
            }

            if (isChasing)
            {
                movement = (playerLoc - transform.position).normalized * speed;
                movement = Vector3.ClampMagnitude(movement, speed);
            }

        }
        else
        {
            isChasing = false;
        }

        anim.SetFloat("speed", movement.sqrMagnitude);
        movement.y = downwardForce;
        movement *= Time.deltaTime;
        charControl.Move(movement);
    }

    private bool CanSeePlayer()
    {
        var rayEndpt = player.transform.position;
        rayEndpt.y += 1;
        var ray = new Ray(transform.position, rayEndpt - transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, sightRange, visibleLayers))
        {
            return hit.collider.gameObject.CompareTag("Player");
        }
        return false;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (isDead) return;

        var playerLife = hit.gameObject.GetComponent<PlayerLife>();
        if (playerLife != null && Time.time - lastAttackTime > attackDelay)
        {
            playerLife.TakeDamage(attackDamage);
            lastAttackTime = Time.time;
        }
    }

    public void TakeDamage(float amount)
    {
        StartCoroutine(HurtFlashForTime(damageColorDuration));
        currHealth -= amount;
        if (currHealth <= 0)
        {
            isDead = true;
            anim.SetBool("isDead", true);
            StartCoroutine(DestroyAfterTime(15));
        }
    }

    private IEnumerator DestroyAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }

    private IEnumerator HurtFlashForTime(float seconds)
    {
        meshRend.material.color = damageColor;
        yield return new WaitForSeconds(seconds);
        meshRend.material.color = originalColor;
    }

	// Magnet gun uses this to determine how to interact with other gameobjects
	public string getMaterial()
	{
		return "flesh";
	}
    
}
