using UnityEngine;
using UnityEngine.SceneManagement;
public class PaUILevelSelector : MonoBehaviour
{
    public int levelNumber = 1;

    void OnMouseDown()
    {
       
        SceneManager.LoadScene("ParkLevel" + levelNumber.ToString("00"));
    }
}
