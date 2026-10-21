using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleportScript : MonoBehaviour
{
    private LogicScript logic;
    public string MapName;

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
        if (collision.gameObject.layer == 6)
        {
            logic.teleportPortal(MapName);
        }

        else
        {
            Destroy(collision.gameObject);
        }
    }
}
