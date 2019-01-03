using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Repository
{
    public class DCWalletRepository
    {

        PlatformDBEntities _repository;
        public DCWalletRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }

        public List<DCWallet> GetAll()
        {
            var dcWalletList = _repository.DCWallets.ToList<Sql.DCWallet>();
            return dcWalletList;
        }

        public DCWallet GetById(int walletId)
        {
            var dcWallet = _repository.DCWallets.Find(walletId);
            return dcWallet;
        }

        public DCWallet GetByDCId(int dCId)
        {
            var dcWallet = _repository.DCWallets.Where(d => d.DCId == dCId).FirstOrDefault();
            return dcWallet;
        }


        public void Add(DCWallet dCWallet)
        {
            if (dCWallet != null)
            {
                _repository.DCWallets.Add(dCWallet);
              

            }
        }

        public void Update(DCWallet dCWallet)
        {

            if (dCWallet != null)
            {
                _repository.Entry<Sql.DCWallet>(dCWallet).State = System.Data.Entity.EntityState.Modified;
                //  _repository.SaveChanges();
            }



        }

        public void Delete(int id)
        {
            var dCWallet = _repository.DCWallets.Where(x => x.DCId == id).FirstOrDefault();
            if (dCWallet != null)
                _repository.DCWallets.Remove(dCWallet);

            // _repository.SaveChanges();

        }
    }
}
