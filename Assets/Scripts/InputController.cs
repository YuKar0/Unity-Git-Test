using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.InputSystem.Interactions;

public class InputController : MonoBehaviour
{
    //���ֿ���
    public PlayableDirector musicController;
    //timeline��Դ
    public TimelineAsset timelineResource;
    //���������ַ���ʹ�ü�
    public Dictionary<string, List<IMarker>> dictionary = new Dictionary<string, List<IMarker>>();

    //������
    public static InputController instance;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
        //Ԥ�ȼ�¼timeline�е����н���(�¼�)��
        foreach(var trackResource in timelineResource.GetOutputTracks())
        {
            //print(trackResource.name);
            if (trackResource.name.Contains("Check"))
            {
                List<IMarker> pointList = new List<IMarker>();
                foreach(var point in trackResource.GetMarkers())
                {
                    pointList.Add(point);
                }
                //print(pointList.Count);
                dictionary.Add(trackResource.name, pointList);
            }
        }
        //print("-------");
        //foreach(var item in dictionary)
        //{
        //    print(item.Key);
        //}
    }


    /*
     * 
     * ���´������ڰ��°�����������������ɵ�ͼ��(��������)
     * 
     * 
     */
    //����ͼ���жϵ��ö������
    public enum musicPtType
    {
        ptQ,ptW,ptE,ptR
    }
    //����������ͼ���жϵ����
    Queue<GameObject> queQ = new Queue<GameObject>();
    Queue<GameObject> queW = new Queue<GameObject>();
    Queue<GameObject> queE = new Queue<GameObject>();
    Queue<GameObject> queR = new Queue<GameObject>();
    //����ж���
    public void addMusicPt(GameObject obj,musicPtType type)
    {
        switch (type)
        {
            case musicPtType.ptQ:
                queQ.Enqueue(obj);
                //print("Qadd");
                break;
            case musicPtType.ptW:
                queW.Enqueue(obj);
                //print("Wadd");
                break;
            case musicPtType.ptE:
                queE.Enqueue(obj);
                //print("Eadd");
                break;
            case musicPtType.ptR:
                queR.Enqueue(obj);
                //print("Radd");
                break;
            default:
                break;
        }
    }

    /*
     * 
     * �������ڸ��·���
     * 
     */
    public GameObject score_obj;
    int score = 0;

    public GameObject perfect_image;
    public GameObject good_image;
    public GameObject miss_image;
    private void Update()
    {
        score_obj.GetComponent<Text>().text = score.ToString();
    }

    /*
     * 
     * ���´������ڼ��������ж�
     * 
     * 
     */
    [Range(0, 1)]
    public float none = 0.5f;
    [Range(0, 1)]
    public float miss = 0.4f;
    [Range(0, 1)]
    public float good = 0.25f;
    [Range(0, 1)]
    public float perfect = 0.15f;

    //Q������
    public void inputQ(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            //��������ʱ����һ��
            case InputActionPhase.Performed:
                if(context.interaction is PressInteraction)
                {
                    string checkPoint = "CheckQ";
                    if(dictionary[checkPoint].Count > 0)
                    {
                        //��ȥͼ�ο�
                        if((float)dictionary[checkPoint][0].time - (float)musicController.time >= 0 
                            && (float)dictionary[checkPoint][0].time - (float)musicController.time <= 0.4)
                        {
                            while(queQ.Count > 0)
                            {
                                Destroy(queQ.Dequeue());
                            }
                        }

                        getScore(checkPoint, pressQ);
                    }
                }
                break;
        }
    }

    //W������
    public void inputW(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            //��������ʱ����һ��
            case InputActionPhase.Performed:
                if (context.interaction is PressInteraction)
                {
                    string checkPoint = "CheckW";
                    if (dictionary[checkPoint].Count > 0)
                    {
                        //��ȥͼ�ο�
                        if ((float)dictionary[checkPoint][0].time - (float)musicController.time >= 0
                            && (float)dictionary[checkPoint][0].time - (float)musicController.time <= 0.4)
                        {
                            while (queW.Count > 0)
                            {
                                Destroy(queW.Dequeue());
                            }
                        }

                        getScore(checkPoint, pressW);
                    }
                }
                break;
        }
    }

    //E������
    public void inputE(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            //��������ʱ����һ��
            case InputActionPhase.Performed:
                if (context.interaction is PressInteraction)
                {
                    string checkPoint = "CheckE";
                    if (dictionary[checkPoint].Count > 0)
                    {
                        //��ȥͼ�ο�
                        if ((float)dictionary[checkPoint][0].time - (float)musicController.time >= 0
                            && (float)dictionary[checkPoint][0].time - (float)musicController.time <= 0.4)
                        {
                            while (queE.Count > 0)
                            {
                                Destroy(queE.Dequeue());
                            }
                        }

                        getScore(checkPoint, pressE);
                    }
                }
                break;
        }
    }

    //R������
    public void inputR(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            //��������ʱ����һ��
            case InputActionPhase.Performed:
                if (context.interaction is PressInteraction)
                {
                    string checkPoint = "CheckR";
                    if (dictionary[checkPoint].Count > 0)
                    {
                        //��ȥͼ�ο�
                        if ((float)dictionary[checkPoint][0].time - (float)musicController.time >= 0
                            && (float)dictionary[checkPoint][0].time - (float)musicController.time <= 0.4)
                        {
                            while (queR.Count > 0)
                            {
                                Destroy(queR.Dequeue());
                            }
                        }

                        getScore(checkPoint, pressR);
                    }
                }
                break;
        }
    }

    
    //�÷ֵ��ж�
    void getScore(string checkPoint,IEnumerator pressKey)
    {
        float value = (float)dictionary[checkPoint][0].time - (float)musicController.time;
        if(value > none)
        {
            print("�հ�");
        }
        else if (value > miss)
        {
            print("miss");
            dictionary[checkPoint].RemoveAt(0);
            Instantiate(miss_image,new Vector3(0,1,0),Quaternion.identity);
        }
        else if(value > good)
        {
            print("good");
            dictionary[checkPoint].RemoveAt(0);
            score += 77;
            Instantiate(good_image, new Vector3(0, 1, 0), Quaternion.identity);
        }
        else if(value > perfect)
        {
            print("perfect");
            dictionary[checkPoint].RemoveAt(0);
            score += 99;
            Instantiate(perfect_image, new Vector3(0, 1, 0), Quaternion.identity);
        }
        else if(value > -perfect)
        {
            print("perfect");
            if (pressKey != null)
            {
                StopCoroutine(pressKey);
                pressKey = null;
            }
            dictionary[checkPoint].RemoveAt(0);
            score += 99;
            Instantiate(perfect_image, new Vector3(0, 1, 0), Quaternion.identity);
        }
        else if (value > -good)
        {
            print("good");
            if (pressKey != null)
            {
                StopCoroutine(pressKey);
                pressKey = null;
            }
            dictionary[checkPoint].RemoveAt(0);
            score += 77;
        }
        else if (value > -miss)
        {
            print("miss");
            if (pressKey != null)
            {
                StopCoroutine(pressKey);
                pressKey = null;
            }
            dictionary[checkPoint].RemoveAt(0);
        }
    }



    //Q����miss�ж�
    IEnumerator pressQ;
    public void checkQ(string name)
    {
        if (dictionary.ContainsKey(name))
        {
            if (dictionary[name].Count > 0)
            {
                if(dictionary[name][0].time - musicController.time <= 0)
                {
                    pressQ = Qmiss(name);
                    StartCoroutine(pressQ);
                }
            }
        }
    }
    IEnumerator Qmiss(string name)
    {
        yield return new WaitForSeconds(miss);
        print("miss");
        dictionary[name].RemoveAt(0);
        yield return null;
    }



    //W����miss�ж�
    IEnumerator pressW;
    public void checkW(string name)
    {
        if (dictionary.ContainsKey(name))
        {
            if(dictionary[name].Count > 0)
            {
                if(dictionary[name][0].time - musicController.time <= 0)
                {
                    pressW = Wmiss(name);
                    StartCoroutine(pressW);
                }
            }
        }
    }
    IEnumerator Wmiss(string name)
    {
        yield return new WaitForSeconds(miss);
        print("miss");
        dictionary[name].RemoveAt(0);
        yield return null;
    }

    //E����miss�ж�
    IEnumerator pressE;
    public void checkE(string name)
    {
        if (dictionary.ContainsKey(name))
        {
            if (dictionary[name].Count > 0)
            {
                if (dictionary[name][0].time - musicController.time <= 0)
                {
                    pressE = Emiss(name);
                    StartCoroutine(pressE);
                }
            }
        }
    }
    IEnumerator Emiss(string name)
    {
        yield return new WaitForSeconds(miss);
        print("miss");
        dictionary[name].RemoveAt(0);
        yield return null;
    }

    //R����miss�ж�
    IEnumerator pressR;
    public void checkR(string name)
    {
        if (dictionary.ContainsKey(name))
        {
            if (dictionary[name].Count > 0)
            {
                if (dictionary[name][0].time - musicController.time <= 0)
                {
                    pressR = Rmiss(name);
                    StartCoroutine(pressR);
                }
            }
        }
    }
    IEnumerator Rmiss(string name)
    {
        yield return new WaitForSeconds(miss);
        print("miss");
        dictionary[name].RemoveAt(0);
        yield return null;
    }
}



