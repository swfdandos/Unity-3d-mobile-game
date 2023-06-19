using UnityEngine;

public class kameraTakip : MonoBehaviour
{
    

    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float startDistance = 5.0f; // Baþlangýç pozisyonundaki mesafe
    private Vector3 startPosition;

    void Start()
    {
        startPosition = target.position - offset.normalized * startDistance; // Baþlangýç pozisyonunu belirleme
        transform.position = startPosition; // Kameranýn baþlangýç pozisyonunu güncelleme
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(target);
    }

}
