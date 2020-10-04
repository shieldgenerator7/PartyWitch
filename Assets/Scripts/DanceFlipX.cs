using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceFlipX : MonoBehaviour
{
	public bool random = false;
	public float randomMin	= 0f;
	public float randomMax = 5f;
	public float fixedRate	= 1f;
	private float timeRemaining = .5f;
	private float timeRemainingFixed = 1f;
	private bool FacingRight = true;  // For determining which way the player is currently facing.
	
	public bool spriteSwap = false; 
	private bool isSprite01 = true;
	public SpriteRenderer spriteRenderer;
	public Sprite sprite01;
	public Sprite sprite02;
	
	void Start(){
		timeRemainingFixed = fixedRate;
	}
	
    // Update is called once per frame
    void Update()
    {	
		//random
		if (random)
		{
			//countdown
			if (timeRemaining > 0)
			{
				timeRemaining -= Time.deltaTime;
			}
			//flip reset to random
			else
			{
				if (spriteSwap)
				{
					if (isSprite01)
					{
						spriteRenderer.sprite = sprite02; 
						isSprite01 = !isSprite01;
						timeRemaining = Random.Range(randomMin, randomMax);
					}
					else
					{
						spriteRenderer.sprite = sprite01; 
						isSprite01 = !isSprite01;
						timeRemaining = Random.Range(randomMin, randomMax);
					}
				}
				else
				{
					Flip();
					timeRemaining = Random.Range(randomMin, randomMax);
				}
				
			}	
		}	
		//not random
		else if (!random)
		{
			//countdown
			if (timeRemaining > 0)
			{
				timeRemaining -= Time.deltaTime;
			}
			//flip reset to random	
			else
			{
				if (spriteSwap)
				{
					if (isSprite01)
					{
						spriteRenderer.sprite = sprite02; 
						isSprite01 = !isSprite01;
						timeRemaining = timeRemainingFixed;
					}
					else
					{
						spriteRenderer.sprite = sprite01; 
						isSprite01 = !isSprite01;
						timeRemaining = timeRemainingFixed;
					}
				}
				else
				{
					Flip();
					timeRemaining = timeRemainingFixed;
				}
			}
		}
	
		void Flip()
		{
			// Switch the way the player is labelled as facing.
			FacingRight = !FacingRight;
			
			// Multiply the player's x local scale by -1.
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}
}
