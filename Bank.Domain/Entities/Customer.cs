

using Bank.Domain.Enums;
using Bank.Domain.ValueObjects;

namespace Bank.Domain.Entities;

public class Customer : BaseEntity
{
    public string Name { get; private set; }
    public string LastName { get; private set; }
    public IdentityDocument Document { get; private set; }//private set es igual que un init

    public CustomerStatus Status { get; private set; }

    private Customer() { }

    /*Los agregados permiten enriquecer un modelo, permiten agregar funcionalidad asociada al negocio entorno a  la entidad en que 
    estamos trabajando; tambien son agregados propiedades que le agregamos para cumplir con funcionalidades especificas del negocio,
    es decir se agregan propiedades o metodos dentro de la misma entidad*/

    /*Agregate root hacen referencia a la entidad rica, es el conjungo de agregados de una entidad, es el todo de una entidad */
    private Customer(string name, string lastName, IdentityDocument document)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Name cannot be null or empty", nameof(name));

        if (string.IsNullOrEmpty(lastName)) throw new ArgumentNullException("Lastname cannot be null or empty", nameof(lastName));

        if (document == null) throw new ArgumentNullException("Document cannot be null or empty", nameof(document));

        Name = name;
        LastName = lastName;
        Document = document;
    }


    public static Customer Create(string name, string lastName, IdentityDocument document)
    {        
        return new Customer(name, lastName, document);
    }


    public void UpdateStatus(CustomerStatus status) { 
        
        this.Status = status;
    }
}

