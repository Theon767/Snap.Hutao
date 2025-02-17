﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using CommunityToolkit.WinUI.UI.Converters;

namespace Snap.Hutao.View.Converter;

/// <summary>
/// This class converts a collection size into a boolean value.
/// </summary>
public class EmptyCollectionToBoolConverter : EmptyCollectionToObjectConverter
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmptyCollectionToVisibilityConverter"/> class.
    /// </summary>
    public EmptyCollectionToBoolConverter()
    {
        EmptyValue = false;
        NotEmptyValue = true;
    }
}
