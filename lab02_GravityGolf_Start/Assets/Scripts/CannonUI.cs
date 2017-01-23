using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CannonUI : MonoBehaviour {

    public Image powerFillBar;
    public Text powerText;

    public Color lowPower;
    public Color highPower;

	public void SetPower(float power, float powerMax)
    {
        float powerPerc = power / powerMax;
        powerFillBar.fillAmount = powerPerc;
        powerText.text = "" + (int)power;
        powerText.color = Color.Lerp(lowPower, highPower, powerPerc);
    }
}
