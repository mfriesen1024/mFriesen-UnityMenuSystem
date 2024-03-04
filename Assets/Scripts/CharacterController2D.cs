using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] float primaryMoveSpeed = 1;
    [SerializeField] float sprintMultiplier = 2;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    Vector2 move;
    bool isSprinting = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
        UpdateMovementAnimation();
    }

    void OnSprint(InputValue value)
    {
        isSprinting = Convert.ToBoolean(value.Get<float>());
        //Debug.Log(isSprinting);
    }

    private void UpdateMovementAnimation()
    {
        bool isMoving = false;

        // Animation things
        if (move != Vector2.zero)
        {
            // Set isMoving to true, because we know we're moving.
            isMoving = true;

            animator.SetFloat("moveX", move.x);
            animator.SetFloat("moveY", move.y);
        }
        animator.SetBool("move", isMoving);
        animator.SetBool("sprint", isSprinting);
    }

    void OnQuit()
    {
        Application.Quit();
    }


    private void MoveCharacter()
    {
        Vector2 tempMove = move.normalized * Time.deltaTime * primaryMoveSpeed;

        if (isSprinting) { tempMove *= sprintMultiplier; }

        float x = tempMove.x + transform.position.x;
        float y = tempMove.y + transform.position.y;
        tempMove = new Vector2(x, y);
        rb.MovePosition(tempMove);
    }
}
