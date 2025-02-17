﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Snap.Hutao.Model.Intrinsic;

namespace Snap.Hutao.Model.InterChange.Achievement;

/// <summary>
/// UIAF 项
/// </summary>
public class UIAFItem
{
    /// <summary>
    /// 成就Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 完成时间
    /// </summary>
    public long Timestamp { get; set; }

    /// <summary>
    /// 当前值
    /// 对于progress为1的项，该属性始终为0
    /// </summary>
    public int Current { get; set; }

    /// <summary>
    /// 完成状态
    /// </summary>
    public AchievementInfoStatus Status { get; set; }
}