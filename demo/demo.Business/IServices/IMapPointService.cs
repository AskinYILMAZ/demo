using demo.Model.ViewModel;
using System.Threading.Tasks;

namespace demo.Business.IServices
{
    public interface IMapPointService
    {
        Task<MapPointViewModel> FindNearestAtm(MapPointViewModel mapPointViewModel);
    }
}
