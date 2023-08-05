using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private GameObject origin;
    [SerializeField] private GameObject objective;
    [SerializeField] private GameObject ammo;
    private bool stillInArea = false;
    [SerializeField] private float period = 3f;
    private void OnTriggerEnter2D(Collider2D collider)
    {  
           
        if(collider.gameObject.tag == "Player")
        {
            objective = collider.gameObject;
            //Debug.Log(collider.gameObject.tag);
            stillInArea = true;
            InvokeRepeating("BulletInstancer", 0f, period);
            
        }
        
    }
    private void OnTriggerExit2D(Collider2D collider)
    {        
        if(collider.gameObject.tag == "Player")
        {
            //Objective = collider.gameObject;
            //InvokeRepeating("BulletInstancer", .5f, 3f);
            stillInArea = false;
        }
        
    }

    public void BulletInstancer()
    {
        if (stillInArea)
        {
            ammo.GetComponent<BulletBehaviour>().target = objective;
            GameObject bullet = Instantiate(ammo, transform);
        }
            
    }
    private void Update()
    {
        if (stillInArea)
        {
            //Debug.DrawRay(transform.position, objective.transform.position - transform.position);
            //Physics2D.Raycast(transform.position, objective.transform.position - transform.position);
        }
    }


}
