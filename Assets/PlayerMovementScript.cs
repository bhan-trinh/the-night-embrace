using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float movementSpeed;
    public Animator animator;
	public bool m_FacingRight;
    // Update is called once per frame
    void Update()
    {
		// Handle user input

		animator.SetBool("Moving", false);
        Vector2 targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Move(targetVelocity, Input.GetAxisRaw("Horizontal"));

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 || Mathf.Abs(Input.GetAxisRaw("Vertical"))>0)
        {
            animator.SetBool("Moving",true);
        }


    }


	void Move(Vector2 targetVelocity, float move)
	{

		// If the input is moving the player right and the player is facing left...
		if (move > 0 && !m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (move < 0 && m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}

		// Set rigidbody velocity
		rb.velocity = (targetVelocity * movementSpeed * 10); //* Time.deltaTime; // Multiply the target by deltaTime to make movement speed consistent across different framerates
	}


	void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;


	}
}