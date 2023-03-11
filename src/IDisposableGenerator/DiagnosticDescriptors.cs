// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiagnosticDescriptors.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

namespace IDisposableGenerator;

public static class DiagnosticDescriptors
{
    private const string Category = "IDisposableGenerator";

    public static readonly DiagnosticDescriptor DisposeMethodAlreadyExists = new (
        "IDG001",
        "The Dispose method already exists.",
        "The GenerateIDisposable class '{0}' has Dispose method but it is not allowed.",
        DiagnosticDescriptors.Category,
        DiagnosticSeverity.Error,
        true);
}
