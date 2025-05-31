using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField]
    private int maxHP;
    private int currentHP;
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
