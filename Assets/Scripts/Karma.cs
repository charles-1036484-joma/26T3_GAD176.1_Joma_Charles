using System;
using UnityEngine;
using UnityEngine.Events;

// This should live on the player and track his morality
public class Karma : MonoBehaviour
{
    // Enum = user defined data type. consist of named constant values || const Bad etc.
    public enum KarmaStatus // Had to make this public so subscribe can access
    {
        Bad,
        Neutral,
        Good,
    }

    [SerializeField] // This event will pass two pieces of data to whoever is listening, an int and a karma.karmastatus
    private UnityEvent<int, KarmaStatus> onKarmaChanged = new UnityEvent<int, KarmaStatus>();

    #region Karma Range. Min. Max
    [Header("Karma Range")] // group these fields under this heading
    [SerializeField] 
    private int minValue = 0;

    [SerializeField]
    private int maxValue = 100;

    [SerializeField]
    private int currentValue = 50;
    #endregion

    #region Karma Status
    [Header("Karma Status Thresholds")]
    [SerializeField] // If player is at or below this number they are bad
    private int badThreshold = 30;

    [SerializeField] // Same here ^^^
    private int goodThreshold = 70;

    #endregion

    #region Subscription Events
    public void Subscribe(UnityAction<int, KarmaStatus> listener)
    {
        onKarmaChanged.AddListener(listener);
    }

    public void Unsubcribe(UnityAction<int, KarmaStatus> listener)
    {
        onKarmaChanged.RemoveListener(listener);
    }
    
    #endregion
    #region UNITY SPECIFIC FUNCTIONS
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update() { }
    #endregion
}
