using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // �ޥ� Unity ���q�{ UI

public class ObjInter : MonoBehaviour
{
    int score = 0;
    public Text scoreTxt; // �ϥ� Unity ���q�{ Text ��ܤ���
    public GameObject scorePanel; // �Ω���̲ܳפ��ƪ����O
    public Text finalScoreTxt; // Unity �q�{�� Text ��̲ܳפ���
    private Dictionary<GameObject, int> badTreeCounter = new Dictionary<GameObject, int>();
    private Dictionary<GameObject, int> goodTreeCounter = new Dictionary<GameObject, int>();

    // Start is called before the first frame update
    void Start()
    {
        scoreTxt.text = "Score=0"; // ��l�Ƥ������
        scorePanel.SetActive(false); // �C���}�l�����ä��ƭ��O
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);

        // ��I������Ҭ� "bad tree" ������ɡA�W�[�p�ƾ��å[��
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
                CheckAllBadTreesGone();
            }
            else
            {
                Debug.Log("Bad tree hit count: " + badTreeCounter[collision.gameObject]);
            }

            score += 1; // �C���I�� bad tree �[��
            scoreTxt.text = "Score=" + score.ToString(); // ��s�������
        }

        // ��I������Ҭ� "good tree" ������ɡA�W�[�p�ƾ��æ���
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

            score -= 1; // �C���I�� good tree ����
            scoreTxt.text = "Score=" + score.ToString(); // ��s�������
        }
    }

    // �ˬd�Ҧ� bad tree �O�_����
    void CheckAllBadTreesGone()
    {
        GameObject[] badTrees = GameObject.FindGameObjectsWithTag("bad tree");
        foreach (GameObject tree in badTrees)
        {
            if (tree.activeInHierarchy)
            {
                return; // �p�G���@�� bad tree �٦s�b�A������^
            }
        }

        // ��Ҧ� bad tree ����������ܤ��ƭ��O
        ShowFinalScore();
    }

    // ��̲ܳפ��ƪ� UI
    void ShowFinalScore()
    {
        scorePanel.SetActive(true); // ��ܤ��ƭ��O
        finalScoreTxt.text = "Final Score: " + score.ToString(); // ��s�̲פ������
    }

    // Update is called once per frame
    void Update()
    {
        // �C�V�i�H�B�z��L�޿�
    }
}
