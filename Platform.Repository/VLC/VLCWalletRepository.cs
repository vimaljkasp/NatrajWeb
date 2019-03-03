using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Repository
{
    public class VLCWalletRepository
    {

        PlatformDBEntities _repository;
        public VLCWalletRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }

        public List<VLCWallet> GetAll()
        {
            var vlcWalletList = _repository.VLCWallets.ToList<Sql.VLCWallet>();
            return vlcWalletList;
        }

        public VLCWallet GetById(int walletId)
        {
            var vlcWallet = _repository.VLCWallets.Find(walletId);
            return vlcWallet;
        }

        public VLCWallet GetByVLCId(int vlcId)
        {
            var vlcWallet = _repository.VLCWallets.Where(d => d.VLCId == vlcId).FirstOrDefault();
            return vlcWallet;
        }


        public void Add(VLCWallet vLCWallet)
        {
            if (vLCWallet != null)
            {
                _repository.VLCWallets.Add(vLCWallet);


            }
        }

        public void Update(VLCWallet vLCWallet)
        {

            if (vLCWallet != null)
            {
                _repository.Entry<Sql.VLCWallet>(vLCWallet).State = System.Data.Entity.EntityState.Modified;
                //  _repository.SaveChanges();
            }



        }

        public void Delete(int id)
        {
            var vLCWallet = _repository.VLCWallets.Where(x => x.VLCId == id).FirstOrDefault();
            if (vLCWallet != null)
                _repository.VLCWallets.Remove(vLCWallet);

            // _repository.SaveChanges();

        }
    }
}
