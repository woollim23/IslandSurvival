using UnityEngine;

public class WaterZone : MonoBehaviour
{
    [SerializeField]private GameObject waterResource;

    private void Update()
    {
        if(transform.childCount == 0) // �ڽ��� �ϳ��� ���� ��
        {
            GameObject newWaterResource = Instantiate(waterResource, transform); // ���� ������Ʈ�� �ڽ����� ����
            newWaterResource.transform.localPosition = Vector3.zero; // ��ġ �ʱ�ȭ
        }
    }
}
