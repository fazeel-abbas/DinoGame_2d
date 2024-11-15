using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudMovement : MonoBehaviour
{
    public Vector2 pos1;
    public Vector2 pos2;
    public Vector2 posdiff = new Vector2(35f, 0f);
    float speed = 0.05f;


    // Start is called before the first frame update
    void Start()
    {
        pos1 = transform.position;
        pos2 = pos1 + posdiff;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 1.0f));
    }
 }
