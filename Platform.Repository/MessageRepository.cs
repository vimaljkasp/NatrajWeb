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

        public List<Message> GetMessageByMessageId(string messageId)
        {
            var messages = _repository.Messages.Where(v => v.MessageId == messageId).ToList<Sql.Message>();
            return messages;
        }

       



    }
}
