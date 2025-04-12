using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizClose : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("Map");
    }
}
