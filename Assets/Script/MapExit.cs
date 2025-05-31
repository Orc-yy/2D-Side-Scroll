using UnityEngine;

public class MapExit : MonoBehaviour
{
    public PlayerHP playerHP;
    public PlayerMove playerMove;
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHP.damage(1.0f);

            // Assuming the target position is the player's current position
            Vector2 targetPos = collision.transform.position;
            playerMove.OnDamaged(targetPos);

            if (playerHP.CurrentHP <= 0)
            {
                playerHP.OnDie();
            }

            // Player Reposition
            if(playerHP.CurrentHP >= 1)
            {
                PlayerReposition();
            }
            
        }
    }
    public void PlayerReposition()
    {
        playerMove.transform.position = new Vector3(-57, 32, 0);
        playerMove.VelocityZero();
    }
}
