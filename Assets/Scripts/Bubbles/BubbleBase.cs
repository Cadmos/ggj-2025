using System;
using UnityEngine;

namespace GGJ.Bubbles
{
    public class BubbleBase : MonoBehaviour
    {
        [SerializeField] private float floatSpeed;
        [SerializeField] private Vector2 floatRange;

        private void Update()
        {
            //transform.position += new Vector3(0, floatSpeed, 0);
            //transform.SetPositionAndRotation(new Vector3(transform.position.x, Mathf.PingPong(Time.time * floatSpeed, floatRange.y - floatRange.x) + floatRange.x, transform.position.z), transform.rotation);
            //transform.Translate(new Vector3(0, floatSpeed, 0));
        }

        protected void FloatUp()
        {
            transform.position += new Vector3(0, floatSpeed, 0);
        }
    }
}
