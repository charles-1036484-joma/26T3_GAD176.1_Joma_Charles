using System;
using UnityEngine;

// This should live on the player and track his morality
public class Karma : MonoBehaviour
{
    // Enum = user defined data type. consist of named constant values || const Bad etc.
    private enum KarmaStatus
    {
        Bad,
        Neutral,
        Good,
    }

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

    #region UNITY SPECIFIC FUNCTIONS
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update() { }
    #endregion
}
