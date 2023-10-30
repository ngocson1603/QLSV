using Dapper;
using QLSV.Enums;
using QLSV.Interfaces;
using QLSV.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSV.Repositories
{
    public class HocSinhRepository : GameStoreRepository<HocSinh>, IHocSinhRepository
    {
        public HocSinhRepository(GameStoreDbContext context) : base(context)
        {

        }

        public HocSinh FindByEmail(string email)
        {
            var query = @"select * from HocSinh where Gmail = @email";
            var para = new DynamicParameters();
            para.Add("email", email);

            try
            {
                return (HocSinh)Context.Database.GetDbConnection().Query<HocSinh>(query, para).First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<HocSinh> listhocsinh(int id)
        {
            var query = @"select HocSinh.* from HocSinh,KhoaHoc,DiemHocSinh 
where HocSinh.Id = DiemHocSinh.IdHocSinh and DiemHocSinh.IdKhoaHoc = KhoaHoc.Id and KhoaHoc.Id = @id";
            var parameter = new DynamicParameters();
            parameter.Add("id", id);
            var data = Context.Database.GetDbConnection().Query<HocSinh>(query, parameter);
            return data.ToList();
        }
        public void updateBalance(int userID,decimal price,int type)
        {
            HocSinh user=this.GetById(userID);
            if(type==(int)marketType.buy)
            {
                user.Balance = user.Balance - price;
            }
            else if(type==(int)marketType.sell)
            {
                user.Balance=user.Balance+price;
            }
            else if (type == (int)marketType.paypal)
            {
                user.Balance = user.Balance + price;
            }    
            this.Update(user);
        }
    }
}
