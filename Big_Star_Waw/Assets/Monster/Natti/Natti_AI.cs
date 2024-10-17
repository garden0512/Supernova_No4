using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Animator))]

public class Natti_AI : MonoBehaviour
{
    public float speed;
    public float stopDistance;

    private Transform target;
    private Animator animator;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // �������� �Ÿ� ���
        float distance = Vector2.Distance(transform.position, target.position);

        // ���� �Ÿ� �̻��� ���� ��� ����
        if (distance > stopDistance)
        {
            animator.SetBool("isWalking", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void FixedUpdate()
    {
        // ����� �������� ���� ���
        Vector2 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // 4���� (0��, 90��, 180��, 270��) �� ���� ����� �������� ����
        float[] fourDirections = { 0, 90, 180, 270 };
        float closestDirection = GetClosestDirection(angle, fourDirections);
        // �ִϸ��̼� ������Ʈ
        UpdateAnimation(closestDirection);
    }

    // ������ �޾� 4����(0, 90, 180, 270��) �� ���� ����� ���� ��ȯ
    private float GetClosestDirection(float angle, float[] directions)
    {
        float closest = directions[0];
        float minDifference = Mathf.Abs(Mathf.DeltaAngle(angle, closest));

        foreach (float dir in directions)
        {
            float difference = Mathf.Abs(Mathf.DeltaAngle(angle, dir));
            if (difference < minDifference)
            {
                minDifference = difference;
                closest = dir;
            }
        }

        return closest;
    }

    private void UpdateAnimation(float direction)
    {
        // ������ ���� �ִϸ����� �Ķ���� ����
        if (direction == 0)
        {
            animator.SetFloat("xDir", 1);
            animator.SetFloat("yDir", 0);
        }
        else if (direction == 90)
        {
            animator.SetFloat("xDir", 0);
            animator.SetFloat("yDir", 1);
        }
        else if (direction == 180)
        {
            animator.SetFloat("xDir", -1);
            animator.SetFloat("yDir", 0);
        }
        else if (direction == 270)
        {
            animator.SetFloat("xDir", 0);
            animator.SetFloat("yDir", -1);
        }
    }
}
