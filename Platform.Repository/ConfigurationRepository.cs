using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Sql;

namespace Platform.Repository
{
    public class ConfigurationRepository
    {

        PlatformDBEntities _repository;
        public ConfigurationRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }
        public List<NatrajConfiguration> GetAll()
        {
            var configurations = _repository.NatrajConfigurations.ToList<Sql.NatrajConfiguration>();
            return configurations;
        }


        public NatrajConfiguration GetById(int id)
        {
            var configuration = _repository.NatrajConfigurations.Where(c=>c.Id==id).FirstOrDefault();
            return configuration;
        }

        public string GetConfiguration(string keyData,string keyName,string defaultVal)
        {
            var messages = _repository.NatrajConfigurations
                .Where(v => v.KeyData.Equals(keyData,StringComparison.InvariantCultureIgnoreCase) && v.KeyName.Equals
                (keyName,StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (messages != null)
                return messages.DataVal;
            else
                return defaultVal;
        }

        public void Update(NatrajConfiguration natrajConfiguration)
        {
            if (natrajConfiguration != null)
            {
                _repository.Entry<Sql.NatrajConfiguration>(natrajConfiguration).State = System.Data.Entity.EntityState.Modified;

            }

        }



    }
}
