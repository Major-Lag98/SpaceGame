using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    static public Hero S { get; private set; }
    [Header("inscribed")]
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;

    [Header("dynamic")]
    private float _shieldLevel = 4;

    [Header("Gun Heat")]
    public float maxHeat = 10f;
    public float heatIncreaseRate = 10f;
    public float heatDecreseRate = 10f;
    public float cooldownRate = 5f;
    public GameObject GunHeatSliderObject;
    private float currentHeat = 0f;
    private bool isOverheated = false;
    private Slider GunHeatSlider;
    private Image FillImage;

    private GameObject lastTriggerGo;
    // Start is called before the first frame update
    void Awake()
    {
        if(S==null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("Hero.Awake() - Attempted to create a hero that is not null");
        }
    }
    private void Start()
    {
        GunHeatSlider = GunHeatSliderObject.GetComponent<Slider>();
        GunHeatSlider.maxValue = maxHeat;

        FillImage = GunHeatSlider.fillRect.gameObject.GetComponent<Image>();
        FillImage.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += hAxis * speed * Time.deltaTime;
        pos.y += vAxis * speed * Time.deltaTime;

        transform.position = pos;
        transform.rotation = Quaternion.Euler(vAxis * pitchMult, hAxis * rollMult, 0);
        if(!isOverheated && Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }

        if (currentHeat > maxHeat)
        {
            isOverheated = true;
            FillImage.color = Color.red;
        }

        currentHeat -= heatDecreseRate * Time.deltaTime;

        if (currentHeat <= 0)
        {
            currentHeat = 0;
            isOverheated = false;
            FillImage.color = Color.yellow;
        }

        GunHeatSlider.value = currentHeat;

    }
    public float shieldLevel
    {
        get { return (_shieldLevel); }
        private set
        {
            _shieldLevel = Mathf.Min(value, 4);
            if(value < 0)
            {
                Destroy(this.gameObject);
                GameManager.Instance.Lose();
                //Main.HERO_DIED();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        if (go == lastTriggerGo) return;
        lastTriggerGo = go;
        Enemy enemy = go.GetComponent<Enemy>();
        if(enemy!=null)
        {
            shieldLevel--;
            enemy.DestroyEnemy();
        }
        else
        {
            //Debug.Log("Shield trigger hit by non enemy: " + go.name);
        }
    }
    void Fire()
    {
        // Check if the gun is not overheated before firing
        if (!isOverheated)
        {
            GameObject projGO = Instantiate<GameObject>(projectilePrefab);
            projGO.transform.position = transform.position;
            Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
            rigidB.velocity = Vector3.up * projectileSpeed;

            // Increase heat when firing
            currentHeat += heatIncreaseRate;
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(cooldownRate);
        isOverheated = false;
        currentHeat = 0f;
    }
}
