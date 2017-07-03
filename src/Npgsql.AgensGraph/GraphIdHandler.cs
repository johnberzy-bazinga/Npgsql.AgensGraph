using Npgsql.BackendMessages;
using Npgsql.TypeHandling;

namespace Npgsql.AgensGraph
{
    /// <summary>
    /// Type handler for the Agens graphid type.
    /// </summary>
    /// <remarks>
    /// https://github.com/bitnine-oss/agens-graph
    /// </remarks>
    class GraphIdHandlerFactory : NpgsqlTypeHandlerFactory<GraphId>
    {
        protected override NpgsqlTypeHandler<GraphId> Create(NpgsqlConnection conn)
            => new GraphIdHandler();
    }

    class GraphIdHandler : NpgsqlSimpleTypeHandler<GraphId>, INpgsqlSimpleTypeHandler<string>
    {
        public override GraphId Read(NpgsqlReadBuffer buf, int len, FieldDescription fieldDescription = null)
        {
            var id = (ulong)buf.ReadInt64();
            var labId = (ushort)(id >> (32 + 16));
            var locId = id & 0x0000ffffffffffff;
            return new GraphId(labId, locId);
        }

        string INpgsqlSimpleTypeHandler<string>.Read(NpgsqlReadBuffer buf, int len, FieldDescription fieldDescription)
            => Read(buf, len, fieldDescription).ToString();

        protected override int ValidateAndGetLength(object value, NpgsqlParameter parameter = null)
        {
            if (!(value is GraphId))
                throw CreateConversionException(value.GetType());
            return 8;
        }

        protected override void Write(object value, NpgsqlWriteBuffer buf, NpgsqlParameter parameter = null)
        {
            var v = (GraphId)value;
            var graphid = (((ulong)(v.LabelId)) << (32 + 16)) |
                 (((ulong)(v.LocationId)) & 0x0000ffffffffffff);
            buf.WriteInt64((long)graphid);
        }
    }
}
