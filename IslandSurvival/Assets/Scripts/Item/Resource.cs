using System.Collections;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public enum ResourceType { Tree, Rock }
    public ResourceType resourceType;

    public ItemData itemToGive;
    public int quantityPerHit = 1;
    public int capacity;
    public int initialCapacity;
    public GameObject treeStumpPrefab;
    public GameObject RockRemainsPrefab;

    private GameObject stumpInstance;

    private void Start()
    {
        initialCapacity = capacity;
    }

    public void Gather(Vector3 hitPoint, Vector3 hitNormal)
    {
        for (int i = 0; i < quantityPerHit; i++)
        {
            if (capacity <= 0) break;

            capacity -= 1;
            Instantiate(itemToGive.dropPrefab, hitPoint + Vector3.up, Quaternion.LookRotation(hitNormal, Vector3.up));
        }

        if (capacity <= 0)
        {
            ShowStump(hitPoint); // 자원에 따라 다른 프리팹 생성(광석이 추가된다면.현재는 나무 밑동)
            gameObject.SetActive(false); // 자원 오브젝트 비활성화

            // 리스폰 매니저에 자원 리스폰 요청
            RespawnManager.Instance.StartRespawn(gameObject, 5f, transform.position, stumpInstance);
        }
    }

    private void ShowStump(Vector3 position)
    {
        GameObject prefabToInstantiate = resourceType == ResourceType.Tree ? treeStumpPrefab : RockRemainsPrefab;

        if (prefabToInstantiate != null)
        {
            // Raycast로 지면 위치에 자원의 흔적(밑동) 생성
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
            {
                Vector3 groundPosition = hit.point;
                stumpInstance = Instantiate(prefabToInstantiate, groundPosition, Quaternion.identity);
            }
            else
            {
                stumpInstance = Instantiate(prefabToInstantiate, position, Quaternion.identity);
            }
        }
    }

    public void ResetResource()
    {
        capacity = initialCapacity;
    }
}