using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHandParentScript : MonoBehaviour
{
    public Transform circleOrigin;
    public float radius;
    public int attackPower;


    private void Start()
    {
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }


    public float delay = 2f;
    public void detectCollidersAttack()
    {
        attackPower = GameObject.Find("Logic Manager").GetComponent<LogicScript>().attackPower;
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius))
        {

            HealthScript health;
            if (health = collider.GetComponent<HealthScript>())
            {
                Debug.Log(collider.GetComponent<SpriteRenderer>());
               
                health.GetHit(attackPower, transform.parent.gameObject);
               
                

            }
        }


    }

    public void detectCollidersUse() //find objects in range to use item on
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius))
        {
            HealthScript health;
            if (health = collider.GetComponent<HealthScript>())
            {
                Debug.Log(collider.GetComponent<SpriteRenderer>());

                health.GetUsedOn(); //Checking if the item on hand is the right one and use item
            }
        }

    }

}
