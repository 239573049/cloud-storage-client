﻿namespace CloudStoage.Domain.HttpModule.Input;

public class CreateTokenInput
{
    /// <summary>
    /// 账号
    /// </summary>
    public string? Account { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string? Password { get; set; }
}
