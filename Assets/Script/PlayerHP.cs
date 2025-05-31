using System.Text.RegularExpressions;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public PlayerMove playerMove;
    // 캔버스
    [SerializeField]
    private ManageHeart manageHeart;

    // 플레이어의 최대 체력
    [SerializeField]
    private float maxHP = 3.0f;

    // maxHP 의 프로퍼티 (only get)
    public float MaxHP => maxHP;

    // 플레이어의 현재 체력
    private float currentHP;

    // currentHP 의 프로퍼티 (set, get)
    public float CurrentHP
    {
        set => currentHP = Mathf.Clamp(value, 0, maxHP);
        get => currentHP;
    }
    void Awake()
    {
        currentHP = maxHP;
    }
    // damage 만큼 플레이어의 체력이 하락한다.
    public void damage(float damage)

    {
        manageHeart.ApplyHeart(damage);

        currentHP -= damage;

        if (currentHP <= 0)

        {
            OnDie();
        }

    }
    public void OnDie()
    {
        playerMove.DieEffect();

        // Result UI

        //Retry Button UI
    }
}
