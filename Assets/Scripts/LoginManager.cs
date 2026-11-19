using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [Header("登录弹窗 UI 引用")]
    public GameObject loginPanel;      // 登录弹窗面板
    public InputField usernameInput;   // 账号输入框
    public InputField passwordInput;   // 密码输入框
    public Text tipText;               // 提示文字（显示成功或报错）

    // 给账号的键名加上统一前缀，防止和以后保存的音量、最高分重名
    private const string ACCOUNT_PREFIX = "Account_";
    private const string LAST_USER_KEY = "LastLoginUser";

    // 1. 点击主界面的“登录”按钮：打开弹窗
    public void OpenLoginPanel()
    {
        loginPanel.SetActive(true);
        tipText.text = ""; // 清空上一次残留的提示文字

        // 尝试读取上次登录过的账号并自动填入（方便直观验证本地数据真的保存下来了）
        string lastUser = PlayerPrefs.GetString(LAST_USER_KEY, "");
        usernameInput.text = lastUser;
        passwordInput.text = "";
    }

    // 2. 点击弹窗里的“关闭”按钮：隐藏弹窗
    public void CloseLoginPanel()
    {
        loginPanel.SetActive(false);
    }

    // 3. 点击“注册”按钮：把账号和密码以键值对形式存到本地
    public void OnRegisterClicked()
    {
        // Trim() 会去掉玩家不小心在账号前后敲的多余空格
        string username = usernameInput.text.Trim();
        string password = passwordInput.text;

        // 防止玩家什么都不填就点按钮
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            tipText.text = "账号和密码不能为空！";
            return;
        }

        // 拼出唯一的键名，比如输入 "xiaoming"，键名就是 "Account_xiaoming"
        string key = ACCOUNT_PREFIX + username;

        // 先检查储物柜里是不是已经有这个账号了
        if (PlayerPrefs.HasKey(key))
        {
            tipText.text = "该账号已存在，请直接登录！";
            return;
        }

        // 【核心代码】写入键值对：Key = "Account_账号名"，Value = 密码
        PlayerPrefs.SetString(key, password);
        PlayerPrefs.Save(); // 立刻保存到电脑硬盘

        tipText.text = "注册成功！现在可以点击登录了。";
    }

    // 4. 点击“确认登录”按钮：去本地查密码并核对
    public void OnLoginClicked()
    {
        string username = usernameInput.text.Trim();
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            tipText.text = "请输入账号和密码！";
            return;
        }

        string key = ACCOUNT_PREFIX + username;

        // 先检查本地有没有存过这个账号
        if (!PlayerPrefs.HasKey(key))
        {
            tipText.text = "账号不存在，请先注册！";
            return;
        }

        // 【核心代码】通过键名，把之前存进硬盘的密码取出来
        string savedPassword = PlayerPrefs.GetString(key);

        if (savedPassword == password)
        {
            // 登录成功，顺便把“最近登录的账号”也存进本地
            PlayerPrefs.SetString(LAST_USER_KEY, username);
            PlayerPrefs.Save();

            tipText.text = "登录成功！欢迎回来，" + username;
        }
        else
        {
            tipText.text = "密码错误，请重新输入！";
        }
    }
}