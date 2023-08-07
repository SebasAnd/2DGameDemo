using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float health = 10f;

    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject allBar;
    [SerializeField] private TMPro.TMP_Text damageText;
    [SerializeField] private GameObject damageCanvas;
    private bool canBeDamaged = true;
    Animator anim;
    [SerializeField]private LayerMask solidObjectsLayer;
    private bool isReacting = false;
    private Vector3 reactionDirection;
    private float maxHealth = 10f;
    public AttackArea attackArea;
    void Start()
    {
        anim = GetComponent<Animator>();
        maxHealth = health;
        allBar.SetActive(false);
        damageCanvas.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {        
        if( collider && collider.gameObject.tag  == "Attack" && canBeDamaged )
        {
            PlayerDamage playerDamage = collider.gameObject.GetComponent<PlayerDamage>();
            anim.Play("SlimeAttackReaction");
            StartCoroutine(DecrementHealth(playerDamage.player.damage));
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
            damageText.text = "-"+damage;
            healthBar.fillAmount = health / maxHealth; 
        }
        if(health > 0)
        {            
            allBar.SetActive(true);
            damageCanvas.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            damageCanvas.SetActive(false);
            yield return new WaitForSeconds(1.5f);
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
        GameManager gm = GameManager.instance;
        yield return new WaitForSeconds(1f);
         int selected = Random.Range(0, gm.generalItems.Length);
        Instantiate(gm.generalItems[selected],transform.position,Quaternion.Euler(0f,0f,0f),gm.consumableItemsContainer.transform);
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
