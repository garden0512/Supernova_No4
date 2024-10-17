using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Array : MonoBehaviour
{
    private float xpos;
    private float ypos;
    private float objectHeight;

    private Transform tf;
    private Renderer rend;

    private void Start()
    {
        tf = GetComponent<Transform>();
        rend = GetComponent<Renderer>();

        if (rend != null)
        {
            objectHeight = rend.bounds.size.y;
        }
    }

    private void Update()
    {
        xpos = tf.position.x;
        ypos = tf.position.y;

        float bottomY = ypos - objectHeight / 2 + 0.3f;
        tf.position = new Vector3(xpos, ypos, bottomY / 1000.0f);
    }
}
