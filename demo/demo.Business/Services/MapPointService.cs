using demo.Business.IServices;
using demo.Model.Dto;
using demo.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace demo.Business.Services
{
    public class MapPointService : IMapPointService
    {
        //harita işlemleri bu servis ile eklenebilir.
        public async Task<MapPointViewModel> FindNearestAtm(MapPointViewModel mapPointViewModel)
        {
            //en yakın atm burada bulunabilir.
            mapPointViewModel.Name = "GÖZTEPE/İSTANBUL";
            mapPointViewModel.CityName = "İSTANBUL";
            mapPointViewModel.Address = "GÖZTEPE MAH. TÜTÜNCÜ MEHMETEFENDI CAD. D ERYA APT. NO: 107/4 KADIKÖY İSTANBUL";
            return mapPointViewModel;
        }
    }
}
