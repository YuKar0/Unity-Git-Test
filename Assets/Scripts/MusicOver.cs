using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicOver : MonoBehaviour
{
    public GameObject mask;
    public GameObject button_over;

    public GameObject image_over_Score;//���շ���UI
    public GameObject score_source;//��ȡ���շ���
    string over_score;//�����ı�

    public void GameOver()
    {
        //print("GameOver");
        mask.SetActive(true);
        button_over.SetActive(true);

        over_score = "�÷�:" + score_source.GetComponent<Text>().text;
        image_over_Score.SetActive(true);
        image_over_Score.transform.GetChild(0).GetComponent<Text>().text = over_score;
    }
}
