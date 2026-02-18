using UnityEngine;

public class PlateformJump : MonoBehaviour
{
    [SerializeField] private float _force = 60;
    [SerializeField] private float _duration = 0.4f;
    [SerializeField] private AnimationCurve _curve = AnimationCurve.EaseInOut(0, 1, 1, 0);
    private void OnTriggerEnter(Collider col)
    {
        if (Player.Owner && col.gameObject == Player.Owner.gameObject)
        {
            Debug.Log("PLAYER HIT");
            Player.Owner.AddExtraForce(Vector3.up * _force, true, _duration, _curve);
        }
        
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
