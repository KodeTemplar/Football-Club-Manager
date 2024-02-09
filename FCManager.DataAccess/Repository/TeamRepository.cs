using FCManager.DataAccess.Data;
using FCManager.DataAccess.Interfaces;
using FCManager.Models.Models;
using FCManager.Models.ViewModels.Team;
using FCManager.Models.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace FCManager.DataAccess.Repository
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        private ApplicationDbContext _db;

        public TeamRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Response<TeamViewModel>> UpdateTeam(TeamViewModel request)
        {
            try
            {
                var existingTeam = await _db.Teams.FirstOrDefaultAsync(x => x.TeamId == request.TeamId);
                if (existingTeam != null)
                {
                    existingTeam.Name = request.Name;
                    existingTeam.Country = request.Country;
                    existingTeam.Stadium = request.Stadium;
                    existingTeam.HomePageURL = request.HomePageURL;

                    using (var trans = _db.Database.BeginTransaction())
                    {
                        _db.Teams.Update(existingTeam);
                        var save = await _db.SaveChangesAsync();
                        if (save > 0)
                        {
                            if (request.Image != null)
                            {
                                var existingLogo = await _db.TeamLogo.FirstOrDefaultAsync(x => x.TeamId == request.TeamId);
                                if (existingLogo != null)
                                {
                                    existingLogo.Logo = request.Image;
                                    _db.TeamLogo.Update(existingLogo);
                                    var updateLogo = await _db.SaveChangesAsync();
                                    if (updateLogo < 1)
                                    {
                                        trans.Rollback();
                                        return ApplicationResponse.FailureMessage<TeamViewModel>(null, "Unsuccessful");
                                    }
                                }
                                else
                                {
                                    var image = new TeamLogo
                                    {
                                        TeamId = request.TeamId,
                                        Logo = request.Image
                                    };
                                    await _db.TeamLogo.AddAsync(image);
                                    var saveImage = await _db.SaveChangesAsync();
                                    if (saveImage < 1)
                                    {
                                        trans.Rollback();
                                        return ApplicationResponse.FailureMessage<TeamViewModel>(null, "Unsuccessful");
                                    }
                                }
                            }
                            trans.Commit();
                            return ApplicationResponse.SuccessMessage<TeamViewModel>(request, "Successful");
                        }
                        else
                        {
                            trans.Rollback();
                            return ApplicationResponse.FailureMessage<TeamViewModel>(null, "Unsuccessful");
                        }
                    }
                }
                return ApplicationResponse.NotFoundMessage<TeamViewModel>(null, "Team not found");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
