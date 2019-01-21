using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Sql;

namespace Platform.Repository
{
    public class SMSRepository
    {

        PlatformDBEntities _repository;
        public SMSRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }
        public List<NatrajSMSLog> GetAll()
        {
            var natrajSMs = _repository.NatrajSMSLogs.ToList<Sql.NatrajSMSLog>();
            return natrajSMs;
        }

        public List<NatrajSMSLog> GetSMSBySMSId(int smsId)
        {
            var sms = _repository.NatrajSMSLogs.Where(v => v.SMSId == smsId).ToList<Sql.NatrajSMSLog>();
            return sms;
        }

        public void Add(NatrajSMSLog natrajSMSLog)
        {
            if (natrajSMSLog != null)
                _repository.NatrajSMSLogs.Add(natrajSMSLog);

        }


        public void Update(NatrajSMSLog natrajSMSLog)
        {

            if (natrajSMSLog != null)
            {
                _repository.Entry<Sql.NatrajSMSLog>(natrajSMSLog).State = System.Data.Entity.EntityState.Modified;
                
            }



        }



        public void Delete(int id)
        {
            var sms = _repository.NatrajSMSLogs.Where(x => x.SMSId == id).FirstOrDefault();
            if (sms != null)
                _repository.NatrajSMSLogs.Remove(sms);

         

        }


    }
}
