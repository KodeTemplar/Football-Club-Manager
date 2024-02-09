using Microsoft.AspNetCore.Http;

namespace FCManager.Utility
{
    public class ViewAuthorization : Attribute
    {
        private readonly ISession _session;

        public ViewAuthorization(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public bool IsAuthorized(string role)
        {
            if (!_session.IsAvailable)
            {
                return false;
            }
            else
            {
                var roleName = _session.TryGetValue("roleName", out byte[] value);
                if (!roleName)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
