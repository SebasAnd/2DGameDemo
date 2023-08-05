using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private bool isMoving;

    public Vector2 input;

    private Rigidbody2D rb;

    private Animator anim;

    [SerializeField] private GameObject[] attackPoints;

    [SerializeField]private LayerMask solidObjectsLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
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
        }
        if(Input.GetButtonUp("Attack")&& !isMoving)
        {
            anim.SetBool("isAttacking", false);
            HidePoints();
        }
        if(Input.GetButtonDown("Attack") && !isMoving)
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
        else{
            //anim.SetBool("isAttacking", false);
            anim.SetBool("isMoving", isMoving);
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

}
