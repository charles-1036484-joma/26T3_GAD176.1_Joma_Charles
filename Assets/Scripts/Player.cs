using SAE.GAD170.P1.CBC;
using UnityEngine;

public class Player : CharacterBaseClass
{
    [SerializeField]private Karma playerKarma;


    #region Unity Specific Functions
    void Start()
    {
        playerKarma = GetComponent<Karma>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
}
