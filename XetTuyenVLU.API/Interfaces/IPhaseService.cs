using XetTuyenVLU.Models;
using XetTuyenVLU.ViewModels.Phase;

namespace XetTuyenVLU.Interfaces
{
    public interface IPhaseService
    {
        public List<PhaseVM> GetAllPhases();
        public Task<int> CreatePhase(Dot request);
        public Task<bool> ChangeStatusPhase(int id);
        public bool ValidateAllPhasesWereExpired();
    }
}
