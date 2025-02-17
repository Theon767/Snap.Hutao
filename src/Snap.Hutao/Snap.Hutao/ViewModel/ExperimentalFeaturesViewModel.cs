﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Snap.Hutao.Model.Entity.Database;
using Snap.Hutao.Service.Abstraction;
using Windows.Storage;
using Windows.System;

namespace Snap.Hutao.ViewModel;

/// <summary>
/// 实验性功能视图模型
/// </summary>
[Injection(InjectAs.Scoped)]
internal class ExperimentalFeaturesViewModel : ObservableObject
{
    /// <summary>
    /// 构造一个新的实验性功能视图模型
    /// </summary>
    public ExperimentalFeaturesViewModel()
    {
        OpenCacheFolderCommand = new AsyncRelayCommand(OpenCacheFolderAsync);
        OpenDataFolderCommand = new AsyncRelayCommand(OpenDataFolderAsync);
        DeleteUsersCommand = new AsyncRelayCommand(DangerousDeleteUsersAsync);
        DeleteAllScheduleTasksCommand = new RelayCommand(DangerousDeleteAllScheduleTasks);
    }

    /// <summary>
    /// 打开临时文件夹命令
    /// </summary>
    public ICommand OpenCacheFolderCommand { get; }

    /// <summary>
    /// 打开数据文件夹命令
    /// </summary>
    public ICommand OpenDataFolderCommand { get; }

    /// <summary>
    /// 清空用户命令
    /// </summary>
    public ICommand DeleteUsersCommand { get; }

    /// <summary>
    /// 删除所有计划任务命令
    /// </summary>
    public ICommand DeleteAllScheduleTasksCommand { get; }

    private Task OpenCacheFolderAsync()
    {
        return Launcher.LaunchFolderAsync(ApplicationData.Current.LocalCacheFolder).AsTask();
    }

    private Task OpenDataFolderAsync()
    {
        return Launcher.LaunchFolderPathAsync(Core.CoreEnvironment.DataFolder).AsTask();
    }

    private async Task DangerousDeleteUsersAsync()
    {
        using (IServiceScope scope = Ioc.Default.CreateScope())
        {
            AppDbContext appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await appDbContext.Users.ExecuteDeleteAsync().ConfigureAwait(false);

            IInfoBarService infoBarService = scope.ServiceProvider.GetRequiredService<IInfoBarService>();
            infoBarService.Success("清除用户数据成功,请重启胡桃");
        }
    }

    private void DangerousDeleteAllScheduleTasks()
    {
        IInfoBarService infoBarService = Ioc.Default.GetRequiredService<IInfoBarService>();
        if (Core.ScheduleTaskHelper.UnregisterAllTasks())
        {
            infoBarService.Success("清除任务计划成功");
        }
        else
        {
            infoBarService.Warning("清除任务计划失败");
        }
    }
}