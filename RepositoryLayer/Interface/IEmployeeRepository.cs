using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http;

namespace RepositoryLayer.Interface
{
    public interface IEmployeeRepository
    {
        List<Employee> Get();
        Employee Get(int id);

        int Post ([FromBody]Employee emp);
        int Put(int id,[FromBody]Employee emp );
        int Delete(int id);
    }
}
