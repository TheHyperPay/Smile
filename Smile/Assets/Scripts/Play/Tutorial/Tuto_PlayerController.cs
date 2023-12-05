using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Ʃ�丮�� ĳ���� ���� ��ũ��Ʈ

public class Tuto_PlayerController : MonoBehaviour
{
    //[SerializeField] private int moveSpeed;

    public IScenePass scenePass;
    public GameObject Cut_Scene_prefab;

    private Color del_color = new Color(0, 0, 0);
    private Color show_color = new Color(1, 1, 1);

    // ��ȸ ����Ʈ ����
    public GameObject[] go_notePoints; // ��ȸ ����Ʈ ������Ʈ

    // ��� ����Ʈ ����
    public GameObject[] go_lifePoints;

    // �÷��̾� �ִϸ��̼�
    Animator playerAinm;

    // ��� ��Ʈ UI
    [Header("������ ��Ʈ ���")] public GameObject Note_Bg;

    public GameObject noteController;

    // Start is called before the first frame update
    void Start()
    {
        scenePass = GetComponent<IScenePass>();
        scenePass.LoadSceneAsync("InGame-RN");
        playerAinm = GetComponent<Animator>();

        if (UniteData.lifePoint == 0)
        {
            Make_Invisible_UI();

            Animator fadeAnimator = GameObject.Find("FadeOut").GetComponent<Animator>();
            // ���̵� �ƿ� �ִϸ��̼� ���� ���� ���� ���� ��ȯ�մϴ�.
            fadeAnimator.SetBool("IsStartFade", true);
        }

        Initialized();
    }

    public void Initialized()
    {
        UniteData.Move_Progress = true;
        UniteData.tuto_meetMonster = false;

        playerAinm.SetBool("IsMoving", true);

        for (int i = 0; i < go_notePoints.Length; i++)
        {
            go_notePoints[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < go_lifePoints.Length; i++)
        {
            go_lifePoints[i].gameObject.SetActive(true);
        }

        //��� ���� �����Ͽ� ���÷���
        foreach (GameObject go in go_notePoints)
        {
            go.GetComponent<Image>().color = del_color;
        }
        for (int x = 0; x < UniteData.notePoint; x++)
        {
            go_notePoints[x].GetComponent<Image>().color = show_color;
        }

        foreach (GameObject go in go_lifePoints)
        {
            go.GetComponent<Image>().color = del_color;
        }
        for (int x = 0; x < UniteData.lifePoint; x++)
        {
            go_lifePoints[x].GetComponent<Image>().color = show_color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + (transform.right * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
#if true //���Ϳ� �����ص� ������ �ʵ��� ������ ��ũ��
        if (!UniteData.NoteSuccess && collision.CompareTag("Monster"))
        {
            if(UniteData.mon_num != 2)
            {
                //moveSpeed = 0;
                Debug.Log("UniteData.notePoint" + UniteData.notePoint);

                // ��� ��Ʈ UI ������
                Note_Bg.SetActive(false);

                // ��ȸ�� �����ִٸ� �� �̵�
                if (UniteData.notePoint > 0)
                {
                    UniteData.notePoint--;

                    // ���Ϳ� ������ �������� ����
                    UniteData.Move_Progress = false;

                    // �÷��̾� �ִϸ��̼� ����
                    playerAinm.SetBool("IsMoving", false);

                    //�� �ִϸ��̼��� �� �� �̵�
                    StartCoroutine(LoadCutScene());
                }

                // ��ȸ�� 0�̶�� ��� ����Ʈ ����
                else if (UniteData.notePoint == 0)
                {
                    Debug.Log("UniteData.lifePoint " + UniteData.lifePoint);
                    // ��� ����Ʈ�� �����ִٸ� ����
                    //if(UniteData.lifePoint >= 0)
                    MeetMonsterFail();
                }
            }

            // 2��° ���Ϳ��� Ʋ�� ��Ʈ ��ġ �� �ε��� ���
            else if (UniteData.mon_num == 2)
            {
                // �̵� ���߱�
                UniteData.Move_Progress = false;

                // ���̵� �ڽ�, �ؽ�Ʈ ǥ��
                UniteData.tuto_meetMonster = true;
                noteController.GetComponent<Tuto_NoteController>().GuideTextSet();
            }
        }
#endif
    }

    public void MeetMonsterFail()
    {
        UniteData.lifePoint--;
        go_lifePoints[UniteData.lifePoint].GetComponent<Image>().color = del_color;

        // 0�� �Ǹ� ���� ����
        if (UniteData.lifePoint == 0)
        {
            // ���� �����϶� ��� ������
            if(UniteData.Closed_Monster == "Boss")
            {
                UniteData.lifePoint = 3;
            }
            Make_Invisible_UI();

            //Animator fadeAnimator = GameObject.Find("FadeOut").GetComponent<Animator>();
            // ���̵� �ƿ� �ִϸ��̼� ���� ���� ���� ���� ��ȯ�մϴ�.
            //fadeAnimator.SetBool("IsStartFade", true);
        }
    }


    public static Vector3 CamabsolutePosition = new Vector3(0, 0, 0);
    private IEnumerator LoadCutScene()
    {
        Make_Invisible_UI();

        GameObject Cam = GameObject.Find("Main Camera");

        //ī�޶��� ������ǥ�� �����´�
        CamabsolutePosition = Cam.transform.localPosition + new Vector3(0, 0, 10);

        //�ִϸ��̼� �ֱ�

        //�ƾ� �� �����
        //Instantiate(Cut_Scene_prefab, CamabsolutePosition, Quaternion.identity);
        Cut_Scene_prefab.transform.position = CamabsolutePosition;

        //�ִϸ��̼� ����
        Animator anim = Cut_Scene_prefab.GetComponent<Animator>();
        anim.SetBool("IsStart", true);

        //�ƾ� �ִϸ��̼��� ������ �� �ٷ� �̵�
        yield return new WaitForSeconds(1.17f);
        scenePass.SceneLoadStart("InGame-RN");
    }

    private void Make_Invisible_UI()
    {
        //���� ������Ʈ �� UI_Touch Tag�� SetActive(false)�� �����Ѵ�
        GameObject[] UI_Touch = GameObject.FindGameObjectsWithTag("PlayScene_UI");
        foreach (GameObject UI in UI_Touch)
        {
            UI.SetActive(false);
        }

        GameObject Cam = GameObject.Find("Main Camera");
    }
}