using UnityEngine;

public class OrbitMotion : MonoBehaviour
{
    [HideInInspector] public bool CanOrbitMotion;
    public Transform centerObject; // ���S�ƂȂ�I�u�W�F�N�g
    public float radius = 5f; // �~�^���̔��a
    public float speed = 1f; // �~�^���̑��x

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (CanOrbitMotion)
        {
            // ���S�I�u�W�F�N�g�̈ʒu���擾
            Vector3 centerPosition = centerObject.position;

            // �~�^���̌v�Z
            float angle = Time.time * speed;
            float x = centerPosition.x + radius * Mathf.Cos(angle);
            float z = centerPosition.z + radius * Mathf.Sin(angle);

            // �I�u�W�F�N�g�̈ʒu���X�V
            transform.position = new Vector3(x, initialPosition.y, z);
        }
    }
}
