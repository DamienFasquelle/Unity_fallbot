using UnityEngine;

public class ElevatorBoat : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float riseHeight = 10f;
    [SerializeField] private float detectionRadius = 2f;

    private Vector3 startPosition;
    private Transform player;

    void Start()
    {
        startPosition = transform.position;
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        bool playerOnBoard = distanceToPlayer < detectionRadius;

        Vector3 targetPosition = playerOnBoard 
            ? startPosition + Vector3.up * riseHeight 
            : startPosition;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}