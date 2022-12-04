﻿using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class ClassMethods
{
    public static string Render(GirModel.Class cls)
    {
        return $@"
using System;
using GObject;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

#nullable enable

namespace {Namespace.GetInternalName(cls.Namespace)};

// AUTOGENERATED FILE - DO NOT MODIFY

{PlatformSupportAttribute.Render(cls as GirModel.PlatformDependent)}
public partial class {cls.Name}
{{
    {Functions.Render(cls.TypeFunction)}
    {Functions.Render(cls.Functions)}
    {Methods.Render(cls.Methods)}
    {Constructors.Render(cls.Constructors)}
}}";
    }
}
