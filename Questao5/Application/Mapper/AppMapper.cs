using AutoMapper;

namespace Questao5.Application.Mapper
{
    public static class AppMapper
    {
        private static readonly Lazy<IMapper> LazyMapper = new Lazy<IMapper>(CreateMapper);

        public static IMapper Mapper => LazyMapper.Value;

        private static IMapper CreateMapper()
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;

                    cfg.AddProfile<OrderingMappingProfile>();
                });

                return config.CreateMapper();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Falha na configuração do AutoMapper.", ex);
            }
        }
    }
}
