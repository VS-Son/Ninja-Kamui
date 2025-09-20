using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState :  IState
{
    float timer;
    private float randomTime;
    public void OnEnter(Enemy enemy)
    {
        enemy.StopMoving();
        timer = 0;
        randomTime = Random.Range(2f, 4f);
    }

    public void OnExit(Enemy enemy)
    {
       
    }

    public void OnExecute(Enemy enemy)
    {
         timer += Time.deltaTime;
                if (timer > randomTime)
                {
                    enemy.ChangeState(new PatrolState());
                }
    }
}

