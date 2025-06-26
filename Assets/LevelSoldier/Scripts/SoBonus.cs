using UnityEngine;

public class SoBonus : MonoBehaviour
{
    public enum BonusType
    {
        Blast=0,Spread=1,Rate=2, Ring=3

    }

    public BonusType bonusType;

    void Start()
    {
        bonusType = (BonusType)Random.Range(1, 4);
    }

    
}
