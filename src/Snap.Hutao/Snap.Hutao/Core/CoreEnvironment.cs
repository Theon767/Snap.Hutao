﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Microsoft.Win32;
using Snap.Hutao.Core.Json;
using Snap.Hutao.Extension;
using Snap.Hutao.Web.Hoyolab.DynamicSecret;
using System.Collections.Immutable;
using System.IO;
using System.Text.Json.Serialization.Metadata;
using Windows.ApplicationModel;

namespace Snap.Hutao.Core;

/// <summary>
/// 核心环境参数
/// </summary>
internal static class CoreEnvironment
{
    /// <summary>
    /// 米游社请求UA
    /// </summary>
    public const string HoyolabUA = $"Mozilla/5.0 (Windows NT 10.0; Win64; x64) miHoYoBBS/{HoyolabXrpcVersion}";

    /// <summary>
    /// 米游社移动端请求UA
    /// </summary>
    public const string HoyolabMobileUA = $"Mozilla/5.0 (Linux; Android 12) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/106.0.5249.126 Mobile Safari/537.36 miHoYoBBS/{HoyolabXrpcVersion}";

    /// <summary>
    /// 米游社 Rpc 版本
    /// </summary>
    public const string HoyolabXrpcVersion = "2.44.1";

    /// <summary>
    /// 盐
    /// </summary>
    // https://github.com/UIGF-org/Hoyolab.Salt
    public static readonly ImmutableDictionary<string, string> DynamicSecrets = new Dictionary<string, string>()
    {
        [nameof(SaltType.K2)] = "dZAwGk4e9aC0MXXItkwnHamjA1x30IYw",
        [nameof(SaltType.LK2)] = "IEIZiKYaput2OCKQprNuGsog1NZc1FkS",
        [nameof(SaltType.X4)] = "xV8v4Qu54lUKrEYFZkJhB8cuOh9Asafs",
        [nameof(SaltType.X6)] = "t0qEgfub6cvueAPgR5m9aQWWVciEer7v",
        [nameof(SaltType.PROD)] = "JwYDpKvLj6MrMqqYU6jTKF17KNO2PXoS",
    }.ToImmutableDictionary();

    /// <summary>
    /// 标准UA
    /// </summary>
    public static readonly string CommonUA;

    /// <summary>
    /// 当前版本
    /// </summary>
    public static readonly Version Version;

    /// <summary>
    /// 米游社设备Id
    /// </summary>
    public static readonly string HoyolabDeviceId;

    /// <summary>
    /// 胡桃设备Id
    /// </summary>
    public static readonly string HutaoDeviceId;

    /// <summary>
    /// 包家族名称
    /// </summary>
    public static readonly string FamilyName;

    /// <summary>
    /// 数据文件夹
    /// </summary>
    public static readonly string DataFolder;

    /// <summary>
    /// 默认的Json序列化选项
    /// </summary>
    public static readonly JsonSerializerOptions JsonOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Encoder = new JsonTextEncoder(),
        PropertyNameCaseInsensitive = true,
        WriteIndented = true,
        TypeInfoResolver = new DefaultJsonTypeInfoResolver()
        {
            Modifiers =
            {
                JsonTypeInfoResolvers.ResolveEnumType,
            },
        },
    };

    private const string CryptographyKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Cryptography\";
    private const string MachineGuidValue = "MachineGuid";

    static CoreEnvironment()
    {
        DataFolder = GetDocumentsHutaoPath();
        Version = Package.Current.Id.Version.ToVersion();
        FamilyName = Package.Current.Id.FamilyName;
        CommonUA = $"Snap Hutao/{Version}";

        // simply assign a random guid
        HoyolabDeviceId = Guid.NewGuid().ToString();
        HutaoDeviceId = GetUniqueUserID();
    }

    private static string GetUniqueUserID()
    {
        string userName = Environment.UserName;
        object? machineGuid = Registry.GetValue(CryptographyKey, MachineGuidValue, userName);
        return Convert.ToMd5HexString($"{userName}{machineGuid}");
    }

    private static string GetDocumentsHutaoPath()
    {
        string myDocument = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
#if RELEASE
            // 将测试版与正式版的文件目录分离
            string folderName = Package.Current.PublisherDisplayName == "DGP Studio CI" ? "HutaoAlpha" : "Hutao";
#else
        // 使得迁移能正常生成
        string folderName = "Hutao";
#endif
        string path = Path.GetFullPath(Path.Combine(myDocument, folderName));
        Directory.CreateDirectory(path);
        return path;
    }
}