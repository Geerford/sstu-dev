namespace Domain.Core
{
    public class Identity
    {
        public int ID { get; set; }
        public string GUID { get; set; }
        public string Picture { get; set; }

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
