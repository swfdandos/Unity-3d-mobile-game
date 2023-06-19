using UnityEngine;

public class kameraTakip : MonoBehaviour
{
    

    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float startDistance = 5.0f; // Ba�lang�� pozisyonundaki mesafe
    private Vector3 startPosition;

    void Start()
    {
        startPosition = target.position - offset.normalized * startDistance; // Ba�lang�� pozisyonunu belirleme
        transform.position = startPosition; // Kameran�n ba�lang�� pozisyonunu g�ncelleme
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(target);
    }

}
