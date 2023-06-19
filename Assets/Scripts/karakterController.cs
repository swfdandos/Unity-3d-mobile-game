using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class karakterController : MonoBehaviour
{
    public float speed;
    public FixedJoystick variableJoystick;
    public Rigidbody rb;
    public Animator animator;

    private bool isWalking = false;

    public void FixedUpdate()
    {
        // karakterin y�n�n� belirlemek i�in joystick'ten gelen y�n vekt�r�
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

        // karakterin rotasyonunu g�ncelle
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
