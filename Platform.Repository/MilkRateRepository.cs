using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Sql;

namespace Platform.Repository
{
    public class MilkRateRepository
    {

        PlatformDBEntities _repository;
        public MilkRateRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }
        public List<MilkRate> GetAll()
        {
            var milkRate = _repository.MilkRates.ToList<Sql.MilkRate>();
            return milkRate;
        }

        public decimal GetMilkRateByFatAndClr(decimal fat, decimal clr)
        {
            var milkRate = _repository.MilkRates.Where(v => v.Fat == fat && v.CLR==clr).FirstOrDefault();
            if (milkRate != null)
                return milkRate.Rate;
            else
                return 0;
        }

        public decimal GetMilkRateByApplicableRate(int applicableRate,decimal fat,decimal clr)
        {
            if (applicableRate == 2)
                return GetMilkRateByFatAndClr(fat, clr);
            else
                return 0;
        }


        public void AddMilkRates(List<MilkRate> milkRates)
        {
            if (milkRates != null)
            {
                _repository.MilkRates.AddRange(milkRates);
                // _repository.SaveChanges();

            }
        }

        //public void AddMilkRates(List<MilkRate> milkRates)
        //{
        //    if (milkRates != null)
        //    {
        //        _repository.MilkRates.AddRange(milkRates);
        //        // _repository.SaveChanges();

        //    }
        //}

        public void Delete()
        {
           var milkRates= _repository.MilkRates.ToList<Sql.MilkRate>();
            _repository.MilkRates.RemoveRange(milkRates);
            _repository.SaveChanges();
        }

        public void Update(MilkRate milkRate)
        {
            if (milkRate != null)
            {
                _repository.Entry<Sql.MilkRate>(milkRate).State = System.Data.Entity.EntityState.Modified;

            }

        }


    }
}
