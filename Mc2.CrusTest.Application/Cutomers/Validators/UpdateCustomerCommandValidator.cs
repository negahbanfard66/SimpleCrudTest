using FluentValidation;
using Mc2.CrudTest.Domain.Interfaces;
using Mc2.CrusTest.Application.Customers.Commands;
using PhoneNumbers;

namespace Mc2.CrusTest.Application.Customers.Validators
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        private readonly ICustomerRepository _repository;

        public UpdateCustomerCommandValidator(ICustomerRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.DateOfBirth).LessThan(DateTime.Now);
            RuleFor(x => x.Email).EmailAddress().Must(BeUniqueEmail).WithMessage("Email must be unique.");
            RuleFor(x => x.PhoneNumber).Must(BeAValidPhoneNumber).WithMessage("Invalid phone number.");
            RuleFor(x => x.BankAccountNumber).NotEmpty().Must(BeAValidBankAccountNumber).WithMessage("Invalid bank account number.");
            RuleFor(x => x).Must(BeUniqueCustomer).WithMessage("A customer with the same first name, last name, and date of birth already exists.");
        }

        private bool BeAValidPhoneNumber(string phoneNumber)
        {
            try
            {
                var phoneUtil = PhoneNumberUtil.GetInstance();
                var number = phoneUtil.Parse(phoneNumber, "US");
                return phoneUtil.IsValidNumber(number);
            }
            catch
            {
                return false;
            }
        }

        private bool BeUniqueEmail(string email)
        {
            return _repository.IsEmailUnique(email);
        }

        private bool BeUniqueCustomer(UpdateCustomerCommand command)
        {
            return _repository.IsCustomerUnique(command.FirstName, command.LastName, command.DateOfBirth);
        }

        private bool BeAValidBankAccountNumber(string bankAccountNumber)
        {
            return bankAccountNumber.All(char.IsDigit) && bankAccountNumber.Length == 10;
        }
    }
}
