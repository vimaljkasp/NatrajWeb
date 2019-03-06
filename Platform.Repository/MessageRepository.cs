using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Sql;

namespace Platform.Repository
{
    public class MessageRepository
    {

        PlatformDBEntities _repository;
        public MessageRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }
        public List<Message> GetAll()
        {
            var messages = _repository.Messages.ToList<Sql.Message>();
            return messages;
        }

        public string GetMessageByMessageCode(string messageCode,string defaultMsg)
        {
            var messages = _repository.Messages.Where(v => v.MessageId == messageCode).FirstOrDefault();
            if (messages != null)
                return messages.MessageString;
            else
                return defaultMsg;
        }


        public void Update(Message message)
        {
            if (message != null)
            {
                _repository.Entry<Sql.Message>(message).State = System.Data.Entity.EntityState.Modified;
              
            }

        }


    }
}
