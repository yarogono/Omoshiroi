using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventoryData;

    private void OnTriggerEnter(Collider collision)
    {

        
        Item item = collision.GetComponent<Item>();
        if (item != null )
        {
            
            int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
            if (reminder == 0)
            {
                item.DestroyItem();
            }           
            else
            {
                item.Quantity = reminder;

            }


            //Item item = collision.GetComponent<Item>();
            //if (item != null)
            //{
            //    // 인벤토리에 아이템 추가 시도
            //    int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);

            //    // 아이템을 추가하는 서버 요청 객체 생성
            //    var addItemRequest = new AddItemRequest
            //    {
            //        ItemID = item.InventoryItem.ItemID.ToString(),
            //        Quantity = item.Quantity
            //    };

            //    // WebManager를 사용하여 서버로 요청 보내기
            //    WebManager.Instance.SendPostRequest<AddItemResponse>("", addItemRequest, response =>
            //    {
            //        // 서버로부터의 응답 처리
            //        if (response.IsSuccess)
            //        {
            //            // 서버에서 아이템 추가가 성공했을 경우
            //            if (reminder == 0)
            //            {
            //                item.DestroyItem(); // 아이템을 게임 세계에서 제거
            //            }
            //            else
            //            {
            //                item.Quantity = reminder; // 서버에서 받은 reminder 값으로 아이템 수량 업데이트
            //            }
            //        }
            //        else
            //        {
            //            // 서버에서 아이템 추가가 실패했을 경우
            //            // 아이템 추가가 실패하면 로컬 인벤토리에서도 롤백 처리를 해야 함
            //        }
            //    });
            //}
        }
    }

}
