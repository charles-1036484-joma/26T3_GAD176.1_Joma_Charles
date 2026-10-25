using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// This should live on the player and track his morality
public class Karma : MonoBehaviour
{
    // Enum = user defined data type. consist of named constant values || const Bad etc.
    // AKA Predefined values
    public enum KarmaStatus
    {
        Bad,
        Neutral,
        Good,
    }

    #region Karma Range. Min. Max
    [Header("Karma Range")] // group these fields under this heading
    [SerializeField]
    private int minKarmaValue = 0;

    [SerializeField]
    private int currentGoodKarmaValue = 0;

    [SerializeField]
    private int currentBadKarmaValue = 0;

    #region Karma Status
    [Header("Karma Status Thresholds")]
    [SerializeField] // If player is at or below this number they are bad
    private int badThreshold = 70;

    [SerializeField] // Same here ^^^
    private int goodThreshold = 70;

    #endregion

    #region OnKarmaChanged Event
    [SerializeField] // This event will pass two pieces of data to whoever is listening, an int and a KarmaStatus
    public UnityEvent<int, KarmaStatus> onKarmaChanged = new UnityEvent<int, KarmaStatus>(); // I made this protected, in future when i expand this system I may need another class inheriting from Karma
    #endregion


    // I WILL NEED TO USE THESE FUNCTIONS LATER WHEN LISTENING TO PROXIMITY, THEY WORK BUT NOT FOR MY PURPOSE ATM
    // #region Event Listener functions
    // public void SubscribeToKarmaListener(UnityAction<int, KarmaStatus> listener)
    // {
    //     onKarmaChanged.AddListener(listener);
    //     Debug.Log("SubscribeToKarmaListener Called");
    // }

    // public void UnsubscribeFromKarmaListener(UnityAction<int, KarmaStatus> listener)
    // {
    //     onKarmaChanged.RemoveListener(listener);
    //     Debug.Log("UnsubscribeFromKarmaListener Called");
    // }

    // #endregion

    #region Functions to allow changing of Karma externally

    // This is the only way to change Karma, positive numbers add and negative subtract
    // This is then used to determine, bad, neutral or good
    public void AddGoodKarma(int amount)
    {
        currentGoodKarmaValue += amount;
        Debug.Log("Karma has changed by: " + amount);
        // Since this changes Karma so we need to call it/invoke it
        // onKarmaChanged?.Invoke(currentKarmaValue, CurrentStatus);
        Debug.Log("You are Character is: " + CurrentStatus);
    }

    public void AddBadKarma(int amount)
    {
        currentBadKarmaValue += amount;
        Debug.Log("Karma has changed by: " + amount);
        // Since this changes Karma so we need to call it/invoke it
        // onKarmaChanged?.Invoke(currentKarmaValue, CurrentStatus);
        Debug.Log("You are Character is: " + CurrentStatus);
    }

    // This is a read-only property, when you use this 'CurrentStatus' it will get and return the result of the if value
    // I only want this script to be able to influence currentStatus
    public KarmaStatus CurrentStatus
    {
        get
        {
            // Neutral Cases
            if (
                currentGoodKarmaValue <= goodThreshold && currentBadKarmaValue < badThreshold
                || currentGoodKarmaValue >= goodThreshold && currentBadKarmaValue >= badThreshold
            )
            {
                Debug.Log("Your Character is Neutral");
                return KarmaStatus.Neutral;
            }
            if (currentGoodKarmaValue >= goodThreshold && currentBadKarmaValue <= badThreshold)
            {
                Debug.Log("Your Character is Good");
                return KarmaStatus.Good;
            }
            if (currentBadKarmaValue >= badThreshold && currentGoodKarmaValue < goodThreshold)
            {
                Debug.Log("Your Character is Bad");
                return KarmaStatus.Bad;
            }
            Debug.Log("Your Character is Neutral");
            return KarmaStatus.Neutral;
        }
    }

    // These are the Getter functions to create accessibility across other scripts, they simply return the respective variables
    public int publicCurrentGoodKarmaValue
    {
        get { return currentGoodKarmaValue; }
    } // the => is shorthand return get method

    // public int CheckKarmaValueAndAddStats
    // {
    //     get { 
    //         if(currentGoodKarmaValue > goodThreshold)
    //         {
                
    //         }

    //      }
    // } // 
    public int publicCurrentBadKarmaValue
    {
        get { return currentBadKarmaValue; }
    } // it is pretty much a get function every time it is called it gets the value, recalculating it.
    #endregion

    #endregion


    #region Unity Start & Update
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update() { }
    #endregion
}
