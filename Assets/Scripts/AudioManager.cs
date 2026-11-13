using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("音效设置 (SFX)")]
    public AudioSource ad;      // 控制音效的音响（对应你场景里的 VFX 物体）
    public AudioClip aclip;     // 吃金币音源（对应 Se_UI_JinBi）

    [Header("背景音乐设置 (BGM)")]
    public AudioSource BGM;     // 背景音乐音响（对应你场景里的 BGM 物体）
    public AudioClip bgm2;      // 第二首背景音乐（对应 Bgm_JieMian）

    private void Awake()
    {
        Instance = this;
    }

    // 播放吃金币音效，并检查是否需要切歌
    public void PlayCoinSoundAndCheckBGM()
    {
        // 1. 播放吃金币音效
        if (ad != null && aclip != null)
        {
            ad.PlayOneShot(aclip, 0.5f);
        }

        // 2. 吃到金币后切换背景音乐
        if (BGM != null && BGM.clip != null && BGM.clip.name == "Bgm_ZhanDou")
        {
            BGM.clip = bgm2;
            BGM.Play();
        }
    }
}
