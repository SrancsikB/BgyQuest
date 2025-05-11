using UnityEngine;
using UnityEngine.SceneManagement;

public class UILevelSelector : MonoBehaviour
{
    public int levelNumber = 1;

    void OnMouseDown()
    {
        int rnd = 1;
        if (levelNumber<=2)  //Random map if more available
        {
            rnd = Random.Range(1, 3);
        }
        
        SceneManager.LoadScene("RailwayLevel" + levelNumber.ToString() + "_" + rnd.ToString());
    }
}
