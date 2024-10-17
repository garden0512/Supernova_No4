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
        // 대상까지의 거리 계산
        float distance = Vector2.Distance(transform.position, target.position);

        // 일정 거리 이상일 때만 대상 추적
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
        // 대상의 방향으로 벡터 계산
        Vector2 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // 4방향 (0도, 90도, 180도, 270도) 중 가장 가까운 방향으로 맞춤
        float[] fourDirections = { 0, 90, 180, 270 };
        float closestDirection = GetClosestDirection(angle, fourDirections);
        // 애니메이션 업데이트
        UpdateAnimation(closestDirection);
    }

    // 각도를 받아 4방향(0, 90, 180, 270도) 중 가장 가까운 방향 반환
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
        // 각도에 따라 애니메이터 파라미터 설정
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
