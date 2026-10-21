using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviourScript : MonoBehaviour
{
    public HealthScript health;
    public int behaviour;
    public Rigidbody2D rb;
    public GameObject portal;
    public LogicScript logic;
    public Animator animator;

    public GameObject flowerprefab;
    private bool counter;

    public bool movingDone;
    public int waypointIndex;
    public GameObject WP;
    public List<Vector2> waypoints;

    public bool hehe;



    // Start is called before the first frame update
    void Start()
    {
        health = gameObject.GetComponent<HealthScript>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        logic = GameObject.Find("Logic Manager").GetComponent<LogicScript>();
        for (int i = 0; i < WP.transform.childCount; i++)
        {
            waypoints.Add(WP.transform.GetChild(i).transform.position);
        }
        waypointIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (health.usedItemOn)
        {
            
            StartCoroutine(WalkToPortal());

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
                        Instantiate(flowerprefab, new Vector2(transform.position.x, transform.position.y - 1), transform.rotation);
                        counter = true;
                    }
                    if (waypointIndex + 1 < waypoints.Count)
                    {
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


        if (health.isDead && !counter)
        {
            Instantiate(flowerprefab, new Vector2(transform.position.x, transform.position.y - 1), transform.rotation);
            Destroy(GameObject.Find("CreatureBlock"));
            counter = true;
        }
    }
      
        

        private void movingTorwards(Vector2 target)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, target, 2 * Time.deltaTime);

        }

    public IEnumerator WalkToPortal()
    {
        if (!hehe)
        {
            Destroy(gameObject.transform.GetChild(0).gameObject);
            Destroy(GameObject.Find("CreatureBlock"));
            logic.activatePortal();
            GameObject.Find("Portal").SetActive(false); 
            gameObject.GetComponent<Animator>().SetBool("Apple", true);
            yield return new WaitForSeconds(2);
            gameObject.GetComponent<Animator>().SetBool("Walking", true);
            hehe = true;
        }

    }

}
