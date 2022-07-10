using Challenge.Models;

namespace Challenge.Interfaces
{
    public interface IProvince
    {
        Province.ProvinceResult GetGeolocationByProvinceName(string province);
    }
}
