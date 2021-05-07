using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoSingleton<GameEvents>
{
       
    public event Action onSelectEnemy; 
    public event Action onEnemyDestroy;
    public event Action onEnemyHealthChanged;

    public event Action onSelectMonster;
    public event Action onMonsterDestroy;
    public event Action onMonsterHealthChanged;

    public event Action onSelectFriend;
    public event Action onFriendDestroy;
    public event Action onFriendHealthChanged;

    public event Action onSelectPlayer;
    public event Action onPlayerDestroy;
    public event Action onPlayerHealthChanged;

    public event Action onDestroyObject;
    public event Action onExitTarget;

    public event Action onAtackToTarget;

    public void AtackToTarget()
    {
        if (onAtackToTarget != null)
        {
            onAtackToTarget();
        }
    }


    public void SelectEnemy() 
    {
        if (onSelectEnemy != null)
        { 
            onSelectEnemy();
        }
    }

    public void EnemyDestroy()
    {

        if (onEnemyDestroy != null)
        {
            onEnemyDestroy();
        }
    }
    public void EnemyHealthChanged()
    {

        if (onEnemyHealthChanged != null)
        {
            onEnemyHealthChanged();
        }
    }

    public void SelectFriend()
    {
        if (onSelectFriend != null)
        {
            onSelectFriend();
        }
    }

    public void FriendDestroy()
    {

        if (onFriendDestroy != null)
        {
            onFriendDestroy();
        }
    }
    public void FriendHealthChanged()
    {

        if (onFriendHealthChanged != null)
        {
            onFriendHealthChanged();
        }
    }

    public void SelectPlayer()
    {
        if (onSelectFriend != null)
        {
            onSelectFriend();
        }
    }

    public void PlayerDestroy()
    {

        if (onPlayerDestroy != null)
        {
            onPlayerDestroy();
        }
    }
    public void PlayerHealthChanged()
    {

        if (onPlayerHealthChanged != null)
        {
            onPlayerHealthChanged();
        }
    }

    public void SelectMonster()
    {
        if (onSelectMonster != null)
        {
            onSelectMonster();
        }
    }


    public void ExitTarget()
    {
        
        if (onExitTarget != null)
        { 
            onExitTarget();
        }
    }

    
    

    public void MonsterDestroy()
    {

        if (onMonsterDestroy != null)
        {
            onMonsterDestroy();
        }
    }


    public void DestroyObject()
    {

        if (onDestroyObject != null)
        { 
            onDestroyObject();
        }
    }

    
     
    public void MonsterHealthChanged()
    {

        if (onMonsterHealthChanged != null)
        {
            onMonsterHealthChanged();
        }
    }
    // Update is called once per frame
    void Update()
    { 
    }
 
}
