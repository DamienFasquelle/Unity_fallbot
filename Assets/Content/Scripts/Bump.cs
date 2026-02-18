using System;
using Unity.VisualScripting;
using UnityEngine;

public class Bump : MonoBehaviour
{
   public static Player Owner { get; private set; }
   [SerializeField] private float _force = 60;
   [SerializeField] private float _duration = .4f;
   [SerializeField] private AnimationCurve curve = AnimationCurve.EaseInOut(0,1,1,0);
   private void OnTriggerEnter(Collider col)
   {
      if(col.tag == "Player")
         Player.Owner.AddExtraForce(Vector3.up * _force,true, _duration,curve);
   }
}
