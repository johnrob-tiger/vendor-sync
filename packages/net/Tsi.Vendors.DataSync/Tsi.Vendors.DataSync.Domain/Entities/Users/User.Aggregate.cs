using System.Runtime.CompilerServices;
using Tsi.Vendors.DataSync.Domain.Base;
using Tsi.Vendors.DataSync.Domain.Entities.Calendars;
using Tsi.Vendors.DataSync.Domain.Entities.MailBoxes;
using Tsi.Vendors.DataSync.Domain.Entities.Users.Events;
using Tsi.Vendors.DataSync.Domain.Shared;
using Tsi.Vendors.DataSync.Domain.Shared.Extensions;

namespace Tsi.Vendors.DataSync.Domain.Entities.Users
{
    public partial class User : BaseEntity<int?>, IAggregateRoot
    {
        public User()
        {
        }

        public User(int id, string userName, string? firstName = null, string? lastName = null, DateTime? birthDate = null, DateTimeOffset? createdDate = null, DateTimeOffset? lastModifiedDate = null)
        {
            this.Update(id, userName, firstName, lastName, birthDate, createdDate, lastModifiedDate);
        }

        public User(string userName, string? firstName = null, string? lastName = null, DateTime? birthDate = null,
            DateTimeOffset? createdDate = null, DateTimeOffset? lastModifiedDate = null)
        {
            this.Update(null, userName, firstName, lastName, birthDate, createdDate, lastModifiedDate);
        }
        
        public void AddCalendar(Calendar calendar)
        {
            Guard.Against.Null(calendar, "calendar");

            Calendars ??= new List<Calendar>();
            Calendars.Add(calendar);
        }

        public void AddCalendars(IList<Calendar> calendars)
        {
            Guard.Against.Null(calendars, "calendars");

            foreach (var calendar in calendars)
            {
                AddCalendar(calendar);
            }
        }

        public void AddMailBoxes(IList<MailBox> mailBoxes)
        {
            Guard.Against.Null(mailBoxes, "mailBoxes");

            foreach (var mailBox in mailBoxes)
            {
                AddMailBox(mailBox);
            }
        }

        public void AddMailBox(MailBox mailBox)
        {
            Guard.Against.Null(Id, "Id", "Cannot add mail box. Save user first.");

            Guard.Against.Null(mailBox, "mailBox");
            Guard.Against.NullOrWhiteSpace(mailBox.Id, "mailBox.Id");

            MailBoxes ??= new List<MailBox>();

            var exists = MailBoxes.All(x => x.Id == mailBox.Id);

            if (exists)
            {
                throw new DomainException($"Mailbox {mailBox.Id} already exists for user: {Id}.");
            }

            MailBoxes.Add(mailBox);
        }

        public void Update(
            int? id,
            string userName, 
            string? firstName = null, 
            string? lastName = null, 
            DateTime? birthDate = null,
            DateTimeOffset? createdDate = null,
            DateTimeOffset? lastModifiedDate = null)
        {

            if (id != null)
            {
                Guard.Against.ZeroOrLess(id.Value, "id");
                Id = id.Value;
            }

            Guard.Against.Null(userName, "userName");
            Guard.Against.MaxLength(userName, 100, "userName");
            Guard.Against.MinLength(userName, 3, "userName");

            if (!string.IsNullOrWhiteSpace(firstName))
            {
                Guard.Against.MinLength(firstName, 3, "firstName");
                Guard.Against.MaxLength(firstName, 100, "firstName");
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                Guard.Against.MaxLength(lastName, 150, "lastName");
            }

            CreatedDate = createdDate ?? DateTimeOffset.UtcNow;
            LastModifiedDate = lastModifiedDate ?? DateTimeOffset.UtcNow;

            GuardTimeStamps();
            
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }

        public static User Create(
            string userName,
            string? firstName = null,
            string? lastName = null,
            DateTime? birthDate = null,
            DateTimeOffset? createdDate = null,
            DateTimeOffset? lastModifiedDate = null)
        {
            var user = new User(userName, firstName, lastName, birthDate, createdDate, lastModifiedDate);

            DomainEvents.Raise(new UserCreated(user));

            return user;
        }
    }
}
