using Tsi.Vendors.DataSync.Domain.Base;
using Tsi.Vendors.DataSync.Domain.Entities.MailBoxes;
using Tsi.Vendors.DataSync.Domain.Interfaces;

namespace Tsi.Vendors.DataSync.Application.MailBoxes
{
    public class MailBoxService : IMailBoxService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MailBoxService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<MailBoxDto>> ListUserMailBoxesAsync(int userId)
        {
            var mailBoxRepository = _unitOfWork.Repository<MailBox>();

            var mailBoxes = await mailBoxRepository.ListAsync(x => x.UserId == userId);

            return mailBoxes?.ToList().ToMailBoxDtos() ?? new List<MailBoxDto>();
        }

        public async Task<MailBoxDto?> GetMailBoxAsync(string id)
        {
            var mailBoxRepository = _unitOfWork.Repository<MailBox>();

            var mailBox = await mailBoxRepository.GetAsync(x => x.Id == id);

            return mailBox?.ToMailBoxDto();
        }

        public async Task<MailBoxDto> CreateMailBox(CreateMailBoxRequest request)
        {
            var mailBoxRepository = _unitOfWork.Repository<MailBox>();

            var existing = await mailBoxRepository.GetAsync(x => 
                x.Id == request.EmailAddress 
                && x.UserId == request.UserId);

            if (existing != null)
            {
                throw new EntityConflictException($"[MailBoxCreated] {request.EmailAddress} already exists for user {request.UserId}.");
            }

            var mailBox = MailBox.Create(
                request.UserId,
                request.EmailAddress,
                request.Provider,
                request.DisplayName ?? request.EmailAddress);

            var n = mailBoxRepository.AddAsync(mailBox);
            
            await _unitOfWork.SaveChangesAsync();

            return mailBox.ToMailBoxDto();
        }

        public async Task DeActivate(string id)
        {
            var mailBoxRepository = _unitOfWork.Repository<MailBox>();
            var mailBox = await mailBoxRepository.GetAsync((x) => x.Id == id);

            if (mailBox == null)
            {
                throw new EntityNotFoundException($"[MailBoxDeActivated] {id} was not found.");
            }
            
            mailBox.DeActivate();

            await mailBoxRepository.UpdateAsync(mailBox);
        }

        public async Task Activate(string id)
        {
            var mailBoxRepository = _unitOfWork.Repository<MailBox>();
            var mailBox = await mailBoxRepository.GetAsync((x) => x.Id == id);

            if (mailBox == null)
            {
                throw new EntityNotFoundException($"[MailBoxActivated] {id} was not found.");
            }

            mailBox.Activate();

            await mailBoxRepository.UpdateAsync(mailBox);
        }
    }
}
