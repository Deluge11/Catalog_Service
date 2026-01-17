using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog_Service_Application.Users.Commands.AddNewUser
{
    public record AddNewUserCommand(int userid ,string username):IRequest<bool>;
}
