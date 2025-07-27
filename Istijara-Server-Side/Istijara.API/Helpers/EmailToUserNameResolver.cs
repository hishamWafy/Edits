using AutoMapper;
using Istijara.Core.DTOs.Identity;
using Istijara.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Istijara.API.Helpers
{
    public class EmailToUserNameResolver : IValueResolver<RegisterModel, ApplicationUser, string>
    {
        public string Resolve(RegisterModel source, ApplicationUser destination, string destMember, ResolutionContext context)
        {
            var emailOrPhone = source.EmailOrPhone;

            if (new EmailAddressAttribute().IsValid(emailOrPhone))
                return emailOrPhone.Split('@')[0];

            return emailOrPhone;
        }
    }
}
