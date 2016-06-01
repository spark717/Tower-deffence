using UnityEngine;
using System.Collections;

public class Detector : MonoBehaviour {
        

    public Object bullet;
    public float rateOfFire;
    public int damage;

    //private Transform enemy = null;
    private Transform spawnPoint;
    private bool spawnPerm = false;
    private string targetName;
    private ArrayList enemys = new ArrayList();
    private Transform enemyInFocus = null;
 



	void Start ()
    {
        targetName = "enemy";
        spawnPoint = transform.Find("spawnPoint");          
    }
	

	void Update ()
    {
        if (enemyInFocus)
        {
            if (enemyInFocus.gameObject.activeInHierarchy)
            {
                transform.LookAt(enemyInFocus);
                if (spawnPerm)
                {
                    StartCoroutine(SpawnNewBullet());
                }
            }
            else
            {
                enemys.Remove(enemyInFocus);
                SetFocus();
            }
            
        }
    }


    void OnTriggerEnter (Collider target)
    {
        if (target.gameObject.name == targetName)
        {
            if (enemys.Count == 0)
            {
                enemyInFocus = target.transform;
                spawnPerm = true;
            }
            enemys.Add(target.transform);

            //Debug.Log(enemys.Count);
            //transform.LookAt(enemyInFocus);           
        }
    }


    void OnTriggerExit (Collider target)
    {
        if (target.gameObject.name == targetName)
        {
            enemys.Remove(target.transform);
            if (target.transform == enemyInFocus)
            {
                SetFocus();
            }
        }
    }


    private IEnumerator SpawnNewBullet ()
    {
        spawnPerm = false;

        GameObject cloneBullet = (GameObject)Instantiate(bullet, spawnPoint.transform.position, spawnPoint.transform.rotation);
        cloneBullet.GetComponent<Pursuit>().StartPursuit(enemyInFocus, damage);
        
        yield return new WaitForSeconds(rateOfFire);

        spawnPerm = true;
    }


    private void SetFocus ()
    {
        if (enemys.Count == 0)
        {
            enemyInFocus = null;
        }
        else
        {
            enemyInFocus = (Transform)enemys[0];
            //spawnPerm = false;
        }
    }
}
