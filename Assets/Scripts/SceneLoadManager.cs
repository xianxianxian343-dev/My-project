using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadManager : MonoBehaviour
{
    [Header("加载界面 UI 引用")]
    public GameObject loadingPanel; // 加载面板
    public Slider progressBar;      // 进度条滑块 (取值范围 0 ~ 1)
    public Text progressText;       // 显示百分比的文字

    // 供“开始游戏”按钮点击调用的方法
    public void OnStartGameClicked()
    {
        // 1. 弹出加载面板
        loadingPanel.SetActive(true);

        // 2. 开启协程，异步加载跑酷场景 "Game1"
        StartCoroutine(LoadSceneAsyncCoroutine("Game1"));
    }

    private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        // 开始后台异步加载场景
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // 先禁止加载完立刻跳转，防止场景太小导致进度条一闪而过看不见
        operation.allowSceneActivation = false;

        float displayProgress = 0f; // 画面上显示的平滑进度 (0 ~ 1)

        // 当进度还没走满到 1 (即 100%) 时，每帧循环更新进度条
        while (displayProgress < 1f)
        {
            // Unity 规定：allowSceneActivation 为 false 时，operation.progress 最高只会到 0.9
            // 所以当真实进度达到 0.9 时，说明后台其实已经加载完毕了，可以把目标进度设为 1.0
            float targetProgress = operation.progress < 0.9f ? operation.progress : 1f;

            // 让显示的进度平滑地向目标进度靠拢，这样进度条看起来是匀速丝滑上涨的
            displayProgress = Mathf.MoveTowards(displayProgress, targetProgress, Time.deltaTime * 0.8f);

            // 更新 UI 滑块和百分比文字
            progressBar.value = displayProgress;
            progressText.text = "加载中... " + Mathf.RoundToInt(displayProgress * 100f) + "%";

            yield return null; // 等待下一帧继续循环
        }

        // 进度条走满 100% 后，允许正式进入新场景
        operation.allowSceneActivation = true;
    }
}