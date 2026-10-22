using SAE.GAD170.See.P1.Player;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private int currentLevel = 1;

    [SerializeField]
    private int MaxLevel = 10;


    public int PublicCurrentPlayerLevel
    {
        get { return currentLevel; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update() { }
}
