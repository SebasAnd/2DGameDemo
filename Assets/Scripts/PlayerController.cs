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
                //rb.velocity = new Vector2(input.x * moveSpeed, input.y * moveSpeed);
                Vector2 targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;
                 
                StartCoroutine(Move(targetPos));
            }
        }
        anim.SetBool("isMoving", isMoving);
    }
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

}
