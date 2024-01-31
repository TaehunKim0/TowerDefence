using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int Coin = 100;
    private int _heart = 10;
    public int MaxHeart = 10;

    void Start()
    {
        _heart = MaxHeart;
    }

    void Update()
    {
        
    }

    public void Damaged(int damage)
    {
        _heart -= damage;
        if(_heart <= 0)
        {
            GameManager.Inst.GameDefeat();
        }
    }
}
