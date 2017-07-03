using System;

namespace Npgsql.AgensGraph
{
    public struct GraphId : IEquatable<GraphId>
    {
        ushort _labelId;
        ulong _locationId;

        public GraphId(ushort labelId, ulong locationId)
        {
            _labelId = labelId;
            _locationId = locationId;
        }

        public ushort LabelId => _labelId;
        public ulong LocationId => _locationId;

        public bool Equals(GraphId other)
            => !ReferenceEquals(other, null) && other.LocationId == LocationId && other.LabelId == LabelId;

        public override bool Equals(object obj) => obj is GraphId && Equals((GraphId)obj);

        public static bool operator ==(GraphId x, GraphId y)
            => x.Equals(y);

        public static bool operator !=(GraphId x, GraphId y) => !(x == y);

        public override int GetHashCode() {
           int hash = 17;
           hash = hash * 31 + _labelId.GetHashCode();
           hash = hash * 31 + _locationId.GetHashCode();
           return hash;
        }

        public override string ToString() =>  $"{LabelId}.{LocationId}";
    }

    public class Vertex  {
        public GraphId Id { get; set; }
        public string Properties { get; set; }
        public override string ToString() => $"[{Id}]{Properties}";
    }

    public class Edge {
        public GraphId Id { get; set; }
        public GraphId Start { get; set; }
        public GraphId End { get; set; }
        public string Properties { get; set; }
        public override string ToString () => $"[{Id}]{Properties}";
    }

    public class Path {
        public Vertex[] Vertices { get; set; }
        public Edge[] Edges { get; set; }
    }
}