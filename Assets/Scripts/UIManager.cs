using UnityEngine;
using UnityEngine.UI; // 只有管 UI 的脚本才需要引用这个命名空间

public class UIManager : MonoBehaviour
{
    // 单例模式：让其他脚本随时可以通过 UIManager.Instance 找到它
    public static UIManager Instance { get; private set; }

    [Header("UI 组件引用")]
    public Text coinText;          // 显示金币数量的文字
    public GameObject gameOverUI;  // “游戏结束”文字物体
    

    private void Awake()
    {
        Instance = this;
    }

    // 公开一个方法：专门用来更新金币文字
    public void UpdateCoinText(int count)
    {
        if (coinText != null)
        {
            coinText.text = "金币：" + count;
        }
    }

    // 公开一个方法：专门用来显示游戏结束画面
    public void ShowGameOverUI()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
    }
}