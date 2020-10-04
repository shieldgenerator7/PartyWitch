using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public float moveSpeed = 5;
    public float endY = 2100;

    public delegate void OnFinish();
    public OnFinish onFinish;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * moveSpeed;
        if (transform.position.y >= endY)
        {
            onFinish?.Invoke();
        }
    }
}
