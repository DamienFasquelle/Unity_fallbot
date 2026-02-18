using UnityEngine;

public class LateralWallMoving : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float distance = 5f;
    [SerializeField] private float ejectForce = 10f;
    [SerializeField] private AnimationCurve curve = AnimationCurve.EaseInOut(0,1,1,0);

    private Vector3 startPosition;
    private float randomOffset;
    private float previousOffset;

    void Start()
    {
        startPosition = transform.position;
        randomOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
        previousOffset = Mathf.Sin(Time.time * speed + randomOffset) * distance;
        float offset = Mathf.Sin(Time.time * speed + randomOffset) * distance;
        transform.position = startPosition + Vector3.right * offset;
    }

    void OnTriggerEnter(Collider col)
    {
        if (Player.Owner && Player.Owner.gameObject == col.gameObject)
        {
            Vector3 force = transform.InverseTransformPoint(col.transform.position).x > 0 ? Vector3.right :  Vector3.left;
            Player.Owner.AddExtraForce(force * 100, false, 0.2f, curve);
            
        }
    }
}