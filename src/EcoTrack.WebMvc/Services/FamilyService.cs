using System;
using System.Linq;
using System.Threading.Tasks;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.Models;

namespace EcoTrack.WebMvc.Services
{
    public class FamilyService : IFamilyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FamilyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Family?> GetFamilyDetailsAsync(Guid familyId)
        {
            return await _unitOfWork.FamilyRepository.GetFamilyWithMembersAsync(familyId);
        }

        public async Task<Family> CreateFamilyAsync(string familyName, Guid creatorUserId)
        {
            var creator = await _unitOfWork.UserRepository.GetByIdAsync(creatorUserId);
            if (creator == null)
            {
                throw new InvalidOperationException("Creator user not found.");
            }
            if (creator.FamilyId.HasValue)
            {
                throw new InvalidOperationException("User is already in a family.");
            }

            var newFamily = new Family { FamilyName = familyName };
            newFamily.Members.Add(creator);

            await _unitOfWork.FamilyRepository.AddAsync(newFamily);
            await _unitOfWork.CompleteAsync();

            return newFamily;
        }

        public async Task AddMemberAsync(Guid familyId, Guid userId)
        {
            var family = await _unitOfWork.FamilyRepository.GetByIdAsync(familyId);
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

            if (family == null || user == null)
            {
                throw new InvalidOperationException("Family or User not found.");
            }
            if (user.FamilyId.HasValue)
            {
                throw new InvalidOperationException("User is already in a family.");
            }

            user.FamilyId = familyId;
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task RemoveMemberAsync(Guid familyId, Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user == null || user.FamilyId != familyId)
            {
                throw new InvalidOperationException("User not found or not a member of this family.");
            }

            user.FamilyId = null;
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<decimal> GetFamilyTotalEmissionAsync(Guid familyId, string period)
        {
            var family = await _unitOfWork.FamilyRepository.GetFamilyWithMembersAsync(familyId);
            if (family == null || !family.Members.Any())
            {
                return 0;
            }

            var memberIds = family.Members.Select(m => m.UserId).ToList();

            DateTime startDate = period.ToLower() == "weekly"
                ? DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek)
                : new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            
            var familyActivities = await _unitOfWork.ActivityRepository.GetActivitiesForUserListSince(memberIds, startDate);

            return familyActivities.Sum(a => a.CarbonEmission);
        }
    }
}