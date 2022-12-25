using System.Collections.Generic;
using Contracts;

namespace Repository
{
    //INFO: Sealed means this class cannot be inhereted. It will throw a compile time error.
    //INFO: We need to create a IoC for this RepositoryManager, go to ServiceExtension.cs file to see it.
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<ICompanyRepository> _companyRepository;//INFO: With the help of Lazy we won't initialize Repositories until we need them.
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _companyRepository = new Lazy<ICompanyRepository>(()=> new CompanyRepository(repositoryContext));
            _employeeRepository = new Lazy<IEmployeeRepository>(()=> new EmployeeRepository(repositoryContext));
        }

        public ICompanyRepository Company => _companyRepository.Value; 
        public IEmployeeRepository Employee => _employeeRepository.Value;
        public void Save() => _repositoryContext.SaveChanges();

        /*
            Explanation what happens above:
            1. We created an IRepositoryManager and inside of it we have ICompanyRepository and IEmployeeRepository.
            2. We implemented the interface.
            3. We have created a RepositoryContext instance.
            4. We have created Lazy instances of ICompanyRepository and IEmployeeRepository interfaces.
            5. We said, for Lazy<ICompanyRepository>, create a CompanyRepository with repositoryContext behind the scene.
            6. We said, For ICompanyRepository instance Company, return the Lazy CompanyRepository.  
        */
    }
}