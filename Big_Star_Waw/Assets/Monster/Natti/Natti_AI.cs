using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Natti_AI : MonoBehaviour
{
    public float speed;
    public float stopDistance;

    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
