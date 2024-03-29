using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDataBeseManager : MonoBehaviour
{
    //���̃f�[�^�x�[�X-----------------------------------------------------------------------------
    
    [SerializeField]
    List<FishData> FishDataBase = new List<FishData>();

    public FishData FishRundom()//�S�̂̋��̒����烉���_���Œ��I����
    {
        return FishDataBase[Random.Range(0, FishDataBase.Count)];
    }



    //�����Ă��鋛�̃f�[�^�x�[�X-------------------------------------------------------------------

    [SerializeField,Tooltip("�����Ă��鋛�̃f�[�^�x�[�X������")]
    PocketFishDataBase PocketFishDataBase;

    void Start() //�Q�[���J�n���Ƀ��X�g�̒����J���ɂ���
    { 
        for (int i = 0; i <  PocketFishDataBase.PocketFishDataBaseList.Count; i++)
        {
            PocketFishDataBase.PocketFishDataBaseList[i].NewFish = true;
        }
        PocketFishDataBase.PocketFishDataBaseList.Clear();
    }

    public void AddFishData(FishData fishData)//����ǉ�����
    {
        PocketFishDataBase.PocketFishDataBaseList.Add(fishData);
    }

    //�����Ă��鋛��Debug.Log����----------

    //void Update()
    //{
    //int count = PocketFishDataBase.PocketFishDataBaseList.Count; // PocketFishDataBaseList�̗v�f��

    //for (int i = 0; i < count; i++)
    //{
    //    FishData FishData = PocketFishDataBase.PocketFishDataBaseList[i];
    //    Debug.Log(FishData.FishName + " " + FishData.ID);
    //}

    //}

    //-------------------------------------
}