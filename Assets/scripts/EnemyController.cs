using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public Canvas healthCanvas;
    public Slider healthBar;
    public int speed;
    public int health;
    public int point;    

    private GameObject manager;
    private Transform[] checkPoints;
    private Vector3 moveDir;
    private int currentCp = 0;

	
	void Start ()
    {
        manager = GameObject.Find("Manager");

        healthCanvas.transform.rotation = Camera.main.transform.rotation;
        healthBar.maxValue = health;
        healthBar.value = health;
    }
	
	
	void Update ()
    {
        CheckPointSwitcher();
        transform.Translate(moveDir*speed*Time.deltaTime, Space.World);
    }


    public void DamageControl (int damage)
    {
        health -= damage;
        DisplayHealth();

        if (health <= 0)
        {
            manager.GetComponent<Manager>().ScoreControl(point);
            gameObject.SetActive(false);
        }
    }


    void DisplayHealth ()
    {
        healthBar.value = health;         
    }


    public void StartParameters (Transform[] checkPoints)
    {
        this.checkPoints = checkPoints;
        CheckPointSwitcher();
    }


    void CheckPointSwitcher ()
    {
        Vector3 myVector = checkPoints[currentCp].position - transform.position;
        float cpSqrDistance = myVector.sqrMagnitude;


        if (cpSqrDistance < 1)
        {
            if (currentCp == checkPoints.Length - 1)
            {
                gameObject.SetActive(false);
                manager.GetComponent<Manager>().BaseHPControl();
            }
            else
            {
                ++currentCp;
                Vector3 myVector2 = checkPoints[currentCp].position - checkPoints[currentCp - 1].position;
                moveDir = myVector2 / myVector2.magnitude;
            }

        }
    }


}
