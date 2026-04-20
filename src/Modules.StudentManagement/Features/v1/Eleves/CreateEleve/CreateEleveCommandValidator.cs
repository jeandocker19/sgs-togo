using FluentValidation;
using FSH.Modules.StudentManagement.Contracts.v1.Eleves.CreateEleve;

namespace FSH.Modules.StudentManagement.Features.v1.Eleves.CreateEleve;

public sealed class CreateEleveCommandValidator : AbstractValidator<CreateEleveCommand>
{

    public CreateEleveCommandValidator()
    {
        RuleFor(x => x.Matricule).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Nom).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Prenom).NotEmpty().MaximumLength(100);
        RuleFor(static x => x.DateNaissance).NotEmpty();
        RuleFor(x => x.Sexe).NotEmpty().Must(t => Enum.TryParse<Domain.Sexe>(t, ignoreCase: true, out _))
            .WithMessage("Sexe doit être  'Masculin' ou 'Féminin'.");
        RuleFor(x => x.Statut).NotEmpty().Must(t => Enum.TryParse<Domain.StatutEleve>(t, ignoreCase: true, out _))
            .WithMessage("Statut doit être 'Actif', 'Inactif' ou 'Transfere'.");
        RuleFor(x => x.Telephone).MaximumLength(20).When(x => x.Telephone is not null);
        RuleFor(x => x.Email).EmailAddress().MaximumLength(256).When(x => x.Email is not null);
        RuleFor(x => x.NomParent).MaximumLength(200).When(x => x.NomParent is not null);
        RuleFor(x => x.TelephoneParent).MaximumLength(20).When(x => x.TelephoneParent is not null);
    

    }
}
