using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Repository
{
    public class VLCExpenseDetailRepository
    {

        PlatformDBEntities _repository;
        public VLCExpenseDetailRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }


        public List<VLCExpenseDetail> GetAll()
        {
            var vlcExpenses = _repository.VLCExpenseDetails.ToList<Sql.VLCExpenseDetail>();
            return vlcExpenses;
        }

        public VLCExpenseDetail GetExpenseByExpenseId(int expenseId)
        {
            var vlcExpenses = _repository.VLCExpenseDetails.Where(v => v.VLCExpenseId == expenseId).FirstOrDefault();
            return vlcExpenses;
        }

        public List<VLCExpenseDetail> GetAllVLCExpenseDetailByVLCId(int vlcId)
        {
            var vlcExpenses = _repository.VLCExpenseDetails.Where(v => v.VLCId == vlcId).ToList<Sql.VLCExpenseDetail>();
            return vlcExpenses;
        }

        //public List<VLCExpenseDetail> GetAllVLCExpenseDetailByDockId(int docKId)
        //{
        //    var vlcExpenses = _repository.VLCExpenseDetails.Where(v => v.DockMilkCollectionId == docKId).ToList<Sql.VLCExpenseDetail>();
        //    return vlcExpenses;
        //}

        //public VLCExpenseDetail GetVLCExpenseDetailByDockCollectionId(int dockMilkCollectionId)
        //{
        //    var vlcExpenses = _repository.VLCExpenseDetails.Where(v => v.DockMilkCollectionId == dockMilkCollectionId).FirstOrDefault();
        //    return vlcExpenses;
        //}

        public void Add(VLCExpenseDetail vlcExpense)
        {
            if (vlcExpense != null)
            {
                _repository.VLCExpenseDetails.Add(vlcExpense);

            }
        }

        public void Update(VLCExpenseDetail vlcExpense)
        {

            if (vlcExpense != null)
            {
                _repository.Entry<Sql.VLCExpenseDetail>(vlcExpense).State = System.Data.Entity.EntityState.Modified;

            }



        }

        public void Delete(int id)
        {
            var vlcExpense = _repository.VLCExpenseDetails.Where(x => x.VLCExpenseId == id).FirstOrDefault();
            if (vlcExpense != null)
                _repository.VLCExpenseDetails.Remove(vlcExpense);

            // _repository.SaveChanges();

        }
    }
}
