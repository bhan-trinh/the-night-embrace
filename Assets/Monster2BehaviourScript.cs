using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Monster2BehaviourScript : MonoBehaviour
{
    public HealthScript health;
    public int behaviour;
    public Rigidbody2D rb;
    public GameObject portal;
    public LogicScript logic;
    public Animator animator;
    public bool Scared;
    public bool counter;
    public GameObject keyPrefab;

    public Text text;
    public bool hehe;

    public bool movingDone;
    public GameObject WP;
    public List<Vector2> waypoints;

    public int waypointIndex;

    public GameObject gate;

    void Start()
    {
        health = gameObject.GetComponent<HealthScript>();
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        logic = GameObject.Find("Logic Manager").GetComponent<LogicScript>();
        for (int i = 0; i < WP.transform.childCount; i++)
        {
            waypoints.Add(WP.transform.GetChild(i).transform.position);
        }
        waypoints.Remove(waypoints[0]);
        waypointIndex = 0;
    }

    void Update()
    {
        if (health.usedItemOn)
        {
            StartCoroutine(Flower());
            if (hehe)
            {

                if (waypoints[waypointIndex] != new Vector2(transform.position.x, transform.position.y))
                {
                    movingTorwards(waypoints[waypointIndex]);
                }

                else
                {
                    if (waypointIndex == 1)
                    {
                        Debug.Log("destroyed");
                        Destroy(gate);
                    }
                    if (waypointIndex + 1 < waypoints.Count)
                    {
                        animator.SetTrigger("Scared");
                        waypointIndex++;

                    }

                    else
                    {
                        movingDone = true;
                        health.usedItemOn = false;
                    }
                }
            }
    
        }

        else if (!Scared)
        {
            StartCoroutine(suddenlyScared());
        }

        if (health.isDead && !counter)
        {
            Destroy(text.gameObject);
            Instantiate(keyPrefab, new Vector2(transform.position.x, transform.position.y - 1), transform.rotation);
            counter = true;
        }
    }

    private IEnumerator suddenlyScared()
    {
        Scared = true;
        yield return new WaitForSeconds(5);
        animator.SetTrigger("Scared");
        yield return new WaitForSeconds(1);
        Scared = false;
    }

    private void movingTorwards(Vector2 target)
    {
        
        transform.position = Vector2.MoveTowards(transform.position, target, 2 * Time.deltaTime);
        
    }

    public IEnumerator Flower()
    {
        if (!hehe)
        {
            
            animator.SetBool("Flower",true);
            yield return new WaitForSeconds(2);
            animator.SetBool("Flower", false);
            text.text = "\n" + "(It wants you to follow.)" + "\n";
            hehe = true;
        }
    }
}

