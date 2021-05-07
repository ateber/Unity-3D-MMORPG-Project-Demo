using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FriendScript : MonoBehaviour
{
    public GameObject NameText{set;get;}
    float textRotate = 70;
    public HealthSystem HealthSystem { get; private set; }
   
    void Start()
    {
        HealthSystem = new HealthSystem(100);
        CreateFriendTextPosObject( ); 
    }

    void CreateFriendTextPosObject( )
    {
        GameObject objectTextPos=new GameObject("TextPos"); 
        objectTextPos.transform.SetParent(transform);

        BoxCollider renderers = this.transform.gameObject.GetComponentInChildren<BoxCollider>(); 
        float objectMaxSize = Mathf.Max(renderers.bounds.size.z, renderers.bounds.size.x);
        objectMaxSize = objectMaxSize / 2 + 2f + renderers.bounds.size.y * 0.1f;  
        objectTextPos.transform.localPosition = new Vector3(0, objectMaxSize, 0); 
    }

    void DestroyFriend()
    { 
        Destroy(NameText);
        Destroy(gameObject);
    }

    public void TakeDamage(int damageAmount)
    { 
        HealthSystem.TakeDamage(damageAmount);
        GameEvents.Instance.FriendHealthChanged();
        if (HealthSystem.isAlive() == false)
        {
            DestroyFriend();
            GameEvents.Instance.FriendDestroy();
        }
    }

    public void Heal(int healAmount)
    {
        HealthSystem.Heal(healAmount);
        GameEvents.Instance.FriendHealthChanged();
    }
     
    void Update()
    { 
    }

    private void OnDestroy()
    { 
    }
}