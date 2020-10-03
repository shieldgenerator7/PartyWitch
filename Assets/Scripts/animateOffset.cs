using UnityEngine;
using System.Collections;

public class animateOffset : MonoBehaviour
{
 
     public bool on = true;
	public bool AxisX = false;
	public bool AxisZ = true;
     public float scrollSpeed;
	 public float maxTime = 99999;
	private float offsetX;
	 private float offsetZ;
	 private float rate;
     
     private Material _material;
 
    void Awake ()
    {
        _material = GetComponent<Renderer>().material;
		rate = scrollSpeed * Time.deltaTime;
    }
 
    void Update ()
    {
 
            if (AxisX)
            {
                offsetX = Mathf.Min(maxTime, offsetX + rate);
                _material.mainTextureOffset = new Vector2(offsetX, 0);
            }

            if (AxisZ)
            {
                offsetZ = Mathf.Min(maxTime, offsetZ + rate);
                _material.mainTextureOffset = new Vector2(0, offsetZ);
            }
               
     }
 }