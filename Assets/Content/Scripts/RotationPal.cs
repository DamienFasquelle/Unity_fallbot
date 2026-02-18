using UnityEngine;

public class RotationPal : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f; // degrés par seconde
    [SerializeField] private Vector3 rotationAxis = Vector3.up; // axe Y par défaut

    void Update()
    {
        transform.Rotate(rotationAxis * (rotationSpeed * Time.deltaTime));
    }
}