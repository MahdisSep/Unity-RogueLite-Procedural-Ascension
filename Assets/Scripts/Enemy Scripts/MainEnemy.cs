using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class MainEnemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [SerializeField] public int health;

    int maxHealth;

    [Header("IFrame Stuff")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public SpriteRenderer mySprite;
    GameObject bossUI;
    // Extra variables used to detect if a player enter the same room where the boss resides.
    public RectInt room;
    bool playBossMusic;
    //References
    private Animator anim;
    // private Health playerHealth;
    private EnemyFire enemyPatrol;

     // Initializing all the variables before the first frame is called.
    void Start()
    {
        // name = "Skeletrax";
        // boxCollider = GetComponentInChildren<BoxCollider2D>();
        // boxCollider.size = room.size;
        health = 20;
        maxHealth = health;
        bossUI = GameObject.FindGameObjectWithTag("BossHP");
        bossUI.SetActive(false);
        playBossMusic = false;
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyFire>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //Attack only when player in sight?
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("attack");
            }
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        // if (hit.collider != null)
        //     playerHealth = hit.transform.GetComponent<Health>();
        // else{
        //     print(hit.collider);
        // }

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    // private void DamagePlayer()
    // {
    //     if (PlayerInSight())
    //         playerHealth.TakeHurt(damage);
    // }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit!");
        if (collision.CompareTag("Player"))
        {
            if (!playBossMusic)
            {
                FindObjectOfType<MusicSoundManager>().ChangeMusic("bossMusic");
                playBossMusic = true;
            }
            
            bossUI.SetActive(true);
            if (!bossUI.Equals(null))
            {
                bossUI.GetComponentInChildren<TMP_Text>().text = name;
                bossUI.GetComponentInChildren<Slider>().maxValue = maxHealth;
                bossUI.GetComponentInChildren<Slider>().value = health;
            }
            
        }
    }

    // Method to update the boss' health bar while player is nearby.
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Hit2!");
        if (collision.CompareTag("Player"))
        {
            if (!bossUI.Equals(null))
            {
                bossUI.GetComponentInChildren<TMP_Text>().text = name;
                bossUI.GetComponentInChildren<Slider>().maxValue = maxHealth;
                bossUI.GetComponentInChildren<Slider>().value = health;
            }
        }
    }
     // Method to called by a player's class when the enemy gets hit by a player. Reduces enemy health by 1 and plays appropriate damage taken/death animation and sound effects.
    private IEnumerator FlashCo(bool death)
    {
        int temp = 0;
        // Alternating between red flashing and original sprite
        while (temp < numberOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        // If dead, remove self from the game.
        // if (death)
        // {
        //     this.gameObject.SetActive(false);
        // }

    }

    // // Method to called by a player's class when the enemy gets hit by a player. Reduces enemy health by 1 and plays appropriate damage taken/death animation and sound effects.
    public void GetHit()
    {
        Debug.Log("Health: " + health);
        health -= 1;
        if (health > 0)
        {
            SoundManager.PlaySound("skeletonHit");
            StartCoroutine(FlashCo(false));
        }
        else
        {
            // If Skeletrax (the boss) dies, change state of the game to "End" with a player victory boolean.
            SoundManager.PlaySound("skeletonDeath");
            StartCoroutine(FlashCo(true));
            FindObjectOfType<GameManager>().EndGame(true);

        }
    }
}