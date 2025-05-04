using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� UI�� �⺻ ������ �����ϴ� �߻� Ŭ����
public abstract class TopBaseUI : MonoBehaviour
{
    protected TopUIManager uiManager;

    public virtual void Init(TopUIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    // ���� UI ����(UIState) ���� (�ڽ� Ŭ�������� �����ؾ� ��)
    protected abstract UIState GetUIState();
    // ���޵� ���¿� ���� UI�� ���°� ��ġ�ϸ� Ȱ��ȭ, �ƴϸ� ��Ȱ��ȭ
    public void SetActive(UIState state)
    {
        this.gameObject.SetActive(GetUIState() == state);
    }
}