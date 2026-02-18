using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public enum MovementType
    {
        None,
        Linear,
        PingPong,
        Rotation,
        VerticalOscillation
    }

    [System.Serializable]
    private class MovementSettings
    {
        public MovementType Type = MovementType.Linear;

        [Header("Linear / PingPong")]
        public Vector3 Direction = Vector3.forward;
        public float Distance = 5f;

        [Header("Rotation")]
        public Vector3 RotationAxis = Vector3.up;
        
        [Header("Visual Rotation")]
        public bool RotateWhileMoving = false;
        public Vector3 VisualRotationAxis = Vector3.forward;
        public float RotationSpeed = 360f;

        [Header("General")]
        public float Speed = 2f;
        public float StartDelay = 0f;
        public bool Loop = true;
    }

    [SerializeField] 
    private MovementSettings _movementSettings;

    private Vector3 _startPosition;
    private float _movementTime;
    private bool _isActive;

    private void Start()
    {
        _startPosition = transform.position;
        Invoke(nameof(Activate), _movementSettings.StartDelay);
    }

    private void Activate()
    {
        _isActive = true;
    }

    private void Update()
    {
        if (!_isActive)
            return;

        switch (_movementSettings.Type)
        {
            case MovementType.Linear:
                HandleLinearMovement();
                break;

            case MovementType.PingPong:
                HandlePingPongMovement();
                break;

            case MovementType.Rotation:
                HandleRotation();
                break;

            case MovementType.VerticalOscillation:
                HandleVerticalOscillation();
                break;
        }
        
        if (_movementSettings.RotateWhileMoving)
        {
            transform.Rotate(
                _movementSettings.VisualRotationAxis *
                _movementSettings.RotationSpeed *
                Time.deltaTime,
                Space.Self
            );
        }
    }


    private void HandleLinearMovement()
    {
        _movementTime += Time.deltaTime * _movementSettings.Speed;

        transform.position = _startPosition + 
            _movementSettings.Direction.normalized * _movementTime;

        if (!_movementSettings.Loop && 
            _movementTime >= _movementSettings.Distance)
        {
            _isActive = false;
        }
    }

    private void HandlePingPongMovement()
    {
        float pingPong = Mathf.PingPong(
            Time.time * _movementSettings.Speed,
            _movementSettings.Distance
        );

        transform.position = _startPosition + 
            _movementSettings.Direction.normalized * pingPong;
    }

    private void HandleRotation()
    {
        transform.Rotate(
            _movementSettings.RotationAxis * 
            _movementSettings.Speed * Time.deltaTime
        );
    }

    private void HandleVerticalOscillation()
    {
        float yOffset = Mathf.Sin(
            Time.time * _movementSettings.Speed
        ) * _movementSettings.Distance;

        transform.position = new Vector3(
            _startPosition.x,
            _startPosition.y + yOffset,
            _startPosition.z
        );
    }
    
   

}
