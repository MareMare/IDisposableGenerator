// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Hoge.cs" company="Harbor Software Corporation">
// Copyright © 2022 Harbor Software Corporation All rights reserved.
// Licensed under the Apache-2.0 license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using IDisposableGenerator;

namespace SampleApp;

[GenerateIDisposable]
internal partial class Hoge
{
    private readonly MemoryStream _stream = new ();
    
    partial void DisposeManagedInstances()
    {
        this._stream.Dispose();
    }
}
