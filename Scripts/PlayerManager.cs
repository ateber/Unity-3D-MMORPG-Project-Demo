using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using TMPro;

public class PlayerManager : MonoBehaviour
{
    float speed = 60f;
    public Joystick joystick;  
    public static bool isSkillActive { get; set; }  
    
    public Transform m_muzzle_L;
    public Transform m_muzzle_R;
    public GameObject m_shotPrefab;
    public GameObject m_Spaceship;

    int angle=90;
    float player_rotation_z = 0;
    float player_rotation_x = 0;
    public Vector3 offset;

    public GameObject NameText{set;get;}
    float textRotate = 70;
    public HealthSystem HealthSystem { get; private set; }
    // Start is called before the
    float rotationJoy=0;
    void Start()
    {
         
        HealthSystem = new HealthSystem(100);
        CreatePlayerTextPosObject(); 
        GameEvents.Instance.onSelectEnemy += onSelectEnemy_FindRotation;
        GameEvents.Instance.onSelectMonster += onSelectMonster_FindRotation; 
    } 

    private void FixedUpdate()
    {
        float player_x = transform.position.x;
        float player_y = transform.position.y;
        float player_z = transform.position.z;

        //float player_rotation_x = transform.rotation.eulerAngles.x;
        float player_rotation_y = m_Spaceship.transform.rotation.eulerAngles.y;
        //int player_rotation_z =(int)transform.rotation.eulerAngles.z;  

        bool is_rotation = false;

        if (Mathf.Abs(joystick.Direction.x) > 0.1 || Mathf.Abs(joystick.Direction.y) > 0.1)
        {

            float increase_x = speed * joystick.Direction.x / 100;
            float increase_y = speed * joystick.Direction.y / 100;

            player_x = player_x + increase_x;
            player_z = player_z + increase_y;

            player_rotation_y = (int)FindRotation(joystick.Direction);

            int dynamic_angle = (int)(Mathf.Atan2(joystick.Direction.y, joystick.Direction.x) * Mathf.Rad2Deg);

            if (dynamic_angle - angle > 0 && player_rotation_z < 15)
            {  //left

                if (player_rotation_z + (dynamic_angle - angle) < 15)
                    player_rotation_z += dynamic_angle - angle;
                is_rotation = true;
            }

            else if (dynamic_angle - angle < 0 && player_rotation_z > -15) //right
            {
                if (player_rotation_z + (dynamic_angle - angle) > -15)
                    player_rotation_z += dynamic_angle - angle;
                is_rotation = true;
            }

            if (Mathf.Abs(dynamic_angle - angle) > 0 && player_rotation_x < 5)
            {
                //if (player_rotation_x + Mathf.Abs(dynamic_angle - angle) < 5)  
                player_rotation_x += 0.2f;
                is_rotation = true;
            }
            angle = dynamic_angle;


        }

        if (!is_rotation)
        {
            if (player_rotation_z > 0)
                player_rotation_z -= 0.5f;
            else if (player_rotation_z < 0)
                player_rotation_z += 0.5f;

            if (player_rotation_x > 0)
                player_rotation_x -= 0.1f;
        }

        if (GameManager.Instance.AttackMode == true && GameManager.Instance.Target != null)
        {
            Quaternion lookRotation = FindRotationByTarget(transform.position, GameManager.Instance.Target.position); 
            m_Spaceship.transform.rotation = Quaternion.Slerp(m_Spaceship.transform.rotation, lookRotation, 20 * Time.deltaTime);
            AttackToTarget(1, 10); 
        }
        else
        {
            m_Spaceship.transform.rotation = Quaternion.Euler(player_rotation_x, player_rotation_y, player_rotation_z);
        }


        transform.position = new Vector3(player_x, player_y, player_z);


        //    float player_x = transform.position.x;
        //    float player_y = transform.position.y;
        //    float player_z = transform.position.z;

        //    float player_rotation_y =  transform.rotation.eulerAngles.y; 

        //    bool is_rotation = false; 

        //    if (Mathf.Abs(joystick.Direction.x) > 0.01 || Mathf.Abs( joystick.Direction.y) > 0.01)
        //    { 
        //        rotationJoy= FindRotation(joystick.Direction) ; 
        //        if (GameManager.Instance.isPlayerAtack() )
        //        {   
        //            transform.Translate(Vector3.right * joystick.Direction.x * speed * Time.deltaTime);
        //            transform.Translate(Vector3.forward * joystick.Direction.y * speed * Time.deltaTime); 


        //        }  

        //        else{    
        //            if(rotationJoy <= 165 || rotationJoy>=195) 
        //                transform.Translate(Vector3.forward*joystick.Direction.magnitude*speed*Time.deltaTime);
        //            else if ((rotationJoy<195 || rotationJoy>165) && joystick.Direction.magnitude>0.9)     
        //                transform.Translate(Vector3.back*joystick.Direction.magnitude*speed*Time.deltaTime);
        //            if(rotationJoy>15 && rotationJoy<165 ) { 
        //                player_rotation_y =player_rotation_y+ joystick.Direction.magnitude *rotationJoy*Time.deltaTime; 
        //            }
        //            else if (rotationJoy>195 && rotationJoy<345) { 
        //                player_rotation_y =player_rotation_y-joystick.Direction.magnitude *(360-rotationJoy)*Time.deltaTime;  
        //            }
        //        }    
        //    } 
        //    if (GameManager.Instance.isPlayerAtack() )
        //    {   
        //        Quaternion lookRotation = FindRotationByTarget(transform.position,GameManager.Instance.Target.position); 
        //        transform.rotation=Quaternion.Slerp(transform.rotation,lookRotation,10*Time.deltaTime);
        //        AttackToTarget(0.5f,10);   
        //    }
        //    else
        //        transform.rotation = Quaternion.Euler(player_rotation_x, player_rotation_y, player_rotation_z);

    }

