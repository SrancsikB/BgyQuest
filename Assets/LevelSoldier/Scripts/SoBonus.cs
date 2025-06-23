using UnityEngine;

public class SoBonus : MonoBehaviour
{
    public enum BonusType
    {
        Blast=0,Spread=1

    }

    public BonusType bonusType;

    void Start()
    {
        bonusType = (BonusType)Random.Range(0, 2);
    }

    
}
