using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerScript : MonoBehaviour
{
    public Transform player;
    public bool isCamFollow;
    public bool endingPanCam;

    // Start is called before the first frame update

    private void OnLevelWasLoaded(int level)
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCamFollow)
        {
            if (player != null)
            {
                transform.position = player.transform.position + new Vector3(0, 1, -5);
            }
           
        }
        
        else if (endingPanCam)
        {
        
        }
    }
}
