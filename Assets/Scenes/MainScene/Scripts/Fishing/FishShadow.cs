using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class FishShadow : MonoBehaviour
{
    /// <summary>
    /// 1.�����J�[�\�������ɉ�](�J�[�\���A���E���h�ɓ������Ēނ�Ƃ̐��\���������Ă�����)
    /// 2.�����J�[�\���ɋ߂Â�
    /// 3.�J�[�\������(����͎��s���Ȃ�)
    /// 4.�q�b�g
    /// 5.����(�������F���[�^�[���E�[�܂ōs������ && OK�͈͂̒��ɗ��߂���)
    /// 6.����������(�������F���[�^�[�����[�܂ōs������ && OK�͈͂̊O�Ȃ�)
    /// </summary>
    

    [Tooltip("�y�t���O�zFishing�����s����Ă��邩�ǂ���")]
    bool FishingRun;
    [Tooltip("�y�t���O�z�J�����̃Y�[���ǐU�����s�������ǂ���")]
    bool CamZoomRun;
    [Tooltip("�y�t���O�z���̉�]���s���Ă��邩�ǂ���")]
    bool FishTurnRun;
    [Tooltip("�y�t���O�z�����J�[�\���ɂ����Â��Ă��邩�ǂ���")]
    bool FishApproacheRun;
    [Tooltip("�y�t���O�z����HIT���Ă��邩�ǂ���")]
    bool FishHitRun;
    //[Tooltip("�y�����蔻��z�J�[�\���A���E���h�ɓ������Ă��邩�ǂ���")]
    //bool CorsorAroundCollided;
    [Tooltip("�y�����蔻��z�J�[�\���ɓ����������ǂ���")]
    bool CorsorCollided;

    [Tooltip("���̃f�[�^(�C���X�y�N�^�[�ŕҏW���Ȃ���)")]
    public FishData FishData;

    [Tooltip("�J�[�\���ɋ߂Â����̊p�x���L�^���Ă���")]
    float InitialRotation;

    [SerializeField, Tooltip("HIT���̃��[�^�[�v���n�u")]
    GameObject FishingMeterPrefab;
    [Tooltip("���[�^�[�v���n�u�̃I�u�W�F�N�g")]
    GameObject FishingMeterObj;
    [Tooltip("HIT���̃��[�^�[�}�X�N�̃g�����X�t�H�[��")]
    Transform FishingMeterMaskTrs;
    [Tooltip("�J�[�\���̃g�����X�t�H�[��")]
    Transform CorsorTrs;
    [Tooltip("�J�����̃X�N���v�g")]
    Cam CamScript;
    [Tooltip("�v���C���[�X�N���v�g")]
    //Player PlayerScript;


    void Awake()
    {
        //PlayerScript = GameObject.Find("Reizi").GetComponent<Player>();
        CamScript = GameObject.Find("Main Camera").GetComponent<Cam>();
    }

    //�����蔻��---------------------------------------------------------------

    void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "CorsorAround")
        {
            //CorsorAroundCollided = true;
        }

        if (collision.gameObject.name == "Corsor")
        {
            CorsorCollided = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "CorsorAround")
        {
            //CorsorAroundCollided = false;
        }

        if (collision.gameObject.name == "Corsor")
        {
            CorsorCollided = false;
        }
    }


    void Update()
    {
        // �����蔻����Ƃ��ăt���O�𗧂Ă� && �v���C���[���E�L�𕂂��ׂĂ��邩-----------------------------------
        //if (CorsorAroundCollided && PlayerScript.F_FishingFloat)
        //{
            if (!FishingRun)
            {
                //�J�[�\���̎���ɓ�����������Ԋu�Ńt���O�𗧂Ă�(Fishing�����s)
                StartCoroutine(Fishing());
                FishingRun = true;
            }
        //}
        else
        {
            //�J�[�\���̎���ɓ������Ă��Ȃ�������t���O���O��
            FishTurnRun = false;
            FishApproacheRun = false;
            FishHitRun = false;
            FishingRun = false;
            CamZoomRun = false;
        }


        //�t���O������������s------------------------------------------------

        if (FishTurnRun)
        {
            GameObject CorsorObj = GameObject.Find("Corsor");
            CorsorTrs = CorsorObj.GetComponent<Transform>();
            // ���������������v�Z
            Vector3 dir = CorsorTrs.position - transform.position;
            // �����Ō������������ɉ�]�����Ă܂�
            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * FishData.FishTurnSmoothness);
        }// 1.�����J�[�\�������ɉ�]

        if (FishApproacheRun)
        {
            transform.position = Vector3.Lerp(transform.position, CorsorTrs.position, FishData.FishShadowSpeed * Time.deltaTime);
        }// 2.���J�[�\���ɋ߂Â�

        if (FishHitRun && CorsorCollided)
        {
            if (!CamZoomRun)//��x�����J�����𑀍�
            {
                CamScript.CamReset();
                CamScript.CamMove(5, transform.position - GameObject.Find("Reizi").GetComponent<Transform>().position);//(�J�����̃X�s�[�h, +�ړ�������W)
                CamScript.CamZoom(1.2f, 5);// �J�����Y�[��(�Y�[���{��, �Y�[���X�s�[�h)
                //CamScript.CamShake(0.05f);// �J�����U��(�U���̑傫��)
                CamZoomRun = true;
            }
            // ���݂̎��ԂɊ�Â��Ċp�x���v�Z�i�I�V���[�V�����j(FishTurnSmoothness��20�{�̃X�s�[�h�ŉ�])
            float rotation = InitialRotation + FishData.FishTurnAngle * Mathf.Sin(Time.time * FishData.FishTurnSmoothness * 20);
            transform.eulerAngles = new Vector3(0, 0, rotation);// �V�����p�x��ݒ�

            // ���[�^�[����
            if (Input.GetKeyDown(KeyCode.Return))
            {
                FishingMeterMaskTrs.localScale += new Vector3(0.1f, 0, 0);
            }
            if (FishingMeterMaskTrs.localScale.x >= 0) FishingMeterMaskTrs.localScale -= new Vector3(0.001f, 0, 0);
        }// 4.����HIT
    }


    IEnumerator Fishing() // �e�t���O�����Ԋu�ŗ��Ă܂��B�J�[�\���ɓ��������u�ԂɈ�x�������s���܂��B
    {
        // 1.�����J�[�\�������ɉ�]
        {
            FishTurnRun = true;
            yield return new WaitForSeconds(Random.Range(FishData.FishBiteTime, FishData.FishBiteTime * 1.5f));
        }

        // 2.���J�[�\���ɋ߂Â�
        {
            FishApproacheRun = true;
            yield return new WaitForSeconds(Random.Range(FishData.FishBiteTime / 2, FishData.FishBiteTime / 1.5f));
        }

        // 4.����HIT
        {
            InitialRotation = transform.eulerAngles.z;// ���̊p�x�����Ă���
            // ���[�^�[�𐶐�
            Instantiate(FishingMeterPrefab, transform.position, Quaternion.identity);// HIT���̃��[�^�[�𐶐�����
            FishingMeterObj = GameObject.Find("FishingMeter(Clone)");
            FishingMeterMaskTrs = FishingMeterObj.transform.Find("FishingMeterMask").gameObject.transform;// FishingMeterMaskTrs�Ƀg�����X�t�H�[��������
            FishingMeterMaskTrs.localScale = new Vector3(2, 1, 1);
            // PlayerScript.FishingHitNow = true;
            FishHitRun = true;
            // yield return new WaitForSeconds();
        }

        //6.����������(�������F���[�^�[�����[�܂ōs������ && OK�͈͂̊O�Ȃ�)
        {
            //PlayerScript.FishingHitNow = false;
            //FishiHitTimeEnd
        }
    }

}
