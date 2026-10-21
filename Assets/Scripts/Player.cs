using SAE.GAD170.P1.CBC;
using UnityEngine;
using UnityEngine.UI;

namespace SAE.GAD170.See.P1.Player
{
    public class Player : CharacterBaseClass
    {
        [SerializeField]
        private Karma playerKarma;

        [SerializeField]
        public Level playerLevel
        {
            get { return playerLevel; }
        }

        [SerializeField]
        private Health playerHealth;

        [SerializeField]
        private int playerSpeed;

        [SerializeField]
        private int playerFearFactor;

        [SerializeField]
        private int playerJumpHeight;

        protected Karma.KarmaStatus type;

        public StatsManager GoodSlider;
        public StatsManager BadSlider;

        #region Unity specific functions
        void Start()
        {
            playerKarma = GetComponent<Karma>(); // Get the Karma "Component" that is attached to the object this Script is attached to
            // playerLevel = GetComponent<Level>();
            playerHealth = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            // if player touches red cube
            // null check onKarmaChanged
            // invoke RemoveKarma

            /*
                if player touches gold cube
                    null check onKarmaChanged
                    invoke AddKarma
            */
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "GoodAct")
            {
                // Add good karma to player
                playerKarma.AddGoodKarma(10);
                // Adjust the good karma slider, add the value to the slider
                GoodSlider.GoodKarmaBar.value = playerKarma.publicCurrentGoodKarmaValue;
                // this just uses the get as the callback function, but i want to make the function it invokes more useful rather than just a get.
                playerKarma.onKarmaChanged.Invoke(
                    playerKarma.publicCurrentGoodKarmaValue,
                    playerKarma.CurrentStatus
                );
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag == "BadAct")
            {
                playerKarma.AddBadKarma(10);
                BadSlider.BadKarmaBar.value = playerKarma.publicCurrentBadKarmaValue;
                playerKarma.onKarmaChanged.Invoke(
                    playerKarma.publicCurrentGoodKarmaValue,
                    playerKarma.CurrentStatus
                );
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.tag == "Neutral")
            {
                return;
            }
        }
        #endregion
    }
}
