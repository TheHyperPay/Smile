/*
*노드의 프리팹을 제거하는 스크립트
*
*구현 목표
*-노드 삭제 명령어
*-일정 시간이 지난 노드의 강제 삭제
*
*위 스크립트의 초기 버전은 김시훈이 작성하였음
*문의사항은 gkfldys1276@yandex.com으로 연락 바랍니다 (카톡도 가능).
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node_delete : MonoBehaviour
{
    public GameObject node_prefab;
    public node_management n_m;

    private const float MAX_TIME = 2.25f;

    public void delete_node_after_click()
    {
        //노드의 Prefab을 제거합니다.
        Destroy(node_prefab);
    }

    private IEnumerator delete_node_AFK_state()
    {
        // 대기할 시간을 계산합니다.
        float waitTime = MAX_TIME - n_m.call_animation_time();

        // 음수가 되지 않도록 대기 시간을 보장합니다.
        if (waitTime < 0)
        {
            waitTime = 0;
        }

        // 계산된 시간만큼 대기합니다.
        yield return new WaitForSeconds(waitTime);

        //실패처리
        UnityEngine.Debug.Log("MISS: NON-CLICK");

        // 노드의 Prefab을 제거합니다.
        Destroy(node_prefab);
    }

    private void Start()
    {
        //StartCoroutine(delete_node_AFK_state());
    }
}
