using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // UI 네임스페이스 추가
using UnityEngine.SceneManagement; // 씬 관리를 위한 네임스페이스 추가
public class GameDirector : MonoBehaviour
{
    GameObject timeText;
    GameObject pointText;
    float time = 30.0f;
    int point = 0;
    GameObject generator;
    public Button restartButton; // Restart 버튼 참조
    int bestScore = 0;
    GameObject bestScoreText;
    // Start is called before the first frame update
    void Start()
    {
        this.timeText = GameObject.Find("Time");
        this.pointText = GameObject.Find("Point");
        this.generator = GameObject.Find("ItemGenerator");
        this.bestScoreText = GameObject.Find("BestScore");
        LoadBestScore();
        UpdateBestScoreUI();
    }
    void LoadBestScore()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    void SaveBestScore()
    {
        if (point > bestScore)
        {
            bestScore = point;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
    }

    void UpdateBestScoreUI()
    {
        bestScoreText.GetComponent<TextMeshProUGUI>().text = "Best Score: " + bestScore.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        this.time -= Time.deltaTime;

        if(this.time < 0){
            this.time = 0;
            this.generator.GetComponent<ItemGenerator>().SetParameter(10000.0f,0,0);
            SaveBestScore();
            UpdateBestScoreUI();
        }
        else if(0 <= this.time && this.time < 4){
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.3f, -0.06f,0);
        }
        else if(4 <= this.time && this.time < 12){
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.5f, -0.05f,6);
        }
        else if(12 <= this.time && this.time < 23){
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.8f, -0.04f,4);
        }
        else if(23 <= this.time && this.time < 30){
            this.generator.GetComponent<ItemGenerator>().SetParameter(1.0f, -0.03f,2);
        }
        
        this.timeText.GetComponent<TextMeshProUGUI>().text = this.time.ToString("F1");
        this.pointText.GetComponent<TextMeshProUGUI>().text = this.point.ToString()+" point";
        if(this.time <= 0 && restartButton != null)
        {
            restartButton.gameObject.SetActive(true); // 시간이 끝나면 버튼 표시
        }
    }
    public void GetApple()
    {
        this.point += 100;
    }
    public void GetBoomb()
    {
        this.point /= 2;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 재로드
    }
}
