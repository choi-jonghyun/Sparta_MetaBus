using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopInteractionTrigger : MonoBehaviour
{

    public GameObject interactionPopup; // ��ȣ�ۿ� �ȳ� UI (�����Ϳ��� ����)
    public bool isPlayerInRange = false;    // �÷��̾ ���� �ȿ� �ִ���
    public string miniGameSceneName = "TopDownScene"; //�̴ϰ��� �̸�
    

    private void Start()
    {
        isPlayerInRange = false;
        interactionPopup.SetActive(false);
    }
    void Update()
    {

        // ���� �ȿ� �ְ� FŰ�� ������ ��ȣ�ۿ� ����
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {

            SceneManager.LoadScene(miniGameSceneName);
          
        }
    }
    // �÷��̾ Ʈ���ſ� ������ �� ����Ǵ� �Լ�
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ʈ���ſ� ���� ������Ʈ�� "Player" �±׸� ������ �ִ��� Ȯ��
        if (other.CompareTag("Player"))
        {

            isPlayerInRange = true;
            interactionPopup.SetActive(true);   // �÷��̾ �����ϸ� �˾� ǥ��

            // ���ϴ� �̺�Ʈ ���� (��: �̴ϰ��� ����, ��ȭ â ���� ��)
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactionPopup.SetActive(false);  // �÷��̾ ����� �˾� ����
        }
    }
}
