using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static private Main S;
    // Start is called before the first frame update
    [Header("Inscribed")]
    public GameObject[] prefabEnemies;
    public float enemySpawnPerSecond = 0.5f;
    public float enemyInsetDefault = 1.5f;
    public float gameRestartDelay = 2;

    private BoundsCheck bndCheck;
    void Awake()
    {
        S = this;

        bndCheck = GetComponent<BoundsCheck>();
        Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
    }

    public void SpawnEnemy()
    {
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);
        Enemy thisEnemy = go.GetComponent<Enemy>();
        switch (GameManager.Instance.myDifficulty)
        {
            case GameManager.Difficulty.Hard:
                thisEnemy.MaxHealth = Random.Range(1,4);
                //thisEnemy.ChangeColor(Color.red);
                break;
            case GameManager.Difficulty.Medium:
                thisEnemy.MaxHealth = Random.Range(1, 3);
                //thisEnemy.ChangeColor(Color.yellow);
                break;
            default:
                thisEnemy.MaxHealth = 1;
                //thisEnemy.ChangeColor(Color.cyan);
                break;
        }
        if (thisEnemy.MaxHealth == 3)
        {
            thisEnemy.ChangeColor(Color.red);
            thisEnemy.speed = thisEnemy.speed / 3;
            //print("Health == 3");
        }
        else if (thisEnemy.MaxHealth == 2)
        {
            thisEnemy.ChangeColor(Color.yellow);
            thisEnemy.speed = thisEnemy.speed / 2;
            //print("Health == 2");
        }
        else
        {
            thisEnemy.ChangeColor(Color.cyan);
            //print("Health == 1");
        }

        //thisEnemy.ChangeColor(Color.red);

        float enemyInset = enemyInsetDefault;
        if(go.GetComponent<BoundsCheck>() != null)
        {
            enemyInset = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);

            Vector3 pos = Vector3.zero;
            float xMin = -bndCheck.camWidth + enemyInset;
            float xMax = bndCheck.camWidth - enemyInset;

            pos.x = Random.Range(xMin, xMax);
            pos.y = bndCheck.camHeight + enemyInset;
            go.transform.position = pos;

            Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
        }
    }
/*
    void DelayedRestart()
    {
        Invoke(nameof(Restart), gameRestartDelay);
    }
    void Restart()
    {
        SceneManager.LoadScene("__Scene_0");
    }
    static public void HERO_DIED()
    {
        S.DelayedRestart();
    }
*/
    
}
