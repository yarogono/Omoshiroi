using UnityEngine;

public class t_UIManager : MonoBehaviour
{
    
    public void LeaveGameRoom()
    {
        NetworkManager.Instance.LeaveGame();
    }
}
