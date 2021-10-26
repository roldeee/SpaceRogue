using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Health")]
    [SerializeField] public int maxHP = 100;
    [SerializeField] public int curHP;

    public HPbar bar;

    private void Start()
    {
        curHP = maxHP;
        bar.setMaxHP(maxHP);
        bar.SetHP(maxHP);
        Debug.Log(bar.slider.value);
    }

    private void Update()
    {
        // press space to get damage by 10
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetDamage(10);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            GetDamage(10);
        }
    }

    void GetDamage(int damage)
    {
        curHP -= damage;
        bar.SetHP(curHP);
        Debug.Log(bar.slider.value);
    }
}