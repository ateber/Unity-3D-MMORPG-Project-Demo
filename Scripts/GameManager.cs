using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoSingleton<GameManager>
{
    public bool AttackMode  { get; set; }
    public bool isSkillActive { get; set; }

    public int atackSpeed = 1; 
    public HealthSystem PlayerHealthSystem { get; private set; }
    volatile Transform m_target;
    public  Transform Target { get =>m_target; set => m_target = value;  } 
     
    public Joystick Joystick { get; private set; } 
    public Button Button { get; private set; }

    public Toggle AttackToggle { get; private set; } 

    public Transform TargetBar { get; private set; }

    public Transform Player { get; private set; }

    public bool isPlayerAtack(){
        if (Target != null &&  AttackMode == true  )
            return true;
        else
            return false;
    }

    public bool isTargetActive()
    {
        return Target != null;
    } 
    void Start()
    {   
        PlayerHealthSystem = new HealthSystem(100); 

        Joystick =  GameObject.FindObjectOfType<Joystick>() ; 
        TargetBar = GameObject.FindGameObjectWithTag("TargetBar").transform; 
        Player = GameObject.FindGameObjectWithTag("Player").transform; 
        AttackToggle = GameObject.FindObjectOfType<Toggle>();
         
        AttackToggle.onValueChanged.AddListener(toggleChanged);

        //Button = GameObject.FindObjectOfType<Button>();
        //Button.onClick.AddListener(attack);
    }

    public void toggleChanged(bool isChecked)
    {
        if (isChecked)
        {
            AttackMode = false;
            AttackToggle.transform.GetChild(0).GetComponent<Image>().color = new Color32(0, 255, 0, 150);
        }
        else
        {
            AttackMode = true;
            AttackToggle.transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 0, 0, 150);
            
        } 
    }


     

     

    // Update is called once per frame
    void Update()
    {
        
    } 

    
}
