using UnityEngine;
using UnityEditor;

public class ObstacleGenerator
{
    // 在顶部菜单添加：工具 -> 批量生成障碍物
    [MenuItem("工具/批量生成障碍物")]
    public static void GenerateObstacles()
    {
        const int count = 20;            // 生成数量
        const float rangeX = 20f;        // X 轴随机范围 (-20 到 20)
        const float rangeZ = 20f;        // Z 轴随机范围 (-20 到 20)
        const float fixedY = 0.5f;       // Y 轴高度（默认 Cube 尺寸为 1，中心在 0.5 刚好落在 Y=0 平面上）

        // 创建一个父物体用来存放生成的障碍物，保持 Hierarchy 视图整洁
        GameObject root = new GameObject("Obstacles");
        Undo.RegisterCreatedObjectUndo(root, "批量生成障碍物");
        
        for (int i = 0; i < count; i++)
        {
            // 生成立方体（自带 BoxCollider 和 MeshRenderer）
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.name = $"Obstacle_{i + 1}";

            // 随机位置
            float randomX = Random.Range(-rangeX, rangeX);
            float randomZ = Random.Range(-rangeZ, rangeZ);
            cube.transform.position = new Vector3(randomX, fixedY, randomZ);

            // 设为 root 的子物体
            cube.transform.SetParent(root.transform);

            // 注册撤销操作（支持 Ctrl + Z 撤销）
            Undo.RegisterCreatedObjectUndo(cube, "批量生成障碍物");
        }

        // 自动在场景和层级窗口中选中生成的父物体
        Selection.activeGameObject = root;

        // 标记当前场景已被修改（触发保存提示）
        if (!Application.isPlaying)
        {
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(
                UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene()
            );
        }

        Debug.Log($"<color=green>[ObstacleGenerator]</color> 成功生成 {count} 个随机障碍物，已放入 'Obstacles' 节点下！");
    }
}

