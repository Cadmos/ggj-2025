using UnityEngine;

namespace GGJ.GameFlow
{
    [CreateAssetMenu(fileName = "GravityAbility", menuName = "Scriptable Objects/Character/Abilities/Gravity")]
    public class Gravity : AbilityBase
    {
        public float gravity = -9.81f;
        
        public override void UpdateAbility(float deltaTime)
        {
            controller.Move(new Vector3(0, gravity * deltaTime, 0));
        }
    }
}
