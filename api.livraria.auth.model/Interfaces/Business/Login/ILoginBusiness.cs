using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.livraria.auth.model.Request;

namespace api.livraria.auth.model.Interfaces.Business.Login
{
    public interface ILoginBusiness
    { 
        object FindByLogin(TokenRequest user);

    }
}
