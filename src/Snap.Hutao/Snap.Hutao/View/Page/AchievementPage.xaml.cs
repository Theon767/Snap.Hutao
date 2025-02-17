﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Snap.Hutao.Control;
using Snap.Hutao.ViewModel;

namespace Snap.Hutao.View.Page;

/// <summary>
/// 成就页面
/// </summary>
public sealed partial class AchievementPage : ScopedPage
{
    /// <summary>
    /// 构造一个新的成就页面
    /// </summary>
    public AchievementPage()
    {
        InitializeWith<AchievementViewModel>();
        InitializeComponent();
    }
}