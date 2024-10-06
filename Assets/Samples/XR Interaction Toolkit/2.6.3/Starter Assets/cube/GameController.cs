using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject mainMenuPanel; // �D��歱�O
    public Text scoreTxt; // �Ω���ܳ��W�Ѿl bad tree �ƶq���奻
    public GameObject scorePanel; // ��̲ܳפ��ƪ����O
    public Text finalScoreTxt; // �Ω���̲ܳפ��ƪ��奻
    public Button startButton; // �}�l�C�����s
    public GameObject background; // �I������
    public GameObject axe; // ���Y���C������

    private bool isGameStarted = false; // �C���O�_�}�l

    // �p�ƾ��r��
    private Dictionary<GameObject, int> badTreeCounter = new Dictionary<GameObject, int>();
    private Dictionary<GameObject, int> goodTreeCounter = new Dictionary<GameObject, int>();

    void Start()
    {
        // ��l�ơG��ܥD���A���ä��ƭ��O
        mainMenuPanel.SetActive(true);
        scorePanel.SetActive(false);
        scoreTxt.gameObject.SetActive(false); // ���ä��Ƥ奻

        // �]�w���s�I���ƥ�
        startButton.onClick.AddListener(StartGame);
    }

    void Update()
    {
        // �p�G�C���w�g�}�l�A��ɧ�s�Ѿl bad tree ���ƶq���
        if (isGameStarted)
        {
            UpdateScore(); // ��s���
            CheckGameOver(); // �ˬd�C����������
        }
    }

    // �}�l�C��
    public void StartGame()
    {
        Debug.Log("Game Started"); // �T�{���s�Q�I��
        isGameStarted = true;
        mainMenuPanel.SetActive(false); // ���åD���
        scorePanel.SetActive(false); // �T�O���ƭ��O����
        scoreTxt.gameObject.SetActive(true); // ��ܤ��Ƥ奻
        background.SetActive(false); // ���íI��
        startButton.gameObject.SetActive(false); // ���ö}�l�C�����s
        axe.transform.position = new Vector3(-2, 1, -6); // ���m���Y��m
        Rigidbody axeRb = axe.GetComponent<Rigidbody>();
        if (axeRb != null)
        {
            axeRb.useGravity = false; // �T�έ��O
        }

        // ��l�Ƥ������
        UpdateScore();
    }

    // ��s������ܬ��Ѿl�� bad tree �ƶq
    void UpdateScore()
    {
        scoreTxt.text = "Kill the Trees !! " 
    }

    // �ˬd�C����������]����W�S�� bad tree �ɵ����C���^
    void CheckGameOver()
    {
        if (GameObject.FindGameObjectsWithTag("bad tree").Length == 0)
        {
            ShowFinalScore();
        }
    }

    // ��̲ܳפ��ƪ� UI
    void ShowFinalScore()
    {
        isGameStarted = false; // ����C��
        scorePanel.SetActive(true); // ��ܤ��ƭ��O
        background.SetActive(true); // ��ܭI��
        scoreTxt.gameObject.SetActive(false); // ���ä��Ƥ奻
        finalScoreTxt.text = "All bad trees cleared!"; // ��̲ܳ׵��G
    }

    // ����P��L����o�͸I����
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);

        // �ˬd�O�_�O���Y
        if (collision.gameObject.CompareTag("axe"))
        {
            return; // �p�G�O���Y�A�h���B�z
        }

        // ��I������Ҭ� "bad tree" ������ɡA�W�[�p�ƾ������ê���
        if (collision.gameObject.CompareTag("bad tree"))
        {
            if (badTreeCounter.ContainsKey(collision.gameObject))
            {
                badTreeCounter[collision.gameObject] += 1;
            }
            else
            {
                badTreeCounter[collision.gameObject] = 1;
            }

            if (badTreeCounter[collision.gameObject] >= 3)
            {
                collision.gameObject.SetActive(false); // ���øӪ���
                Debug.Log("Bad tree disappeared after 3 hits!");
            }
            else
            {
                Debug.Log("Bad tree hit count: " + badTreeCounter[collision.gameObject]);
            }

            UpdateScore(); // �C�������s�������
        }

        // ��I������Ҭ� "good tree" ������ɡA���ê���]�������A�u���á^
        if (collision.gameObject.CompareTag("good tree"))
        {
            if (goodTreeCounter.ContainsKey(collision.gameObject))
            {
                goodTreeCounter[collision.gameObject] += 1;
            }
            else
            {
                goodTreeCounter[collision.gameObject] = 1;
            }

            if (goodTreeCounter[collision.gameObject] >= 3)
            {
                collision.gameObject.SetActive(false); // ���øӪ���
                Debug.Log("Good tree disappeared after 3 hits!");
            }
            else
            {
                Debug.Log("Good tree hit count: " + goodTreeCounter[collision.gameObject]);
            }
        }
    }
}
