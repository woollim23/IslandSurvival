using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;
    public Condition hunger;
    public Condition stamina;
    public Condition thirst;
    public Condition temperature;
    void Start()
    {
        CharacterManager.Instance.Player.condition.uiCondition = this;
    }


    void Update()
    {

    }
}
