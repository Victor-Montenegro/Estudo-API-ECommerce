using AutoMapper;
using Estudo.TRIMANIA.Domain.Commands.Users;
using Estudo.TRIMANIA.Domain.Entities;
using Estudo.TRIMANIA.Domain.Exceptions;
using Estudo.TRIMANIA.Domain.Extensions;
using Estudo.TRIMANIA.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Estudo.TRIMANIA.Application.Mediators.Users
{
    public class SignUpHandler : BaseHandler<SignUpRequest, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SignUpHandler> _logger;

        public SignUpHandler(IMapper mapper,ILogger<SignUpHandler> logger, IUnitOfWork unitOfWork, IValidator<SignUpRequest> validator) : base(validator)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        protected async override Task<Unit> HandleBase(SignUpRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var user = await _unitOfWork.UserRepository.GetUserByLogin(request.Login);

                if (user is not null)
                    throw new ServiceException($"Usuário com o login {request.Login} já existe!", HttpStatusCode.BadRequest);

                user = _mapper.Map<User>(request);

                user.SetPassworld(request.Password.Encrypt());
                user.SetAddress(_mapper.Map<Address>(request.Address));

                await _unitOfWork.UserRepository.Insert(user);

                await _unitOfWork.Commit();

                return Unit.Value;
            }
            catch (ServiceException)
            {
                await _unitOfWork.RollBack();

                throw;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollBack();
                _logger.LogError(ex, "Erro in SignUpHandler");
                throw;
            }
        }
    }
}
