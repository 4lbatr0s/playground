namespace Contracts{
    public interface IRepositoryManager
    {
        ICompanyRepository Company {get;}
        IEmployeeRepository Employee {get;}

        Task Save(); //INFO: Implements the SaveChanges method. It needs to be asynchronous.
    }
}