using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Manager : MonoBehaviour {


    public Object enemy;
    public Text scoreText;
    public Text baseHpText;
    public float spawnInterval;
    public int baseHp;

    private bool spawn = true;
    private Vector3 spawnPos;
    private Quaternion spawnRot;
    private int score = 0;
    private int cpCount = 0;
    private Transform[] checkPoints;



    void Start ()
    {
        CollectCheckPoints();
    }
	

	void Update ()
    {
	    if (spawn)
        {
            StartCoroutine(EnemySpawn());
        }
	}


    private IEnumerator EnemySpawn ()
    {
        spawn = false;

        GameObject enemyClone = (GameObject)Instantiate(enemy, spawnPos, spawnRot);
        enemyClone.name = "enemy";
        enemyClone.GetComponent<EnemyController>().StartParameters(checkPoints);

        yield return new WaitForSeconds(spawnInterval);

        spawn = true;
    }


    public void ScoreControl (int point)
    {
        score += point;
        scoreText.text = "Coins: " + score;
    }


    public void BaseHPControl ()
    {
        --baseHp;
        baseHpText.text = "Base HP: " + baseHp.ToString();
    }


    private void CollectCheckPoints ()
    {
        string name = "checkpoint (0)";
        while (GameObject.Find(name)) 
        {
            ++cpCount;
            name = "checkpoint (" + cpCount + ")";            
        }

        checkPoints = new Transform[cpCount];
        for (int i = 0; i <= cpCount-1; i++)
        {
            name = "checkpoint (" + i + ")";
            checkPoints[i] = GameObject.Find(name).transform;
        }

        spawnPos = checkPoints[0].position;
        spawnRot = checkPoints[0].rotation;
    }



}
