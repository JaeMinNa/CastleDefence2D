using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlider : MonoBehaviour
{
    private Slider _slider;
    [SerializeField] private AttackButton _attackButton;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = _attackButton.ClickTime / _attackButton.SkillCoolTime;
    }
}
