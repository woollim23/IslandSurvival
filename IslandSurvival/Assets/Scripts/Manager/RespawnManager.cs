using System.Collections;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    private static RespawnManager _instance;
    public static RespawnManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("RespawnManager");
                _instance = go.AddComponent<RespawnManager>();
            }
            return _instance;
        }
    }

    public void StartRespawn(GameObject target, float delay, Vector3 position, GameObject remainResource = null)
    {
        StartCoroutine(RespawnCoroutine(target, delay, position, remainResource));
    }

    private IEnumerator RespawnCoroutine(GameObject target, float delay, Vector3 position, GameObject remainResource)
    {
        yield return new WaitForSeconds(delay);

        // �������� ��, ������ �����ϰ� �ٽ� Ȱ��ȭ
        if (remainResource != null)
        {
            Destroy(remainResource); // �ص�, (����) ���� ����
        }

        // Ȱ��ȭ�� ��ġ ����
        target.SetActive(true);
        target.transform.position = position;

        Resource resource = target.GetComponent<Resource>();
        if (resource != null)
        {
            resource.ResetResource();
        }
    }
}