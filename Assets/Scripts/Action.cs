using UnityEngine;

public class Action : MonoBehaviour
{
    private string[] listOfActions = new string[]
    {
        "Kill somebody",
        "Help somebody",
        "Watch crime being committed",
    };

    [SerializeField]
    private string actionName = "Random Action";

    [SerializeField]
    private enum actionType
    {
        Good,
        Bad,
        Neutral,
    };

    [SerializeField]
    private string actionDescription = "Lorem ipsum";

    private void ReturnActInfo(BadAct badAct, GoodAct goodAct)
    {
        if (badAct)
        {
            badAct.ReturnActDetails();
        }
    }
}
