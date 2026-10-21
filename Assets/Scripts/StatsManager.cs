using SAE.GAD170.See.P1.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    [SerializeField]
    private Slider HealthBar;

    [SerializeField]
    public Slider GoodKarmaBar;

    [SerializeField]
    public Slider BadKarmaBar;

    [SerializeField]
    private Karma playerKarma;

    [SerializeField]
    private Player playerLevel;

    public TextMeshProUGUI playerLevelUiText;
    public TextMeshProUGUI GoodKarmaUiText;
    public TextMeshProUGUI BadKarmaUiText;

    private void SetHealth(int health)
    {
        HealthBar.value = health;
    }

    private void SetGoodKarma(int amount)
    {
        GoodKarmaBar.value = playerKarma.publicCurrentGoodKarmaValue;
    }

    private void SetBadKarma(int amount)
    {
        GoodKarmaBar.value = playerKarma.publicCurrentBadKarmaValue;
    }

    private void DisplayKarmaStats()
    {
        Debug.Log(playerKarma.CurrentStatus);
    }

    #region Unity Specific functions
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisplayKarmaStats();
        SetHealth(100);
        SetBadKarma(0);
        SetGoodKarma(0);
        Debug.Log(playerLevel.playerLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerKarma.onKarmaChanged != null) { }
    }
    #endregion
}
