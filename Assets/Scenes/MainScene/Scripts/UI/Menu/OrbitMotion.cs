using UnityEngine;

public class OrbitMotion : MonoBehaviour
{
    [HideInInspector] public bool CanOrbitMotion;
    public Transform centerObject; // 中心となるオブジェクト
    public float radius = 5f; // 円運動の半径
    public float speed = 1f; // 円運動の速度

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (CanOrbitMotion)
        {
            // 中心オブジェクトの位置を取得
            Vector3 centerPosition = centerObject.position;

            // 円運動の計算
            float angle = Time.time * speed;
            float x = centerPosition.x + radius * Mathf.Cos(angle);
            float z = centerPosition.z + radius * Mathf.Sin(angle);

            // オブジェクトの位置を更新
            transform.position = new Vector3(x, initialPosition.y, z);
        }
    }
}
