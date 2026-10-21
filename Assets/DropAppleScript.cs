using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAppleScript : MonoBehaviour
{
    public HealthScript health;
    public GameObject applePrefab;
    public List<GameObject> applePositions;
    private bool counter = true;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            applePositions.Add(gameObject.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health.isDead && counter)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Instantiate(applePrefab, applePositions[i].transform.position, transform.rotation);
            }
            counter = false;
        }
    }
}
