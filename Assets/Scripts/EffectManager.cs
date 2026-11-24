using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }

    [Header("特效预制体")]
    public GameObject coinEffectPrefab; // 用来拖入刚才做的 CoinEffect 预制体

    private void Awake()
    {
        Instance = this;
    }

    // 在指定坐标位置播放吃金币特效
    public void PlayCoinEffect(Vector3 position)
    {
        if (coinEffectPrefab != null)
        {
            // 1. 在金币位置动态生成一个特效物体（Quaternion.identity 代表默认不旋转）
            GameObject effect = Instantiate(coinEffectPrefab, position, Quaternion.identity);

            // 2. 1.5 秒后自动销毁这个特效物体，防止场景里残留一堆空物体占内存
            Destroy(effect, 1.5f);
        }
    }
}