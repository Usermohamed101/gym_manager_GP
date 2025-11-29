using GymManagmentSystem.DataBase;

namespace GymManagmentSystem.Repos
{
    public interface IMemberMembershipRepo
    {
        Task<bool> AddAsync(MemberMembership entity);
        Task<bool> RemoveAsync(string memberId, int membershipId);
        Task<MemberMembership?> GetActiveMembership(string memberId);
    }
}
