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

        // 리스폰될 때, 흔적을 제거하고 다시 활성화
        if (remainResource != null)
        {
            Destroy(remainResource); // 밑동, (광석) 흔적 제거
        }

        // 활성화될 위치 설정
        target.SetActive(true);
        target.transform.position = position;

        Resource resource = target.GetComponent<Resource>();
        if (resource != null)
        {
            resource.ResetResource();
        }
    }
}