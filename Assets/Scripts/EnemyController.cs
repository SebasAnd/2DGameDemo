using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float health = 10f;

    [SerializeField] private float EnemyDamage = 1f;
    [SerializeField] private float runSpeed = 3f;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject allBar;
    private bool canBeDamaged = true;
    Animator anim;
    [SerializeField]private LayerMask solidObjectsLayer;
    private bool isReacting = false;
    private Vector3 reactionDirection;
    private float maxHealth = 10f;
    void Start()
    {
        anim = GetComponent<Animator>();
        maxHealth = health;
        allBar.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {        
        if( collider && collider.gameObject.tag  == "Attack" && canBeDamaged )
        {            
            anim.Play("SlimeAttackReaction");
            StartCoroutine(DecrementHealth(1f));
            AttackReaction(collider.transform.position);
        }
    }
    public IEnumerator DecrementHealth(float damage)
    {
        canBeDamaged = false;        
        if(health - damage < 0f)
        {
            health = 0;
        }else{
            health -= damage;
            healthBar.fillAmount = health / maxHealth; 
        }
        if(health > 0)
        {
            Debug.Log("enemy health = " + health);
            allBar.SetActive(true);
            yield return new WaitForSeconds(2f);
            canBeDamaged = true;
            allBar.SetActive(false);
            anim.Play("SlimeEnemyIdle");
        }else{
            anim.Play("SlimeDeath");
            StartCoroutine(Death());
        }
        
    }

    public void AttackReaction(Vector3 otherPosition)
    {
        float[] values = new float[] { -1.5f, -2.0f,-2.5f,1.5f,2.0f,2.5f };
        Vector3 limit  = new Vector3(otherPosition.x + values[Random.Range(0, values.Length - 1)], otherPosition.y + values[Random.Range(0, values.Length - 1)], transform.position.z);// + values[Random.Range(0,values.Length-1)] * transform.position; 
        reactionDirection = limit;
        if(IsAvaliablePosition(limit))
        {
            if(health > 0)
            {
                transform.position = limit;
                isReacting = true;
            }
            
        }else{
            AttackReaction(otherPosition);
        }
        
    }
    private bool IsAvaliablePosition(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, .2f, solidObjectsLayer) != null)
        {
            return false;
        }
        else {
            return true;
        }
    }

    IEnumerator Reaction()
    {
        transform.position = Vector2.MoveTowards(transform.position, reactionDirection, Time.deltaTime * 0.0001f);
        yield return new WaitForSeconds(1f);
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
    

    void Update()
    {
        if(isReacting && health > 0)
        {
            if(transform.position != reactionDirection)
            {
                Reaction();
                
            }else{
                isReacting = false;
            }
            
        }
        
    }
}
