using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float health = 10f;
    [SerializeField] private float EnemyDamage = 1f;
    [SerializeField] private float runSpeed = 3f;
    private bool canBeDamaged = true;
    Animator anim;
    [SerializeField]private LayerMask solidObjectsLayer;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.gameObject.name);
        if( collider && collider.gameObject.tag  == "Attack" && canBeDamaged)
        {            
            anim.Play("SlimeAttackReaction");
            StartCoroutine(DecrementHealth(1f));
            AttackReaction(collider.transform.position);
        }
    }
    // Update is called once per frame


    public IEnumerator DecrementHealth(float damage)
    {
        canBeDamaged = false;        
        if(health - damage < 0f)
        {
            health = 0;
        }else{
            health -= damage;
        }
        Debug.Log("enemy health = " + health);
        yield return new WaitForSeconds(2f);
        canBeDamaged = true;
        anim.Play("SlimeEnemyIdle");
    }

    public void AttackReaction(Vector3 otherPosition)
    {
        Vector3 limit = transform.position = otherPosition + Random.Range(-0.5f,0.5f) *transform.position; 
        if(IsAvaliablePosition(limit))
        {
            transform.position = limit;
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

    void Update()
    {
        
    }
}
