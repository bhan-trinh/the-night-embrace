using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleGravityScript : MonoBehaviour
{
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AppleGravity());

    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator AppleGravity()
    {
        yield return new WaitForSeconds(0.5f);
        rb.gravityScale = 0.0f;
        rb.velocity = Vector2.zero;
        Debug.Log("hi");
    }
}
