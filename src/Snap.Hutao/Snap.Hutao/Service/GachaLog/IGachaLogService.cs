﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Snap.Hutao.Model.Binding.Gacha;
using Snap.Hutao.Model.Entity;
using System.Collections.ObjectModel;

namespace Snap.Hutao.Service.GachaLog;

/// <summary>
/// 祈愿记录服务
/// </summary>
internal interface IGachaLogService
{
    /// <summary>
    /// 当前存档
    /// </summary>
    GachaArchive? CurrentArchive { get; set; }

    /// <summary>
    /// 获取可用于绑定的存档集合
    /// </summary>
    /// <returns>存档集合</returns>
    ObservableCollection<GachaArchive> GetArchiveCollection();

    /// <summary>
    /// 获取祈愿日志Url提供器
    /// </summary>
    /// <param name="option">刷新模式</param>
    /// <returns>祈愿日志Url提供器</returns>
    IGachaLogUrlProvider? GetGachaLogUrlProvider(RefreshOption option);

    /// <summary>
    /// 获得对应的祈愿统计
    /// </summary>
    /// <param name="archive">存档</param>
    /// <returns>祈愿统计</returns>
    Task<GachaStatistics> GetStatisticsAsync(GachaArchive? archive = null);

    /// <summary>
    /// 异步初始化
    /// </summary>
    /// <param name="token">取消令牌</param>
    /// <returns>是否初始化成功</returns>
    ValueTask<bool> InitializeAsync(CancellationToken token = default);

    /// <summary>
    /// 刷新祈愿记录
    /// 切换选中的存档
    /// </summary>
    /// <param name="query">查询语句</param>
    /// <param name="strategy">刷新策略</param>
    /// <param name="progress">进度</param>
    /// <param name="token">取消令牌</param>
    /// <returns>任务</returns>
    Task RefreshGachaLogAsync(string query, RefreshStrategy strategy, IProgress<FetchState> progress, CancellationToken token);
}