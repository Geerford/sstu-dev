using Domain.Core;
using System.Web;

namespace sstu_nevdev.Models
{
    public class IdentityAPIModel
    {
        public Identity Identity { get; set; }

        public HttpPostedFileBase File { get; set; }
    }
}