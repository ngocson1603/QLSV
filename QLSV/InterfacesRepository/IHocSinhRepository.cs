using QLSV.Models;
using System.Collections.Generic;

namespace QLSV.Interfaces
{
    public interface IHocSinhRepository:IGameStoreRepository<HocSinh>
    {
        void updateBalance(int userID, decimal price, int type);
        HocSinh FindByEmail(string email);
        List<HocSinh> listhocsinh(int id);
    }
}
