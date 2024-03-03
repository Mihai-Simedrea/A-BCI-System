namespace DataGathering.Api.Contracts
{
    public sealed class RecordResponse
    {
        public Guid Id { get; set; }
        public IEnumerable<int> Data { get; set; }
    }
}
