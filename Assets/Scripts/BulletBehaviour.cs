using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public GameObject target;

    public Vector3 position;

    private Rigidbody2D rb;

    [SerializeField] private float bulletLife = 1f;
    public float damage;

    void  Start()
    {
        rb = GetComponent<Rigidbody2D>();
        position = target.transform.position - transform.position;
        StartCoroutine(BulletCleaner());
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag != "AttackArea" && collider.gameObject.tag != "Enemy")
        {
            Destroy(this.gameObject); 
        }      
    }

    private IEnumerator BulletCleaner()
    {
        yield return new WaitForSeconds(bulletLife);
        Destroy(this.gameObject);
    }
    


    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, position, Time.deltaTime * bulletSpeed);
        rb.velocity= position * 0.5f;   
    }
}
