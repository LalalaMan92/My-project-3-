using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 引用 Unity 的默認 UI

public class ObjInter : MonoBehaviour
{
    int score = 0;
    public Text scoreTxt; // 使用 Unity 的默認 Text 顯示分數
    public GameObject scorePanel; // 用於顯示最終分數的面板
    public Text finalScoreTxt; // Unity 默認的 Text 顯示最終分數
    private Dictionary<GameObject, int> badTreeCounter = new Dictionary<GameObject, int>();
    private Dictionary<GameObject, int> goodTreeCounter = new Dictionary<GameObject, int>();

    // Start is called before the first frame update
    void Start()
    {
        scoreTxt.text = "Score=0"; // 初始化分數顯示
        scorePanel.SetActive(false); // 遊戲開始時隱藏分數面板
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);

        // 當碰撞到標籤為 "bad tree" 的物件時，增加計數器並加分
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
                collision.gameObject.SetActive(false); // 隱藏該物件
                Debug.Log("Bad tree disappeared after 3 hits!");
                CheckAllBadTreesGone();
            }
            else
            {
                Debug.Log("Bad tree hit count: " + badTreeCounter[collision.gameObject]);
            }

            score += 1; // 每次碰撞 bad tree 加分
            scoreTxt.text = "Score=" + score.ToString(); // 更新分數顯示
        }

        // 當碰撞到標籤為 "good tree" 的物件時，增加計數器並扣分
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
                collision.gameObject.SetActive(false); // 隱藏該物件
                Debug.Log("Good tree disappeared after 3 hits!");
            }
            else
            {
                Debug.Log("Good tree hit count: " + goodTreeCounter[collision.gameObject]);
            }

            score -= 1; // 每次碰撞 good tree 扣分
            scoreTxt.text = "Score=" + score.ToString(); // 更新分數顯示
        }
    }

    // 檢查所有 bad tree 是否消失
    void CheckAllBadTreesGone()
    {
        GameObject[] badTrees = GameObject.FindGameObjectsWithTag("bad tree");
        foreach (GameObject tree in badTrees)
        {
            if (tree.activeInHierarchy)
            {
                return; // 如果找到一棵 bad tree 還存在，直接返回
            }
        }

        // 當所有 bad tree 都消失時顯示分數面板
        ShowFinalScore();
    }

    // 顯示最終分數的 UI
    void ShowFinalScore()
    {
        scorePanel.SetActive(true); // 顯示分數面板
        finalScoreTxt.text = "Final Score: " + score.ToString(); // 更新最終分數顯示
    }

    // Update is called once per frame
    void Update()
    {
        // 每幀可以處理其他邏輯
    }
}
