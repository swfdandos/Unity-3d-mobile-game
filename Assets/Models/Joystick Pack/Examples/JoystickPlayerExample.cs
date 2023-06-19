using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public FixedJoystick variableJoystick;
    public Rigidbody rb;
    public Animator animator;

    private bool isWalking = false;

    public void FixedUpdate()
    {
        // karakterin yönünü belirlemek için joystick'ten gelen yön vektörü
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;

        if (direction.magnitude > 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        animator.SetBool("isWalking", isWalking);

        // karakterin rotasyonunu güncelle
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(rotation);
            rb.angularVelocity = Vector3.zero;
        }

        // karakteri hareket ettir
        rb.velocity = direction.normalized * speed;
    }
}