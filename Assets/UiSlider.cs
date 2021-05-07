using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiSlider : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        DelegateManager.Instance.GetDamageOperate += ShowHP;
    }
    public void ShowHP()
    {
        this.slider.value = playerController.GetHp() / 150;
    }

}
