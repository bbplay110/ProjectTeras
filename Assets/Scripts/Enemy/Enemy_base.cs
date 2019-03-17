using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_base{
    private float HP;
    private float AttackDamage;
    private float MoveSpeed;
    private float ViewRange;
    public Enemy_base(float tmpHP,float tmpAttackDamage,float tmpMoveSpeed,float tmpViewRange ){
        HP = tmpHP;
        AttackDamage = tmpAttackDamage;
        MoveSpeed = tmpMoveSpeed;
        ViewRange = tmpViewRange;
    }
    public void SetValue() {


    }
    public float View {
        get { return ViewRange; }
        set {
            if (value < 0) {
                Debug.Log("view<0!");
                value = 0;
            }

            ViewRange = value; }
    }

}
