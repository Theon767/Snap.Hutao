﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Snap.Hutao.Core.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Snap.Hutao.Model.Entity;

/// <summary>
/// 祈愿记录存档
/// </summary>
[Table("gacha_archives")]
public class GachaArchive : ISelectable
{
    /// <summary>
    /// 内部Id
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid InnerId { get; set; }

    /// <summary>
    /// 记录的Uid
    /// </summary>
    public string Uid { get; set; } = default!;

    /// <inheritdoc/>
    public bool IsSelected { get; set; }

    /// <summary>
    /// 卡池物品
    /// </summary>
    public virtual ICollection<GachaItem> Items { get; set; } = default!;

    /// <summary>
    /// 构造一个新的卡池存档
    /// </summary>
    /// <param name="uid">uid</param>
    /// <returns>新的卡池存档</returns>
    public static GachaArchive Create(string uid)
    {
        return new() { Uid = uid };
    }
}