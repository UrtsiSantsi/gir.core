﻿namespace Generator3.Model.Native
{
    public class ArrayStringParameter : Parameter
    {
        private GirModel.ArrayType ArrayType => Model.AnyType.AsT1;

        public override string NullableTypeName => ArrayType.GetName();

        public override string Attribute => ArrayType.Length is null
            ? string.Empty
            : $"[MarshalAs(UnmanagedType.LPArray, SizeParamIndex={ArrayType.Length})]";

        protected internal ArrayStringParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyArrayType<GirModel.String>();
        }
    }
}
