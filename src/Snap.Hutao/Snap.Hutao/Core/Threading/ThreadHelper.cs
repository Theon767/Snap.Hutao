﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using System.Runtime.CompilerServices;

namespace Snap.Hutao.Core.Threading;

/// <summary>
/// 线程帮助类
/// </summary>
internal static class ThreadHelper
{
    /// <summary>
    /// 使用此静态方法以 异步切换到 后台线程
    /// </summary>
    /// <remarks>使用 <see cref="SwitchToMainThreadAsync"/> 异步切换到 主线程</remarks>
    /// <returns>等待体</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ThreadPoolSwitchOperation SwitchToBackgroundAsync()
    {
        return default;
    }

    /// <summary>
    /// 使用此静态方法以 异步切换到 主线程
    /// </summary>
    /// <remarks>使用 <see cref="SwitchToBackgroundAsync"/> 异步切换到 后台线程</remarks>
    /// <returns>等待体</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DispatherQueueSwitchOperation SwitchToMainThreadAsync()
    {
        return new(Program.DispatcherQueue!);
    }

    /// <summary>
    /// 在主线程上同步等待执行操作
    /// </summary>
    /// <param name="action">操作</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void InvokeOnMainThread(Action action)
    {
        if (Program.DispatcherQueue!.HasThreadAccess)
        {
            action();
        }
        else
        {
            Program.DispatcherQueue.Invoke(action);
        }
    }
}