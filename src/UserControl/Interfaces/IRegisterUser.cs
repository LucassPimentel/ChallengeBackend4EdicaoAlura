using FluentResults;
using UserControl.Dtos;
using UserControl.Dtos.Requests;

namespace UserControl.Interfaces
{
    public interface IRegisterUser
    {
        Result ActivateUserAccount(ActivateAccountRequest activateAccountRequest);
        Result RegisterUser(PostUserDto user);
    }
}
