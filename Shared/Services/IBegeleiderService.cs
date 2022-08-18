using HerexamenTry.Shared.Domain;
using HerexamenTry.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerexamenTry.Shared.Services
{
   public interface IBegeleiderService
    {
        Task<Jongere> DeleteJongere(string email);
        Task<Jongere> CreateJongere(JongereDTO jongere);
        Task<IEnumerable<Jongere>> GetJongereAsync();
        Task<IEnumerable<Reactie>> GetReacties();

        Task<Reactie> createReactieAsync(ReactieDTO post,int id);
        void DeletePost(int id);
    }
}
