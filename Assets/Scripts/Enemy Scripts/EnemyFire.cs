using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft; //if it is true then enemy moves to left

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration; //when enemy reaches the edges , then waits for some times.
    private float idleTimer; //how much time is the idle time that enemy could be reach ro edges

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        initScale = enemy.localScale;
    }
    private void OnDisable()
    {
        anim.SetBool("moving", false); //stop moving animation ?
    }

    private void Update()
    {
        if (movingLeft) //change direction based on the edges that we made.
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1); //left direction
            else
                DirectionChange();
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);  //right direction
            else
                DirectionChange();
        }
    }

    private void DirectionChange() //handle when enemy round to other direction.
    {
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true); //make enemy run

        //Make enemy face direction and have correct direction face (looking left / right)
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
            initScale.y, initScale.z);

        //Move in that direction (moving left / right)
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }
}