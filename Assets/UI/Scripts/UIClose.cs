using UnityEngine;
using UnityEngine.SceneManagement;
public class UIClose : MonoBehaviour
{
    public void Close()
    {
        SceneManager.LoadScene("Map");
    }
}
