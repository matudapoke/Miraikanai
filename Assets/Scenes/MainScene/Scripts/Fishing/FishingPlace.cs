using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPlace : MonoBehaviour
{
    [Header("�o�����鋛�̃��X�g")]
    public List<FishData> FishBornList = new List<FishData>();
    public enum Direction
    {
        Up,
        Down,
        Right,
        Left,
        UpRight,
        UpLeft,
        DownRight,
        DownLeft,
    }
    [Header("�ނ��̕���")]
    public Direction direction;


    //���e���� 

    //[Tooltip("��������͈�A"),SerializeField]
    //Transform RangeA;
    //[Tooltip("��������͈�B"),SerializeField]
    //Transform RangeB;
    //[Tooltip("�������鋛�̐�"),SerializeField]
    //int FishBornInt;


    //void Start()
    //{
    //������
    //  for (int i = 0; i <= FishBornInt; i++)
    //{
    //  FishData BornFishData = ChooseFishBasedOnRarity();//FishBornList���狛�𒊑I
    //GameObject BornFIshShadow = BornFishData.FishShadow;

    //float x = Random.Range(RangeA.position.x, RangeB.position.x);//rangeA��rangeB��x���W�͈͓̔��Ń����_���Ȑ��l���쐬
    //float y = Random.Range(RangeA.position.y, RangeB.position.y);//rangeA��rangeB��y���W�͈͓̔��Ń����_���Ȑ��l���쐬
    //Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
    //Instantiate(BornFIshShadow, new Vector2(x, y), rotation);//������

    //FishShadow BornFIshShadowScript = BornFIshShadow.GetComponent<FishShadow>();
    //BornFIshShadowScript.FishData = BornFishData;
    //}
    //}
}
