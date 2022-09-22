using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject followTarget; // 想让相机跟随的东西
    public float moveSpeed; // 跟随的速度


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 检查是否有可用的目标，如果没有，摄像机就不会跟随。
        if (followTarget != null) {
            // 计算CameraMount所需的位置。
            // Lerp()有三个参数:3D空间中的起始位置，3D空间中的结束位置，
            // 以及一个介于0和1之间的值，表示起始点和结束点之间的点
            // 的位置。Lerp()返回3D空间中开始和结束位置之间的一个点
            // 由最后一个值决定。
            // 例如，如果最后一个值被设置为0，那么Lerp()将返回起始位置。如果
            // 最后一个值是1，它返回结束位置。如果最后一个值是0.5，则返回一个点
            // 在起点和终点的中间位置。
            // 在这种情况下，你将提供相机安装位置作为开始和播放器
            // 位置作为结束。最后，将自上一帧速率以来的时间乘以一个速度
            // 乘数，为最后一个参数获取合适的值。实际上，这使得
            // 随着时间的推移，摄像机安装位置会平稳地移动到玩家所在的位置。
            // 保存代码并返回Unity。如果你现在查看检查器，你会看到两个新的
            // 字段名为跟踪目标和移动速度。如前所述，这些是
            // 由Unity从你刚添加到脚本的公共变量中自动派生。
            // 这些变量需要一些值。
            // 在层级中仍然选择CameraMount的情况下，将spacemine拖到Follow
            // 目标字段，并设置移动速度为20。
            transform.position = Vector3.Lerp(transform.position,
            followTarget.transform.position, Time.deltaTime * moveSpeed);
        }
    }
}
