using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public PlayerMove playerMove;
   public void StartGame()
    {
        SceneManager.LoadScene("Game");
        
    }
    public void ReteyGame()
    {
        SceneManager.LoadScene("Game");
        Invoke("Move", 1);
    }
    private void Move()
    {
        playerMove.transform.position = new Vector3(-57, 32, 0);
        playerMove.VelocityZero();
    }
    
}
