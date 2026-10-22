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
    private Player player;

    [SerializeField]
    private int playerLevel;

    [SerializeField]
    private Karma playerKarma;

    [SerializeField]
    private float playerSpeed;

    [SerializeField]
    private Slider GoodSlider;

    [SerializeField]
    private Slider BadSlider;

    public TextMeshProUGUI playerLevelUiText;
    public TextMeshProUGUI GoodKarmaUiText;
    public TextMeshProUGUI BadKarmaUiText;

    private void DisplayKarmaStats()
    {
        Debug.Log(playerKarma.CurrentStatus);
    }

    #region Karma Functions
    public void UpdateGoodAct(Collision collision)
    {
        // Add good karma to player
        playerKarma.AddGoodKarma(10);
        // Adjust the good karma slider, add the value to the slider
        GoodSlider.value = playerKarma.publicCurrentGoodKarmaValue;
        // this just uses the get as the callback function, but i want to make the function it invokes more useful rather than just a get.
        playerKarma.onKarmaChanged.Invoke(
            playerKarma.publicCurrentGoodKarmaValue,
            playerKarma.CurrentStatus
        );
        Destroy(collision.gameObject);
    }

    public void UpdateBadAct(Collision collision)
    {
        playerKarma.AddBadKarma(10);
        Debug.Log(BadSlider.value);
        BadSlider.value = playerKarma.publicCurrentBadKarmaValue;
        playerKarma.onKarmaChanged.Invoke(
            playerKarma.publicCurrentGoodKarmaValue,
            playerKarma.CurrentStatus
        );
        Destroy(collision.gameObject);
    }

    public void UpdateBigBadAct(Collision collision)
    {
        playerKarma.AddBadKarma(30);
        Debug.Log(BadSlider.value);
        BadSlider.value = playerKarma.publicCurrentBadKarmaValue;
        playerKarma.onKarmaChanged.Invoke(
            playerKarma.publicCurrentGoodKarmaValue,
            playerKarma.CurrentStatus
        );
        Destroy(collision.gameObject);
    }
    #endregion

    #region Unity Specific functions
    void Awake()
    {
        playerLevel = player.playerLevel.PublicCurrentPlayerLevel;
        playerSpeed = player.playerSpeed.walkSpeed;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // DisplayKarmaStats();
        playerLevelUiText.text = $"Level: {playerLevel.ToString()}";
    }

    // Update is called once per frame
    void Update() { }
    #endregion
}
