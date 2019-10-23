using UnityEngine;
using UnityEngine.UI;

public class TramManager : MonoBehaviour
{
    [Header("Health")]
    public Canvas tramUI;
    public Slider hpSlider;
    public Image hpFill;
    public float maxHP = 500f;
    public float curHP = 0f;

    private void Start()
    {
        curHP = maxHP;
    }

    private void Update()
    {
        tramUI.transform.LookAt(Camera.main.transform);
        hpSlider.value = Mathf.Clamp01(curHP / maxHP);
        if (curHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            curHP -= col.GetComponent<EnemyManager>().damage * 1.5f;
        }
    }
}
