using UnityEngine;

namespace SAE.GAD170.P1.CBC
{
    public class CharacterBaseClass : MonoBehaviour
    {
        // Put what both Player and NPC should have in this base class
        [SerializeField]
        private int maxHealth = 100;

        [SerializeField]
        private int health = 100;

        [SerializeField]
        private enum Action
        {
            Good,
            Neutral,
            Bad,
        }

        public void DealDamage(int amount) { }

        public void TakeDamage(int amount)
        {
            health = Mathf.Clamp(health - amount, 0, maxHealth);
        }

        public void Heal(int amount)
        {
            health = Mathf.Clamp(health + amount, 0, maxHealth);
        }

        #region UNITY SPECIFIC FUNCTIONS
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start() { }

        // Update is called once per frame
        void Update() { }
        #endregion
    }
}
