using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [Header("Movement")]
    private NavMeshAgent nav;
    public GameObject target;
    public float enemySpeed = 5f;
    public Canvas enemyUI;

    [Header("Attack")]
    public bool isBoss;
    public float damage = 5f;

    [Header("Health")]
    public Slider hpSlider;
    public Image hpFill;
    public float maxHP = 10f;
    public float curHP = 0f;

    private void Start()
    {
        //Health
        curHP = maxHP;
    }

    void Update()
    {
        //Movement
        enemyUI.transform.LookAt(Camera.main.transform);
        if (isBoss)
        {
            target = GameObject.FindGameObjectWithTag("Target");
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().lastResort == true)
            {
                target = GameObject.FindGameObjectWithTag("Player");
            }
            else
            {
                target = GameObject.FindGameObjectWithTag("Target");
            }
        }
        nav = GetComponent<NavMeshAgent>();
        nav.SetDestination(target.transform.position);

        //Health
        hpSlider.value = Mathf.Clamp01(curHP / maxHP);
        if (curHP <= 0)
        {
            Destroy(gameObject);
        }

        
    }
}
