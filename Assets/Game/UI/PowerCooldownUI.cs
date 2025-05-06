using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerCooldownUI : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private PowerAttackHandler powerHandler;
    [SerializeField] private Slider barFill;
    [SerializeField] private Image  fillImage;          
    [SerializeField] private TMP_Text label;            

    [Header("Cores")]
    [SerializeField] private Color readyColor     = Color.red;     
    [SerializeField] private Color cooldownColor  = Color.blue;   

    [Header("Piscar")]
    [SerializeField] private float flashSpeed = 4f;  
    private bool flash = false;

    void Update()
    {
        if (powerHandler == null) return;

        float t = powerHandler.CooldownFraction;  
        barFill.value = 1f - t;

        if (t <= 0.01f)      
        {
            flash = true;
            float pulse = (Mathf.Sin(Time.time * flashSpeed) + 1f) * 0.5f; 
            Color c = Color.Lerp(Color.white, readyColor, pulse);
            if (fillImage) fillImage.color = c;
            if (label)     label.color     = c;
        }
        else                
        {
            flash = false;
            if (fillImage) fillImage.color = cooldownColor;
            if (label)     label.color     = Color.white;
        }
    }
}
