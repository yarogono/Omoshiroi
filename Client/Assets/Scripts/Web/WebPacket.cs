
using System.Collections.Generic;

public class AccountLoginReq
{
    public string AccountName;
    public string AccountPassword;
}

public class AccountLoginRes
{
    public bool IsLoginSucceed { get; set; }
    public List<PlayerItemRes> Items { get; set; }
}


public class PlayerItemRes
{
    public int TemplateId { get; set; }
    public int Quantity { get; set; }
}