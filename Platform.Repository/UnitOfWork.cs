using Platform.Repository;
using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
    public class UnitOfWork : IDisposable
    {
        private CustomerRepository customerRepository;
        private CustomerBankRepository customerBankRepository;
        private CustomerAgricultureRepository customerAgricultureRepository;
        private DashboardRepository dashboardRepository;
        private UserRepository userRepository;
        private MessageRepository messageRepository;
        private VLCRepository vLCRepository;
        private VLCMilkCollectionRepository vLCMilkCollectionRepository;
        private VLCMilkCollectionDtlRepository vLCMilkCollectionDtlRepository;

        PlatformDBEntities _repository;
        public UnitOfWork()
        {
            _repository= new PlatformDBEntities();
        }


        public CustomerRepository CustomerRepository
        {
            get
            {
                if (customerRepository == null)
                    return customerRepository = new CustomerRepository(_repository);

                else
                {
                    return customerRepository;
                }
            }

        }


        public CustomerBankRepository CustomerBankRepository
        {
            get
            {
                if (customerBankRepository == null)
                    return customerBankRepository = new CustomerBankRepository(_repository);

                else
                {
                    return customerBankRepository;
                }
            }

        }

        public CustomerAgricultureRepository CustomerAgricultureRepository
        {
            get
            {
                if (customerAgricultureRepository == null)
                    return customerAgricultureRepository = new CustomerAgricultureRepository(_repository);

                else
                {
                    return customerAgricultureRepository;
                }
            }

        }

        public DashboardRepository DashboardRepository
        {
            get
            {
                if (dashboardRepository == null)
                    return dashboardRepository = new DashboardRepository(_repository);

                else
                {
                    return dashboardRepository;
                }
            }

        }


        public VLCRepository VLCRepository
        {
            get
            {
                if (vLCRepository == null)
                    return vLCRepository = new VLCRepository(_repository);

                else
                {
                    return vLCRepository;
                }
            }
        }

        public VLCMilkCollectionRepository VLCMilkCollectionRepository
        {
            get
            {
                if (vLCMilkCollectionRepository == null)
                    return vLCMilkCollectionRepository = new VLCMilkCollectionRepository(_repository);
                else
                {
                    return vLCMilkCollectionRepository;
                }
            }
        }

        public VLCMilkCollectionDtlRepository VLCMilkCollectionDtlRepository
        {
            get
            {
                if (vLCMilkCollectionDtlRepository == null)
                    return vLCMilkCollectionDtlRepository = new VLCMilkCollectionDtlRepository(_repository);
                else
                {
                    return vLCMilkCollectionDtlRepository;
                }
            }
        }

        public UserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                    return userRepository = new UserRepository(_repository);

                else
                {
                    return userRepository;
                }
            }
        }

        public MessageRepository MessageRepository
        {
            get
            {
                if (messageRepository == null)
                    return messageRepository = new MessageRepository(_repository);
                else
                {
                    return messageRepository;
                }
            }
        }

        //To save multiple repository and maintain consistency
        public void SaveChanges()
        {
            this._repository.SaveChanges();
        }


        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_repository != null)
                {
                    _repository.Dispose();
                    _repository = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
