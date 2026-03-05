
namespace Bank.Domain.ValueObjects;

public record IdentityDocument
{
    public string? Type { get; init; } //init indica que solo puedes darle valor en el constructor o usando un inicializador de objetos.
    public string? Number { get; init; }
    private IdentityDocument() { }


    private IdentityDocument(string type, string number) { 

        if (string.IsNullOrEmpty(number))  throw new ArgumentNullException("Number cannot be null or empty", nameof(number)); 

        if (string.IsNullOrEmpty(type))  throw new ArgumentNullException("Type cannot be null or empty", nameof(type));

        Type = type;
        Number = number;
    }


        
    public static IdentityDocument Create(string type, string number)//Patron Factory, este patron define que no se debe tener abierta la asignacion de valores a una entidad de manera directa por
        //el constructor sino se debe delegar esta tarea a um metodo especifico para poder fabricar instancias nuevas de un entidad determinada
    {
        return new IdentityDocument(type, number);
    }


}
