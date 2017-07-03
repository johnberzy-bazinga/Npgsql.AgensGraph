using Npgsql.TypeMapping;

namespace Npgsql.AgensGraph
{
    public static class AgensGraphExtensions
    {
        public static INpgsqlTypeMapper UseAgensDb(this INpgsqlTypeMapper mapper)
        {
            mapper.MapComposite<Vertex>("vertex");
            mapper.MapComposite<Path>("graphpath");
            mapper.MapComposite<Edge>("edge");

            mapper.AddMapping(new NpgsqlTypeMappingBuilder
            {
                PgTypeName = "graphid",
                ClrTypes = new [] { typeof(GraphId) },
                TypeHandlerFactory = new GraphIdHandlerFactory()
            }.Build());
            return mapper;
        }
    }
}
