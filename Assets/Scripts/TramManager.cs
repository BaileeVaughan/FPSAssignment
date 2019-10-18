using UnityEngine;
using UnityEngine.UI;

public class TramManager : MonoBehaviour
{
    [Header("Health")]
    public Slider hpSlider;
    public Image hpFill;
    public float maxHP = 500f;
    public float curHP = 0f;
    public Canvas tramUI;

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
}
