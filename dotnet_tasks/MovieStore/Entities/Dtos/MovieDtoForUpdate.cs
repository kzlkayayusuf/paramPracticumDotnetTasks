using Entities.Models.Enums;

namespace Entities.Dtos;

// struct değer tip
// record refereans tip class lar gibi
/*
 * Data Transfer Objects (Dto) Features:
 * readonly - sadece okunabilir
 * immutable - değişmez
 * LINQ desteği vardır sorgular yazılabilir
 * Ref Type
 * Ctor (Dto)
 * init => readonly ve immutable özelliklerini kazandırmak için init kullanılır. Yani tanımlandığı yerde değerini de vermelisin,sonradan değeri değiştirilemez
 * https://sharplab.io/ => sitesinde arka planda neler olduğunu görebilirsin.

public record MovieDtoForUpdate
{
    public int Id { get; init; }
    public String Name { get; init; }
    public int ReleaseYear { get; init; }
    public Genre Genre { get; init; }
    public decimal Price { get; init; }
}
*/

public record MovieDtoForUpdate(int Id, String Name, int ReleaseYear, Genre Genre, decimal Price);
