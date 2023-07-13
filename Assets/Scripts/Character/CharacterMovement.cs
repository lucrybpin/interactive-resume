using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(Animator))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float walkSpeed = 1f;

    [SerializeField] Vector2 direction;
    Rigidbody2D rigidBody;
    Animator animator;

    public Vector2 Direction { get => direction; }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rigidBody.velocity = direction * walkSpeed;

        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        animator.SetFloat("speed", rigidBody.velocity.magnitude);
        animator.SetFloat("horizontal", rigidBody.velocity.x);
        animator.SetFloat("vertical", rigidBody.velocity.y);
    }

    public void ResetInput()
    {
        direction = Vector2.zero;
        rigidBody.velocity = direction * 0;
        UpdateAnimations();
    }

}
