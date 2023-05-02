/*
*���� �� �� �񵿱������� ��ȯ�ϴ� ��ũ��Ʈ
*
*���� ��ǥ
*-�� ��ȯ
*-�̸� �� �ε�
*-�׳� �Ϸ��� �ε� ���� ���� �� �ε�
*
*�� ��ũ��Ʈ�� �ʱ� ������ ������� �ۼ��Ͽ���
*���ǻ����� gkfldys1276@yandex.com���� ���� �ٶ��ϴ� (ī�嵵 ����).
*/
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;


public interface IScenePass
{
    public void LoadSceneAsync(string sceneName);
    public void SceneLoadStart();
}


public class ScenePass : MonoBehaviour, IScenePass
{
    private AsyncOperation asyncLoad;
    private bool enable=false;
    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
    }

    private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                enable = true;
            }

            yield return null;
        }
    }

    public void SceneLoadStart()
    {
        while(!enable) 
        {
            
        }
        asyncLoad.allowSceneActivation = true;
        return;
    }

    public void SceneLoad_Immediately(string sceneName) //�ִϸ��̼ǿ��� �� �̵��� �����ϱ� ���� ���
    {
        SceneManager.LoadScene(sceneName);
    }
}