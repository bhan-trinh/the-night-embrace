using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTileScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthScript health;
        if (health = collision.GetComponent<HealthScript>())
        {

            IEnumerator HurtAnim()
            {
                collision.GetComponent<SpriteRenderer>().color = new Color32(255, 163, 172, 255);
                yield return new WaitForSeconds(0.2f);
                collision.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            }

            health.GetHit(1, gameObject);
            StartCoroutine(HurtAnim());

        }
    }
}
