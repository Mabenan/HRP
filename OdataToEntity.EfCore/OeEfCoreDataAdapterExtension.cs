using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using OdataToEntity.ModelBuilder;
using System;

namespace OdataToEntity.EfCore
{
    public static class OeEfCoreDataAdapterExtension
    {
        public static EdmModel BuildEdmModelFromEfCoreModel(this Db.OeDataAdapter dataAdapter, Type[] excludeTypes, params IEdmModel[] refModels)
        {
            var context = (DbContext)dataAdapter.CreateDataContext();
            try
            {
                var modelBuilder = new OeEdmModelBuilder(dataAdapter, new OeEfCoreEdmModelMetadataProvider(context.Model));
                return modelBuilder.BuildEdmModel(excludeTypes, refModels);
            }
            finally
            {
                dataAdapter.CloseDataContext(context);
            }
        }
    }
}
