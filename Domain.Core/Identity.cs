using Domain.Core.Logs;
using Newtonsoft.Json.Linq;

namespace Domain.Core
{
    [Auditable(AuditScope.ClassAndProperties)]
    public class Identity : IDescribable
    {
        public int ID { get; set; }
        public string GUID { get; set; }
        public string Picture { get; set; }

        /// <summary>
        /// Implements <see cref="IDescribable.Describe()"/>
        /// </summary>
        public string Describe()
        {
            dynamic json = new JObject();
            json.ID = ID;
            json.GUID = GUID;
            json.Picture = Picture;
            return json.ToString();
        }

        public override bool Equals(object obj)
        {
            var result = false;
            if (obj is Identity item)
            {
                result = ID == item.ID;
                result &= GUID.Equals(item.GUID);
                result &= Picture.Equals(item.Picture);
                return result;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashcode = ID.GetHashCode();
            hashcode ^= GUID.GetHashCode();
            hashcode ^= Picture.GetHashCode();
            return hashcode;
        }
    }
}
