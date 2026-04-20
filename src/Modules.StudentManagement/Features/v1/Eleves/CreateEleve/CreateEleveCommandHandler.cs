
using FSH.Framework.Core.Context;
using FSH.Framework.Core.Exceptions;
using FSH.Modules.StudentManagement.Contracts.DTOs;
using FSH.Modules.StudentManagement.Contracts.v1.Eleves.CreateEleve;
using FSH.Modules.StudentManagement.Domain;
using FSH.Modules.StudentManagement.Persistence;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace FSH.Modules.StudentManagement.Features.v1.Eleves.CreateEleve;

public sealed class CreateEleveCommandHandler : ICommandHandler<CreateEleveCommand,Guid>
{

  
    private readonly StudentDbContext _dbContext;    
    private readonly ICurrentUser _currentUser;


    public CreateEleveCommandHandler(StudentDbContext dbContext, ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }


    public async ValueTask<Guid> Handle(CreateEleveCommand command, CancellationToken cancellationToken)
    {

        ArgumentNullException.ThrowIfNull(command);


        var exists = await _dbContext.Eleves
           .AnyAsync(e => e.Matricule == command.Matricule, cancellationToken)
           .ConfigureAwait(false);


        if (exists)
            throw new CustomException(
                $"Un élève avec le matricule '{command.Matricule}' existe déjà.",
                (IEnumerable<string>?)null,
                System.Net.HttpStatusCode.Conflict);


        var sexe = Enum.Parse<Sexe>(command.Sexe, ignoreCase: true);

        var statutEleve = Enum.Parse<StatutEleve>(command.Statut, ignoreCase: true);

        var eleve = Eleve.Create(
           command.Matricule,
           command.Nom,
           command.Prenom,
           command.DateNaissance,
           sexe,                                       
           statutEleve,                                
           command.LieuNaissance,                      
           command.Adresse,
           command.NomParent,
           command.Telephone,
           command.Email,
           command.TelephoneParent,
           _currentUser.GetUserId().ToString());

        
        _dbContext.Eleves.Add(eleve);

       
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return eleve.Id;

    }

}
