using QLSV.Models;
using QLSV.OtpModels;
using System.Collections.Generic;

namespace QLSV.Interfaces
{
    public interface IRefundRepository:IGameStoreRepository<Refund>
    {
        OtpModels.RefundRequest refundRequest(int UserID, int productID);
        //List<gameRefund> listgameRefund(int userid);
        List<RefundUser> GetRefundHocSinh();
    }
}
