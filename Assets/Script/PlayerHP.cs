using System.Text.RegularExpressions;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public PlayerMove playerMove;
    // ĵ����
    [SerializeField]
    private ManageHeart manageHeart;

    // �÷��̾��� �ִ� ü��
    [SerializeField]
    private float maxHP = 3.0f;

    // maxHP �� ������Ƽ (only get)
    public float MaxHP => maxHP;

    // �÷��̾��� ���� ü��
    private float currentHP;

    // currentHP �� ������Ƽ (set, get)
    public float CurrentHP
    {
        set => currentHP = Mathf.Clamp(value, 0, maxHP);
        get => currentHP;
    }
    void Awake()
    {
        currentHP = maxHP;
    }
    // damage ��ŭ �÷��̾��� ü���� �϶��Ѵ�.
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
