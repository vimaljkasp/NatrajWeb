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
        private SMSRepository sMSRepository;

        private VLCRepository vLCRepository;
        private VLCMilkCollectionRepository vLCMilkCollectionRepository;
        private VLCMilkCollectionDtlRepository vLCMilkCollectionDtlRepository;
        private DistributionCenterRepository distributionCenterRepository;
        private DCAddressRepository dCAddressRepository;
        private DCOrderRepository dCOrderRepository;
        private DCOrderDtlRepository dCOrderDtlRepository;
        private DCWalletRepository dCWalletRepository;
        private DCPaymentDetailRepository dCPaymentDetailRepository;

        private ProductRepository productRepository;
        private ProductCategoryRepository productCategoryRepository;

        public String ImagePath { get; set; }

        PlatformDBEntities _repository;
        public UnitOfWork()
        {
            _repository= new PlatformDBEntities();
            ImagePath = @"http://service.natrajdairy.com/img/";
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


        public DistributionCenterRepository DistributionCenterRepository
        {
            get
            {
                if (distributionCenterRepository == null)
                    return distributionCenterRepository = new DistributionCenterRepository(_repository);
                else
                    return distributionCenterRepository;
            }
        }

        public DCAddressRepository DCAddressRepository
        {
            get
            {
                if (dCAddressRepository == null)
                    return dCAddressRepository = new DCAddressRepository(_repository);
                else
                    return dCAddressRepository;
            }
        }

        public DCOrderRepository DCOrderRepository
        {
            get
            {
                if (dCOrderRepository == null)
                    return dCOrderRepository = new DCOrderRepository(_repository);
                else
                    return dCOrderRepository;
            }
        }

        public DCOrderDtlRepository DCOrderDtlRepository
        {
            get
            {
                if (dCOrderDtlRepository == null)
                    return dCOrderDtlRepository = new DCOrderDtlRepository(_repository);
                else
                    return dCOrderDtlRepository;
            }
        }

        public DCPaymentDetailRepository DCPaymentDetailRepository
        {
            get
            {
                if (dCPaymentDetailRepository == null)
                    return dCPaymentDetailRepository = new DCPaymentDetailRepository(_repository);
                else
                    return dCPaymentDetailRepository;
            }
        }

        public DCWalletRepository DCWalletRepository
        {
            get
            {
                if (dCWalletRepository == null)
                    return dCWalletRepository = new DCWalletRepository(_repository);
                else
                    return dCWalletRepository;
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


        public SMSRepository SMSRepository
        {
            get
            {
                if (sMSRepository == null)
                    return sMSRepository = new SMSRepository(_repository);
                else
                {
                    return sMSRepository;
                }
            }
        }

        public ProductRepository ProductRepository
        {
            get
            {
                if (productRepository == null)
                    return productRepository = new ProductRepository(_repository);
                else
                    return productRepository;
            }
        }

        public ProductCategoryRepository ProductCategoryRepository
        {
            get
            {
                if (productCategoryRepository == null)
                    return productCategoryRepository = new ProductCategoryRepository(_repository);
                else
                    return productCategoryRepository;
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
