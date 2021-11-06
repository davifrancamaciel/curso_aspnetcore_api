using System;

namespace Api.Domain.Models
{
    public class CityModel : BaseModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _ibgeId;
        public int IbgeId
        {
            get { return _ibgeId; }
            set { _ibgeId = value; }
        }
        private Guid _stateId;
        public Guid StateId
        {
            get { return _stateId; }
            set { _stateId = value; }
        }

    }
}
