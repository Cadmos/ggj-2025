using System;
using UnityEngine;

namespace GGJ
{
    [RequireComponent(typeof(Transform))]
    public class FloatUp : MonoBehaviour
    {
        private Transform _transform;
        [SerializeField] private float initialFloatSpeed;
        [SerializeField] private float floatSpeed;
        [SerializeField] private float maxFloatSpeed;
        private void Start()
        {
            _transform = GetComponent<Transform>();
            floatSpeed = initialFloatSpeed;
        }

        private void Update()
        {
            AlignDirection();
            ModifySpeed();
            UpdatePosition();
        }

        private void AlignDirection()
        {
            
        }

        private void ModifySpeed()
        {
            if (floatSpeed > maxFloatSpeed)
            {
                floatSpeed = initialFloatSpeed;
                return;
            }
            floatSpeed += floatSpeed;
        }

        private void UpdatePosition()
        {
            
            var newPos = _transform.position;
            newPos.y += floatSpeed;
            _transform.Translate(new Vector3(1,1+newPos.y,1), Space.Self);
            //_transform.SetPositionAndRotation(newPos, _transform.rotation);
        }
    }
}
