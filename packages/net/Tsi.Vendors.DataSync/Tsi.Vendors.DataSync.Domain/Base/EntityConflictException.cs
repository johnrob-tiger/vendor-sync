namespace Tsi.Vendors.DataSync.Domain.Base
{
    public class EntityConflictException : DomainException
    {
        public EntityConflictException(string message) : base(message)
        {
        }

        public EntityConflictException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
