using Catalog_Service_Core.Entities;
using Catalog_Service_Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Catalog_Service_Application.Users.Commands.AddNewUser
{
    public class AddNewUserHandler : IRequestHandler<AddNewUserCommand, bool>
    {
        public AddNewUserHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

        async Task<bool> IRequestHandler<AddNewUserCommand, bool>.Handle(AddNewUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Id = request.userid,
                Name = request.username
            };

            await UnitOfWork.Users.Add(user);
            return await UnitOfWork.Complete() > 0;
        }
    }
}
