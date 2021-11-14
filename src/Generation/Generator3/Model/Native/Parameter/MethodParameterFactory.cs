﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Model.Native
{
    public static class MethodParameterFactory
    {
        public static IEnumerable<Parameter> CreateNativeModelsForMethod(this IEnumerable<GirModel.Parameter> parameters)
            => parameters.Select(CreateNativeModelForMethod);
        
        private static Parameter CreateNativeModelForMethod(this GirModel.Parameter parameter) => parameter.AnyType.Match<Parameter>(
            type => type switch
            {
                GirModel.String => new StringParameter(parameter),
                GirModel.Pointer => new PointerParameter(parameter),
                GirModel.UnsignedPointer => new UnsignedPointerParameter(parameter),
                GirModel.Class => new ClassParameter(parameter),
                GirModel.Interface => new InterfaceParameter(parameter),
                GirModel.Union => new UnionParameter(parameter),
                GirModel.Record => new SafeHandleRecordParameter(parameter),
                GirModel.PrimitiveValueType => new StandardParameter(parameter),
                GirModel.Callback => new CallbackParameter(parameter),
                GirModel.Enumeration => new StandardParameter(parameter),
                GirModel.Bitfield => new StandardParameter(parameter),
                GirModel.Void => new VoidParameter(parameter),
                
                _ => throw new Exception($"Parameter \"{parameter.Name}\" of type {parameter.AnyType} can not be converted into a model")
            },
            arrayType => arrayType.Type switch
            {
                GirModel.Record => new ArrayPointerRecordParameterForMethod(parameter),
                GirModel.String => new ArrayStringParameterForMethod(parameter),
                _ => new StandardParameter(parameter)
            }
        );
    }
}
