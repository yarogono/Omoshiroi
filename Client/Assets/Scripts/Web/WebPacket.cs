public class AccountLoginReq
{
    public string AccountName;
    public string AccountPassword;
}

public class AccountLoginRes
{
    public int LoginOk;
}

public class AddItemRequest
{
    public string ItemID { get; set; }
    public int Quantity { get; set; }
}
public class AddItemResponse
{
    public bool IsSuccess;
}
