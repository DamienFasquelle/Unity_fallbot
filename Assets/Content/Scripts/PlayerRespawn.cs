using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Player))]
public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private float deathHeight = -8f;

    private Vector3 currentCheckpoint;

    private CharacterController controller;
    private Player player;

    private bool isRespawning;

    void Start()
    {
        currentCheckpoint = transform.position;

        controller = GetComponent<CharacterController>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (transform.position.y <= deathHeight && !isRespawning)
        {
            Respawn();
        }
    }

    public void SetCheckpoint(Vector3 newCheckpointPosition)
    {
        currentCheckpoint = newCheckpointPosition;
        Debug.Log("Checkpoint mis à jour !");
    }

    public void Respawn()
    {
        isRespawning = true;

      
        controller.enabled = false;

     
        player.State.VerticalVelocity = 0f;
        player.State.HorizontalVelocity = Vector2.zero;
        player.State.ExtraVelocity = Vector3.zero;
        player.State.GroundVelocity = Vector3.zero;
        player.State.IsGrounded = false;

    
        player.State.CurrentState = Player.PlayerState.Idle;

      
        transform.position = currentCheckpoint;

     
        controller.enabled = true;

        
        Invoke(nameof(ResetRespawnFlag), 0.2f);
    }

    void ResetRespawnFlag()
    {
        isRespawning = false;
    }
}