using UnityEngine;

public class WaterZone : MonoBehaviour
{
    [SerializeField]private GameObject waterResource;

    private void Update()
    {
        if(transform.childCount == 0) // 자식이 하나도 없을 때
        {
            GameObject newWaterResource = Instantiate(waterResource, transform); // 현재 오브젝트의 자식으로 생성
            newWaterResource.transform.localPosition = Vector3.zero; // 위치 초기화
        }
    }
}
