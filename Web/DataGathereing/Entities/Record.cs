namespace DataGathering.Api.Entities
{
    public sealed class Record : BaseEntity
    {
        public IEnumerable<int> Data { get; set; }
    }
}
