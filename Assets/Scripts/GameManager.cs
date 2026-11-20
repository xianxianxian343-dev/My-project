using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // 管场景切换的命名空间移到这里

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool isGameOver = false; // 游戏是否已经结束
    private int coinCount = 0;      // 当前吃到的金币数量

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60; // 全局帧率设置归总管负责
        // 【新增】一进入游戏场景，就从本地读取玩家在设置里调好的音量
        float savedVolume = PlayerPrefs.GetFloat(SettingsManager.VOLUME_KEY, 1f);
        AudioListener.volume = savedVolume;
    }

    private void Start()
    {
        // 游戏刚开始时，让 UIManager 把金币显示初始化为 0
        UIManager.Instance.UpdateCoinText(coinCount);
    }

    // 当玩家吃到金币时，调用这个方法
    public void AddCoin()
    {
        if (isGameOver) return;

        coinCount++; // 1. 总管自己把金币数 +1
        UIManager.Instance.UpdateCoinText(coinCount); // 2. 吩咐 UI 经理刷新文字
        AudioManager.Instance.PlayCoinSoundAndCheckBGM(); // 3. 吩咐音频经理放声音、切歌
    }

    // 当玩家撞到障碍物时，调用这个方法
    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true; // 1. 标记游戏结束
        Debug.Log("碰到障碍物了");

        // 【新增】游戏结束时，检查本局分数有没有打破历史最高纪录
        SaveHighScoreIfNeeded();

        UIManager.Instance.ShowGameOverUI(); // 2. 吩咐 UI 经理弹出结束文字
        StartCoroutine(RestartScene()); // 3. 开启两秒后重开的倒计时
    }

    // 等待两秒并且重新加载场景
    private IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(2f);
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    // 【新增】供“返回主页”按钮调用的方法
    public void ReturnToStartScene()
    {
        // 如果玩家在半路点返回，也先帮他把当前吃到的金币结算一下最高分
        SaveHighScoreIfNeeded();

        // 切回开始页面场景
        SceneManager.LoadScene("StartScene");
    }

    // 【新增】核心逻辑：检查并保存历史最高分
    private void SaveHighScoreIfNeeded()
    {
        // 1. 先去储物柜里查一下之前的最高分是多少（默认 0）
        int currentHighScore = PlayerPrefs.GetInt(SettingsManager.HIGH_SCORE_KEY, 0);

        // 2. 只有当“本局金币数”大于“历史最高分”时，才覆盖保存！
        if (coinCount > currentHighScore)
        {
            PlayerPrefs.SetInt(SettingsManager.HIGH_SCORE_KEY, coinCount);
            PlayerPrefs.Save();
            Debug.Log("恭喜打破纪录！新最高分：" + coinCount);
        }
    }


}
