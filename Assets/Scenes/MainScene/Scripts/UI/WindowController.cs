using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowController : MonoBehaviour
{
    [SerializeField, Header("�V��̋�")]
    GameObject NewFishWindow_Prefab;
    GameObject NewFishWindow_Obj;
    RectTransform NewFishWindow_RectTransform;
    bool NewFishWindow_Born;
    [SerializeField, Header("�ނꂽ���ɕ\�����鋛�̉摜�B���prefab������")]
    GameObject FishImage_Prefab;
    GameObject FishImage;
    RectTransform FishImage_RectTransform;
    [HideInInspector] public bool ShakeRun;
    void Update()
    {
        if (NewFishWindow_Born)
        {
            // �E�B���h�E���ړ�
            NewFishWindow_RectTransform.anchoredPosition = Vector2.Lerp(NewFishWindow_RectTransform.anchoredPosition, new Vector2(0, 30), 2.5f * Time.deltaTime);
            // ���̉摜���ړ�
            FishImage_RectTransform.anchoredPosition = Vector2.Lerp(FishImage_RectTransform.anchoredPosition, new Vector2(0, 30), 2.5f * Time.deltaTime);
            // ��x�����J������U��
            if (NewFishWindow_RectTransform.anchoredPosition.y >= FishImage_RectTransform.anchoredPosition.y - 3 && !ShakeRun)
            {
                ShakeRun = true;
                GameObject.Find("Main Camera").GetComponent<Cam>().CamOneShake(0.1f, 0.1f, 0.1f);
            }
        }
    }

    public void NewFishWindow_Creat(FishData FishData)
    {
        // �E�B���h�E�̃v���n�u�𐶐�
        NewFishWindow_Obj = Instantiate(NewFishWindow_Prefab, new Vector3(0, 300, 0), Quaternion.identity, transform);
        // �E�B���h�E�̈ʒu���ړ�
        NewFishWindow_RectTransform = NewFishWindow_Obj.GetComponent<RectTransform>();
        NewFishWindow_RectTransform.anchoredPosition = new Vector3(0, 300, 0);
        // ���̉摜�𐶐�
        FishImage = Instantiate(FishImage_Prefab, new Vector3(0, 300, 0), Quaternion.identity, transform);
        FishImage.GetComponent<Image>().sprite = FishData.FishImage;
        // ���̉摜���ړ�
        FishImage_RectTransform = FishImage.GetComponent<RectTransform>();
        FishImage_RectTransform.anchoredPosition = new Vector3(0, 600, 0);
        // �t���O�𗧂Ă�
        NewFishWindow_Born = true;
        // �V��̋�����Ȃ�����
        FishData.NewFish = false;
        // �E�B���h�E�̃Q�[���I�u�W�F�N�g��Ԃ�
    }
    public void NewFishWindow_Click()
    {
        // �E�B���h�E���ړ�
        NewFishWindow_RectTransform.anchoredPosition = new Vector2(0, 30);
        // ���̉摜���ړ�
        FishImage_RectTransform.anchoredPosition = new Vector2(0, 30);
    }
    public void NewFishWindow_Destroy()
    {
        // �t���O��߂�
        ShakeRun = false;
        NewFishWindow_Born = false;
        // �E�B���h�E������
        Destroy(NewFishWindow_Obj);
        Destroy(FishImage);
    }
}
