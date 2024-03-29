using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugManager;
using UnityEngine.Experimental.GlobalIllumination;
public class FishingManager : MonoBehaviour
{
    // �l
    [SerializeField, Header("�J�[�\���̃X�s�[�h")] float Corsor_Speed;
    Vector3 CorsorPosition;
    float currentScale;
    float OriginalDistance;
    FishData FishData;
    float FishingTime_Hit;
    float FishingTime_Throw;
    float FishingTime_ToHitEnd;
    float FishingTime_SinceHit;
    // �t���O
    bool FishingPlace_Collided;
    bool UpMove;
    bool RightMove;
    bool FloatLandingWater_Run;
    bool StartFishingReturn;
    bool MeterOperation;
    bool HitSuccess;
    bool HitFailure;
    bool FishImage_Move;
    // �Q�[���I�u�W�F�N�g
    GameObject Corsor_Obj;
    GameObject FishingFloat_Obj;
    [SerializeField, Header("�ނ莞�̃��[�^�[")]
    GameObject FishingMeter_Prefab;
    GameObject FishingMeter_Obj;
    Transform FishingMeter_MaskTrs;
    GameObject FishImage_Obj;
    [SerializeField, Header("�ނꂽ���ɕ\�����鋛�̉摜�B���prefab������")]
    GameObject FishImage_Prefab;
    // �R���|�[�l���g
    Animator PlayerAnime;
    CharaOperation charaOperation;
    Cam CamScript;
    FishingPlace FishingPlaceScript;
    Animator FloatAnime;
    // Audio
    [SerializeField, Header("SE")] AudioClip FloatLandingWater;
    [SerializeField] AudioClip FloatThrow;
    enum Phase
    {
        StartFishing,
        StartFloat,
        Hit,
        Result,
        End,
    }
    Phase phase = Phase.End;

