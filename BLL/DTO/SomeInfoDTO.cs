namespace BLL.DTO
{
    public class SomeInfoDTO
    {
        public int SomeInfoID { get; set; }

        public string Name { get; set; }

        public string NameLow {get { return Name.ToLower(); }}
    }
}