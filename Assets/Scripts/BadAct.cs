using UnityEngine;

public class BadAct : MonoBehaviour
{
    private string actName = "Random Action";
    private string actDescription = "Lorem ipsum";

    public enum ActType
    {
        Bad,
    }

    public string GetActName
    {
        get { return actName; }
        set { }
    }

    public string GetActDescription
    {
        get { return actDescription; }
        set { }
    }

    public ActType GetActType
    {
        get { return ActType.Bad; }
        set { }
    }

    public BadAct ReturnActDetails()
    {
        BadAct newBadAct = new BadAct { actName = actName, actDescription = actDescription };

        return newBadAct;
    }
}
