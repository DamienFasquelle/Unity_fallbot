using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraThirdPerson : MonoBehaviour
{
   [System.Serializable]
   private class Settings
   {
      [Tooltip("Camera follow smooth movement (0 = instant, 1 = smooth movement)")]
      [Range(0f, 1f)]
      public float FollowSmoothness = .1f;

      [Header("Position")]
      
      [Tooltip("Camera rotation speed")]
      public float LookSensitivity = 20f;
      
      [Tooltip("Camera distance from player")]
      public float Distance = 5;

      [Tooltip("Vertical offset from player")]
      public float VerticalOffset = 2;
      
      [Header("Pitch")]
      
      [Tooltip("Default pitch angle")]
      public float DefaultPitch= 20;
      
      [Tooltip("Min pitch angle")]
      public float MiniPitch = -30;
      
      [Tooltip("Max pitch angle")]
      public float MaxPitch = 60;
      
   }

   [System.Serializable]
   public class References
   {
      public InputActionAsset InputActions;
   }
   
   [SerializeField]
   private Settings _settings;

   [SerializeField]
   private References _references;
   
   private float _yaw;
   private float _pitch;

   private Vector3 _playerPosition;
   private InputAction _lookAction;

   void OnDrawGizmos() //visualiser qui pense être la position du joueur
   {
      Gizmos.color = Color.darkSalmon;
      Gizmos.DrawWireCube(GetPlayerPosition(), Vector3.one * .2f);
   }

   private void OnValidate()
   {
      _settings.DefaultPitch = Mathf.Clamp(_settings.DefaultPitch, _settings.MiniPitch, _settings.MaxPitch);
      
      _pitch = _settings.DefaultPitch;
      
      _playerPosition = GetPlayerPosition();
      
      SetPosition();
   }

   void OnEnable()
   {
      _lookAction = _references.InputActions.FindActionMap("Player").FindAction("Look");
      _lookAction?.Enable();
   }

   void OnDisable()
   {
      _lookAction?.Disable();
   }
   
   void Start()
   {
      
   }

   void Update()
   {
      float deltaTime = Time.deltaTime;
       
      _playerPosition = Vector3.Lerp(_playerPosition, GetPlayerPosition(),(1.1f - _settings.FollowSmoothness) * 20 * deltaTime);
      
      SetCursor();
      SetYawAndPitch(deltaTime);
      SetPosition();
   }

   private void SetCursor()
   {
      bool lockCursor = Player.Owner && !Player.Owner.State.IsPaused;

      Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
      Cursor.visible = !lockCursor;
   }

   private void SetYawAndPitch(float deltaTime)
   {

      if (!Player.Owner || Player.Owner.State.IsPaused)
         return;
        Vector2 lookInput = _lookAction ?.ReadValue<Vector2>() ?? Vector2.zero;

        _pitch -= lookInput.y * _settings.LookSensitivity * deltaTime;
        _yaw += lookInput.x * _settings.LookSensitivity * deltaTime;
        
        
   }
   
   private void SetPosition()
   {
      
      Vector3 cameraPosition = Vector3.forward * -_settings.Distance;
      cameraPosition = Quaternion.Euler(_pitch, _yaw, 0) * cameraPosition;
      cameraPosition +=  _playerPosition; 
      cameraPosition.y += _settings.VerticalOffset;
      
      transform.position = cameraPosition;
      transform.rotation = Quaternion.LookRotation(_playerPosition - cameraPosition,Vector3.up);
   }
   
   
   private Vector3 GetPlayerPosition()
   {
      Vector3 offset = Vector3.up * _settings.VerticalOffset;
      if (!Player.Owner)
         return Vector3.zero + offset;
      
      return Player.Owner.transform.position + offset;
   }
}