    Quaternion FindRotationByTarget(Vector3 current,Vector3 target)
    { 
        Vector3 direction = (target - current).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        return lookRotation;
    }

    float FindRotation(Vector2 direction)
    {
        Vector3 newDirection = new Vector3(direction.x, 0, direction.y).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(newDirection.x, 0, newDirection.z));
        return lookRotation.eulerAngles.y; 
    }

    void CreatePlayerTextPosObject( )
    {
        GameObject objectTextPos=new GameObject("TextPos"); 
        objectTextPos.transform.SetParent(transform);

        BoxCollider renderers = this.transform.gameObject.GetComponentInChildren<BoxCollider>(); 
        float objectMaxSize = Mathf.Max(renderers.bounds.size.z, renderers.bounds.size.x);
        objectMaxSize = objectMaxSize / 2 + 2f + renderers.bounds.size.y * 0.1f;  
        objectTextPos.transform.localPosition = new Vector3(0, objectMaxSize, 0);
        
    } 
    
    void DestroyPlayer()
    { 
        Destroy(NameText, 0.2f);
        Destroy(gameObject, 0.2f);
    }

    public void TakeDamage(int damageAmount)
    { 
        HealthSystem.TakeDamage(damageAmount);
        GameEvents.Instance.PlayerHealthChanged();
        if (HealthSystem.isAlive() == false)
        {
            DestroyPlayer();
            GameEvents.Instance.PlayerDestroy();
        }
    }

    public void Heal(int healAmount)
    {
        HealthSystem.Heal(healAmount);
        GameEvents.Instance.PlayerHealthChanged(); 
    }

    void onSelectEnemy_FindRotation()
    {
        FindRotationByTarget(transform.position, GameManager.Instance.Target.position);
    }
    void onSelectMonster_FindRotation()
    {
        FindRotationByTarget(transform.position, GameManager.Instance.Target.position);
    }
    
    public 
    
    void AttackEffect()
    {
        GameObject go1 = GameObject.Instantiate(m_shotPrefab, m_muzzle_L.position, m_muzzle_L.rotation) as GameObject;
        GameObject go2 = GameObject.Instantiate(m_shotPrefab, m_muzzle_R.position, m_muzzle_R.rotation) as GameObject;
    }            

    void AttackToTarget(float time, int damage)
    {
        if (GameManager.Instance.Target != null && !isSkillActive)
        {
            if (GameManager.Instance.Target.tag == "Enemy")
            {
                StartCoroutine(SkillCoolDown(time));
                AttackEffect();
                GameManager.Instance.Target.GetComponent<EnemyScript>().TakeDamage(damage);
                
            }

            else if (GameManager.Instance.Target.tag == "Monster")
            {
                StartCoroutine(SkillCoolDown(time));
                AttackEffect();
                GameManager.Instance.Target.GetComponent<MonsterScript>().TakeDamage(damage);
                 
            } 
        } 
    }

    IEnumerator SkillCoolDown(float time)
    {
        isSkillActive = true;  
        yield return new WaitForSeconds(time);
        isSkillActive = false;
    }

    
}
 
