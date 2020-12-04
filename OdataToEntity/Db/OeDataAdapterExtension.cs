using Microsoft.OData.Edm;
using OdataToEntity.ModelBuilder;
using System;

namespace OdataToEntity
{
    public static class OeDataAdapterExtension
    {
        public static EdmModel BuildEdmModel(this Db.OeDataAdapter dataAdapter, Type[] excludedTypes, params IEdmModel[] refModels)
        {
            var modelBuilder = new OeEdmModelBuilder(dataAdapter, new OeEdmModelMetadataProvider());
            return modelBuilder.BuildEdmModel(excludedTypes, refModels);
        }
    }
}