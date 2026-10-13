using SAE.GAD170.P1.CBC;
using UnityEngine;

public class Player : CharacterBaseClass
{

    private Karma playerKarma;

   
    void Start()
    {
        playerKarma = GetComponent<Karma>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (playerKarma.onKarmaChanged != null) // Null Check
                {
                    playerKarma.onKarmaChanged.Invoke(10, playerKarma.CurrentStatus);
                    Debug.Log(playerKarma.CurrentStatus);
                }
        }
        Debug.Log(playerKarma.publicCurrentKarmaValue);
    }

}
