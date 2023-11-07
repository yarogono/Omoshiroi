public class AccountLoginReq
{
    public string AccountName;
    public string AccountPassword;
}

public class AccountLoginRes
{
    public bool IsLoginSucceed;
}

public class AddItemRequest
{
    public int ItemID { get; set; }
    public int Quantity { get; set; }
}
public class AddItemResponse
{
    public bool IsSuccess;
}

public class PlayerItemRes
{
    public int TemplateId { get; set; }
    public int Quantity { get; set; }
}
