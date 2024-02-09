namespace FCManager.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        ITeamRepository Team { get; }
        ITeamMemberRepository TeamMember { get; }

        void Save();
    }
}
