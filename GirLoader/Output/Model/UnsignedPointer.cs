﻿namespace GirLoader.Output.Model
{
    public class UnsignedPointer : PrimitiveType
    {
        public UnsignedPointer(string cTypeName) : base(new CType(cTypeName), new SymbolName("UIntPtr")) { }
    }
}
