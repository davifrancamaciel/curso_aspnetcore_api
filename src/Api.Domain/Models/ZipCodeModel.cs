using System;

namespace Api.Domain.Models
{
    public class ZipCodeModel : BaseModel
    {
        private string _zipCode;
        public string ZipCode
        {
            get { return _zipCode; }
            set { _zipCode = value; }
        }
        private string _adress;
        public string Adress
        {
            get { return _adress; }
            set { _adress = value; }
        }
        private string _number;
        public string Nuber
        {
            get { return _number; }
            set { _number = string.IsNullOrWhiteSpace(value) ? "S/N" : value; }
        }
        private Guid _cityId;
        public Guid CityId
        {
            get { return _cityId; }
            set { _cityId = value; }
        }

    }
}
