using UnityEngine;

namespace GGJ
{
    public interface IAbility
    {
        void Initialize(GameObject owner);
        void OnInput(Vector2 input);   // or separate callbacks like OnButtonDown, OnButtonUp, etc.
        void Tick(float deltaTime);
        void OnEnableAbility();
        void OnDisableAbility();
    }
}
