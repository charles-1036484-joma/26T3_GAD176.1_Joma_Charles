using System;
using UnityEngine;
using UnityEngine.Events;

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

    #region OnKarmaChanged
    [SerializeField] // This event will pass two pieces of data to whoever is listening, an int and a KarmaStatus
    protected UnityEvent<int, KarmaStatus> onKarmaChanged = new UnityEvent<int, KarmaStatus>(); // I made this protected, in future when i expand this system I may need another class inheriting from Karma
    #endregion

    #region Karma Range. Min. Max
    [Header("Karma Range")] // group these fields under this heading
    [SerializeField]
    private int minKarmaValue = 0;

    [SerializeField]
    private int maxKarmaValue = 100;

    [SerializeField]
    private int currentKarmaValue = 50;

    // The are Read only variables so other scripts can look at values but not change it.
    public int publicCurrentKarmaValue => currentKarmaValue;
    public int publicMinKarmaValue => minKarmaValue;
    public int publicMaxKarmaValue => maxKarmaValue;
    #endregion

    #region Karma Status
    [Header("Karma Status Thresholds")]
    [SerializeField] // If player is at or below this number they are bad
    private int badThreshold = 30;

    [SerializeField] // Same here ^^^
    private int goodThreshold = 70;

    #endregion

    #region Functions to allow changing of Karma externally

    // This is the only way to change Karma, positive numbers add and negative subtract 
    // This is then used to determine, bad, neutral or good
    public void AddKarma(int amount)
    {
        currentKarmaValue = currentKarmaValue += amount;
        Debug.Log("Karma has changed by: " + amount + ". You are a ");

        // Since this changes Karma so we need to call it/invoke it
        onKarmaChanged?.Invoke(currentKarmaValue, CurrentStatus);
    }

    // This is a read-only property, when you use this 'CurrentStatus' it will get and return the result of the if value
    // I only want this script to be able to influence currentStatus 
    public KarmaStatus CurrentStatus
    {
        get
        {
            if (currentKarmaValue <= badThreshold)
            {
                Debug.Log("Your Character is Bad");
                return KarmaStatus.Bad;
            }
            if (currentKarmaValue >= goodThreshold)
            {
                Debug.Log("Your Character is Good");
                return KarmaStatus.Good;
            }
            Debug.Log("Your Character is Neutral");
            return KarmaStatus.Neutral;
        }
    }

    #endregion

    #region Event Listener functions
    public void Subscribe(UnityAction<int, KarmaStatus> listener)
    {
        onKarmaChanged.AddListener(listener);
        Debug.Log("Karma Status Changed: Listener Add");
    }

    public void Unsubscribe(UnityAction<int, KarmaStatus> listener)
    {
        onKarmaChanged.RemoveListener(listener);
        Debug.Log("Karma Status Changed: Removed Add");
    }

    #endregion

    #region Unity Start & Update
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update() { }
    #endregion
}
