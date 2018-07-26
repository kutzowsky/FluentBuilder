# FluentBuilder
C# library providing fluent API to build objects a.k.a fun fun fun on the autobahn (with reflection).

## Sample usage
```c#
class Person
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
}

dynamic personBuilder = new FluentBuilder<Person>();

Person person = personBuilder
    .WithName("John")
    .AndSurname("Rambo")
    .AndAge(71)
    .Get();
```


## Technologies & tools
* Reflection,
* `dynamic` type and `DynamicObject` class,
* [Xunit](https://github.com/xunit/xunit),
* [Shouldly](https://www.google.com),
* [AutoFixture](https://github.com/AutoFixture/AutoFixture)


## Disclaimer
It's mostly an excercise, not production ready code (maybe not even practical). It came to my mind when I was lying in bed with a fever,
so be gentle :).
