using FCManager.DataAccess.Data;
using FCManager.DataAccess.Interfaces;

namespace FCManager.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Team = new TeamRepository(_db);
            TeamMember = new TeamMemberRepository(_db);
        }
        public ITeamRepository Team { get; private set; }
        public ITeamMemberRepository TeamMember { get; private set; }


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
