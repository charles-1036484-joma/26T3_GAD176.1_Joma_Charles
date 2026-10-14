using SAE.GAD170.P1.CBC;
using UnityEngine;

public class Player : CharacterBaseClass
{
    private Karma playerKarma;

    [SerializeField]
    private Karma.KarmaStatus CurrentStatus; 

    void Start()
    {
        playerKarma = GetComponent<Karma>(); // Get the Karma "Component" that is attached to the object this Script is attached to
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
        
    }
}
