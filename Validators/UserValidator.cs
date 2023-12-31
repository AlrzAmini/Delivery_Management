﻿using DriversManagement.Models.Data.Entities;
using DriversManagement.Models.DTOs.User;
using FluentValidation;

namespace DriversManagement.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user=>user.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(user => user.Mobile)
                .NotNull()
                .NotEmpty();

            RuleFor(user => user.Password)
                .NotNull()
                .NotEmpty();

            RuleFor(user => user.RoleId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);
        }
    }

    public class CreateUserValidator : AbstractValidator<CreateUserRequestDto>
    {
        public CreateUserValidator()
        {
            RuleFor(user => user.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(user => user.Mobile)
                .NotNull()
                .NotEmpty();

            RuleFor(user => user.Password)
                .NotNull()
                .NotEmpty();
        }
    }
}
