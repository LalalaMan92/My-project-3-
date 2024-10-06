using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject mainMenuPanel; // 主菜單面板
    public Text scoreTxt; // 用於顯示場上剩餘 bad tree 數量的文本
    public GameObject scorePanel; // 顯示最終分數的面板
    public Text finalScoreTxt; // 用於顯示最終分數的文本
    public Button startButton; // 開始遊戲按鈕
    public GameObject background; // 背景物件
    public GameObject axe; // 斧頭的遊戲物件

    private bool isGameStarted = false; // 遊戲是否開始

    // 計數器字典
    private Dictionary<GameObject, int> badTreeCounter = new Dictionary<GameObject, int>();
    private Dictionary<GameObject, int> goodTreeCounter = new Dictionary<GameObject, int>();

    void Start()
    {
        // 初始化：顯示主菜單，隱藏分數面板
        mainMenuPanel.SetActive(true);
        scorePanel.SetActive(false);
        scoreTxt.gameObject.SetActive(false); // 隱藏分數文本

        // 設定按鈕點擊事件
        startButton.onClick.AddListener(StartGame);
    }

    void Update()
    {
        // 如果遊戲已經開始，實時更新剩餘 bad tree 的數量顯示
        if (isGameStarted)
        {
            UpdateScore(); // 更新顯示
            CheckGameOver(); // 檢查遊戲結束條件
        }
    }

    // 開始遊戲
    public void StartGame()
    {
        Debug.Log("Game Started"); // 確認按鈕被點擊
        isGameStarted = true;
        mainMenuPanel.SetActive(false); // 隱藏主菜單
        scorePanel.SetActive(false); // 確保分數面板隱藏
        scoreTxt.gameObject.SetActive(true); // 顯示分數文本
        background.SetActive(false); // 隱藏背景
        startButton.gameObject.SetActive(false); // 隱藏開始遊戲按鈕
        axe.transform.position = new Vector3(-2, 1, -6); // 重置斧頭位置
        Rigidbody axeRb = axe.GetComponent<Rigidbody>();
        if (axeRb != null)
        {
            axeRb.useGravity = false; // 禁用重力
        }

        // 初始化分數顯示
        UpdateScore();
    }

    // 更新分數顯示為剩餘的 bad tree 數量
    void UpdateScore()
    {
        scoreTxt.text = "Kill the Trees !! " 
    }

    // 檢查遊戲結束條件（當場上沒有 bad tree 時結束遊戲）
    void CheckGameOver()
    {
        if (GameObject.FindGameObjectsWithTag("bad tree").Length == 0)
        {
            ShowFinalScore();
        }
    }

    // 顯示最終分數的 UI
    void ShowFinalScore()
    {
        isGameStarted = false; // 停止遊戲
        scorePanel.SetActive(true); // 顯示分數面板
        background.SetActive(true); // 顯示背景
        scoreTxt.gameObject.SetActive(false); // 隱藏分數文本
        finalScoreTxt.text = "All bad trees cleared!"; // 顯示最終結果
    }

    // 當物件與其他物體發生碰撞時
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);

        // 檢查是否是斧頭
        if (collision.gameObject.CompareTag("axe"))
        {
            return; // 如果是斧頭，則不處理
        }

        // 當碰撞到標籤為 "bad tree" 的物件時，增加計數器並隱藏物件
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
            }
            else
            {
                Debug.Log("Bad tree hit count: " + badTreeCounter[collision.gameObject]);
            }

            UpdateScore(); // 每次砍樹後更新分數顯示
        }

        // 當碰撞到標籤為 "good tree" 的物件時，隱藏物件（不扣分，只隱藏）
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
        }
    }
}
