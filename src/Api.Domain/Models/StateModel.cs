namespace Api.Domain.Models
{
    public class StateModel : BaseModel
    {
        private string _sigla;
        public string Sigla
        {
            get { return _sigla; }
            set { _sigla = value; }
        }

        private int _name;
        public int Name
        {
            get { return _name; }
            set { _name = value; }
        }


    }
}
