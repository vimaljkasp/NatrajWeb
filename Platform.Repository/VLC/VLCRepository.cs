using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Sql;

namespace Platform.Repository
{
    public class VLCRepository
    {

        PlatformDBEntities _repository;
        public VLCRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }
        public List<VLC> GetAll()
        {
            var vlcs = _repository.VLCs.Where(v=>v.IsDeleted==false).ToList<Sql.VLC>();
            return vlcs;
        }

        public List<VLC> GetVLCByCount(int? pageNumber, int? count)
        {
            var takePage = pageNumber ?? PagingConstant.DefaultPageNumber;
            var takeCount = count ?? PagingConstant.DefaultRecordCount;

            PlatformDBEntities context = new PlatformDBEntities();

            var vlcs = context.VLCs
                                 .OrderBy(c => c.VLCId)
                                .Skip((takePage - 1) * takeCount)
                                .Take(takeCount)
                                .ToList<Sql.VLC>();

            return vlcs;
        }

        public VLC GetById(int id)
        {

            var vlc = _repository.VLCs.FirstOrDefault(x => x.VLCId == id);



            return vlc;
        }


        public VLC GetByUserName(string userName)
        {
            var vlc = _repository.VLCs.Where(v => v.VLCName.Equals(userName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            return vlc;
        }

        public String GetEmployeeNameByVLCId(int vlcId)
        {
            string vlcAgentName = "System";
            if (vlcId>0)
            {
                var vlc = _repository.VLCs.FirstOrDefault(x => x.VLCId == vlcId);
                if (vlc != null)
                    vlcAgentName = vlc.AgentName;
            }
            return vlcAgentName;
          
        }


        public void Add(VLC vlc)
        {
            if (vlc != null)
            {
                _repository.VLCs.Add(vlc);
                // _repository.SaveChanges();

            }
        }

        public void Update(VLC vlc)
        {

            if (vlc != null)
            {
                _repository.Entry<Sql.VLC>(vlc).State = System.Data.Entity.EntityState.Modified;
                //  _repository.SaveChanges();
            }



        }



        public void Delete(int id)
        {
            var vlc = _repository.VLCs.Where(x => x.VLCId == id).FirstOrDefault();
            if (vlc != null)
                _repository.VLCs.Remove(vlc);

            // _repository.SaveChanges();

        }

       




    }
}
