using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class MonsterScript: MonoBehaviour
{
    public GameObject NameText{set;get;}
    float textRotate = 70;
    public HealthSystem HealthSystem { get; private set; }

    bool m_isTakeDamage = false;
    float m_atackModeCoolDown = 10f;


    // Start is called before the first frame update
    void Start()
    {

        HealthSystem = new HealthSystem(200);
        agent = GetComponent<NavMeshAgent>();
        CreateMonsterTextPosObject( ); 
    }
    NavMeshAgent agent;
    void Update()
    {
        if (m_isTakeDamage)
        {
            float distance = Vector3.Distance(transform.position, GameManager.Instance.Player.position);
            if (distance <= 10)
            {
                if (!m_isSkillActive)
                {
                    StartCoroutine(SkillCoolDown(1));
                    GameManager.Instance.Player.GetComponent<PlayerManager>().TakeDamage(10);
                } 
            }
            else
            { 
                transform.position = Vector3.MoveTowards(transform.position, GameManager.Instance.Player.position, 5f * Time.deltaTime); 
            }
            Quaternion lookRotation = FindRotationByTarget(transform.position, GameManager.Instance.Player.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 1* Time.deltaTime);
        }
         
    }

    private void OnDestroy()
    { 
    }
    Quaternion FindRotationByTarget(Vector3 current, Vector3 target)
    {
        Vector3 direction = (target - current).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        return lookRotation;
    }

    void CreateMonsterTextPosObject( )
    {
        GameObject objectTextPos=new GameObject("TextPos"); 
        objectTextPos.transform.SetParent(transform);

        BoxCollider renderers = this.transform.gameObject.GetComponentInChildren<BoxCollider>(); 
        float objectMaxSize = Mathf.Max(renderers.bounds.size.z, renderers.bounds.size.x);
        objectMaxSize = objectMaxSize / 2 + 2f + renderers.bounds.size.y * 0.1f;    
        objectTextPos.transform.localPosition = new Vector3(0, objectMaxSize, 0);  
    }

    void DestroyMonster() { 
        Destroy(NameText, 0.2f);
        Destroy(gameObject, 0.2f);
    }
    IEnumerator m_AtackModeCoolDownEnumerator;
    IEnumerator AtackModeCoolDownEnumerator(float time)
    {
        m_isTakeDamage = true;
        yield return new WaitForSeconds(time);
        m_isTakeDamage = false;
    }

    void WaitAtackModeCoolDown(float time)
    {
        if (m_AtackModeCoolDownEnumerator != null)
            StopCoroutine(m_AtackModeCoolDownEnumerator); 
        else
            StartCoroutine(AtackModeCoolDownEnumerator(time));
    }

    bool m_isSkillActive = false;
    IEnumerator SkillCoolDown(float time)
    {
        m_isSkillActive = true;
        yield return new WaitForSeconds(time);
        m_isSkillActive = false;
    }

    public void TakeDamage (int damageAmount) {
        WaitAtackModeCoolDown(m_atackModeCoolDown);
        HealthSystem.TakeDamage(damageAmount);
        GameEvents.Instance.MonsterHealthChanged();
        if (HealthSystem.isAlive() == false)
        {
            DestroyMonster(); 
            GameEvents.Instance.MonsterDestroy();
        }
    }

    public void Heal(int healAmount)
    {
        HealthSystem.Heal(healAmount);
        GameEvents.Instance.MonsterHealthChanged();
        
    }


}
