﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Snap.Hutao.Context.FileSystem;
using Snap.Hutao.Core.Logging;
using Snap.Hutao.Model.Entity;

namespace Snap.Hutao.Context.Database;

/// <summary>
/// 此类只用于在生成迁移时提供数据库上下文
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public class LogDbContextDesignTimeFactory : IDesignTimeDbContextFactory<LogDbContext>
{
    /// <inheritdoc/>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public LogDbContext CreateDbContext(string[] args)
    {
        MyDocumentContext myDocument = new(new());
        return LogDbContext.Create($"Data Source={myDocument.Locate("Log.db")}");
    }
}