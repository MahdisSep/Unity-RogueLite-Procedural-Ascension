using System.Collections;

using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float initialHealth;
    public float currentHealth {get ; private set;} //only can change in this script but we can get it in another scripts
    private Animator anim;
    private bool dead;

    [Header ("iFrames")]
    [SerializeField] private float invalunarabilityTime;
    [SerializeField] private int numOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    private void Awake()
    {
        currentHealth = initialHealth ;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeHurt(float _damage)
    {
        if(invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage , 0, initialHealth);

        if (currentHealth > 0)
        {
            //player hurt
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }
        else
        {
            if(!dead)
            {
                 //player dead
            
            // GetComponent<PlayerMovement>().enabled = false; //player can not move if it is dead.

            // //enemy dead
            // if(GetComponent<EnemyFire>() != null)
            //     GetComponent<EnemyFire>().enabled = false;
            // if(GetComponent<MainEnemy>() != null)
            //     GetComponent<MainEnemy>().enabled = false;
            // dead = true;
             anim.SetTrigger("die");
             foreach (Behaviour component in components)
                    component.enabled = false;
             //anim.SetBool("grounded",true);
             

                dead = true;
           
            }
        }   
    }

    public void addHealth(float _heart)
    {
        currentHealth = Mathf.Clamp(currentHealth + _heart , 0, initialHealth);

    }

    // private void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.E))
    //     {
    //         TakeHurt(1); // everytime we tap E red heart gonna disapeared -> just for testing
    //     }
    // }

    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11,true);

        //invulnerability duration

        for (int i = 0 ; i < numOfFlashes ; i++)
        {
            spriteRend.color = new Color(1,0,0,0.4f); //after collision with enemy player'color will be changed.
            yield return new WaitForSeconds(invalunarabilityTime / (numOfFlashes *2)); //wait a few seconds and then execute next line, ( for example 2 seconds  -> 3 times go to red and then back)
            spriteRend.color = Color.white; //change the color of player to the original.
            yield return new WaitForSeconds(invalunarabilityTime / (numOfFlashes *2)); //wait a few seconds and then execute next line.

        }

        Physics2D.IgnoreLayerCollision(10,11,false);
        invulnerable = false;
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

     public void Respawn()
    {
        dead = false ;
        addHealth(initialHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
        StartCoroutine(Invulnerability());

        //Activate all attached component classes
        foreach (Behaviour component in components)
            component.enabled = true;
    }
}



