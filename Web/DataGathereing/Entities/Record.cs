namespace DataGathering.Api.Entities
{
    public sealed class Record : BaseEntity
    {
        public required IEnumerable<int> Data { get; set; }
    }
}
