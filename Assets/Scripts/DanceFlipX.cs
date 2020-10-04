using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceFlipX : MonoBehaviour
{
	public bool random = false;
	public float randomMin	= 0f;
	public float randomMax = 5f;
	public float fixedRate	= 1f;
	private float timeRemaining = 10f;
	public float timeRemainingStored = 10f;
	private bool FacingRight = true;  // For determining which way the player is currently facing.
	
    // Update is called once per frame
    void Update()
    {
		if (timeRemaining > 0)
		{
			timeRemaining -= Time.deltaTime;
		}
		else
		{
			Flip();
		}
		
		if (random)
		{
			if (timeRemaining > 0)
			{
				timeRemaining -= Time.deltaTime;
			}
			else
			{
				Flip();
				timeRemaining = Random.Range(randomMin, randomMax);
			}	
		}	
		else
		{
		
			if (timeRemaining > 0)
				{
					timeRemaining -= Time.deltaTime;
				}
			else
				{
				Flip();
					timeRemaining = timeRemainingStored;
				}
		}
		

    }
	
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        FacingRight = !FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
