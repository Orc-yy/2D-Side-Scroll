using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public GameObject menuBar;
    
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuBar.activeSelf)
            {
                menuBar.SetActive(false);
            }
            else
                menuBar.SetActive(true);
        }
    }
    public void ExitTitle()
    {
        SceneManager.LoadScene("Title");
    }
    
}