    void Start()
    {
        // �Q�[���I�u�W�F�N�g
        Corsor_Obj = GameObject.Find("Corsor");
        Corsor_Obj.SetActive(false);
        FishingFloat_Obj = GameObject.Find("FishingFloat");
        FishingFloat_Obj.SetActive(false);
        // �R���|�[�l���g
        charaOperation = GetComponent<CharaOperation>();
        PlayerAnime = GetComponent<Animator>();
        CamScript = GameObject.Find("Main Camera").GetComponent<Cam>();
        FloatAnime = FishingFloat_Obj.GetComponent<Animator>();
    }
    void Update()
    {
        // �ނ���J�n
        if (FishingPlace_Collided && Input.GetKeyDown(KeyCode.Space) && phase == Phase.End)
        {
            StartCoroutine(Fishing());
        }
        // �J�[�\���̑���
        if (phase == Phase.StartFishing)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) UpMove = true;
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) UpMove = false;
            if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)) UpMove = true;
            if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) UpMove = false;
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) RightMove = true;
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) RightMove = false;
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) RightMove = true;
            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) RightMove = false;
            if (FishingPlaceScript.direction == FishingPlace.Direction.Up)
            {
                if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && UpMove && Corsor_Obj.transform.position.y - transform.position.y <= 8) Corsor_Obj.transform.position += new Vector3(0, Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !UpMove && Corsor_Obj.transform.position.y >= transform.position.y) Corsor_Obj.transform.position += new Vector3(0, -Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && RightMove && Corsor_Obj.transform.position.x - transform.position.x <= 9) Corsor_Obj.transform.position += new Vector3(Corsor_Speed, 0, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !RightMove && Corsor_Obj.transform.position.x - transform.position.x >= -9) Corsor_Obj.transform.position += new Vector3(-Corsor_Speed, 0, 0) * Time.deltaTime;
            }
            else if (FishingPlaceScript.direction == FishingPlace.Direction.Down)
            {
                if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && UpMove && Corsor_Obj.transform.position.y <= transform.position.y) Corsor_Obj.transform.position += new Vector3(0, Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !UpMove && Corsor_Obj.transform.position.y - transform.position.y >= -8) Corsor_Obj.transform.position += new Vector3(0, -Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && RightMove && Corsor_Obj.transform.position.x - transform.position.x <= 9) Corsor_Obj.transform.position += new Vector3(Corsor_Speed, 0, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !RightMove && Corsor_Obj.transform.position.x - transform.position.x >= -9) Corsor_Obj.transform.position += new Vector3(-Corsor_Speed, 0, 0) * Time.deltaTime;
            }
            else if (FishingPlaceScript.direction == FishingPlace.Direction.Right)
            {
                if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && UpMove && Corsor_Obj.transform.position.y - transform.position.y <= 4) Corsor_Obj.transform.position += new Vector3(0, Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !UpMove && Corsor_Obj.transform.position.y - transform.position.y >= -4) Corsor_Obj.transform.position += new Vector3(0, -Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && RightMove && Corsor_Obj.transform.position.x - transform.position.x <= 10) Corsor_Obj.transform.position += new Vector3(Corsor_Speed, 0, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !RightMove && Corsor_Obj.transform.position.x >= transform.position.x) Corsor_Obj.transform.position += new Vector3(-Corsor_Speed, 0, 0) * Time.deltaTime;
            }
            else if (FishingPlaceScript.direction == FishingPlace.Direction.Left)
            {
                if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && UpMove && Corsor_Obj.transform.position.y - transform.position.y <= 4) Corsor_Obj.transform.position += new Vector3(0, Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !UpMove && Corsor_Obj.transform.position.y - transform.position.y >= -4) Corsor_Obj.transform.position += new Vector3(0, -Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && RightMove && Corsor_Obj.transform.position.x <= transform.position.x) Corsor_Obj.transform.position += new Vector3(Corsor_Speed, 0, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !RightMove && Corsor_Obj.transform.position.x - transform.position.x >= -10) Corsor_Obj.transform.position += new Vector3(-Corsor_Speed, 0, 0) * Time.deltaTime;
            }
            else if (FishingPlaceScript.direction == FishingPlace.Direction.UpRight)
            {
                if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && UpMove && Corsor_Obj.transform.position.y - transform.position.y <= 8) Corsor_Obj.transform.position += new Vector3(0, Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !UpMove && Corsor_Obj.transform.position.y >= transform.position.y) Corsor_Obj.transform.position += new Vector3(0, -Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && RightMove && Corsor_Obj.transform.position.x - transform.position.x <= 10) Corsor_Obj.transform.position += new Vector3(Corsor_Speed, 0, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !RightMove && Corsor_Obj.transform.position.x >= transform.position.x) Corsor_Obj.transform.position += new Vector3(-Corsor_Speed, 0, 0) * Time.deltaTime;
            }
            else if (FishingPlaceScript.direction == FishingPlace.Direction.UpLeft)
            {
                if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && UpMove && Corsor_Obj.transform.position.y - transform.position.y <= 8) Corsor_Obj.transform.position += new Vector3(0, Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !UpMove && Corsor_Obj.transform.position.y >= transform.position.y) Corsor_Obj.transform.position += new Vector3(0, -Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && RightMove && Corsor_Obj.transform.position.x <= transform.position.x) Corsor_Obj.transform.position += new Vector3(Corsor_Speed, 0, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !RightMove && Corsor_Obj.transform.position.x - transform.position.x >= -10) Corsor_Obj.transform.position += new Vector3(-Corsor_Speed, 0, 0) * Time.deltaTime;
            }
            else if (FishingPlaceScript.direction == FishingPlace.Direction.DownRight)
            {
                if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && UpMove && Corsor_Obj.transform.position.y <= transform.position.y) Corsor_Obj.transform.position += new Vector3(0, Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !UpMove && Corsor_Obj.transform.position.y - transform.position.y >= -8) Corsor_Obj.transform.position += new Vector3(0, -Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && RightMove && Corsor_Obj.transform.position.x - transform.position.x <= 10) Corsor_Obj.transform.position += new Vector3(Corsor_Speed, 0, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !RightMove && Corsor_Obj.transform.position.x >= transform.position.x) Corsor_Obj.transform.position += new Vector3(-Corsor_Speed, 0, 0) * Time.deltaTime;
            }
            else if (FishingPlaceScript.direction == FishingPlace.Direction.DownLeft)
            {
                if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && UpMove && Corsor_Obj.transform.position.y <= transform.position.y) Corsor_Obj.transform.position += new Vector3(0, Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !UpMove && Corsor_Obj.transform.position.y - transform.position.y >= -8) Corsor_Obj.transform.position += new Vector3(0, -Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && RightMove && Corsor_Obj.transform.position.x <= transform.position.x) Corsor_Obj.transform.position += new Vector3(Corsor_Speed, 0, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !RightMove && Corsor_Obj.transform.position.x - transform.position.x >= -10) Corsor_Obj.transform.position += new Vector3(-Corsor_Speed, 0, 0) * Time.deltaTime;
            }

        }
        // �E�L������(�A�j���[�V����)
        if (phase == Phase.StartFloat)
        {
            FishingFloat_Obj.transform.position = Vector2.Lerp(FishingFloat_Obj.transform.position, CorsorPosition, 2 * Time.deltaTime);
            // �ڕW�ʒu�܂ł̋������v�Z
            float distanceToTarget = Vector3.Distance(FishingFloat_Obj.transform.position, CorsorPosition);
            // �X�P�[����ω������違�A�j���[�V�������J�n����
            if (distanceToTarget >= OriginalDistance / 2)
            {
                currentScale += Time.deltaTime * 2;
                currentScale = Mathf.Clamp(currentScale, 1, 3);
            }
            else
            {
                // ���B�����O�ł̓X�P�[��������������
                currentScale -= Time.deltaTime * 2;
                currentScale = Mathf.Clamp(currentScale, 1, 3);
                if (!FloatLandingWater_Run && currentScale <= 1.1 && currentScale >= 1.01)
                {
                    FloatLandingWater_Run = true;
                    FloatAnime.SetBool("LandingWater", true);
                    GetComponent<AudioSource>().PlayOneShot(FloatLandingWater);
                }
            }
            // �I�u�W�F�N�g�̃X�P�[�����X�V
            FishingFloat_Obj.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
        }
        // HIT�����[�^�[����
        if (MeterOperation)
        {
            // ���[�^�[����
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                CamScript.CamOneShake(0.05f, 0.1f, 0.2f);
                FishingMeter_MaskTrs.localScale += new Vector3(0.3f, 0, 0);
                // SE(FishingMeterOpration)
                GetComponents<AudioSource>()[1].Play();
            }
            // ���̒�R
            if (FishingMeter_MaskTrs.localScale.x >= 0)
            { 
                FishingMeter_MaskTrs.localScale -= new Vector3(1, 0, 0) * Time.deltaTime; 
            }
            //HIT�I��(����)
            FishingTime_SinceHit += Time.deltaTime;
            if (FishingTime_SinceHit >= FishingTime_ToHitEnd)
            {
                //FishData��OKLevel�͈͓��������琬��
                if (FishingMeter_MaskTrs.localScale.x >= FishData.FishingMeterOKLevelMin && FishingMeter_MaskTrs.localScale.x <= FishData.FishingMeterOKLevelMax)
                {
                    MeterOperation = false;
                    HitSuccess = true;
                }
                //�͈͊O�������玸�s
                else
                {
                    MeterOperation = false;
                    HitFailure = true;
                }
            }
            // HIT�I��(���[�^�[�[)
            // �E�[�������琬��
            else if (FishingMeter_MaskTrs.localScale.x >= 5.5f)
            {
                MeterOperation = false;
                HitSuccess = true;
            }
            // ���[�������玸�s
            else if (FishingMeter_MaskTrs.localScale.x <= 0.2f)
            {
                MeterOperation = false;
                HitFailure = true;
            }
        }
        else
        {
            FishingTime_SinceHit = 0;
            FishingTime_ToHitEnd = 0;
        }
        // �ނ�グ�����𓮂���
        if (FishImage_Move)
        {
            FishImage_Obj.transform.position = Vector2.Lerp(FishImage_Obj.transform.position, Corsor_Obj.transform.position + new Vector3(0, 15, 0), 3 * Time.deltaTime);
        }
    }


    IEnumerator Fishing()
    {
        phase = Phase.StartFishing;
        while (phase != Phase.End)
        {
            yield return null;
            switch (phase)
            {
                case Phase.StartFishing:
                    Debug.Log("�ނ���J�n");
                    //�v���C���[�L�����̐L�яk�݂���ߑ�����󂯕t���Ȃ�����
                    gameObject.GetComponent<Strech>().StrechCan = false;
                    charaOperation.CanRun = false;
                    // �A�j���[�V�������C��
                    PlayerAnime.SetBool("FishingFloatEnd", false);
                    //�J�[�\�����o��
                    Corsor_Obj.SetActive(true);
                    //�J�����ړ�������������(���ڂ̂ݓ�����)
                    if (!StartFishingReturn)
                    {
                        if (FishingPlaceScript.direction == FishingPlace.Direction.Up)
                        {
                            CamScript.CamMove(6, new Vector3(0, 3, 0));
                            Corsor_Obj.transform.position += new Vector3(0, 2, 0);
                            PlayerAnime.SetBool("BackLook", true);
                            PlayerAnime.SetBool("RunBack", false);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.Down)
                        {
                            CamScript.CamMove(6, new Vector3(0, -3, 0));
                            Corsor_Obj.transform.position += new Vector3(0, -2, 0);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.Right)
                        {
                            CamScript.CamMove(6, new Vector3(3, 0, 0));
                            Corsor_Obj.transform.position += new Vector3(2, 0, 0);
                            if (!charaOperation.RightLook)
                            {
                                charaOperation.RightLook = true;
                            }
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.Left)
                        {
                            CamScript.CamMove(6, new Vector3(-3, 0, 0));
                            Corsor_Obj.transform.position += new Vector3(-2, 0, 0);
                            if (charaOperation.RightLook)
                            {
                                charaOperation.RightLook = false;
                            }
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.UpRight)
                        {
                            CamScript.CamMove(6, new Vector3(3, 3, 0));
                            Corsor_Obj.transform.position += new Vector3(2, 2, 0);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.UpLeft)
                        {
                            CamScript.CamMove(6, new Vector3(-3, 3, 0));
                            Corsor_Obj.transform.position += new Vector3(-2, 2, 0);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.DownRight)
                        {
                            CamScript.CamMove(6, new Vector3(3, -3, 0));
                            Corsor_Obj.transform.position += new Vector3(2, -2, 0);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.DownLeft)
                        {
                            CamScript.CamMove(6, new Vector3(-3, -3, 0));
                            Corsor_Obj.transform.position += new Vector3(-2, -2, 0);
                        }
                        else { Debug.Log("�G���[�FFishingPlace�̕�������͂���"); }
                    }
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X));
                    // ���^�[��orZ�ŃE�L�𕂂��ׂ�
                    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
                    {
                        phase = Phase.StartFloat;
                    }
                    // �X�y�[�XorX�Œނ���I��
                    else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X))
                    {
                        FishingEnd();
                        phase = Phase.End;
                    }
                    break;
                case Phase.StartFloat:
                    // �J�[�\���������ăE�L���o��
                    Corsor_Obj.SetActive(false);
                    FishingFloat_Obj.SetActive(true);
                    // �A�j���[�V����
                    {
                        PlayerAnime.SetBool("Fishing", true);
                        PlayerAnime.SetBool("FishingFloatEnd", false);
                        if (FishingPlaceScript.direction == FishingPlace.Direction.Up)
                        {
                            PlayerAnime.SetBool("ThrowFloatBack", true);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.Down)
                        {
                            PlayerAnime.SetBool("ThrowFloatFlont", true);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.Right)
                        {
                            PlayerAnime.SetBool("ThrowFloatSide", true);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.Left)
                        {
                            PlayerAnime.SetBool("ThrowFloatSide", true);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.UpRight)
                        {

                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.UpLeft)
                        {

                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.DownRight)
                        {

                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.DownLeft)
                        {

                        }
                    }
                    // �E�L�𓮂�������
                    FishingFloat_Obj.transform.position = transform.position;
                    FishingFloat_Obj.transform.localScale = new Vector3(1, 1, 1);
                    CorsorPosition = Corsor_Obj.transform.position;
                    OriginalDistance = Vector3.Distance(FishingFloat_Obj.transform.position, CorsorPosition);
                    //SE
                    GetComponent<AudioSource>().PlayOneShot(FloatThrow);
                    // �Ȃ�̋����ނ��H
                    FishData = ChooseFishBasedOnRarity(FishingPlaceScript.FishBornList);
                    // ���b���HIT�H
                    FishingTime_Hit = Random.Range(3.0f, 9.9f);//<------------�����͏����ς���
                    FishingTime_Throw = Time.time;
                    Debug.Log("�E�L���J�n�F" + FishingTime_Hit + "�b���HIT");
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X) || FishingTime_Hit + FishingTime_Throw <= Time.time);
                    // ���^�[��orZ�ŃJ�[�\���ɖ߂�
                    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
                    {
                        FishingFloatEnd();
                        phase = Phase.StartFishing;
                    }
                    // �X�y�[�XorX�Œނ���I��
                    else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X))
                    {
                        FishingEnd();
                        phase = Phase.End;
                    }
                    // ���ԂɂȂ�����HIT
                    else
                    {
                        phase = Phase.Hit;
                    }
                    break;
                case Phase.Hit:
                    // �J��������
                    CamScript.CamReset();
                    CamScript.CamMove(5, (FishingFloat_Obj.transform.position - transform.position) / 2);//(�J�����̃X�s�[�h, +�ړ�������W)
                    CamScript.CamZoom(1.1f, 3);
                    // �}�[�N
                    GameObject.Find("EventManager").GetComponent<Reaction>().Suprise(transform.position + new Vector3(1, 1.5f, 0), 0.75f);
                    // 0.75�b�҂�
                    yield return new WaitForSeconds(0.75f);
                    // �J����
                    CamScript.CamZoom(1.2f, 5);// �J�����Y�[��(�Y�[���{��, �Y�[���X�s�[�h)
                    CamScript.CamShake(0.005f, 0.1f);// �J�����U��(�U���̑傫��)
                    // SE(FishBuzzing)
                    GetComponent<AudioSource>().Play();
                    // �ނ�̒���
                    FishingTime_ToHitEnd = Random.Range(3.0f, 5.0f);
                    Debug.Log("HIT�F" + FishingTime_ToHitEnd + "�b���HIT�I��");
                    // ���[�^�[�𐶐�
                    FishingMeter_Obj = Instantiate(FishingMeter_Prefab, FishingFloat_Obj.gameObject.transform.position, Quaternion.identity);// HIT���̃��[�^�[�𐶐�����
                    FishingMeter_MaskTrs = FishingMeter_Obj.transform.Find("FishingMeterMask").gameObject.transform;// FishingMeterMaskTrs�Ƀg�����X�t�H�[��������
                    FishingMeter_MaskTrs.localScale = new Vector3(2, 1, 1);
                    FishingMeter_Obj.transform.Find("FishingMeterOKLineLower").gameObject.transform.localScale = new Vector3(FishData.FishingMeterOKLevelMin, 1, 1);
                    FishingMeter_Obj.transform.Find("FishingMeterOKLineUpper").gameObject.transform.localScale = new Vector3(FishData.FishingMeterOKLevelMax, 1, 1);
                    MeterOperation = true;
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X) || HitSuccess || HitFailure);
                    MeterOperation = false;
                    // SE���~
                    GetComponent<AudioSource>().Stop();
                    GetComponents<AudioSource>()[1].Stop();
                    // �X�y�[�XorX�Ȃ�ނ���I��
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X))
                    {
                        PlayerAnime.SetBool("FishingEnd", true);
                        FishingEnd();
                        phase = Phase.End;
                    }
                    // �����������͎��s���Ă�����Result��
                    else if (HitSuccess || HitFailure)
                    {
                        phase = Phase.Result;
                    }
                    break;
                case Phase.Result:
                    // �A�j���[�V����
                    PlayerAnime.SetBool("FishingFloatEnd", true);
                    PlayerAnime.SetBool("ThrowFloatBack", false);
                    PlayerAnime.SetBool("ThrowFloatFlont", false);
                    PlayerAnime.SetBool("ThrowFloatSide", false);
                    // ���[�^�[������
                    Destroy(FishingMeter_Obj);
                    //�J�����𓮂���&�J�[�\�������̈ʒu�ɖ߂�
                    CamScript.CamReset();
                    {
                        if (FishingPlaceScript.direction == FishingPlace.Direction.Up)
                        {
                            CamScript.CamMove(6, new Vector3(0, 3, 0));
                            Corsor_Obj.transform.position += new Vector3(0, 2, 0);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.Down)
                        {
                            CamScript.CamMove(6, new Vector3(0, -3, 0));
                            Corsor_Obj.transform.position += new Vector3(0, -2, 0);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.Right)
                        {
                            CamScript.CamMove(6, new Vector3(3, 0, 0));
                            Corsor_Obj.transform.position += new Vector3(2, 0, 0);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.Left)
                        {
                            CamScript.CamMove(6, new Vector3(-3, 0, 0));
                            Corsor_Obj.transform.position += new Vector3(-2, 0, 0);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.UpRight)
                        {
                            CamScript.CamMove(6, new Vector3(3, 3, 0));
                            Corsor_Obj.transform.position += new Vector3(2, 2, 0);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.UpLeft)
                        {
                            CamScript.CamMove(6, new Vector3(-3, 3, 0));
                            Corsor_Obj.transform.position += new Vector3(-2, 2, 0);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.DownRight)
                        {
                            CamScript.CamMove(6, new Vector3(3, -3, 0));
                            Corsor_Obj.transform.position += new Vector3(2, -2, 0);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.DownLeft)
                        {
                            CamScript.CamMove(6, new Vector3(-3, -3, 0));
                            Corsor_Obj.transform.position += new Vector3(-2, -2, 0);
                        }
                        else { Debug.Log("�G���[�FFishingPlace�̕�������͂���"); }
                    }
                    // ����
                    if (HitSuccess)
                    {
                        // �A�C�e�����|�P�b�g�f�[�^�x�[�X�ɒǉ�
                        GameObject FishDataBaseManagerObj = GameObject.Find("DataBaseManager");
                        FishDataBeseManager FishDataBaseManagerScript = FishDataBaseManagerObj.GetComponent<FishDataBeseManager>();
                        FishDataBaseManagerScript.AddFishData(FishData);
                        // ����ނ�グ��
                        FishImage_Obj = Instantiate(FishImage_Prefab, FishingFloat_Obj.gameObject.transform.position, Quaternion.identity);
                        FishImage_Obj.GetComponent<SpriteRenderer>().sprite = FishData.FishImage;
                        FishImage_Move = true;
                        // �����V��Ȃ�E�B���h�E��\��
                        if (FishData.NewFish)
                        {
                            // ���̉摜����������
                            FishImage_Obj.GetComponent<SpriteRenderer>().color = Color.black;
                            yield return new WaitForSeconds(0.75f);
                            // �E�B���h�E�𐶐�����
                            WindowController window = GameObject.Find("WindowContoller").GetComponent<WindowController>();
                            window.NewFishWindow_Creat(FishData);
                            // �L�[�����������S�Ɉړ�����̂�҂�
                            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z) || window.ShakeRun);
                            // ���������ꂩ�̃L�[����������E�B���h�E�����S�Ɉړ�����
                            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
                            {
                                window.NewFishWindow_Click();
                                yield return new WaitForSeconds(0.1f);
                            }
                            // �L�[���������̂�҂�
                            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z));
                            window.NewFishWindow_Destroy();
                            FishImage_Move = false;
                        }
                        // �V��łȂ��Ȃ�A�C�e���l���|�b�v�A�b�v���o��
                        else
                        {
                            yield return new WaitForSeconds(0.75f);
                            // �|�b�v�A�b�v��\�����|�b�v�A�b�v�R���g���[���Ɏw��
                            GameObject.Find("PopupController").GetComponent<PopupController>().SubmitPopup(FishData.FishName, FishData.FishImage);
                            FishingFloatEnd();
                        }
                    }
                    // �t���O��߂�
                    HitSuccess = false;
                    HitFailure = false;
                    // �J�[�\���֖߂�
                    FishingFloatEnd();
                    phase = Phase.StartFishing;
                    break;
                case Phase.End:
                    Debug.Log("Phase�����������ɂ�");
                    break;
            }
        }
    }

    void FishingEnd()
    {
        // �v���C���[�L�������L�яk�݂��ĊJ�������t
        gameObject.GetComponent<Strech>().StrechCan = true;
        charaOperation.CanRun = true;
        //�J�[�\����߂�
        Corsor_Obj.SetActive(false);
        Corsor_Obj.transform.position = transform.position;
        // �E�L��߂�
        FishingFloat_Obj.SetActive(false);
        // �J�������ړ�
        CamScript.CamReset();
        // ���t���O��߂�
        StartFishingReturn = false;
        FloatLandingWater_Run = false;
        // �A�j���[�V����
        PlayerAnime.SetBool("Fishing", false);
        PlayerAnime.SetBool("FishingFloatEnd", false);
        PlayerAnime.SetBool("ThrowFloatBack", false);
        PlayerAnime.SetBool("ThrowFloatFlont", false);
        PlayerAnime.SetBool("ThrowFloatSide", false);
        if (FishingMeter_Obj != null)
        {
            Destroy(FishingMeter_Obj);
        }
        Debug.Log("�I��");
    }
    void FishingFloatEnd()
    {
        // �Q��ڂ̃t���O�𗧂Ă�
        StartFishingReturn = true;
        // �J�[�\�������̈ʒu�ɖ߂�
        Corsor_Obj.transform.position = CorsorPosition;
        Corsor_Obj.SetActive(true);
        // �E�L���\���ɂ���
        FishingFloat_Obj.SetActive(false);
        // �t���O��߂�
        FloatLandingWater_Run = false;
        // �A�j���[�V����
        PlayerAnime.SetBool("FishingFloatEnd", true);
        PlayerAnime.SetBool("ThrowFloatBack", false);
        PlayerAnime.SetBool("ThrowFloatFlont", false);
        PlayerAnime.SetBool("ThrowFloatSide", false);
    }
    public FishData ChooseFishBasedOnRarity(List<FishData> FishList)// ���̃��A���e�B�ɉ����ă����_���ɒ��I����
    {
        float total = 0;
        foreach (var fish in FishList)
        {
            if (fish.Rarity < 1 || fish.Rarity > 5) Debug.Log(fish + "�̃��A���e�B���K��l���O��Ă��܂�");
            total += 6.0f - fish.Rarity;
        }
        float randomPoint = Random.value * total;

        for (int i = 0; i < FishList.Count; i++)
        {
            if (randomPoint < 6.0f - FishList[i].Rarity)
                return FishList[i];
            else
                randomPoint -= 6.0f - FishList[i].Rarity;
        }

        return null; // �����ɓ��B���邱�Ƃ͂���܂���
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FishingPlace"))
        {
            FishingPlaceScript = collision.gameObject.GetComponent<FishingPlace>();
            FishingPlace_Collided = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FishingPlace"))
        {
            FishingPlace_Collided = false;
        }
    }
}