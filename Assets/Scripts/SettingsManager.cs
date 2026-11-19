using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("设置弹窗 UI 引用")]
    public GameObject settingsPanel; // 设置弹窗面板
    public Slider volumeSlider;      // 音量滑块
    public Text volumeValueText;     // 显示音量百分比的文字
    public Text highScoreText;       // 显示历史最高分的文字

    // 声明成 public const，方便第四步在 Game1 场景的脚本里直接引用，避免把键名拼错
    public const string VOLUME_KEY = "MasterVolume";
    public const string HIGH_SCORE_KEY = "HighScore";

    private void Start()
    {
        Application.targetFrameRate = 60;
        // 游戏一启动，就读取上次保存的音量（如果是第一次玩游戏，默认满音量 1f）
        float savedVolume = PlayerPrefs.GetFloat(VOLUME_KEY, 1f);

        // 立刻应用到整个游戏的全局音量
        AudioListener.volume = savedVolume;

        // 同步更新滑块位置和文字
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
        }
        UpdateVolumeText(savedVolume);
    }

    // 1. 点击主界面的“设置”按钮：打开弹窗并刷新最新数据
    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);

        // 读取本地保存的最高分（如果还没玩过，默认返回 0）
        int highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        if (highScoreText != null)
        {
            highScoreText.text = "历史最高分：" + highScore;
        }

        // 确保滑块显示的是当前保存的音量
        float savedVolume = PlayerPrefs.GetFloat(VOLUME_KEY, 1f);
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
        }
        UpdateVolumeText(savedVolume);
    }

    // 2. 点击弹窗里的“关闭”按钮：隐藏弹窗并立刻把设置写入硬盘
    public void CloseSettingsPanel()
    {
        PlayerPrefs.Save();
        settingsPanel.SetActive(false);
    }

    // 3. 当玩家拖动音量滑块时自动触发（注意：参数 float value 会自动接收滑块当前的值）
    public void OnVolumeChanged(float value)
    {
        // 调节整个游戏的总音量（取值 0 ~ 1）
        AudioListener.volume = value;

        // 更新画面上的百分比文字
        UpdateVolumeText(value);

        // 把新的音量存进本地储物柜
        PlayerPrefs.SetFloat(VOLUME_KEY, value);
    }

    // 辅助方法：把 0 ~ 1 的小数格式化成 "音量：80%"
    private void UpdateVolumeText(float volume)
    {
        if (volumeValueText != null)
        {
            volumeValueText.text = "音量：" + Mathf.RoundToInt(volume * 100f) + "%";
        }
    }
}