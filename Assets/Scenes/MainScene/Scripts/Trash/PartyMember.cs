using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PartyMember : MonoBehaviour
{
    [Tooltip("�����X�s�[�h")]
    float MoveSpeedPlayer;
    [SerializeField, Tooltip("�v���C���[�̓����X�s�[�h�̉��{��")]
    float MoveSpeedRatio;
    [SerializeField, Tooltip("�ǔ����邩�ǂ���(����鋗����\���I�u�W�F�N�g�ɐN�����Ă��邩�ǂ���)")]
    bool Follow;
    [SerializeField, Tooltip("����鋗����\���Q�[���I�u�W�F�N�g")]
    GameObject DistanceObj;
    [SerializeField, Tooltip("�ǔ�����Ώۂ̃Q�[���I�u�W�F�N�g")]
    GameObject TargetObj;
    [Tooltip("�ǔ�����I�u�W�F�N�g�̃g�����X�t�H�[��")]
    Transform TargetTrs;

    //�u�Ԉړ�
    [SerializeField, Tooltip("�u�Ԉړ����鋗��")]
    float DistanceMax;
    [SerializeField, Tooltip("�X�s�[�h��1.5�{�ɂȂ鋗��")]
    float DistanceA;
    [SerializeField, Tooltip("�X�s�[�h��2�{�ɂȂ鋗��")]
    float DistanceB;
    [Tooltip("MoveSpeedRatio�����ɓ���Ă���")]
    float MoveSpeedRatioTem;

    //List�ɍ��W�����ăv���C���[��ǔ�
    [Tooltip("�v���C���[�̍��W�����Ă������X�g")]
    List<Vector3> PlayerPositions = new List<Vector3>();
    [SerializeField, Tooltip("�v���C���[�̍��W���Ď擾����܂ł̎���")]
    float RecordInterval;
    [Tooltip("�v���C���[�̍��W�𐔂���")]
    int PlayerPositionsCount = 0;


    void Start()
    {
        TargetTrs = TargetObj.GetComponent<Transform>();
        PlayerPositions.Add(TargetTrs.position);//�ŏ��Ƀ��X�g�Ƀv���C���[�̍��W������

        //Player��MoveSpeed���擾
        //Player PlayerScript;
        //PlayerScript = TargetObj.GetComponent<Player>(); //�t���Ă���X�N���v�g���擾
        //MoveSpeedPlayer = PlayerScript.MoveSpeed;
        
        Follow = true;//�ŏ��͂��Ă���
        MoveSpeedRatioTem = MoveSpeedRatio;
    }

    void Update()
    {
        if(Follow)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerPositions[PlayerPositionsCount], MoveSpeedPlayer * MoveSpeedRatio * Time.deltaTime);
            if (PlayerPositions[PlayerPositionsCount].x + 0.5 >= transform.position.x && PlayerPositions[PlayerPositionsCount].x - 0.5 <= transform.position.x && PlayerPositions[PlayerPositionsCount].y + 0.5 >= transform.position.y && PlayerPositions[PlayerPositionsCount].y - 0.5 <= transform.position.y)
                {
                    //timer = 0f;
                    PlayerPositions.Add(TargetTrs.position);
                    PlayerPositionsCount++;
                }
        }

        var Heading = TargetTrs.position - transform.position;//�^�[�Q�b�g�Ƃ̋���
        if (Heading.x >= DistanceA || Heading.x <= -DistanceA || Heading.y >= DistanceA || Heading.y <= -DistanceA)
        {
            MoveSpeedRatio = 3;
            //Debug.Log("1.5");
        }
        if (Heading.x >= DistanceB || Heading.x <= -DistanceB || Heading.y >= DistanceB || Heading.y <= -DistanceB)
        {
            MoveSpeedRatio = 5;
            //Debug.Log("2");
        }
        if (Heading.x >= DistanceMax || Heading.x <= -DistanceMax || Heading.y >= DistanceMax || Heading.y <= -DistanceMax)
        {
            MoveSpeedRatio = 100;
            //Debug.Log("100");
        }
        if (Heading.x <= 7 && Heading.x >= -7 || Heading.y <= 7 && Heading.y >-7)
        {
            MoveSpeedRatio = MoveSpeedRatioTem;
            //Debug.Log("1");
        }

        //
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject==DistanceObj)Follow = false;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject==DistanceObj)Follow = true;
    }
}
