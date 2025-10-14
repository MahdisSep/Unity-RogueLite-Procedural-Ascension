using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Characters")
        {
            collision.GetComponent<Health>().TakeHurt(damage); // jori ke rexa damage mibine bayad ok she
        }
    }
}
