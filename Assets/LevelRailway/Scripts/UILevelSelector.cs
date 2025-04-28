using UnityEngine;
using UnityEngine.SceneManagement;

public class UILevelSelector : MonoBehaviour
{
    public int levelNumber = 1;

    void OnMouseDown()
    {
        int rnd = Random.Range(1, 3);
        SceneManager.LoadScene("RailwayLevel" + levelNumber.ToString() + "_" + rnd.ToString());
    }
}
