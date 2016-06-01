using UnityEngine;
using System.Collections;

public class Pursuit : MonoBehaviour {

    public float speed;

    private Transform enemy = null;
    private int damage;


    void Start () {
        
	}
	

	void Update () {
        if (enemy.gameObject.activeInHierarchy)
        {
            Vector3 dist = enemy.position - transform.position;
            Vector3 dir = dist / dist.magnitude;
            transform.Translate(dir * speed * Time.deltaTime, Space.World);
        }    
        else
        {
            Destroy(gameObject);
        }         
    }


    void OnTriggerEnter (Collider target)
    {
        if (target.transform == enemy)
        {
            target.GetComponent<EnemyController>().DamageControl(damage);
            Destroy(gameObject);
        }
    }
   


    public void StartPursuit (Transform enemy, int damage)
    {
        this.enemy = enemy;
        this.damage = damage;
    }


}
