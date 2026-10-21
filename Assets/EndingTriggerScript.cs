using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTriggerScript : MonoBehaviour
{
    public LogicScript logic;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.Find("Logic Manager").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer==6)
        {
            Debug.Log("ending activae");
            StartCoroutine(logic.Ending());
        }
    }
}
