using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private bool isMoving;

    public Vector2 input;

    private Animator anim;

    [SerializeField] private GameObject[] attackPoints;

    [SerializeField] private LayerMask solidObjectsLayer;

    public float damage = 1f;

    [SerializeField] private Image Health;
    [SerializeField] private TMPro.TMP_Text damagetext;
    [SerializeField] float maxHealth = 10f;
    [SerializeField] float currentHealth = 10f;

    [SerializeField] private GameObject deathInterface;

    private Vector3 initialPosition;
    void Awake()
    {
        anim = GetComponent<Animator>();
        if (damage <= 0)
        {
            damage = 1f;
        }
        damagetext.text = "+" + damage;
        initialPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving && currentHealth > 0)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            if (input.x != 0) input.y = 0;
            if (input.y != 0) input.x = 0;

            if (input != Vector2.zero)
            {
                anim.SetFloat("moveX", input.x);
                anim.SetFloat("moveY", input.y);
                Vector2 targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
                
            }
            if (Input.GetButtonUp("Attack") )
            {
                anim.SetBool("isAttacking", false);
                HidePoints();
            }
            if (Input.GetButtonDown("Attack") )
            {
                anim.SetBool("isAttacking", true);
                if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "RightIdle")
                {
                    ShowPoint(2);
                }
                if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "UpIdle")
                {
                    ShowPoint(0);
                }
                if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "LeftIdle")
                {
                    ShowPoint(3);
                }
                if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "DownIdle")
                {
                    ShowPoint(1);
                }

            }
            else
            {
                //anim.SetBool("isAttacking", false);
                anim.SetBool("isMoving", isMoving);
            }
        }
        
        
    }
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        anim.SetBool("isAttacking", false);
        HidePoints();
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }
    private void HidePoints()
    {
        for(int i=0; i < attackPoints.Length; i++)
        {
            attackPoints[i].SetActive(false);
        }
    }
    private void ShowPoint(int index)
    {
        attackPoints[index].SetActive(true);      
    }
    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, .2f, solidObjectsLayer) != null)
        {
            return false;
        }
        else {
            return true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            BulletBehaviour enemy = collision.GetComponent<BulletBehaviour>();
            anim.SetBool("isHurt", true);
            currentHealth -= enemy.damage;
            StartCoroutine(WaitHurt());
            Health.fillAmount = currentHealth / maxHealth;
        }
        if (currentHealth <= 0)
        {
            anim.SetBool("isDeath", true);            
            StartCoroutine(DeathInterface());
        }
    }
    IEnumerator DeathInterface()
    {
        yield return new WaitForSeconds(1f);
        deathInterface.SetActive(true);
    }
    public void Restart()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        transform.position = initialPosition;
        currentHealth = maxHealth;
        Health.fillAmount = currentHealth / maxHealth;
        anim.SetBool("isDeath", false);
        deathInterface.SetActive(false);
        StartCoroutine(Rebirth());
    }

    IEnumerator Rebirth()
    {
        yield return new WaitForSeconds(1f);
        deathInterface.SetActive(false);
        GameManager.instance.RefreshEnemies();
    }
    IEnumerator WaitHurt()
    {
        yield return new WaitForSeconds(.2f);
        anim.SetBool("isHurt", false);
        
    }
}
