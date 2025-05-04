using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopGameUI : TopBaseUI
{
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private Slider hpSlider;

    private void Start()
    {
        UpdateHPSlider(1); // ���� �� ü�� �����̴��� ���� ä�� (100%)
    }

    // ü�� �����̴� ���� �ۼ�Ʈ(0~1)�� ����
    public void UpdateHPSlider(float percentage)
    {
        hpSlider.value = percentage;
    }

    // ���̺� �ؽ�Ʈ�� ���� ���̺� ���ڷ� ����
    public void UpdateWaveText(int wave)
    {
        waveText.text = wave.ToString();
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}