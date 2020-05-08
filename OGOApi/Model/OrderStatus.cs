namespace OgoShip.Models.WebApi.V1
{
    public enum OrderStatus
    {
        New,
        Draft,
        Reserved,
        Locked,
        Collecting,
        Pending,
        Cancelled,
        Shipped,
        Returned
    }
}