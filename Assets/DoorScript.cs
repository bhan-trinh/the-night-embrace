using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public HealthScript health;
    public GameObject doorText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health.usedItemOn)
        {
            Destroy(doorText);
            Destroy(gameObject);
        }
    }
}
