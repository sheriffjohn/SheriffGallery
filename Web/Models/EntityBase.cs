namespace Web.Models
{
    public abstract class EntityBase<TId, TRowId>
    {
        public TId Id { get; set; }
        public TRowId RowId { get; set; }
    }
}