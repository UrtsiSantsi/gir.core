﻿using System.Collections.Generic;
using System.Linq;

namespace Generator3.Generation.Union
{
    public class InternalStructModel
    {
        private readonly GirModel.Union _union;

        public string Name => _union.Name;
        public string NamespaceName => _union.Namespace.GetInternalName();
        public IEnumerable<UnionField> Fields => _union.Fields.Select(field => new UnionField(field));

        public InternalStructModel(GirModel.Union union)
        {
            _union = union;
        }
    }
}
